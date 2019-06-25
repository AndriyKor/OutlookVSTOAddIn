using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutlookVSTOAddIn.Global.CustomConfigurationManager;
using OutlookVSTOAddIn.Global;

namespace OutlookVSTOAddIn
{
    public partial class SettingsPane : UserControl
    {
        public SettingsPane()
        {
            InitializeComponent();
        }

        private void SettingsPane_Load(object sender, EventArgs e)
        {
            // - - - DOCUMENT GROUP - - - //

            // Bind dictionary with divisions
            comboBoxDefaultDocumentGroup.DataSource = new BindingSource(CustomConfigurationManager.GetDocumentGroupList(), null);
            comboBoxDefaultDocumentGroup.DisplayMember = "Value";
            comboBoxDefaultDocumentGroup.ValueMember = "Key";

            // set default division
            string defaultDocumentGroup = CustomConfigurationManager.GetDefaultDocumentGroup();

            foreach (KeyValuePair<string, string> item in comboBoxDefaultDocumentGroup.Items)
            {
                if (item.Key == defaultDocumentGroup)
                {
                    comboBoxDefaultDocumentGroup.SelectedIndex = comboBoxDefaultDocumentGroup.Items.IndexOf(item);
                    break;
                }
            }

            // - - - BASE URL - - - //

            // Bind dictionary with Base Urls
            comboBoxBaseUrlListName.DataSource = new BindingSource(CustomConfigurationManager.GetBaseUrlList(), null);
            comboBoxBaseUrlListName.DisplayMember = "Key";
            comboBoxBaseUrlListName.ValueMember = "Value";

            // set default Base Url
            string defaultBaseUrl = CustomConfigurationManager.GetDefaultBaseUrlName();

            foreach (KeyValuePair<string, string> item in comboBoxBaseUrlListName.Items)
            {
                if (item.Key == defaultBaseUrl)
                {
                    comboBoxBaseUrlListName.SelectedIndex = comboBoxBaseUrlListName.Items.IndexOf(item);
                    break;
                }
            }

            // Disable "Apply" button
            buttonSettingsApply.Enabled = false;
        }

        private void comboBoxBaseUrlListName_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxBaseUrlValue.Text = ((KeyValuePair<string, string>)comboBoxBaseUrlListName.SelectedItem).Value ?? "";

            // Enable "Apply" button
            buttonSettingsApply.Enabled = true;
        }

        private void comboBoxDefaultDocumentGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable "Apply" button
            buttonSettingsApply.Enabled = true;
        }

        private void SettingsPane_Resize(object sender, EventArgs e)
        {
            buttonSettingsApply.Top = this.Height - 60;
        }

        private void buttonSettingsApply_Click(object sender, EventArgs e)
        {
            // - - - SAVE SETINGS - - - //

            // Save default Document Type
            CustomConfigurationManager.SetDefaultDocumentGroup(((KeyValuePair<string, string>)comboBoxDefaultDocumentGroup.SelectedItem).Key);

            // Save default Base Url
            CustomConfigurationManager.SetDefaultBaseUrl(((KeyValuePair<string, string>)comboBoxBaseUrlListName.SelectedItem).Key);


            // Disable "Apply" button
            buttonSettingsApply.Enabled = false;
        }
    }
}
