using System;

namespace GEIMS.Common
{
	public class ExceptionWritter
	{
		static WriteException objWrite = new WriteException();

		/// <summary>
		/// Returns false if not able to log the exception else true.
		/// </summary>
		/// <param name="Exc">Exception object</param>
		/// <returns>True or False</returns>
		public static bool Write(Exception Exc)
		{
			return (objWrite.WriteLog(Exc));
		}
	}

	public enum LogType
	{
		/// <summary>
		/// Write Error to Windows Event Log.
		/// </summary>
		EventLog,
		/// <summary>
		/// Write Error in XML file
		/// </summary>
		XMLLog,
		/// <summary>
		/// Write Error in Text file
		/// </summary>
		TextLog
	}

	public class WriteException
	{
		private string strLogFileName;
		private bool blnOverWriteFile;
		private bool blnWriteInnerTrace;
		private LogType LogTypeFile;

		/// <summary>
		/// Default Constructor for WriteException it will read values from Web.Config and write log file accordingly.
		/// </summary>
		public WriteException()
		{
			if (System.Configuration.ConfigurationSettings.AppSettings["LogPath"] != null)
				strLogFileName = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + System.Configuration.ConfigurationSettings.AppSettings["LogPath"]) + "ErrorLog" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml";
			else
				strLogFileName = AppDomain.CurrentDomain.BaseDirectory + "ErrorLog" + DateTime.Now.ToString("dd-MM-yyyy") + ".xml";

