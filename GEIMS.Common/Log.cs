using System;
using System.Globalization;
using System.Threading;
using System.IO;

namespace GEIMS.Common
{
	public class Log
	{

		private static StreamWriter mswLogFileDirectory = null;
		private static string mstrLogFileName = null;

		static Log()
		{
			Init(AppDomain.CurrentDomain.BaseDirectory + "Log", "Log");
		}
		/// <summary>
		/// Gets the name of the log file with the full path
		/// </summary>
		public static string LogFileName
		{
			get
			{
				return mstrLogFileName;
			}
		}
		/// <summary>
		/// Write message to the log
		/// </summary>
		/// <param name="logEventType">Log Event Type</param>
		/// <param name="context">Context</param>
		/// <param name="message">Message</param>
		public static void Write(LogEventType logEventType, string context, string message)
		{
			//construct the log string
			if (null == mswLogFileDirectory) return;

			string lstrLogMessage = "[" + logEventType.ToString() + "]";
			if (null != context && 0 < context.Trim().Length)
				lstrLogMessage += "[" + context + "]";
			lstrLogMessage += " " + message;
			WriteToLog(lstrLogMessage);
		}

		/// <summary>
		/// Write message to the log
		/// </summary>
		/// <param name="logEventType">Log Event Type</param>
		/// <param name="message">Message</param>
		public static void Write(LogEventType logEventType, string message)
		{
			Write(logEventType, null, message);
		}

		/// <summary>
		/// Write the Exception details to the log
		/// </summary>
		/// <param name="exception">Exception</param>
		public static void Write(Exception exception)
		{
			Write(null, null, exception);
		}

		/// <summary>
		/// Write the Exception details to the log
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="exception">Exception</param>
		public static void Write(string context, Exception exception)
		{
			Write(context, null, exception);
		}

		/// <summary>
		/// Write the Exception details to the log
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="message">Message</param>
		/// <param name="exception">Exception</param>
		public static void Write(string context, string message, Exception exception)
		{
			if (null == mswLogFileDirectory) return;

			string lstrLogMessage = "[" + LogEventType.ERROR.ToString() + "]" +
				((null != context && 0 < context.Trim().Length) ? "[" + context + "]" : "") + " " +
				((null != message && 0 < message.Trim().Length) ? message + " - " : "") + exception.Message;

			if (null != exception.InnerException)
			{
				lstrLogMessage += " - " + exception.InnerException.Message.ToString();
			}

			lstrLogMessage += "\r\n" + exception.StackTrace;
			WriteToLog(lstrLogMessage);
		}

		/// <summary>
		/// Gets the current thread name
		/// </summary>
		/// <returns>Thread name</returns>
		private static string GetThreadName()
		{
			string lstrThreadName = Thread.CurrentThread.Name;
			if (null != lstrThreadName && 0 < lstrThreadName.Length)
				return "[" + lstrThreadName + "]";
			else return "";
		}

		/// <summary>
		/// Writes the provided message to the log
		/// </summary>
		/// <param name="message">message</param>
		protected static void WriteToLog(string message)
		{
			message = DateTime.Now.ToString("[dd/MM/yyyy HH:mm:ss:ffff]", DateTimeFormatInfo.InvariantInfo) +
				GetThreadName() + message;
			lock (typeof(Log))
			{
				try
				{
					mswLogFileDirectory.WriteLine(message);
					mswLogFileDirectory.Flush();
				}
				catch (Exception) { }
			}
			return;
		}

		/// <summary>
		/// Initializes the Log file
		/// </summary>
		/// <param name="logFileDirectory">Log directory</param>
		/// <returns>Status of the initialization</returns>
		public static bool Init(string logFileDirectory)
		{
			return Init(logFileDirectory, null);
		}

		/// <summary>
		/// Initializes the Log file
		/// </summary>
		/// <param name="logFileDirectory">Log directory</param>
		/// <param name="logFilePrefix">Prefix added to the log file generated</param>
		/// <returns>Status of the initialization</returns>
		public static bool Init(string logFileDirectory, string logFilePrefix)
		{
			bool lblnLogInitialized = false;
			if (null != logFileDirectory)
			{
				try
				{
					//Create Directory, if required
					DirectoryInfo ldiLogDir = new DirectoryInfo(logFileDirectory);
					ldiLogDir.Create();

					//If Directory successfully created, proceed.
					logFileDirectory = logFileDirectory.TrimEnd(new char[] { '\\', '/' }) + @"\";

					//Generate the file name for the log-file
					string lstrlogFileName = "";
					if (null != logFilePrefix && 0 < logFilePrefix.Length)
						lstrlogFileName += logFilePrefix + "-";

					lstrlogFileName += DateTime.Now.ToString("yyyy_MM_dd_HHmmssffff", DateTimeFormatInfo.InvariantInfo) + ".log";
					mstrLogFileName = logFileDirectory + lstrlogFileName;

					mswLogFileDirectory = new StreamWriter(logFileDirectory + lstrlogFileName);
					lblnLogInitialized = true;
				}
				catch
				{
					//Do nothing
				}
			}
			return lblnLogInitialized;
		}

		/// <summary>
		/// Closes the already open LogFileDirectory writer
		/// Use this function call only when destroying the Log object.
		/// </summary>
		public static void Close()
		{
			if (null != mswLogFileDirectory)
				mswLogFileDirectory.Close();
		}
	}

	/// <summary>
	/// LogEventType
	/// </summary>
	public enum LogEventType
	{
		INFO = 0,
		ERROR = 1,
		WARNING = 2
	}
}
