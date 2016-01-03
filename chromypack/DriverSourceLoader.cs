using System.Net.Http;
using System.Threading.Tasks;

namespace chromypack
{
	public class DriverSourceLoader : IContentLoader
	{
		public Task<byte[]> DownloadAsync(string url)
		{
			using (var client = new HttpClient())
			{
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
