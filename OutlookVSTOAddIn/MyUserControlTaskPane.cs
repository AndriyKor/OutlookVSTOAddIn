using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutlookVSTOAddIn.Global;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net;
using System.Xml;
using System.Reflection;
using OutlookVSTOAddIn.Global.CustomConfigurationManager;

namespace OutlookVSTOAddIn
{
    public partial class MyUserControlTaskPane : UserControl
    {
        private FileLogger logger = FileLogger.Instance;

        public MyUserControlTaskPane()
        {
            InitializeComponent();

            // Lunch task to retrieve entities
            //Task.Run(DocumentTypeManager.Instance.RetrieveEntities);
            Task.Run(() => DocumentTypeManager.Instance.RetrieveEntities());

            this.Load += MyUserControlTaskPane_Load;
        }

        private void MyUserControlTaskPane_Load(object sender, EventArgs e)
        {
            // Bind dictionary with divisions
            comboBoxDocumentGroup.DataSource = new BindingSource(CustomConfigurationManager.GetDocumentGroupList(), null);
            comboBoxDocumentGroup.DisplayMember = "Value";
            comboBoxDocumentGroup.ValueMember = "Key";

            // set default division
            string defaultDocumentGroup = CustomConfigurationManager.GetDefaultDocumentGroup();

            foreach (KeyValuePair<string, string> item in comboBoxDocumentGroup.Items)
            {
                if (item.Key == defaultDocumentGroup)
                {
                    comboBoxDocumentGroup.SelectedIndex = comboBoxDocumentGroup.Items.IndexOf(item);
                    break;
                }
            }
            
            //comboBoxDocumentGroup.SelectedIndex = 0;

            var productVersion = Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = "V" + productVersion;


        }

        private void MyUserControlTaskPane_Resize(object sender, EventArgs e)
        {
            panelAttributes.Height = this.Height - panelAttributes.Top - 70;
            buttonArchive.Top = this.Height - 60;
            labelVersion.Top = this.Height - 38;
        }

        private void comboBoxDocumentGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // test
            // var testConfig = CustomConfigurationManager.DefaultUser;

            // CustomConfigurationManager.DefaultUser = "123";

            // var testConfig2 = CustomConfigurationManager.DefaultUser;
            //var ttt = testConfig;

            // List<Entity> tmp = DocumentTypeManager.Instance.GetEntites("M3_CUNO", Common.FilterType.ContainsAttribute);
            //var tmp = DocumentTypeManager.Instance.GetSingleEntityByName("MDS_File");
            //var tmp2 = "";

            // end test

            /*
            if (ItemTypes.Instance.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<ItemType> itemTypes = ItemTypes.Instance.Get(division);

                comboBoxItemType.Items.Clear();

                if (itemTypes.Count != 0)
                {
                    foreach (ItemType item in itemTypes)
                    {
                        if (item.Name != null)
                        {
                            comboBoxItemType.Items.Add(item.Name);
                        }
                    }

                    comboBoxItemType.SelectedIndex = 0;

                }
            }
            */

            Dictionary<string, string> emptyDictionary = new Dictionary<string, string>();
            emptyDictionary.Add("NO_DOCUMENT_TYPES_FOUND", "No Document Types Fround");
            comboBoxDocumentType.DataSource = new BindingSource(emptyDictionary, null);
            comboBoxDocumentType.DisplayMember = "Value";
            comboBoxDocumentType.ValueMember = "Key";

            // Fill in Entity list
            string division = ((KeyValuePair<string, string>)comboBoxDocumentGroup.SelectedItem).Key;
            string displayText = ((KeyValuePair<string, string>)comboBoxDocumentGroup.SelectedItem).Value;

            // Value list
            Dictionary<string, string> documentTypeList = new Dictionary<string, string>();

            // Entity list
            List<Entity> entityList = new List<Entity>();

