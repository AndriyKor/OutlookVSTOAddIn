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

namespace OutlookVSTOAddIn.Global
{
    class Common
    {
        private static NetworkCredential credentials;
        // private static string urlBase = "http://eu-be-sos05:21105/ca"; // TST
        // private static string urlBase = "http://eu-be-sos05:20105/ca"; // PRD
        private static string urlFull = urlBase + "/secure-jsp/mds/api";
        private static FileLogger logger = FileLogger.Instance;

        public static Tuple<HttpStatusCode, XmlDocument> callAPI(string url, string method, bool withCredentials, string xml = "")
        {
            logger.Log("API call: " + url + " with method " + method + ". Credentials: " + withCredentials.ToString());

            HttpStatusCode httpStatusCode;
            XmlDocument xmlResponse = new XmlDocument();


            WebRequest request = WebRequest.Create(url);
            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Method = method;
            request.ContentType = "application/xml";

            if (withCredentials)
            {
                if (credentials == null)
                {
                    LogInForm loginForm = new LogInForm();
                    loginForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult result = loginForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string user = loginForm.UserName;
                        string password = loginForm.Password;
                        credentials = new NetworkCredential(user, password);
                        url = UrlFull + "/login.jsp";

                        Tuple<HttpStatusCode, XmlDocument> resp = callAPI(url, "GET", true);

                        if (resp.Item1 == HttpStatusCode.OK)
                        {
                            // log in successfull. Go on
                            request.Credentials = credentials;
                            logger.Log("Login successful!");
                        }
                        else
                        {
                            MessageBox.Show("Login failed! Reason: " + resp.Item1.ToString(), "Authorization error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger.Log("Login failed! Result: " + resp.Item2.ToString());
                            credentials = null;

                            XmlDocument errorXml = new XmlDocument();
                            errorXml.LoadXml("<error>Unauthorized</error>");
                            return new Tuple<HttpStatusCode, XmlDocument>(HttpStatusCode.Unauthorized, errorXml);
                        }
                    }
                    else
                    {
                        XmlDocument errorXml = new XmlDocument();
                        errorXml.LoadXml("<error>Unauthorized</error>");
                        return new Tuple<HttpStatusCode, XmlDocument>(HttpStatusCode.Unauthorized, errorXml);
                    }
                }
                else
                {
                    request.Credentials = credentials;
                }
            }
            else
            {
                request.Credentials = CredentialCache.DefaultCredentials;
            }

            Stream dataStream;

            try
            {
                if (method == "POST")
                {
                    string postData = xml;
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = byteArray.Length;
                    dataStream = request.GetRequestStream();

                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                using (WebResponse response = request.GetResponse())
                {
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string respText = reader.ReadToEnd();

                    reader.Close();
                    dataStream.Close();

                    httpStatusCode = ((HttpWebResponse)response).StatusCode;
                    xmlResponse = validateXml(respText);

                    response.Close();

                    logger.Log("API call: successful!");
                }
            }
            catch (WebException ex)
            {
                string respText = ex.Message;
                xmlResponse.LoadXml("<error>" + respText + "</error>");
                logger.Log("API call failed!");
                logger.Log(ex.StackTrace);

                if (ex.Response == null)
                {
                    switch (ex.Status)
                    {
                        case WebExceptionStatus.NameResolutionFailure:
                        case WebExceptionStatus.ConnectFailure:
                        case WebExceptionStatus.ProtocolError:
                        case WebExceptionStatus.ConnectionClosed:
                        case WebExceptionStatus.ServerProtocolViolation:
                        case WebExceptionStatus.ProxyNameResolutionFailure:
                            httpStatusCode = HttpStatusCode.ServiceUnavailable;
                            break;
                        case WebExceptionStatus.Pending:
                        case WebExceptionStatus.Timeout:
                            httpStatusCode = HttpStatusCode.GatewayTimeout;
                            break;
                        default:
                            httpStatusCode = HttpStatusCode.Unused;
                            break;
                    }
                }
                else
                {
                    using (WebResponse response = ex.Response)
                    {
                        httpStatusCode = ((HttpWebResponse)response).StatusCode;
                    }
                }
            }
            return Tuple.Create(httpStatusCode, xmlResponse);
        }

        private static XmlDocument validateXml(string xml)
        {
            XmlDocument result = new XmlDocument();

            try
            {
                result.LoadXml(xml);
            }
            catch (XmlException ex)
            {
                result.LoadXml("<error>" + ex.Message + "</error>");
            }

            return result;
        }

        private static string urlBase
        {
            get
            {
                string result = "";

                try
                {
                    XmlDocument configurationDocument = new XmlDocument();

                    //using (Stream s = File.OpenRead(Constants.configurationFileFullPath))
                    using (Stream s = File.Open(Constants.configurationFileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        configurationDocument.Load(s);
                    }

                    XmlNode serverAPIUrl= configurationDocument.DocumentElement.SelectSingleNode("/appSettings/serverAPIUrl");

                    result = serverAPIUrl.InnerText;

                }
                catch (Exception ex)
                {
                    result = "ERROR_URL";
                    Console.WriteLine("Error while loading xml configuration file");
                    Console.WriteLine(ex.Message);
                }

                return result;
            }
        }

        public static string UrlFull
        {
            get
            {
                return urlFull;
            }
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
        private string filePath;

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