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
	public class LoggerNearRealTime
	{
		public static void Initialize()
		{
		}

		public static void LogError(String logMsg)
		{
			LoggerInitializer.nearrealtimelogger.Error(logMsg);
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

			LoggerInitializer.nearrealtimelogger.Error(errorMessage);
		}

		public static void LogInfo(String logMsg)
		{
			LoggerInitializer.nearrealtimelogger.Info(logMsg);
		}

		public static void LogWarning(String logMsg)
		{
			LoggerInitializer.nearrealtimelogger.Warn(logMsg);
		}

		public static void LogDebug(String logMsg)
		{
			LoggerInitializer.nearrealtimelogger.Debug(logMsg);
		}
	}
}