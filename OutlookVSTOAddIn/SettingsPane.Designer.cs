namespace OutlookVSTOAddIn
{
    partial class SettingsPane
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSettings = new System.Windows.Forms.Label();
            this.labelDefaultDocumentGroup = new System.Windows.Forms.Label();
            this.comboBoxDefaultDocumentGroup = new System.Windows.Forms.ComboBox();
            this.labelBaseURL = new System.Windows.Forms.Label();
            this.comboBoxBaseUrlListName = new System.Windows.Forms.ComboBox();
            this.textBoxBaseUrlValue = new System.Windows.Forms.TextBox();
            this.buttonSettingsApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSettings
            // 
            this.labelSettings.AutoSize = true;
            this.labelSettings.Font = new System.Drawing.Font("Calibri", 11F);
            this.labelSettings.Location = new System.Drawing.Point(16, 11);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(57, 18);
            this.labelSettings.TabIndex = 0;
            this.labelSettings.Text = "Settings";
            // 
            // labelDefaultDocumentGroup
            // 
            this.labelDefaultDocumentGroup.AutoSize = true;
            this.labelDefaultDocumentGroup.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.labelDefaultDocumentGroup.Location = new System.Drawing.Point(28, 35);
            this.labelDefaultDocumentGroup.Name = "labelDefaultDocumentGroup";
            this.labelDefaultDocumentGroup.Size = new System.Drawing.Size(142, 15);
            this.labelDefaultDocumentGroup.TabIndex = 1;
            this.labelDefaultDocumentGroup.Text = "Default Document Group";
            // 
            // comboBoxDefaultDocumentGroup
            // 
            this.comboBoxDefaultDocumentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefaultDocumentGroup.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.comboBoxDefaultDocumentGroup.FormattingEnabled = true;
            this.comboBoxDefaultDocumentGroup.Location = new System.Drawing.Point(28, 56);
            this.comboBoxDefaultDocumentGroup.Name = "comboBoxDefaultDocumentGroup";
            this.comboBoxDefaultDocumentGroup.Size = new System.Drawing.Size(240, 23);
            this.comboBoxDefaultDocumentGroup.TabIndex = 2;
            this.comboBoxDefaultDocumentGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultDocumentGroup_SelectedIndexChanged);
            // 
            // labelBaseURL
            // 
            this.labelBaseURL.AutoSize = true;
            this.labelBaseURL.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.labelBaseURL.Location = new System.Drawing.Point(28, 95);
            this.labelBaseURL.Name = "labelBaseURL";
            this.labelBaseURL.Size = new System.Drawing.Size(76, 15);
            this.labelBaseURL.TabIndex = 6;
            this.labelBaseURL.Text = "Environment";
            // 
            // comboBoxBaseUrlListName
            // 
            this.comboBoxBaseUrlListName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaseUrlListName.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.comboBoxBaseUrlListName.FormattingEnabled = true;
            this.comboBoxBaseUrlListName.Location = new System.Drawing.Point(28, 113);
            this.comboBoxBaseUrlListName.Name = "comboBoxBaseUrlListName";
            this.comboBoxBaseUrlListName.Size = new System.Drawing.Size(240, 23);
            this.comboBoxBaseUrlListName.TabIndex = 7;
            this.comboBoxBaseUrlListName.SelectedIndexChanged += new System.EventHandler(this.comboBoxBaseUrlListName_SelectedIndexChanged);
            // 
            // textBoxBaseUrlValue
            // 
            this.textBoxBaseUrlValue.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.textBoxBaseUrlValue.Location = new System.Drawing.Point(28, 142);
            this.textBoxBaseUrlValue.Name = "textBoxBaseUrlValue";
            this.textBoxBaseUrlValue.ReadOnly = true;
            this.textBoxBaseUrlValue.Size = new System.Drawing.Size(240, 23);
            this.textBoxBaseUrlValue.TabIndex = 9;
            // 
            // buttonSettingsApply
            // 
            this.buttonSettingsApply.Enabled = false;
            this.buttonSettingsApply.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.buttonSettingsApply.Location = new System.Drawing.Point(158, 562);
            this.buttonSettingsApply.Name = "buttonSettingsApply";
            this.buttonSettingsApply.Size = new System.Drawing.Size(110, 36);
            this.buttonSettingsApply.TabIndex = 10;
            this.buttonSettingsApply.Text = "Apply";
            this.buttonSettingsApply.UseVisualStyleBackColor = true;
            this.buttonSettingsApply.Click += new System.EventHandler(this.buttonSettingsApply_Click);
            // 
            // SettingsPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSettingsApply);
            this.Controls.Add(this.textBoxBaseUrlValue);
            this.Controls.Add(this.comboBoxBaseUrlListName);
            this.Controls.Add(this.labelBaseURL);
            this.Controls.Add(this.comboBoxDefaultDocumentGroup);
            this.Controls.Add(this.labelDefaultDocumentGroup);
            this.Controls.Add(this.labelSettings);
            this.Name = "SettingsPane";
            this.Size = new System.Drawing.Size(300, 620);
            this.Load += new System.EventHandler(this.SettingsPane_Load);
            this.Resize += new System.EventHandler(this.SettingsPane_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSettings;
        private System.Windows.Forms.Label labelDefaultDocumentGroup;
        private System.Windows.Forms.ComboBox comboBoxDefaultDocumentGroup;
        private System.Windows.Forms.Label labelBaseURL;
        private System.Windows.Forms.ComboBox comboBoxBaseUrlListName;
        private System.Windows.Forms.TextBox textBoxBaseUrlValue;
        private System.Windows.Forms.Button buttonSettingsApply;
    }
}
