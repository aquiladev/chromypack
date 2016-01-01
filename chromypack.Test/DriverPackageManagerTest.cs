using Xunit;

namespace chromypack.Test
{
	public class DriverPackageManagerTest
	{
		private readonly DriverPackageManager _manager;

		public DriverPackageManagerTest()
		{
			_manager = new DriverPackageManager();
		}

		[Fact]
		public void TestMethod1()
		{
			_manager.BuildPackage();
		}
	}
}
