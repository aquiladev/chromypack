using System.Collections.Generic;

namespace chromypack
{
	public interface IRepository<T>
	{
		IEnumerable<T> FindById(string id);
		void Create(DriverPackage pkg);
		void Push(T pkg);
	}
}