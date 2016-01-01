using System.Net.Http;
using System.Threading.Tasks;

namespace chromypack
{
	public class VersionLoader
	{
		public Task<byte[]> DownloadAsync(string version)
		{
			using (var client = new HttpClient())
			{
				var url = $"http://chromedriver.storage.googleapis.com/{version}/chromedriver_win32.zip";
				return client.GetAsync(url)
					.ContinueWith(request =>
					{
						HttpResponseMessage response = request.Result;
						response.EnsureSuccessStatusCode();
						return response.Content.ReadAsByteArrayAsync();
					}).Result;
			}
		}
	}
}
