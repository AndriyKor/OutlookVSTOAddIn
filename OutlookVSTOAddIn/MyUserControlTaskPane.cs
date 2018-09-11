using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutlookVSTOAddIn.Global;
using System.Globalization;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net;
using System.Xml;
using System.Reflection;

namespace OutlookVSTOAddIn
{
    public partial class MyUserControlTaskPane : UserControl
    {
        private FileLogger logger = FileLogger.Instance;

        public MyUserControlTaskPane()
        {
            InitializeComponent();
            this.Load += MyUserControlTaskPane_Load;
        }

        private void MyUserControlTaskPane_Load(object sender, EventArgs e)
        {
            // set default division
            comboBoxDivision.SelectedIndex = 1;

            var productVersion = Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = "V" + productVersion;

        }

        private void MyUserControlTaskPane_Resize(object sender, EventArgs e)
        {
            panelAttributes.Height = this.Height - panelAttributes.Top - 70;
            buttonArchive.Top = this.Height - 60;
            labelVersion.Top = this.Height - 38;
        }

        private void comboBoxDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            string division = comboBoxDivision.Text;

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
        }

        private void comboBoxItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelAttributes.Controls.Clear();

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

        }

        private bool validateData()
        {
            ItemType itemType = ItemTypes.Instance.GetSingeItem(comboBoxItemType.Text);
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


            return result;
        }

        private void buttonArchive_Click(object sender, EventArgs e)
        {
            if (validateData())
            {
                this.Enabled = false;

                // save email
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

                // convert to base64 string
                string base64;
                using (FileStream reader = new FileStream(location + "\\" + fileName, FileMode.Open))
                {
                    byte[] buffer = new byte[reader.Length];
                    reader.Read(buffer, 0, (int)reader.Length);
                    base64 = Convert.ToBase64String(buffer);
                }

                // add email to IDM
                ItemType item = ItemTypes.Instance.GetSingeItem(comboBoxItemType.Text);
                addItem(item, base64, fileName);

                this.Enabled = true;

            }
        }

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
                    value = ((TextBox)panelAttributes.Controls.Find("textbox_" + itemAttribute.Name, false)[0]).Text;
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
                comboBoxDivision.SelectedIndex = 0;
            }
        }
    }
}