            // If value is number - old external item types
            if (int.TryParse(division, out int outDivision))
            {
                entityList = DocumentTypeManager.Instance.GetEntites("_" + division, Common.FilterType.EntityNameEndWith);
            }
            else
            {
                // Otherwise - other
                entityList = DocumentTypeManager.Instance.GetEntites(division, Common.FilterType.EntityNameStartWith);
            }

            // Fill in Entity list
            foreach (Entity entity in entityList)
            {
                documentTypeList.Add(entity.name, entity.desc);
            }

            // Bind dictionary with document types
            if (documentTypeList.Count > 0)
            {
                comboBoxDocumentType.DataSource = new BindingSource(documentTypeList, null);
            }

            // Set default document type
            // Get value from Configuration file
            //string defaultDocumentGroupKey = CustomSection.
        }

        private void comboBoxDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelAttributes.Controls.Clear();

            // Get selected Entity name from UI
            string selectedEntityName = ((KeyValuePair<string, string>)comboBoxDocumentType.SelectedItem).Key;

            // Get Entity by name
            Entity entity = DocumentTypeManager.Instance.GetSingleEntityByName(selectedEntityName);

            int step = 23;
            int counter = -1;

            foreach (Attr attr in entity.attrs.attr)
            {
                counter++;

                Label label = new Label();
                TextBox textBox;
                DateTimePicker dateTimePicker;
                ComboBox comboBox;

                label.Text = attr.desc;
                label.Name = "label_" + attr.name;
                label.Top = step * (counter++);
                label.Left = 29;
                label.AutoSize = true;
                label.Font = new Font("Calibri", (float)9.75);
                label.Height = 17;

                // add required flag
                if (bool.TryParse(attr.required, out bool requiredValue))
                {
                    label.Text += requiredValue ? " *" : "";
                }

                panelAttributes.Controls.Add(label);

                switch (attr.type)
                {
                    case "7":
                    case "8":
                    case "9":
                        dateTimePicker = new DateTimePicker();
                        //dateTimePicker.Name = "dateTimePicker_" + attr.name;
                        dateTimePicker.Name = "attributeControl_" + attr.name;
                        dateTimePicker.Top = step * (counter++);
                        dateTimePicker.Left = 29;
                        dateTimePicker.Font = new Font("Calibri", (float)9.75);
                        dateTimePicker.Width = 240;
                        dateTimePicker.Value = DateTime.Now;

                        // Set date format
                        switch (attr.type)
                        {
                            // Date
                            case "7":
                                dateTimePicker.Format = DateTimePickerFormat.Long;
                                break;
                            // time
                            case "8":
                                dateTimePicker.Format = DateTimePickerFormat.Time;
                                break;
                            // Time Stamp
                            case "9":
                                dateTimePicker.Format = DateTimePickerFormat.Custom;
                                dateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm:ss";
                                break;
                            default:
                                dateTimePicker.Format = DateTimePickerFormat.Long;
                                break;
                        }

                        panelAttributes.Controls.Add(dateTimePicker);
                        dateTimePicker.BringToFront();

                        break;
                    case "1":
                    case "3":
                    case "4":
                    case "6":
                    case "10":
                    case "20":
                        // If it is values set - show as combobox
                        if (attr.valueset != null && attr.valueset.value.Count > 0)
                        {
                            comboBox = new ComboBox(); ;
                            //comboBox.Name = "comboBox_" + attr.name;
                            comboBox.Name = "attributeControl_" + attr.name;
                            comboBox.Top = step * (counter++);
                            comboBox.Left = 29;
                            comboBox.Font = new Font("Calibri", (float)9.75);
                            comboBox.Width = 240;
                            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                            Dictionary<string, string> values = new Dictionary<string, string>();

                            foreach (Value value in attr.valueset.value)
                            {
                                values.Add(value.name, value.desc);
                            }

                            comboBox.DataSource = new BindingSource(values, null);
                            comboBox.DisplayMember = "Value";
                            comboBox.ValueMember = "Key";

                            panelAttributes.Controls.Add(comboBox);

                            // default
                            comboBox.SelectedIndex = 0;

                            comboBox.BringToFront();
                        }
                        // else - textbox
                        else
                        {
                            textBox = new TextBox();
                            //textBox.Name = "textbox_" + attr.name;
                            textBox.Name = "attributeControl_" + attr.name;
                            textBox.Top = step * (counter++);
                            textBox.Left = 29;
                            textBox.Font = new Font("Calibri", (float)9.75);
                            textBox.Width = 240;
                            // Size property is NOT zero only for Type 1
                            // Otherwise - use length of maximun value
                            if (attr.type == "1")
                            {
                                textBox.MaxLength = Convert.ToInt32(attr.size);
                            }
                            else
                            {
                                textBox.MaxLength = attr.max.Length;
                            }
                            textBox.AllowDrop = true;
                            textBox.DragEnter += TextBox_DragEnter;
                            textBox.DragDrop += TextBox_DragDrop;

                            // add event for validation
                            textBox.TextChanged += TextBox_TextChanged;

                            panelAttributes.Controls.Add(textBox);
                            textBox.BringToFront();
                        }

                        break;
                    default:
                        // Do nothing
                        break;
                }

                // Add error label

                Label labelError = new Label();
                labelError.Text = "";
                labelError.Name = "label_error_" + attr.name;
                labelError.Top = step * (counter) + 2;
                labelError.Left = 29;
                labelError.AutoSize = true;
                labelError.Font = new Font("Calibri", (float)8.75);
                labelError.ForeColor = Color.Red;
                labelError.Height = 17;
                labelError.Visible = false;

                panelAttributes.Controls.Add(labelError);
            }

