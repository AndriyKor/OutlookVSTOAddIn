using System;
using System.Configuration;
using System.Collections;

namespace OutlookVSTOAddIn.Global.CustomConfigurationManager
{
    public class DocumentGroupConfigElement : ConfigurationElement
    {
        // Constructor allowing name, url, and port to be specified.
        public DocumentGroupConfigElement(String newName, String newValue)
        {
            Name = newName;
            Value = newValue;
        }

        // Default constructor, will use default values as defined
        // below.
        public DocumentGroupConfigElement()
        {
        }

        // Constructor allowing name to be specified, will take the
        // default values for url and port.
        public DocumentGroupConfigElement(string elementName)
        {
            Name = elementName;
        }

        [ConfigurationProperty("name", DefaultValue = "M3_EXT", IsRequired = true, IsKey = true)]
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

        [ConfigurationProperty("value", DefaultValue = "M3 External [Merged]", IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
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