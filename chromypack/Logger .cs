using System;
using System.Diagnostics;

namespace chromypack
{
	public class Logger : ILogger
	{
		public void Information(string message)
		{
			Trace.TraceInformation(message);
		}

		public void Information(string format, params object[] vars)
		{
			Trace.TraceInformation(format, vars);
		}

		public void Information(Exception exception, string format, params object[] vars)
		{
			Trace.TraceInformation(string.Format(format, vars) + ";Exception Details={0}", exception);
		}

		public void Warning(string message)
		{
			Trace.TraceWarning(message);
		}

		public void Warning(string format, params object[] vars)
		{
			Trace.TraceWarning(format, vars);
		}

		public void Warning(Exception exception, string format, params object[] vars)
		{
			Trace.TraceWarning(string.Format(format, vars) + ";Exception Details={0}", exception);
		}

		public void Error(string message)
		{
			Trace.TraceError(message);
		}

		public void Error(string format, params object[] vars)
		{
			Trace.TraceError(format, vars);
		}

		public void Error(Exception exception, string format, params object[] vars)
		{
			Trace.TraceError(string.Format(format, vars) + ";Exception Details={0}", exception);
		}
	}
}