            /*
            string selectedItem = comboBoxItemType.Text;

            
            ItemType item = ItemTypes.Instance.GetSingeItem(selectedItem);

            if (item != null)
            {
                int step = 23;
                int counter = -1;
                foreach (ItemAttribute attr in item.Attrs)
                {
                    counter++;

                    Label label = new Label();
                    TextBox textBox;
                    DateTimePicker dateTimePicker;
                    ComboBox comboBox;

                    label.Text = attr.Desc;
                    label.Name = "label_" + attr.Name;
                    label.Top = step * (counter++);
                    label.Left = 29;
                    label.AutoSize = true;
                    label.Font = new Font("Calibri", (float)9.75);
                    label.Height = 17;

                    // add required flag
                    if (attr.Flag == "10" || attr.Flag == "26")
                    {
                        label.Text += " *";
                    }

                    panelAttributes.Controls.Add(label);

                    if (attr.Type == "7")
                    {
                        dateTimePicker = new DateTimePicker();
                        dateTimePicker.Name = "dateTimePicker_" + attr.Name;
                        dateTimePicker.Top = step * (counter++);
                        dateTimePicker.Left = 29;
                        dateTimePicker.Font = new Font("Calibri", (float)9.75);
                        dateTimePicker.Width = 240;
                        dateTimePicker.Value = DateTime.Today;

                        panelAttributes.Controls.Add(dateTimePicker);
                        dateTimePicker.BringToFront();
                    }
                    else if(attr.Name == "M3_DIVI")
                    {
                        comboBox = new ComboBox(); ;
                        comboBox.Name = "comboBox_" + attr.Name;
                        comboBox.Top = step * (counter++);
                        comboBox.Left = 29;
                        comboBox.Font = new Font("Calibri", (float)9.75);
                        comboBox.Width = 240;
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                        comboBox.Items.AddRange(new object[] {
                            "101",
                            "201",
                            "202",
                            "299",
                            "301",
                            "601",
                            "602",
                            "603",
                            "606",
                            "607",
                            "608" });

                        comboBox.SelectedIndex = 0;

                        if (!item.Name.StartsWith("M3_EXT_"))
                        {
                            comboBox.Text = comboBoxDivision.Text;
                            comboBox.Enabled = false;
                        }

                        panelAttributes.Controls.Add(comboBox);
                        comboBox.BringToFront();

                    }
                    else
                    {
                        textBox = new TextBox();
                        textBox.Name = "textbox_" + attr.Name;
                        textBox.Top = step * (counter++);
                        textBox.Left = 29;
                        textBox.Font = new Font("Calibri", (float)9.75);
                        textBox.Width = 240;
                        textBox.MaxLength = Convert.ToInt32(attr.Size);
                        textBox.AllowDrop = true;
                        textBox.DragEnter += TextBox_DragEnter;
                        textBox.DragDrop += TextBox_DragDrop;

                        // add event for validation
                        textBox.TextChanged += TextBox_TextChanged;

                        panelAttributes.Controls.Add(textBox);
                        textBox.BringToFront();
                    }

                    Label labelError = new Label();
                    labelError.Text = "";
                    labelError.Name = "label_error_" + attr.Name;
                    labelError.Top = step * (counter) + 2;
                    labelError.Left = 29;
                    labelError.AutoSize = true;
                    labelError.Font = new Font("Calibri", (float)8.75);
                    labelError.ForeColor = Color.Red;
                    labelError.Height = 17;
                    labelError.Visible = false;

                    panelAttributes.Controls.Add(labelError);

                }

                Label label_required = new Label();
                label_required.Text = "* Required fields";
                label_required.Name = "label_required";
                label_required.Top = step * (counter + 1);
                label_required.Left = 29;
                label_required.AutoSize = true;
                label_required.Font = new Font("Calibri", (float)9.75);
                label_required.Height = 15;

                panelAttributes.Controls.Add(label_required);

                // env check
                if (Common.UrlFull.Contains("20105"))
                {
                    buttonArchive.Text = "Archive";
                }
                else
                {
                    buttonArchive.Text = "Archive (TST)";
                }

            }
            */

        }

        private bool validateData()
        {
            bool result = true;

            // Get selected Entity name from UI
            string selectedEntityName = ((KeyValuePair<string, string>)comboBoxDocumentType.SelectedItem).Key;

            Entity entity = DocumentTypeManager.Instance.GetSingleEntityByName(selectedEntityName);

            foreach (Control control in panelAttributes.Controls)
            {
                bool valueValidationResult = true;
                // Validate Alphanumeric values

                // Olny attribute controls
                if (control.Name.StartsWith("attributeControl_"))
                {

                    if (control.GetType() == typeof(TextBox))
                    {
                        string attributeName = control.Name.Replace("attributeControl_", "");
                        Attr attribute = null;

                        // Find attribute
                        foreach (Attr attr in entity.attrs.attr)
                        {
                            if (attr.name == attributeName)
                            {
                                attribute = attr;
                                break;
                            }
                        }

                        // Validation
                        if (attribute != null)
                        {
                            string text = control.Text;
                            string errorText = "Error";

                            Label labelError = (Label)panelAttributes.Controls.Find("label_error_" + attribute.name, false)[0];
                            labelError.Text = "";
                            labelError.Visible = false;

                            if (bool.TryParse(attribute.required, out bool requiredParsed))
                            {
                                if (requiredParsed && text == "")
                                {
                                    errorText = "Field is required!";
                                    valueValidationResult = false;
                                }
                                else
                                {
                                    switch (attribute.type)
                                    {
                                        // ALphanumeric
                                        case "1":
                                            // Do nothing
                                            // No possibility check Character Type (attribute type is the same and equals 1)
                                            // Value length in UTF8 format should match the size
                                            int textLengthInUTF8 = System.Text.ASCIIEncoding.UTF8.GetByteCount(text);
                                            if (textLengthInUTF8 > Int32.Parse(attribute.size))
                                            {
                                                errorText = string.Format("Value should be <= {0} (Extra chars are doublesized)", attribute.size);
                                                valueValidationResult = false;
                                            }

                                            break;
                                        // Numeric
                                        case "3":
                                        case "4":
                                        case "6":
                                        case "10":
                                        case "20":
                                            if (!int.TryParse(text, out int textParsed))
                                            {
                                                errorText = "Value must be numeric!";
                                                valueValidationResult = false;
                                            }
                                            else if ((textParsed > Int32.Parse(attribute.max)) || textParsed < Int32.Parse(attribute.min))
                                            {
                                                errorText = string.Format("Value must be in range from {0} to {1}", attribute.min, attribute.max);
                                                valueValidationResult = false;
                                            }
                                            break;
                                        default:
                                            // Do nothing
                                            break;
                                    }
                                }

                                // Show error if needed
                                if (!valueValidationResult)
                                {
                                    control.BackColor = Color.LightYellow;
                                    labelError.Text = errorText;
                                    labelError.Visible = true;
                                }
                            }
                        }
                    }

                    // Validate DateTime values
                    if (control.GetType() == typeof(DateTimePicker))
                    {
                        // no checks for DateTimePicker
                    }

                    result = result && valueValidationResult;
                }

            }

            /*
            ItemType itemType = ItemTypes.Instance.GetSingeItem(comboBoxDocumentType.Text);
            bool result = true;

            foreach (Control control in panelAttributes.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    string attributeName = control.Name.Replace("textbox_", "");
                    ItemAttribute attribute = null;

                    // get attribute 
                    foreach (ItemAttribute attr in itemType.Attrs)
                    {
                        string name = attr.Name;

                        if (attributeName == name)
                        {
                            attribute = attr;
                            break;
                        }
                    }

                    // validate
                    if (attribute != null)
                    {
                        string flag = attribute.Flag;
                        string text = control.Text;

                        Label labelError = (Label)panelAttributes.Controls.Find("label_error_" + attribute.Name, false)[0];
                        labelError.Text = "";
                        labelError.Visible = false;

                        if (flag == "10" || flag == "26")
                        {
                            if (text == "")
                            {
                                control.BackColor = Color.LightYellow;
                                labelError.Text = "Field is required!";
                                labelError.Visible = true;
                                result = false;
                            }
                        }
                    }
                }

                if (control.GetType() == typeof(DateTimePicker))
                {
                    // no checks for DateTimePicker
                }
            }

            */
            return result;
        }

        private async void buttonArchive_Click(object sender, EventArgs e)
        {
            if (validateData())
            {
                //this.Enabled = false;

                #region Save email
                
                System.Reflection.Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
                Uri asseblyLocation = new Uri(assemblyInfo.Location);
                string location = Path.GetDirectoryName(asseblyLocation.LocalPath.ToString());

                Outlook.Inspector activeInspector = Globals.ThisAddIn.Application.ActiveInspector();
                Outlook.MailItem mailItem = (Outlook.MailItem)activeInspector.CurrentItem;
                string fileName = mailItem.Subject;

                List<char> invalidChars = new List<char>(Path.GetInvalidFileNameChars());
                // adding extra characters to be substituted
                invalidChars.Add('&');

                foreach (char invalidChar in invalidChars)
                {
                    fileName = fileName.Replace(invalidChar, '_');
                }

                fileName += ".msg";
                mailItem.SaveAs(location + "\\" + fileName);

                #endregion

                #region Convert email to base64 string

                string base64;
                using (FileStream reader = new FileStream(location + "\\" + fileName, FileMode.Open))
                {
                    byte[] buffer = new byte[reader.Length];
                    reader.Read(buffer, 0, (int)reader.Length);
                    base64 = Convert.ToBase64String(buffer);
                }

                #endregion

                #region Upload document

                if (verifyCredential())
                {
                    // Create Item
                    string documentTypeName = ((KeyValuePair<string, string>)comboBoxDocumentType.SelectedItem).Key;
                    ItemCreate itemCreate = getItemToUpload(documentTypeName, base64, fileName);

                    //DocumentTypeManager.Instance.CreateItem(itemCreate).GetAwaiter();
                    MessageBox.Show("Document is being archived...", "IDM Tools - Victaulic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await Task.Run(() => DocumentTypeManager.Instance.CreateItem(itemCreate));

                    if (DocumentTypeManager.CreateItemAPICallStatus == HttpStatusCode.OK)
                    {
                        MessageBox.Show("Document successfuly archived!", "IDM Tools - Victaulic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Document was not archived. Reason: " + DocumentTypeManager.CreateItemAPICallStatus.ToString(), "IDM Tools - Victaulic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(DocumentTypeManager.CreateItemAPICallResponceError.message);
                    }
                }

                #endregion


                // add email to IDM
                //ItemType item = ItemTypes.Instance.GetSingeItem(comboBoxItemType.Text);
                //addItem(item, base64, fileName);

                //this.Enabled = true;
                

            }
        }

        #region NEW FUNCTIONS!!!
        // Create ItemCreate instance and fill in attributes from UI
        private ItemCreate getItemToUpload(string itemTypeName, string base64, string fileName)
        {
            // Get Entity related to selected Item Type
            Entity entity = DocumentTypeManager.Instance.GetSingleEntityByName(itemTypeName);

            // Attributes
            Attrs attrs = new Attrs();
            attrs.attr = new List<Attr>();

            // Add attributes
            foreach (Attr attribute in entity.attrs.attr)
            {
                string value = "";

                /*                                              
                 * 1:  Character / Variable Character / CLOB    
                 * 3:  Short Integer                            
                 * 4:  Long Integer                             
                 * 6:  Decimal                                  
                 * 7:  Date                                     Format: YYYY-MM-DD
                 * 8:  Time                                     Format: HH.MM.SS
                 * 9:  Time Stamp                               Format: YYYY-MM-DD-HH.MM.SS.NNNNNN (Year-Month-Day-Hour.Minute.Second.Microseconds)
                 * 10: Double                                   
                 * 20: Short Integer (Min: 0, Max: 1)           
                */

                /*
                 * 1, 3, 4, 7, 9, 20 are using at this moment
                */

                Control attributeControl = panelAttributes.Controls.Find("attributeControl_" + attribute.name, false)[0];
                //attributeControl_

                if (attributeControl is DateTimePicker)
                {
                    DateTimePicker dateTimePicker = (DateTimePicker)attributeControl;
                    switch (attribute.type)
                    {
                        case "7":
                            value = dateTimePicker.Value.ToString("yyyy-MM-dd");
                            break;
                        case "8":
                            value = dateTimePicker.Value.ToString("HH:mm:ss");
                            break;
                        case "9":
                            value = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        default:
                            value = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                    }
                }
                else if (attributeControl is ComboBox)
                {
                    ComboBox comboBoxAttribute = (ComboBox)attributeControl;
                    value = ((KeyValuePair<string, string>)comboBoxAttribute.SelectedItem).Key;
                }
                else if (attributeControl is TextBox)
                {
                    value = attributeControl.Text;
                    value = Common.EscapeExtraChars(value);
                }
                else
                {
                    // Do nothing
                }

                /*
                switch (attribute.type)
                {
                    case "7":
                        dateTimePicker = (DateTimePicker)panelAttributes.Controls.Find("dateTimePicker_" + attribute.name, false)[0];
                        // value = dateTimePicker.Value.Year + "-" + dateTimePicker.Value.Month + "-" + dateTimePicker.Value.Day;
                        value = dateTimePicker.Value.ToString("yyyy-MM-dd");

                        break;
                    case "8":
                        dateTimePicker = (DateTimePicker)panelAttributes.Controls.Find("dateTimePicker_" + attribute.name, false)[0];
                        // value = dateTimePicker.Value.Year + "-" + dateTimePicker.Value.Month + "-" + dateTimePicker.Value.Day;
                        value = dateTimePicker.Value.ToString("HH:mm:ss");

                        break;
                    case "9":
                        dateTimePicker = (DateTimePicker)panelAttributes.Controls.Find("dateTimePicker_" + attribute.name, false)[0];
                        // value = dateTimePicker.Value.Year + "-" + dateTimePicker.Value.Month + "-" + dateTimePicker.Value.Day;
                        value = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");

                        break;
                    default:
                        if (attribute.name == "M3_DIVI")
                        {
                            value = ((Control)panelAttributes.Controls.Find("comboBox_" + attribute.name, false)[0]).Text;
                        }
                        else
                        {
                            value = ((Control)panelAttributes.Controls.Find("textbox_" + attribute.name, false)[0]).Text;
                        }

                        value = Common.EscapeExtraChars(value);

                        break;
                }
                */
                attrs.attr.Add(new Attr(attribute.name, value));

            }

            // Add resources
            ResrsCreate resrs = new ResrsCreate();
            resrs.res = new List<ResCreate>() {
                    new ResCreate("ICMBASETEXT", base64, fileName)
            };

            // Create instance of ItemCreate
            ItemCreate itemCreate = new ItemCreate(itemTypeName, attrs, resrs);

            return itemCreate;
        }

        // Ask and check user credential
        private bool verifyCredential()
        {
            bool result = false;
            
            if (!Common.IsCredentialCorrect)
            {
                LogInForm loginForm = new LogInForm();
                loginForm.StartPosition = FormStartPosition.CenterParent;
                loginForm.UserName = CustomConfigurationManager.DefaultUser;

                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    string user = loginForm.UserName;
                    string password = loginForm.Password;

                    CustomConfigurationManager.DefaultUser = user;

                    DocumentTypeManager.Instance.UserCredentials = new NetworkCredential(user, password);

                    Task.Run(DocumentTypeManager.Instance.Login).Wait();

                    if (DocumentTypeManager.LoginAPICallStatus == HttpStatusCode.OK)
                    {
                        // Login is successfull. Store credentials
                        Common.IsCredentialCorrect = true;
                        result = true;
                        Task.Run(DocumentTypeManager.Instance.Logout).Wait();
                        //Task.Run(DocumentTypeManager.Instance.Logout).ConfigureAwait(false);
                    }
                    else
                    {
                        // Login is NOT successfull. Show error message
                        MessageBox.Show("Login failed! Reason: " + DocumentTypeManager.LoginAPICallStatus.ToString(), "Authorization error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        #endregion

        /*
        private bool addItem(ItemType item, string base64, string fileName)
        {
            logger.Log("Trying to add document with name " + fileName);

            bool result = true;
            var url = Common.UrlFull + "/addItemEx.jsp";
            string xml = getRequestXML(item, base64, fileName);
            logger.Log("xml: " + xml.Replace(base64, "base64 string was removed"));

            Tuple<HttpStatusCode, XmlDocument> response = Common.callAPI(url, "POST", true, xml);

            if (response.Item1 == HttpStatusCode.OK)
            {
                logger.Log("Item " + fileName + " has been successfully added!");
                // check in
                XmlDocument respXml = new XmlDocument();
                respXml = response.Item2;

                string pid = respXml.SelectSingleNode("/item/pid").ChildNodes[0].InnerText;
                result = checkInItem(pid);
            }
            else
            {
                result = false;
                MessageBox.Show(this.Parent, "Error while adding document to IDM. Reason: " + response.Item1, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

            return result;
        }

        private bool checkInItem(string pid)
        {
            logger.Log("Trying to check in item with pid: " + pid);

            bool result = true;
            string xml = "<item><pid>" + pid + "</pid></item>";
            var url = Common.UrlFull + "/checkInItem.jsp";

            Tuple<HttpStatusCode, XmlDocument> response = Common.callAPI(url, "POST", true, xml);

            if (response.Item1 == HttpStatusCode.OK)
            {
                logger.Log("Item has been successfully checked in!");
                logger.Log("-----------------------------------------------------------");
                MessageBox.Show(this, "Email was successfully archived!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                result = false;
                MessageBox.Show(this, "Error while checking in document in IDM. Reason: " + response.Item1.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private string getRequestXML(ItemType item, string base64, string fileName)
        {
            string xml;

            // main tag
            xml = "<item>";

            // header (name only)
            // 
            // <entityName>M3_RMA_101</entityName>
            xml += "<entityName>" + item.Name + "</entityName>";

            // attribute list
            // 
            // <attrs>
            //     <attr>
            //         <name>M3_EXT_DocumentReference</name>
            //         <type>2</type>
            //         <qual>M3_EXT_DocumentReference</qual>
            //         <value>21342356</value>
            //     </attr>
            //     ...
            // </attrs>
            xml += "<attrs>";

            List<ItemAttribute> attributeList = item.Attrs;
            foreach (ItemAttribute itemAttribute in attributeList)
            {
                string value = "";

                if (itemAttribute.Type == "2")
                {
                    if (itemAttribute.Name == "M3_DIVI")
                    {
                        value = ((Control)panelAttributes.Controls.Find("comboBox_" + itemAttribute.Name, false)[0]).Text;
                    }
                    else
                    {
                        value = ((Control)panelAttributes.Controls.Find("textbox_" + itemAttribute.Name, false)[0]).Text;

                    }

                    value = Common.EscapeExtraChars(value);
                }

                if (itemAttribute.Type == "7")
                {
                    DateTimePicker dateTimePicker = (DateTimePicker)panelAttributes.Controls.Find("dateTimePicker_" + itemAttribute.Name, false)[0];
                    // value = dateTimePicker.Value.Year + "-" + dateTimePicker.Value.Month + "-" + dateTimePicker.Value.Day;
                    value = dateTimePicker.Value.ToString("yyyy-MM-dd");
                }

                var attrTag = "<attr>";
                attrTag += "<name>" + itemAttribute.Name + "</name>";
                attrTag += "<type>" + itemAttribute.Type + "</type>";
                attrTag += "<qual>" + itemAttribute.Qual + "</qual>";
                attrTag += "<value>" + value + "</value>";
                attrTag += "</attr>";

                xml += attrTag;
            }
            xml += "</attrs>";

            // resourse list
            //
            // <resrs>
            //     <res>
            //         <entityName>ICMBASE</entityName>
            //         <mimetype>application/vnd.ms-outlook</mimetype>
            //         <base64>file-to-BASE64-string</base64>
            //         <filename>20163602-043645-Ticket.msg</filename>
            //     </res>
            // </resrs>
            xml += "<resrs>";
            xml += "<res>";
            xml += "<entityName>ICMBASE</entityName>";
            xml += "<mimetype>application/vnd.ms-outlook</mimetype>";
            xml += "<base64>" + base64 + "</base64>";
            xml += "<filename>" + fileName + "</filename>";
            xml += "</res>";
            xml += "</resrs>";

            // add closed main tag
            xml += "</item>";

            return xml;
        }
        */

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.BackColor = SystemColors.Window;
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is TextBox)
            {
                int i;
                string s;
                string resultText;

                TextBox textBox = (TextBox)sender;

                // Get start position to drop the text.
                i = textBox.SelectionStart;
                s = textBox.Text.Substring(i);
                resultText = textBox.Text.Substring(0, i);

                // Drop the text on to the TextBox.
                resultText += e.Data.GetData(DataFormats.Text).ToString();
                resultText += s;

                textBox.Text = resultText.Substring(0, Math.Min(resultText.Length, textBox.MaxLength));
            }
        }

        private void MyUserControlTaskPane_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                // set default division
                //comboBoxDocumentGroup.SelectedIndex = 0;
            }
        }

        private void labelVersion_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(IDMToolsAsync.BaseUrl.ToString(), "Base Url", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
