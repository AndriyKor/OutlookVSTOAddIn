using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OutlookVSTOAddIn.Global
{
    /*
    public partial class ItemTypes
    {
        private static ItemTypes instance;
        private XmlDocument xmlMain;
        private HttpStatusCode statusCode = HttpStatusCode.Unused;
        private List<ItemType> itemTypes;
        private static FileLogger logger = FileLogger.Instance;

        private ItemTypes()
        {
            // this.Force();
        }

        public static ItemTypes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemTypes();
                }

                return instance;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return statusCode;
            }
        }

        public List<ItemType> Get()
        {
            if (itemTypes == null)
            {
                if (xmlMain != null)
                {
                    itemTypes = new List<ItemType>();

                    XmlNodeList nodeList;
                    XmlNode root = xmlMain.DocumentElement;
                    nodeList = root.SelectNodes("entity");

                    foreach (XmlNode node in nodeList)
                    {
                        // take header fields for entity
                        ItemType item = new ItemType();
                        item.Name = node.SelectSingleNode("name")?.LastChild.InnerText;
                        item.Desc = node.SelectSingleNode("desc")?.LastChild.InnerText;
                        item.Type = node.SelectSingleNode("type")?.LastChild.InnerText;
                        item.ClaSS = node.SelectSingleNode("class")?.LastChild.InnerText;
                        item.Root = node.SelectSingleNode("root")?.LastChild.InnerText;
                        item.Search = node.SelectSingleNode("search")?.LastChild.InnerText;
                        item.ResEnabled = node.SelectSingleNode("resEnabled")?.LastChild.InnerText;
                        item.Attrs = new List<ItemAttribute>();
                        item.Attr = new ItemAttribute();

                        // fill in attribute list
                        XmlNodeList attributes = node.SelectNodes("attrs/attr");
                        foreach (XmlNode attrNode in attributes)
                        {
                            ItemAttribute attr = new ItemAttribute();
                            attr.Name = attrNode.SelectSingleNode("name")?.LastChild.InnerText;
                            attr.Desc = attrNode.SelectSingleNode("desc")?.LastChild.InnerText;
                            attr.Type = attrNode.SelectSingleNode("type")?.LastChild.InnerText;
                            attr.Qual = attrNode.SelectSingleNode("qual")?.LastChild.InnerText;
                            attr.Flag = attrNode.SelectSingleNode("flag")?.LastChild.InnerText;
                            attr.Repr = attrNode.SelectSingleNode("repr")?.LastChild.InnerText;
                            attr.Size = attrNode.SelectSingleNode("size")?.LastChild.InnerText;

                            item.Attrs.Add(attr);
                        }

                        // take attributes of entity itself
                        XmlNode attribute = node.SelectSingleNode("attr");
                        if (attribute != null)
                        {
                            item.Attr.Name = attribute.SelectSingleNode("name")?.LastChild.InnerText;
                            item.Attr.Desc = attribute.SelectSingleNode("desc")?.LastChild.InnerText;
                            item.Attr.Type = attribute.SelectSingleNode("type")?.LastChild.InnerText;
                            item.Attr.Qual = attribute.SelectSingleNode("qual")?.LastChild.InnerText;
                            item.Attr.Flag = attribute.SelectSingleNode("flag")?.LastChild.InnerText;
                            item.Attr.Repr = attribute.SelectSingleNode("repr")?.LastChild.InnerText;
                            item.Attr.Max = attribute.SelectSingleNode("max")?.LastChild.InnerText;
                            item.Attr.Min = attribute.SelectSingleNode("min")?.LastChild.InnerText;
                            item.Attr.Size = attribute.SelectSingleNode("size")?.LastChild.InnerText;
                        }

                        itemTypes.Add(item);
                    }
                }
            }

            return itemTypes;
        }

        public List<ItemType> Get(string filter)
        {
            List<ItemType> attributes = this.Get();
            List<ItemType> result = new List<ItemType>();

            if (filter.Contains("[EXT]"))
            {
                foreach (ItemType item in attributes)
                {
                    string name = item.Name;
                    if (name.StartsWith("M3_EXT_"))
                    {
                        result.Add(item);
                    }
                }
            }
            else {
                foreach (ItemType item in attributes)
                {
                    string name = item.Name;
                    if (name.Substring(name.Length - 3, 3) == filter)
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        
        public ItemType GetSingeItem(string name)
        {
            ItemType result = null;
            foreach (ItemType item in this.itemTypes)
            {
                string itemName = item.Name;
                if (itemName == name)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }
        

        
        public bool Force()
        {
            var url = Common.UrlFull + "/getEntities.jsp";
            bool result = false;

            Tuple<HttpStatusCode, XmlDocument> tuple;
            tuple = Common.callAPI(url, "GET", false);

            HttpStatusCode statusCode = tuple.Item1;
            XmlDocument xmlDoc = tuple.Item2;

            if (statusCode == HttpStatusCode.OK)
            {
                xmlMain = new XmlDocument();
                xmlMain = xmlDoc;

                result = true;
                this.statusCode = statusCode;
            }
            else
            {
                MessageBox.Show("Failed to retrieve Item Type list from server! Reason: " + statusCode.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log("Failed to retrieve Item Type list from server! Reason: " + statusCode.ToString());
            }

            return result;
        }
        

    }
*/
}