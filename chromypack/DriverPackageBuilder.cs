using System;
using System.IO;
using NuGet;

namespace chromypack
{
	public class DriverPackageBuilder
	{
		private readonly ILogger _logger;
		private readonly IContentLoader _loader;
		private readonly IRepository<IPackage> _repository;

		public DriverPackageBuilder(ILogger logger,
			IContentLoader loader,
			IRepository<IPackage> repository)
		{
			_logger = logger;
			_loader = loader;
			_repository = repository;
		}

		public DriverPackage Build(string targetPath, string version)
		{
			var package = new DriverPackage(targetPath, version);

			_logger.Information($"Build started for {package.Id}.{package.Version} path [{targetPath}]");

			try
			{
				LoadDriver(package);
				CopyToolsFile(@"tools\install.pkg", package, "install.ps1");
				CopyToolsFile(@"tools\uninstall.pkg", package, "uninstall.ps1");
				CreateNugetSpec(@"nuspec.pkg", package);
				Save(package);

				_logger.Information("Build finished");
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error: ");
			}
			return package;
		}

		public void Push(DriverPackage package)
		{
			_logger.Information($"Pushing package {package.Id}.{package.Version}");

			try
			{
				IPackage pkg = new ZipPackage(package.NuPkgFullName);
				_repository.Push(pkg);
				_logger.Information("Pushing finished");
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error: ");
			}
		}

		private void LoadDriver(DriverPackage package)
		{
			_logger.Information($"Loading sources for {package.Id}.{package.Version} from {package.SourceUrl}");

			package.PeFile = _loader.DownloadAsync(package.SourceUrl).Result;
			EnsureDirectory(package.ContentDir);

			using (var fs = File.Create(package.PeFileFullName))
			{
				fs.Write(package.PeFile, 0, package.PeFile.Length);
			}
		}

		private void CopyToolsFile(string source, DriverPackage package, string targetFileName = "")
		{
			_logger.Information($"Copying file {source} for {package.Id}.{package.Version}");

			EnsureDirectory(package.ToolsDir);
			FileInfo file = new FileInfo(source);

			string temppath = Path.Combine(package.ToolsDir,
				string.IsNullOrEmpty(targetFileName) ? file.Name : targetFileName);
			file.CopyTo(temppath, true);
		}

		private void CreateNugetSpec(string source, DriverPackage package)
		{
			_logger.Information($"Creating manifest for {package.Id}.{package.Version}");

			var spec = File.ReadAllText(source);
			spec = spec.Replace("$version$", package.Version)
				.Replace("$id$", package.Id);

			using (var sw = new StreamWriter(package.NuSpecFullName))
			{
				sw.Write(spec);
			}
		}

		private void Save(DriverPackage package)
		{
			_repository.Create(package);
		}

		private void EnsureDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
	}
}
