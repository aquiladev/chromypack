using System;

namespace chromypack
{
	public interface ILogger
	{
		void Information(string message);
		void Information(string format, params object[] vars);
		void Information(Exception exception, string format, params object[] vars);

		void Warning(string message);
		void Warning(string format, params object[] vars);
		void Warning(Exception exception, string format, params object[] vars);

		void Error(string message);
		void Error(string format, params object[] vars);
		void Error(Exception exception, string format, params object[] vars);
	}
}
