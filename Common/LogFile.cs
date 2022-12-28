using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.IO;
using System.IO.Compression;

namespace GM.CommonLibs.Common
{

    public class LogFile 
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();   
        private string _appName;  
        private string _appType;  
        private string _controllerName;   

        public LogFile(IConfiguration configuration)   
        {
            _appName = configuration.GetSection("AppName").Value;   
            _appType = configuration.GetSection("AppType").Value;    
        }  

        public LogFile(IConfiguration configurationName, string controllerName)       
        {
            _appName = configuration.GetSection("AppName").Value;  
            _appType = configuration.GetSection("AppType").Value;  
            _controllerName = controllerName;   
        }    

        public void WriteLog(string controllerName, string strLog)   
        {
            string msg = string.Empty;

            if (_appType == "Web")  
            {
                msg = _appName + "|" + controllerName + "| Info : " + string;   
            }  
            else if (_appType == "Service")   
            {
                msg = _appName + "|" + controllerName + "| Info : " + strLog;   
            }

            Logger.Info(msg);  
        }  

        public void WriteLog(string strLog)  
        {
            WriteLog(_controllerName, strLog);   
        }

        public void WriteLogConsole(string strLog, string path, string Function = "")   
        {
            string strInfo = "Info : " + strLog; 
            Console.WriteLine(strInfo);  
            strLog = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "|" + -_appName + "|" + Function + "|" + strInfo;    

            string logFilePath = path + DateTime.Now.ToString("yyyy-MM-dd") + "_" + Function + ".txt";   
            var logFileInfo = new FileInfo(logFilePath);   
            var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);    
            if (!logDirInfo.Exists) logDirInfo.Create();   
            if (!logFileInfo.Exists)
            {
                using (FileStream fileStream = logFileInfo.Create())   
                {
                    using (StreamWriter log = new StreamWriter(fileStream))   
                    {
                        log.WriteLine(strLog);     
                        log.Close();
                    }
                }
            } 
            else
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))   
                {
                    using (StreamWriter log = new StreamWriter(fileStream))   
                    {
                        log.WriteLine(strLog);   
                        log.Close();   
                    }
                }
            }

            #region ZIP OLD LOG   
            logFilePath = path + "Log-" + DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + "_" + Function + ".txt";     
            logFileInfo = new FileInfo(logFilePath);   
            logFileInfo = new DirectoryInfo(logFileInfo.DirectoryName);     
            if (logDirInfo.Exists && logFileInfo.Exists)   
            {
                ZipFile(logFileInfo);  
                file.Delete(logFileInfo.FullName);  
            }
            #endregion  
        }  

        private static void ZipFile(FileInfo fi)   
        {
            // Get the stream of the source file.  
            using (FileStream inFile = fi.OpenRead())  
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".zip")    
                {
                    // Create the compressed file.   
                    using (FileStream outFile = File.Create(fi.FullName + ".zip"))    
                    {
                        using (GZipStream compress = new GZipStream(outFile, CompressionMode.Compress))   
                        {
                            // Copy the source file into  
                            // the compression stream.  
                            inFile.CopyTo(compress);   
                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.", fi.Name, fi.Length.ToString(), outFile.Length.ToString());

                        }
                    }
                }
            }
        }

    }
}