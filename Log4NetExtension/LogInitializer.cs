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
	public static class LoggerInitializer
	{
		public static readonly ILog nearrealtimelogger = LogManager.GetLogger("WWI.signalr");
		public static readonly ILog wwilogger = LogManager.GetLogger("WWI");

		public static void Initialize()
		{
			try
			{
				log4net.Config.XmlConfigurator.Configure();
			}
			catch (System.Exception ex)
			{
				System.Console.WriteLine(new System.Text.StringBuilder().AppendFormat("Unable to start the logging: {1}", ex.ToString()));

				throw;
			}
		}
	}
}