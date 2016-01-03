using System.IO;
using System.IO.Compression;
using System.Linq;

namespace chromypack
{
	public class DriverPackage
	{
		public string Id { get; } = "Chromium.ChromeDriver";

		public string PeFileName { get; } = "chromedriver.exe";

		public string Version { get; }

		public string Path { get; }

		public string SourceUrl => $"http://chromedriver.storage.googleapis.com/{Version}/chromedriver_win32.zip";

		public string ContentDir => $@"{Path}\content";

		public string ToolsDir => $@"{Path}\tools";

		public string PeFileFullName => $@"{ContentDir}\{PeFileName}";

		public string NuSpecName => $"{Id}.nuspec";

		public string NuSpecFullName => $@"{Path}\{NuSpecName}";

		public string NuPkgFullName => $@"{Path}\{Id}.{Version}.nupkg";

		private byte[] _peFile;
		public byte[] PeFile
		{
			get { return _peFile; }
			set
			{
				if (value == null)
				{
					return;
				}
				_peFile = Uncompress(value);
			}
		}

		public DriverPackage(string path = "", string version = "0")
		{
			Path = path;
			Version = version;
		}

		private byte[] Uncompress(byte[] data)
		{
			using (var outputStream = new MemoryStream())
			using (var inputStream = new MemoryStream(data))
			{
				ZipArchive archive = new ZipArchive(inputStream);
				ZipArchiveEntry entry = archive.Entries.FirstOrDefault(x => x.Name.Equals(PeFileName));
				entry?.Open().CopyTo(outputStream);
				return outputStream.ToArray();
			}
		}
	}
}
