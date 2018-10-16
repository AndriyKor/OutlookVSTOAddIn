using System;
using System.Configuration;
using System.Collections;

namespace OutlookVSTOAddIn.Global.CustomConfigurationManager
{
    public class UrlConfigElement : ConfigurationElement
    {
        // Constructor allowing name, url, and port to be specified.
        public UrlConfigElement(String newName, String newUrl, int newPort)
        {
            Name = newName;
            Url = newUrl;
            Port = newPort;
        }

        // Default constructor, will use default values as defined
        // below.
        public UrlConfigElement()
        {
        }

        // Constructor allowing name to be specified, will take the
        // default values for url and port.
        public UrlConfigElement(string elementName)
        {
            Name = elementName;
        }

        [ConfigurationProperty("name", DefaultValue = "TST", IsRequired = true, IsKey = true)]
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

        [ConfigurationProperty("url", DefaultValue = "http://m3v10.victaulic.com", IsRequired = true)]
        [RegexStringValidator(@"\w+:\/\/[\w.]+\S*")]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("port", DefaultValue = (int)21105, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 99999, ExcludeRange = false)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }

        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
            // You can your custom processing code here.
        }

        protected override bool SerializeElement(System.Xml.XmlWriter writer, bool serializeCollectionKey)
        {
            bool ret = base.SerializeElement(writer, serializeCollectionKey);
            // You can enter your custom processing code here.
            return ret;

        }

        protected override bool IsModified()
        {
            bool ret = base.IsModified();
            // You can enter your custom processing code here.
            return ret;
        }
    }
}