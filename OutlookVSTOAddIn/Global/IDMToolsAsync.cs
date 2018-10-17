using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace OutlookVSTOAddIn.Global
{
    #region General Types

    public class Value
    {
        public string name { get; set; }
        public string desc { get; set; }
    }

    public class ValueSet
    {
        public List<Value> value { get; set; }
    }

    public class Attr
    {
        private Attr()
        {
            //Console.WriteLine("Calling default constructor for Attr class");
        }

        public Attr(
            string name, string desc, string type, string qual, string @default, string required, string unique,
            string searchable, string repr, string max, string min, string size, string value) : base()
        {
            this.name = name;
            this.desc = desc;
            this.type = type;
            this.qual = qual;
            this.@default = @default;
            this.required = required;
            this.unique = unique;
            this.searchable = searchable;
            this.repr = repr;
            this.max = max;
            this.min = min;
            this.size = size;
            this.value = value;
        }

        public Attr(string name, string type, string qual, string value) : this(name, "", type, qual, "", "", "", "", "", "", "", "", value)
        //public Attr(string name, string type, string qual, string value) : this(name, null, type, qual, null, null, null, null, null, null, null, null, value)
        {
        }

        public Attr(string name, string value) : this(name, "", "", value)
        {
        }

        public string name { get; set; }
        public string desc { get; set; }

        /* 
         * 1:  Character / Variable Character / CLOB
         * 3:  Short Integer
         * 4:  Long Integer
         * 6:  Decimal
         * 7:  Date
         * 8:  Time
         * 9:  Time Stamp
         * 10: Double
         * 20: Short Integer (Min: 0, Max: 1)
        */

        /*
         * 1, 3, 4, 7, 9, 20 are using at this moment
        */
        public string type { get; set; }

        public string qual { get; set; }
        public string @default { get; set; }
        public string required { get; set; }
        public string unique { get; set; }
        public string searchable { get; set; }
        public string repr { get; set; }
        public string max { get; set; }
        public string min { get; set; }
        public string size { get; set; }
        public ValueSet valueset { get; set; }

        // added for using as retrived document type
        // will be used for uploading documnet
        public string value { get; set; }
    }

    public class Attrs
    {
        public List<Attr> attr { get; set; } = new List<Attr>();
    }

    public class Acl
    {
        private Acl()
        {
            //Console.WriteLine("Calling default constructor for Acl class");
        }

        public Acl(string name, string desc) : base()
        {
            this.name = name;
            this.desc = desc;
        }

        public Acl(string name) : this(name, "")
        {
        }

        public string name { get; set; }
        public string desc { get; set; }
    }

    public class Acls
    {
        public List<Acl> acl { get; set; } = new List<Acl>();
    }

    #endregion

    #region Entities Structure

    public class Entity
    {
        public Entity()
        {
            name = "Empty";
        }

        public Entity(string name, string desc, string root, string search, string resEnabled, Attrs attrs, Acls acls) : base()
        {
            this.name = name;
            this.desc = desc;
            this.root = root;
            this.search = search;
            this.resEnabled = resEnabled;
            this.attrs = attrs;
            this.acls = acls;
        }

        public string name { get; set; } = "";
        public string desc { get; set; } = "";
        public string root { get; set; } = "";
        public string search { get; set; } = "";
        public string resEnabled { get; set; } = "";
        public Attrs attrs { get; set; } = new Attrs();
        public Acls acls { get; set; } = new Acls();
    }

    public class Entities
    {
        public List<Entity> entity { get; set; } = new List<Entity>();
    }

    public class RootObjectEntities
    {
        public Entities entities { get; set; }
    }

    #endregion

    #region Item Retrieve Structure

    public class Res
    {
        private Res()
        {
        }

        public Res(string name, string size, string mimetype, string filename, string url) : base()
        {
            this.name = name;
            this.size = size;
            this.mimetype = mimetype;
            this.filename = filename;
            this.url = url;
        }

        public string name { get; set; }
        public string size { get; set; }
        public string mimetype { get; set; }
        public string filename { get; set; }
        public string url { get; set; }
    }

    public class Resrs
    {
        public List<Res> res { get; set; } = new List<Res>();
    }

    public class Item
    {
        private Item()
        {
            //Console.WriteLine("Calling default constructor for Item class");
        }

        public Item(string createdBy, DateTime createdTS, string lastChangedBy, DateTime lastChangedTS, string pid, string id,
            string version, string reprItem, string entityName, Attrs attrs, Resrs resrs, Acl acl) : base()
        {
            this.createdBy = createdBy;
            this.createdTS = createdTS;
            this.lastChangedBy = lastChangedBy;
            this.lastChangedTS = lastChangedTS;
            this.pid = pid;
            this.id = id;
            this.version = version;
            this.reprItem = reprItem;
            this.entityName = entityName;
            this.attrs = attrs;
            this.resrs = resrs;
            this.acl = acl;
        }

        public Item(string entityName, Attrs attrs, Resrs resrs, Acl acl) : this("", DateTime.Today, "", DateTime.Today, "", "", "", "", entityName, attrs, resrs, acl)
        {
        }

        public Item(string entityName, Attrs attrs, Resrs resrs) : this(entityName, attrs, resrs, new Acl(""))
        {
        }

        public string createdBy { get; set; }
        public DateTime createdTS { get; set; }
        public string lastChangedBy { get; set; }
        public DateTime lastChangedTS { get; set; }
        public string pid { get; set; }
        public string id { get; set; }
        public string version { get; set; }
        public string reprItem { get; set; }
        public string entityName { get; set; }
        public Attrs attrs { get; set; }
        public Resrs resrs { get; set; }
        public Acl acl { get; set; }
    }

    public class RootObjectItem
    {
        public Item item { get; set; }
    }
    #endregion

    #region Item Create Structure

    public class ResCreate
    {
        private ResCreate()
        {
        }

        public ResCreate(string entityName, string base64, string filename) : base()
        {
            this.entityName = entityName;
            this.base64 = base64;
            this.filename = filename;
        }

        public string entityName { get; set; }
        public string base64 { get; set; }
        public string filename { get; set; }
    }

    public class ResrsCreate
    {
        public List<ResCreate> res { get; set; }
    }

    public class ItemCreate
    {
        private ItemCreate()
        {
            //Console.WriteLine("Calling default constructor for Item class");
        }

        public ItemCreate(string createdBy, DateTime createdTS, string lastChangedBy, DateTime lastChangedTS, string pid, string id,
            string version, string reprItem, string entityName, Attrs attrs, ResrsCreate resrs, Acl acl) : base()
        {
            this.createdBy = createdBy;
            this.createdTS = createdTS;
            this.lastChangedBy = lastChangedBy;
            this.lastChangedTS = lastChangedTS;
            this.pid = pid;
            this.id = id;
            this.version = version;
            this.reprItem = reprItem;
            this.entityName = entityName;
            this.attrs = attrs;
            this.resrs = resrs;
            this.acl = acl;
        }

        public ItemCreate(string entityName, Attrs attrs, ResrsCreate resrs, Acl acl) : this("", DateTime.Today, "", DateTime.Today, "", "", "", "", entityName, attrs, resrs, acl)
        {
        }

        public ItemCreate(string entityName, Attrs attrs, ResrsCreate resrs) : this(entityName, attrs, resrs, null)
        {
        }

        public string createdBy { get; set; }
        public DateTime createdTS { get; set; }
        public string lastChangedBy { get; set; }
        public DateTime lastChangedTS { get; set; }
        public string pid { get; set; }
        public string id { get; set; }
        public string version { get; set; }
        public string reprItem { get; set; }
        public string entityName { get; set; }
        public Attrs attrs { get; set; }
        public ResrsCreate resrs { get; set; }
        public Acl acl { get; set; }
    }

    public class RootObjectItemCreate
    {
        private RootObjectItemCreate()
        {
        }

        public RootObjectItemCreate(ItemCreate item) : base()
        {
            this.item = item;
        }

        public ItemCreate item { get; set; }
    }

    #endregion

    #region Error Handling Classes

    public class Error
    {
        public string code { get; set; } = "";
        public string message { get; set; } = "";
        public string detail { get; set; } = "";
    }

    public class RootObjectError
    {
        public Error error { get; set; } = new Error();
    }

    #endregion

    static class IDMToolsAsync
    {
        // Define Http Client that is used for communication with IDM APIs
        static HttpClient client;
        static NetworkCredential userCredentials = CredentialCache.DefaultNetworkCredentials;

        // Define Credentials
        public static NetworkCredential UserCredentials
        {
            get { return userCredentials; }
            set
            {
                userCredentials = value;

                // Update client header
                var authenticationBytes = Encoding.ASCII.GetBytes(userCredentials.UserName + ":" + userCredentials.Password);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
            }
        }

        public static Uri BaseUrl
        {
            get { return client.BaseAddress; }
        }

        // Default Static Constructor
        static IDMToolsAsync()
        {
            // Set default HttClient setup
            var httpClientHandler = new HttpClientHandler()
            {
                Credentials = UserCredentials
            };

            client = new HttpClient(httpClientHandler)
            {
                //BaseAddress = new Uri(CustomConfigurationManager.CustomConfigurationManager.GetBaseUrl("TST"))
                BaseAddress = new Uri(CustomConfigurationManager.CustomConfigurationManager.GetBaseUrl())
            };


            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Something else
            // ...

        }

        // Login
        internal static async Task<HttpStatusCode> Login()
        {
            var url = "ca/api/connection/login";
            HttpResponseMessage response = await client.GetAsync(url);

            //response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        // Logout
        internal static async Task<HttpStatusCode> Logout()
        {
            var url = "ca/api/connection/logout";
            HttpResponseMessage response = await client.GetAsync(url);

            return response.StatusCode;
        }

        // Get the list of Entities
        internal static async Task<Tuple<RootObjectEntities, HttpStatusCode>> GetEntitiesAsync()
        {
            RootObjectEntities rootObject = null;
            var url = "ca/api/datamodel/entities";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                rootObject = await response.Content.ReadAsAsync<RootObjectEntities>();
            }

            return Tuple.Create(rootObject, response.StatusCode);
        }

        // Upload Item
        internal static async Task<Tuple<HttpStatusCode, Error>> CreateItemAsync(RootObjectItemCreate item)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "ca/api/items", item);

            RootObjectError rootObjectError = new RootObjectError();

            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                rootObjectError = await response.Content.ReadAsAsync<RootObjectError>();
            }

            return new Tuple<HttpStatusCode, Error>(response.StatusCode, rootObjectError.error);
        }

        // Print Entity to Command Line
        static void ShowEntity(Entity entity)
        {
            Console.WriteLine($"Name: {entity.name}" +
                $"\tDesc: " + $"{entity.desc}" +
                $"\tRoot: {entity.root}" +
                $"\tSearch: {entity.search}" +
                $"\tResEnabled: {entity.resEnabled}" + "\r\n");
        }
    }
}
