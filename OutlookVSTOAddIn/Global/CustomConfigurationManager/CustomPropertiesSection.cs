using System;
using System.Configuration;
using System.Collections;


namespace OutlookVSTOAddIn.Global.CustomConfigurationManager
{
    // Define a custom section containing an individual
    // element and a collection of elements.
    public class CustomPropertiesSection : ConfigurationSection
    {
        [ConfigurationProperty("name", DefaultValue = "customProperties", IsRequired = true, IsKey = false)]
        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("defaultUser", IsDefaultCollection = false)]
        public DefaultConfigElement DefaultUser
        {
            get
            {
                DefaultConfigElement defaultUser = (DefaultConfigElement)base["defaultUser"];
                return defaultUser;
            }
        }

        [ConfigurationProperty("documentGroups", IsDefaultCollection = false)]
        public DocumentGroupsCollection DocumentGroups
        {
            get
            {
                DocumentGroupsCollection documentGroupsCollection = (DocumentGroupsCollection)base["documentGroups"];
                return documentGroupsCollection;
            }
        }

        // Declare a collection element represented 
        // in the configuration file by the sub-section
        // <urls> <add .../> </urls> 
        // Note: the "IsDefaultCollection = false" 
        // instructs the .NET Framework to build a nested 
        // section like <urls> ...</urls>.
        [ConfigurationProperty("urls", IsDefaultCollection = false)]
        public UrlsCollection Urls
        {
            get
            {
                UrlsCollection urlsCollection = (UrlsCollection)base["urls"];
                return urlsCollection;
            }
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
            // You can add custom processing code here.
        }

        protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            string s = base.SerializeSection(parentElement, name, saveMode);
            // You can add custom processing code here.
            return s;
        }
    }
}