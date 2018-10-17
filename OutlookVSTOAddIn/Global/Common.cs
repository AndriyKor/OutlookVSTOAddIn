using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;

namespace OutlookVSTOAddIn.Global
{
    class Common
    {
        // New Fields & Properties
        public static bool IsCredentialCorrect { get; set; } = false;

        private static readonly FileLogger logger = FileLogger.Instance;

        public enum FilterType
        {
            EntityNameStartWith,
            EntityNameEndWith,
            ContainsAttribute
        }

        public static string EscapeExtraChars(string inputString)
        {
            var resultString = "";

            if (!String.IsNullOrEmpty(inputString))
            {
                foreach (char itemChar in inputString.ToCharArray())
                {
                    string validCharEntity = "";
                    switch (itemChar)
                    {
                        case '\'':
                            validCharEntity = "&apos;";
                            break;
                        case '<':
                            validCharEntity = "&lt;";
                            break;
                        case '&':
                            validCharEntity = "&amp;";
                            break;
                        case '>':
                            validCharEntity = "&gt;";
                            break;
                        case '"':
                            validCharEntity = "&quot;";
                            break;
                        default:
                            validCharEntity = itemChar.ToString();
                            break;
                    }

                    resultString += validCharEntity;

                }
            }

            return resultString;
        }

    }

    public abstract class LogBase
    {
        protected readonly object lockObj = new object();

        public abstract void Log(string message);
    }

    public class FileLogger : LogBase
    {
        private static FileLogger instance;
        private readonly string filePath;

        public FileLogger()
        {
            System.Reflection.Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
            Uri uriCodeBase = new Uri(assemblyInfo.CodeBase);
            DateTime currentDate = DateTime.Now;

            this.filePath = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString()) + "\\Logs\\IDMLog_" + currentDate.Day.ToString() + currentDate.Month.ToString() + currentDate.Year.ToString() + ".log";
        }

        public static FileLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileLogger();
                }
                return instance;
            }
        }

        public override void Log(string message)
        {
            try
            {
                lock (lockObj)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                    {
                        DateTime currentDate = DateTime.UtcNow;
                        string prefix = currentDate.ToString("MMdd HHmmss");
                        streamWriter.WriteLine(prefix + ": " + message);
                        streamWriter.Close();
                    }
                }
            }
            catch (Exception)
            {
                // do nothing
            }
        }
    }
}