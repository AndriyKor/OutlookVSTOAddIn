using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookVSTOAddIn
{
    public partial class LogInForm : Form
    {

        private string userName = "";
        private string password = "";

        public LogInForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var tmpUserName = this.textBoxUserName.Text;
            var tmpPassword = this.textBoxPassword.Text;

            if (tmpUserName == "")
            {
                var dialogResult = MessageBox.Show("Please enter User Name", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Cancel)
                {
                    this.Close();
                }
            } else if (tmpPassword == "")
            {
                var dialogResult = MessageBox.Show("Please enter Password", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Cancel)
                {
                    this.Close();
                }
            } else
            {
                userName = tmpUserName;
                password = tmpPassword;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public string UserName
        {
            get {
                return userName;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOk.PerformClick();
            }

            if (e.KeyCode == Keys.Escape)
            {
                buttonCancel.PerformClick();
            }
        }
    }
}
