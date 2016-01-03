using System.Collections.Generic;
using System.IO;
using NuGet;

namespace chromypack
{
	public class PackageRepository : IRepository<IPackage>
	{
		private readonly string _apiKey;
		private readonly IPackageRepository _repo;
		private readonly PackageServer _server;

		public PackageRepository(string apiKey)
		{
			_apiKey = apiKey;
			_repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
			_server = new PackageServer("https://nuget.org", "chromypack");
		}

		public IEnumerable<IPackage> FindById(string id)
		{
			return _repo.FindPackagesById(id);
		}

		public void Create(DriverPackage pkg)
		{
			var builder = new PackageBuilder(pkg.NuSpecFullName, pkg.Path, NullPropertyProvider.Instance, true);
			using (var fs = new FileStream(pkg.NuPkgFullName, FileMode.Create))
			{
				builder.Save(fs);
			}
		}

		public void Push(IPackage package)
		{
			_server.PushPackage(_apiKey, package, package.GetStream().Length, 10000, false);
		}
	}
}
