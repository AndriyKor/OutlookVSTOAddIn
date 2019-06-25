using System;
using System.Configuration;
using System.Collections;

namespace OutlookVSTOAddIn.Global.CustomConfigurationManager
{
    public class DocumentGroupsCollection : ConfigurationElementCollection
    {
        public DocumentGroupsCollection()
        {
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        [ConfigurationProperty("default", IsRequired = true)]
        public string Default
        {
            get
            {
                return (string)base["default"];
            }

            set
            {
                base["default"] = value;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DocumentGroupConfigElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new DocumentGroupConfigElement(elementName);
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((DocumentGroupConfigElement)element).Name;
        }

        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.ClearElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }

        public DocumentGroupConfigElement this[int index]
        {
            get
            {
                return (DocumentGroupConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public DocumentGroupConfigElement this[string Name]
        {
            get
            {
                return (DocumentGroupConfigElement)BaseGet(Name);
            }
        }

        public int IndexOf(DocumentGroupConfigElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(DocumentGroupConfigElement url)
        {
            BaseAdd(url);
            // Add custom code here.
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(DocumentGroupConfigElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }
    }
}