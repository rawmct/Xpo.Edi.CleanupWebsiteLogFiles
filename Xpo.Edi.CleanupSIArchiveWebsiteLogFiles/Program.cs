using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xpo.Edi.CleanupSIArchiveWebsiteLogFiles
{
    class Program
    {
        static Logger _logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {

            int websiteLogFiles = 0;
            int webserviceLogFiles = 0;
            int logCleanupServiceFiles = 0;

            try
            {
                string environment = ConfigurationManager.AppSettings["Environment"].ToString();
                _logger.Info("Started cleaning SI Archive Website log files on " + environment);

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SIArchiveWebsiteLogPath"].ToString()))
                {
                    websiteLogFiles = DeleteSIArchiveWebsiteLogFiles();
                    _logger.Info("Deleted " + websiteLogFiles + " SI Archive Website log files");
                }
                else
                    _logger.Info("SIArchiveWebsiteLogPath config setting is not set on this server");


                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SIArchiveWebserviceLogPath"].ToString()))
                {
                    webserviceLogFiles = DeleteSIArchiveWebserviceLogFiles();
                    _logger.Info("Deleted " + webserviceLogFiles + " SI Archive Webservice log files");
                }
                else
                    _logger.Info("SIArchiveWebserviceLogPath config setting is not set on this server");

                logCleanupServiceFiles = DeleteSICleanupServiceLogFiles();
                _logger.Info("Deleted " + logCleanupServiceFiles + " SI Archive Website cleanup service log files");

                _logger.Info("Completed cleaning SI Archive Website log files on " + environment);
            }
            catch (Exception ex)
            {
                _logger.Error("Program - Main", ex.Message.ToString());
            }
        }

        static int DeleteSIArchiveWebsiteLogFiles()
        {
            string sourceDirectoryPath = String.Empty;
            int fileCounter = 0;

            try
            {
                sourceDirectoryPath = ConfigurationManager.AppSettings["SIArchiveWebsiteLogPath"].ToString();
                DirectoryInfo source = new DirectoryInfo(sourceDirectoryPath);

                // Get info of each file into the directory
                foreach (FileInfo fi in source.GetFiles())
                {
                    var creationTime = fi.CreationTime;

                    if (creationTime < (DateTime.Now - new TimeSpan(30, 0, 0, 0)))
                    {
                        fi.Delete();
                        fileCounter = fileCounter + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Delete SI Archive Website Log Files", ex.Message.ToString());
            }

            return fileCounter;
        }

        static int DeleteSIArchiveWebserviceLogFiles()
        {
            string sourceDirectoryPath = String.Empty;
            int fileCounter = 0;

            try
            {
                sourceDirectoryPath = ConfigurationManager.AppSettings["SIArchiveWebserviceLogPath"].ToString();
                DirectoryInfo source = new DirectoryInfo(sourceDirectoryPath);

                // Get info of each file into the directory
                foreach (FileInfo fi in source.GetFiles())
                {
                    var creationTime = fi.CreationTime;

                    if (creationTime < (DateTime.Now - new TimeSpan(30, 0, 0, 0)))
                    {
                        fi.Delete();
                        fileCounter = fileCounter + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Delete SI Archive Webservice Log Files", ex.Message.ToString());
            }

            return fileCounter;
        }

        static int DeleteSICleanupServiceLogFiles()
        {
            string sourceDirectoryPath = String.Empty;
            int fileCounter = 0;

            try
            {
                sourceDirectoryPath = ConfigurationManager.AppSettings["SICleanupServiceLogPath"].ToString();
                DirectoryInfo source = new DirectoryInfo(sourceDirectoryPath);

                // Get info of each file into the directory
                foreach (FileInfo fi in source.GetFiles())
                {
                    var creationTime = fi.CreationTime;

                    if (creationTime < (DateTime.Now - new TimeSpan(30, 0, 0, 0)))
                    {
                        fi.Delete();
                        fileCounter = fileCounter + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Delete SI Cleanup Service Log Files", ex.Message.ToString());
            }

            return fileCounter;
        }
    }
    
}
