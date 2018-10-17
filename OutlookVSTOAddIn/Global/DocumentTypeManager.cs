using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OutlookVSTOAddIn.Global
{
    class DocumentTypeManager
    {
        #region Fields & Properties

        public NetworkCredential UserCredentials { get; set; } = CredentialCache.DefaultNetworkCredentials;

        // Define status of the Login API call
        public static HttpStatusCode LoginAPICallStatus { get; set; } = HttpStatusCode.Unused;

        // Define status of the Logout API call
        public static HttpStatusCode LogoutAPICallStatus { get; set; } = HttpStatusCode.Unused;

        // Define status of the Retrieve Entities API call
        public static HttpStatusCode RetriveEntitiesAPICallStatus { get; private set; } = HttpStatusCode.Unused;

        // Define status of the last Create Item API call
        public static HttpStatusCode CreateItemAPICallStatus { get; private set; } = HttpStatusCode.Unused;

        // Define status of the last Create Item API call
        public static Error CreateItemAPICallResponceError { get; private set; } = new Error();

        #endregion

        #region Apply Singleton
        private static DocumentTypeManager instance = null;
        private static readonly object padlock = new object();

        private DocumentTypeManager()
        {
        }

        public static DocumentTypeManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DocumentTypeManager();
                    }
                    return instance;
                }
            }
        }
        #endregion

        #region Properties

        // Entities
        public List<Entity> Entities { get; private set; } = new List<Entity>();

        #endregion

        #region Async Functions

        // Login User
        public async Task Login()
        {
            IDMToolsAsync.UserCredentials = UserCredentials;
            LoginAPICallStatus = await IDMToolsAsync.Login();
        }

        // Logoff User
        public async Task Logout()
        {
            // Only if Login was successfull
            if (LoginAPICallStatus == HttpStatusCode.OK)
            {
                //IDMToolsAsync.UserCredentials = UserCredentials;
                LogoutAPICallStatus = await IDMToolsAsync.Logout();
            }
        }

        // Retrieve Entities + status of API call
        // If parameter "force" is true = always do request
        // P.S. Authorization NOT needed
        public async Task RetrieveEntities(bool force = false)
        {
            Tuple<RootObjectEntities, HttpStatusCode> responce = null;

            if (force || Entities.Count <= 0)
            {
                responce = await IDMToolsAsync.GetEntitiesAsync();

                // Get responce values
                Entities = responce.Item1.entities.entity;
                RetriveEntitiesAPICallStatus = responce.Item2;
            }
        }

        // Create Item + status of API call
        // P.S. Authorization needed
        public async Task CreateItem(ItemCreate item)
        {
            Tuple<HttpStatusCode, Error> responce;
            try
            {
                // Login user
                await Login();

                // If Login successfull, go on and create item
                if (LoginAPICallStatus == HttpStatusCode.OK)
                {
                    // Wrap item with root element (for JSON formating during serialization)
                    RootObjectItemCreate rootObjectItemCreate = new RootObjectItemCreate(item);

                    responce = await IDMToolsAsync.CreateItemAsync(rootObjectItemCreate);

                    // Get responce values
                    CreateItemAPICallStatus = responce.Item1;
                    CreateItemAPICallResponceError = responce.Item2;

                }
                // If not... do whatever you want, just not cry... take a candy ;)
                else
                {

                }
            }
            catch (Exception)
            {
                // handle Exception here
                throw;
            }
            finally
            {
                // Logout user
                await Logout();
            }
        }

        #endregion

        #region Public Functions

        // Get Entity by name
        public Entity GetSingleEntityByName(string name)
        {
            Entity result = new Entity();

            // Search Entity
            if (Entities != null)
            {
                foreach (Entity entity in Entities)
                {
                    if (entity.name == name)
                    {
                        result = entity;
                        break;
                    }
                }
            }

            return result;
        }

        // Get the list of Entitites with name starts with 'startWithName'
        public List<Entity> GetEntitiesWithFilter(string startWithName = "")
        {
            List<Entity> result = null;

            if (Entities != null)
            {
                foreach (Entity entity in Entities)
                {
                    if (entity.name.StartsWith(startWithName))
                    {
                        result.Add(entity);
                    }
                }
            }

            return result;
        }

        // Get the list of Entities by filter
        public List<Entity> GetEntites(string filter, Common.FilterType filterType)
        {
            List<Entity> result = new List<Entity>();

            if (Entities != null)
            {
                switch (filterType)
                {
                    case Common.FilterType.EntityNameStartWith:
                        foreach (Entity entity in Entities)
                        {
                            if (entity.name.StartsWith(filter))
                            {
                                result.Add(entity);
                            }
                        }
                        break;
                    case Common.FilterType.EntityNameEndWith:
                        foreach (Entity entity in Entities)
                        {
                            if (entity.name.EndsWith(filter))
                            {
                                result.Add(entity);
                            }
                        }
                        break;
                    case Common.FilterType.ContainsAttribute:
                        foreach (Entity entity in Entities)
                        {
                            // Apply Enumeration!!!!!!!!!!!!!!!!
                            foreach (Attr attr in entity.attrs.attr)
                            {
                                if (attr.name == filter)
                                {
                                    result.Add(entity);
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        #endregion

    }
}
