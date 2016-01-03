using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace chromypack
{
	public class DriverVersionObserver
	{
		private readonly IRepository<NuGet.IPackage> _repository;

		public DriverVersionObserver(IRepository<NuGet.IPackage> repository)
		{
			_repository = repository;
		}

		public Task<string> GetSourceVersionAsync()
		{
			var url = "http://chromedriver.storage.googleapis.com/LATEST_RELEASE";
			using (var client = new HttpClient())
			{
				return client.GetAsync(url)
					.ContinueWith(request =>
					{
						HttpResponseMessage response = request.Result;
						response.EnsureSuccessStatusCode();
						return response.Content.ReadAsStringAsync();
					}).Result;
			}
		}

		public string GetPackageVersion()
		{
			var package = _repository.FindById(new DriverPackage().Id)
				.ToList()
				.FirstOrDefault(x => x.IsLatestVersion);
			return package?.Version.ToString();
		}
	}
}
