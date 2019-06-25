using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookVSTOAddIn.Global.CustomConfigurationManager
{
    class CustomConfigurationManager
    {
        private static FileLogger logger = FileLogger.Instance;

        private static Configuration config;

        private static Configuration configuration
        {
            get
            {
                if (config == null)
                {
                    
                    //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                    UriBuilder uri = new UriBuilder(codeBase);
                    string path = Uri.UnescapeDataString(uri.Path);

                    config = ConfigurationManager.OpenExeConfiguration(path);

                    // - - - 

                    string cfgLocation = Assembly.GetExecutingAssembly().Location.Replace(Assembly.GetExecutingAssembly().ManifestModule.Name, "");
                    config.SaveAs(cfgLocation + "IDM_Plugin_Outlook.config");

                    // - - - 



                    /*
                    string location = Assembly.GetExecutingAssembly().Location;
                    string moduleName = Assembly.GetExecutingAssembly().ManifestModule.Name;
                    string cfgLocation = "";

                    // Is it OK? Does 'location' always contains 'moduleName'? Let's assume - YES
                    if (location.EndsWith(moduleName))
                    {
                        cfgLocation = location.Replace(moduleName, "");
                    }
                    else
                    {
                        cfgLocation = location;
                    }
                    
                    //config.SaveAs(cfgLocation + @"cfg_default.xml", ConfigurationSaveMode.Modified);

                    config = ConfigurationManager.OpenExeConfiguration(cfgLocation + "configuration.xml");
                    //Configuration tmp = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                    */
                }

                return config;
            }
        }

        /*
        // Issue with saving config file
        // Reason: access denied
        // To be fixed in next build
        // Using: Login Form 
        public static string DefaultUser
        {
            get
            {
                CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;

                return myCustomPropertiesSection.DefaultUser.Value;
            }

            set
            {
                CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;

                myCustomPropertiesSection.DefaultUser.Value = value;

                config.Save();
            }
        }
        */

        internal static string GetBaseUrl(string envName)
        {
            string result = "";

            try
            {
                CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;

                if (myCustomPropertiesSection == null)
                {
                    logger.Log("Failed to load UrlsSection.");
                }
                else
                {
                    foreach (UrlConfigElement urlConfigElement in myCustomPropertiesSection.Urls)
                    {
                        if (urlConfigElement.Name == envName)
                        {
                            result = urlConfigElement.Url + ":" + urlConfigElement.Port + "/";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }

            return result;
        }

        internal static string GetBaseUrl()
        {
            string result = "";

            // get default BaseUrlName
            string baseUrlName = GetDefaultBaseUrlName();

            // get BaseUrl
            result = GetBaseUrl(baseUrlName);

            return result;
        }

        internal static Dictionary<string, string> GetBaseUrlList()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            try
            {
                // Get the MyUrls section.
                CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;

                if (myCustomPropertiesSection == null)
                {
                    logger.Log("Failed to load UrlsSection.");
                }
                else
                {
                    foreach (UrlConfigElement urlConfigElement in myCustomPropertiesSection.Urls)
                    {
                        result.Add(urlConfigElement.Name, urlConfigElement.Url + ":" + urlConfigElement.Port);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading configuration file. Reason: " + ex.Message, "IDM Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex.Message);
            }
            finally
            {
                if (result.Count == 0)
                {
                    result.Add("NO_DOCUMENT_GROUPS_FOUND", "No Document Groups found");
                }
            }

            return result;
        }

        internal static Dictionary<string,string> GetDocumentGroupList()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            try
            {

                // Get the MyUrls section.
                CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;
                //CustomPropertiesSection myCustomPropertiesSection = ConfigurationManager. as CustomPropertiesSection;

                if (myCustomPropertiesSection == null)
                {
                    logger.Log("Failed to load UrlsSection.");
                }
                else
                {
                    foreach (DocumentGroupConfigElement documentGroupConfigElement in myCustomPropertiesSection.DocumentGroups)
                    {
                        result.Add(documentGroupConfigElement.Name, documentGroupConfigElement.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading configuration file. Reason: " + ex.Message, "IDM Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex.Message);
            }
            finally
            {
                if (result.Count == 0)
                {
                    result.Add("NO_DOCUMENT_GROUPS_FOUND", "No Document Groups found");
                }
            }

            return result;
        }

        internal static string GetDefaultDocumentGroup()
        {
            string result = "";

            try
            {
                CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;

                result = myCustomPropertiesSection.DocumentGroups.Default;
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }

            return result;
        }

        internal static string GetDefaultBaseUrlName()
        {
            string result = "";

            try
            {
                CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;

                result = myCustomPropertiesSection.Urls.Default;
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }

            return result;
        }

        internal static void SetDefaultBaseUrl(string baseUrlName)
        {
            CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;
            myCustomPropertiesSection.Urls.Default = baseUrlName;

            //configuration.SaveAs(@"D:/cfg_dbu.xml", ConfigurationSaveMode.Full);

            try
            {
                configuration.Save(ConfigurationSaveMode.Modified);
                //configuration.SaveAs(Assembly.GetExecutingAssembly().Location + @"/cfg_dbu.xml", ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not able to save.");
                MessageBox.Show(ex.Message);
            }
        }

        internal static void SetDefaultDocumentGroup(string documentType)
        {
            CustomPropertiesSection myCustomPropertiesSection = configuration.GetSection("customProperties") as CustomPropertiesSection;
            myCustomPropertiesSection.DocumentGroups.Default = documentType;

            //configuration.SaveAs(@"D:/cfg_ddg.xml", ConfigurationSaveMode.Full);
            try
            {
                configuration.Save(ConfigurationSaveMode.Modified);
                //configuration.SaveAs(Assembly.GetExecutingAssembly().Location + @"/cfg_ddg.xml", ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not able to save.");
                MessageBox.Show(ex.Message);
            }
        }

    }
}
