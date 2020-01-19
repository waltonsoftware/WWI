using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using log4net;
using log4net.Config;

namespace WWI.Log4NetExtension
{
	public class Logger
	{
		public static void Initialize()
		{
		}

		public static void LogError(String logMsg)
		{
			LoggerInitializer.wwilogger.Error(logMsg);
		}

		public static void LogError(String logMsg, Exception ex)
		{
			string errorMessage;

			if (ex == null)
			{
				errorMessage = logMsg;
			}
			else
			{
				errorMessage = logMsg + "\nMessage: " + ex.Message + "\nStack Trace: " + ex.StackTrace;
			}

			LoggerInitializer.wwilogger.Error(errorMessage);
		}

		public static void LogInfo(String logMsg)
		{
			try
			{
				LoggerInitializer.wwilogger.Info(logMsg);
			}
			catch (Exception ex)
			{
				string exmsg = ex.Message;

			}
		}

		public static void LogWarning(String logMsg)
		{
			LoggerInitializer.wwilogger.Warn(logMsg);
		}

		public static void LogDebug(String logMsg)
		{
			LoggerInitializer.wwilogger.Debug(logMsg);
		}
	}
}