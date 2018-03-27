using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;

namespace LowFlightFare.Logging
{
    public class Logger
    {
        public void LogException(ExceptionContext filterContext)
        {
            string todayDate = DateTime.Today.Date.ToShortDateString();

            string _applicationRoot = AppliocationRoot;
            string applicationName = ApplicationName;

            //Remove last directory from path (We need solution root)
            _applicationRoot = _applicationRoot.Remove(_applicationRoot.LastIndexOf(applicationName));

            string filePath = _applicationRoot + "Logs\\Exception_" + todayDate.Replace("/", "") + ".txt";
            bool fileExist = File.Exists(filePath);

            if (fileExist)
                File.AppendAllText(filePath, ConstructLogFromException(filterContext));
            else
                File.WriteAllText(filePath, ConstructLogFromException(filterContext));

        }

        private string ConstructLogFromException(ExceptionContext filterContext)
        {
            StringBuilder logFile = new StringBuilder(10000);

            logFile.AppendLine("------------------------------------------------------------------------------");
            logFile.AppendLine("DATE - " + DateTime.Today.Date.ToShortDateString());
            logFile.AppendLine("TIME - " + DateTime.Now.TimeOfDay);
            logFile.AppendLine("USER - " + filterContext.HttpContext.User.Identity.Name);
            logFile.AppendLine("EXCEPTION - " + filterContext.Exception.Message);
            logFile.AppendLine("WHERE - " + filterContext.RouteData.Values.Select(x => x.Value.ToString()).Aggregate((x, y) => x + "/" + y + "/"));
            logFile.AppendLine("STACK TRACE:");
            logFile.AppendLine("******************************************************************************");
            logFile.Append(filterContext.Exception.StackTrace + Environment.NewLine);
            logFile.AppendLine("******************************************************************************");
            logFile.AppendLine("------------------------------------------------------------------------------");

            return logFile.ToString();
        }

        private string AppliocationRoot { get { return HostingEnvironment.ApplicationPhysicalPath; } }
        public string ApplicationName { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; } }
    }
}