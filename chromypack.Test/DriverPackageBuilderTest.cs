using System.Configuration;
using Xunit;

namespace chromypack.Test
{
	public class DriverPackageBuilderTest
	{
		private readonly DriverPackageBuilder _builder;

		public DriverPackageBuilderTest()
		{
			_builder = new DriverPackageBuilder(
				new Logger(),
				new DriverSourceLoader(),
				new PackageRepository(ConfigurationManager.AppSettings["NugetApiKey"]));
		}

		[Fact]
		public void BuildPackage_BuildsPackagesForOneVersion()
		{
			_builder.Build("2.0");
		}

		[Fact]
		public void BuildPackages_BuildsPackagesForAllVersions()
		{
			for (var i = 0; i < 21; i++)
			{
				_builder.Build($"2.{i}");
			}
		}
	}
}
