using System.IO;
using System.IO.Compression;
using System.Linq;

namespace chromypack
{
	public class DriverPackageManager
	{
		private const string DriverFileName = "chromedriver.exe";

		public void BuildPackage()
		{
			var version = "2.0";
			var loader = new VersionLoader();
			var driverArchive = loader.DownloadAsync(version).Result;
			var driver = Uncompress(driverArchive);

			var path = $@"..\build\{version}";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			using (var fs = File.Create($@"{path}\{DriverFileName}"))
			{
				fs.Write(driver, 0, driver.Length);
			}
		}

		private byte[] Uncompress(byte[] data)
		{
			using (var outputStream = new MemoryStream())
			using (var inputStream = new MemoryStream(data))
			{
				ZipArchive archive = new ZipArchive(inputStream);
				ZipArchiveEntry entry = archive.Entries.FirstOrDefault(x => x.Name.Equals(DriverFileName));
				entry?.Open().CopyTo(outputStream);
				return outputStream.ToArray();
			}
		}
	}
}
