using System.Configuration;
using Xunit;

namespace chromypack.Test
{
	public class DriverVersionObserverTest
	{
		private readonly DriverVersionObserver _observer;

		public DriverVersionObserverTest()
		{
			_observer = new DriverVersionObserver(new PackageRepository(ConfigurationManager.AppSettings["NugetApiKey"]));
		}

		[Fact]
		public void GetSourceVersion()
		{
			var version = _observer.GetSourceVersionAsync().Result;
		}

		[Fact]
		public void GetPackageVersion()
		{
			var version = _observer.GetPackageVersion();
		}
	}
}
