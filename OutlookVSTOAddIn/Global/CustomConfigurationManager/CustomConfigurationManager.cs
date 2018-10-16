using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookVSTOAddIn.Global.CustomConfigurationManager
{
    class CustomConfigurationManager
    {
        public static string DefaultUser
        {
            get
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                CustomPropertiesSection myCustomPropertiesSection = config.GetSection("customProperties") as CustomPropertiesSection;

                return myCustomPropertiesSection.DefaultUser.Value;
            }

            set
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                CustomPropertiesSection myCustomPropertiesSection = config.GetSection("customProperties") as CustomPropertiesSection;

                myCustomPropertiesSection.DefaultUser.Value = value;

                config.Save();
            }
        }

        public static string GetBaseUrl(string envName)
        {
            string result = "";

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                CustomPropertiesSection myCustomPropertiesSection = config.GetSection("customProperties") as CustomPropertiesSection;

                if (myCustomPropertiesSection == null)
                {
                    //Console.WriteLine("Failed to load UrlsSection.");
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
                // Write to log
            }

            return result;
        }

        public static Dictionary<string,string> GetDocumentGroupList()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Get the MyUrls section.
                CustomPropertiesSection myCustomPropertiesSection = config.GetSection("customProperties") as CustomPropertiesSection;

                if (myCustomPropertiesSection == null)
                {
                    //Console.WriteLine("Failed to load UrlsSection.");
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
                // Write to log
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

        public static string GetDefaultDocumentGroup()
        {
            string result = "";

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                CustomPropertiesSection myCustomPropertiesSection = config.GetSection("customProperties") as CustomPropertiesSection;

                result = myCustomPropertiesSection.DocumentGroups.Default;
            }
            catch (Exception ex)
            {
                // Write to log
            }

            return result;
        }

    }
}
