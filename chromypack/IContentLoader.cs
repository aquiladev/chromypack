using System.Threading.Tasks;

namespace chromypack
{
	public interface IContentLoader
	{
		Task<byte[]> DownloadAsync(string url);
	}
}