			blnOverWriteFile = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["OverWriteFileLogFile"]);
			blnWriteInnerTrace = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["WriteInnerTrace"]);
			//LogTypeFile = (LogType)Enum.Parse(LogType.XMLLog.GetType(), System.Configuration.ConfigurationSettings.AppSettings["LogType"]);
		}

		/// <summary>
		/// Creates Log file with the provided name.
		/// It can be set later also.
		/// </summary>
		/// <param name="LogFileName">Full Path of the File.</param>
		public WriteException(string LogFileName)
		{
			strLogFileName = LogFileName;
			blnOverWriteFile = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["OverWriteFileLogFile"]);
			blnWriteInnerTrace = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["WriteInnerTrace"]);
			LogTypeFile = (LogType)Enum.Parse(LogType.XMLLog.GetType(), System.Configuration.ConfigurationSettings.AppSettings["LogType"]);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="LogFileName">Full path of the file.</param>
		/// <param name="OverWriteFile">To Overwrite or not exisiting log file.</param>
		public WriteException(string LogFileName, bool OverWriteFile)
		{
			strLogFileName = LogFileName;
			blnOverWriteFile = OverWriteFile;
			blnWriteInnerTrace = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["WriteInnerTrace"]);
			LogTypeFile = (LogType)Enum.Parse(LogType.XMLLog.GetType(), System.Configuration.ConfigurationSettings.AppSettings["LogType"]);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="LogFileName">Full path of the file.</param>
		/// <param name="OverWriteFile">To Overwrite or not exisiting log file.</param>
		/// <param name="WriteInnerTrace">Write Inner Trace in log.</param>	
		public WriteException(string LogFileName, bool OverWriteFile, bool WriteInnerTrace)
		{
			strLogFileName = LogFileName;
			blnOverWriteFile = OverWriteFile;
			blnWriteInnerTrace = WriteInnerTrace;
			LogTypeFile = (LogType)Enum.Parse(LogType.XMLLog.GetType(), System.Configuration.ConfigurationSettings.AppSettings["LogType"]);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="LogFileName">Full path of the file.</param>
		/// <param name="OverWriteFile">To Overwrite or not exisiting log file.</param>
		/// <param name="WriteInnerTrace">Write Inner Trace in log.</param>	
		/// <param name="TypeOfLogFile">Type of Log file</param>
		public WriteException(string LogFileName, bool OverWriteFile, bool WriteInnerTrace, LogType TypeOfLogFile)
		{
			strLogFileName = LogFileName;
			blnOverWriteFile = OverWriteFile;
			blnWriteInnerTrace = WriteInnerTrace;
			LogTypeFile = TypeOfLogFile;
		}

		/// <summary>
		/// Gets or Sets the name of the log file.
		/// Needed for XML and Text log generation.
		/// </summary>
		public string LogFileName
		{
			get
			{
				return (strLogFileName);
			}
			set
			{
				strLogFileName = value;
			}
		}

		/// <summary>
		/// Get or Sets whether to overwrite existing Log file or not.
		/// Needed for XML and Text log generation.
		/// </summary>
		public bool OverWriteFile
		{
			get
			{
				return (blnOverWriteFile);
			}
			set
			{
				blnOverWriteFile = value;
			}
		}

		/// <summary>
		/// Get or Sets whether to iterate through Inner Execption or not.
		/// Needed for XML and Text log generation.
		/// </summary>
		public bool WriteInnerTrace
		{
			get
			{
				return (blnWriteInnerTrace);
			}
			set
			{
				blnWriteInnerTrace = value;
			}
		}


		/// <summary>
		/// Get or Sets the type of the log file.
		/// It can be XML,Text or EventLog.
		/// </summary>
		public Common.LogType LogFileType
		{
			get
			{
				return (LogTypeFile);
			}
			set
			{
				LogTypeFile = value;
			}
		}
		private void ToTextLog(Exception Error)
		{
			//Yet to implement
		}

		private void ToEventLog(Exception Error)
		{
			//Yet to implement
		}

		private void ToXMLLog(Exception Error)
		{
			System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
			System.Xml.XmlElement[] xmlEle = new System.Xml.XmlElement[6];
			string strTargetSite = "";

			if (System.IO.File.Exists(strLogFileName))
				xmlDoc.Load(strLogFileName);
			else
			{
				byte[] bytContents = System.Text.Encoding.Default.GetBytes("<Errors></Errors>");
				System.IO.FileStream objFile = System.IO.File.Create(strLogFileName);
				objFile.Write(bytContents, 0, bytContents.Length);
				objFile.Close();
				objFile = null;
			}
			xmlDoc.Load(strLogFileName);
			xmlEle[0] = GetNode(xmlDoc, "Exception", null, null);

			xmlEle[1] = GetNode(xmlDoc, "Description", null, Error.Message);
			xmlEle[0].AppendChild(xmlEle[1]);

			xmlEle[2] = GetNode(xmlDoc, "InnerTrace", null, GetInnerTrace(Error));
			xmlEle[0].AppendChild(xmlEle[2]);

			if (Error.TargetSite != null)
			{
				strTargetSite = Error.TargetSite.ToString();
			}

			xmlEle[3] = GetNode(xmlDoc, "Method", null, strTargetSite);
			xmlEle[0].AppendChild(xmlEle[3]);

			xmlEle[4] = GetNode(xmlDoc, "Trace", null, Error.StackTrace);
			xmlEle[0].AppendChild(xmlEle[4]);

			xmlEle[5] = GetNode(xmlDoc, "DateTime", null, System.DateTime.Now.ToString());
			xmlEle[0].AppendChild(xmlEle[5]);

			xmlDoc.DocumentElement.AppendChild(xmlEle[0]);
			xmlDoc.Save(strLogFileName);
		}

		private System.Xml.XmlElement GetNode(System.Xml.XmlDocument xmlDoc, string strName, System.Collections.Specialized.NameValueCollection xmlAttributes, string xmlInnerText)
		{
			System.Xml.XmlElement xmlEle;
			xmlEle = xmlDoc.CreateElement(strName);
			xmlEle.InnerText = xmlInnerText;
			if (xmlAttributes != null)
			{
				for (int i = 0; i < xmlAttributes.Count; i++)
				{
					xmlEle.SetAttribute(xmlAttributes.GetKey(i), null, xmlAttributes[i]);
				}
			}
			return xmlEle;
		}

		private string GetInnerTrace(Exception Exc)
		{
			System.Text.StringBuilder objReturn = new System.Text.StringBuilder();
			Exception objException = Exc;
			while (objException.InnerException != null)
			{
				objReturn.Append(objException.Message + "		");
				objException = objException.InnerException;
			}
			return objReturn.ToString();
		}


		/// <summary>
		/// Returns true if Execption is written successfully to the specified log file
		/// </summary>
		/// <param name="enmLogType">Determines to write Exception log.</param>
		/// <param name="Error">Error object to be written in the log.</param>
		/// <returns>True or False</returns>
		public bool WriteLog(LogType enmLogType, Exception Error)
		{
			try
			{
				//				if(enmLogType==LogType.EventLog)
				//					this.ToEventLog(Error);
				//				else if(enmLogType==LogType.TextLog)
				//					this.ToTextLog(Error);
				//				else if(enmLogType==LogType.XMLLog)
				this.ToXMLLog(Error);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Returns true if Execption is written successfully to the specified log file
		/// </summary>
		/// <param name="Exc">Exception object to be written in the log.</param>
		/// <returns>True or False</returns>
		public bool WriteLog(Exception Exc)
		{
			try
			{
				//				if(LogTypeFile==LogType.EventLog)
				//					this.ToEventLog(Exc);
				//				else if(LogTypeFile==LogType.TextLog)
				//					this.ToTextLog(Exc);
				//				else if(LogTypeFile==LogType.XMLLog)
				this.ToXMLLog(Exc);
			}
			catch (Exception innerExc)
			{
				//return false;				
				throw innerExc;
			}
			return true;
		}
	}
}
