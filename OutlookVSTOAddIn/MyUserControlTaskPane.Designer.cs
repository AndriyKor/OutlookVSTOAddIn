namespace OutlookVSTOAddIn
{
    partial class MyUserControlTaskPane
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
            this.labelDocumentGroup = new System.Windows.Forms.Label();
            this.labelDocumentType = new System.Windows.Forms.Label();
            this.comboBoxDocumentType = new System.Windows.Forms.ComboBox();
            this.comboBoxDocumentGroup = new System.Windows.Forms.ComboBox();
            this.panelAttributes = new System.Windows.Forms.Panel();
            this.labelAttributes = new System.Windows.Forms.Label();
            this.labelFilter = new System.Windows.Forms.Label();
            this.buttonArchive = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDocumentGroup
            // 
            this.labelDocumentGroup.AutoSize = true;
            this.labelDocumentGroup.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDocumentGroup.Location = new System.Drawing.Point(28, 35);
            this.labelDocumentGroup.Name = "labelDocumentGroup";
            this.labelDocumentGroup.Size = new System.Drawing.Size(99, 15);
            this.labelDocumentGroup.TabIndex = 0;
            this.labelDocumentGroup.Text = "Document Group";
            // 
            // labelDocumentType
            // 
            this.labelDocumentType.AutoSize = true;
            this.labelDocumentType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDocumentType.Location = new System.Drawing.Point(28, 94);
            this.labelDocumentType.Name = "labelDocumentType";
            this.labelDocumentType.Size = new System.Drawing.Size(89, 15);
            this.labelDocumentType.TabIndex = 2;
            this.labelDocumentType.Text = "Document Type";
            // 
            // comboBoxDocumentType
            // 
            this.comboBoxDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDocumentType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDocumentType.FormattingEnabled = true;
            this.comboBoxDocumentType.Location = new System.Drawing.Point(28, 115);
            this.comboBoxDocumentType.Name = "comboBoxDocumentType";
            this.comboBoxDocumentType.Size = new System.Drawing.Size(240, 23);
            this.comboBoxDocumentType.TabIndex = 5;
            this.comboBoxDocumentType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDocumentType_SelectedIndexChanged);
            // 
            // comboBoxDocumentGroup
            // 
            this.comboBoxDocumentGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDocumentGroup.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDocumentGroup.FormattingEnabled = true;
            this.comboBoxDocumentGroup.Location = new System.Drawing.Point(28, 56);
            this.comboBoxDocumentGroup.Name = "comboBoxDocumentGroup";
            this.comboBoxDocumentGroup.Size = new System.Drawing.Size(240, 23);
            this.comboBoxDocumentGroup.TabIndex = 6;
            this.comboBoxDocumentGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxDocumentGroup_SelectedIndexChanged);
            // 
            // panelAttributes
            // 
            this.panelAttributes.AutoScroll = true;
            this.panelAttributes.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelAttributes.Location = new System.Drawing.Point(-1, 173);
            this.panelAttributes.Name = "panelAttributes";
            this.panelAttributes.Size = new System.Drawing.Size(300, 373);
            this.panelAttributes.TabIndex = 7;
            // 
            // labelAttributes
            // 
            this.labelAttributes.AutoSize = true;
            this.labelAttributes.Font = new System.Drawing.Font("Calibri", 11F);
            this.labelAttributes.Location = new System.Drawing.Point(16, 151);
            this.labelAttributes.Name = "labelAttributes";
            this.labelAttributes.Size = new System.Drawing.Size(71, 18);
            this.labelAttributes.TabIndex = 8;
            this.labelAttributes.Text = "Attributes";
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Font = new System.Drawing.Font("Calibri", 11F);
            this.labelFilter.Location = new System.Drawing.Point(16, 11);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(41, 18);
            this.labelFilter.TabIndex = 9;
            this.labelFilter.Text = "Filter";
            // 
            // buttonArchive
            // 
            this.buttonArchive.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.buttonArchive.Location = new System.Drawing.Point(158, 562);
            this.buttonArchive.Name = "buttonArchive";
            this.buttonArchive.Size = new System.Drawing.Size(110, 36);
            this.buttonArchive.TabIndex = 10;
            this.buttonArchive.Text = "Archive";
            this.buttonArchive.UseVisualStyleBackColor = true;
            this.buttonArchive.Click += new System.EventHandler(this.buttonArchive_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelVersion.Location = new System.Drawing.Point(17, 586);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(43, 12);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "V 1.0.0.0";
            this.labelVersion.DoubleClick += new System.EventHandler(this.labelVersion_DoubleClick);
            // 
            // MyUserControlTaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.comboBoxDocumentGroup);
            this.Controls.Add(this.comboBoxDocumentType);
            this.Controls.Add(this.buttonArchive);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.labelAttributes);
            this.Controls.Add(this.panelAttributes);
            this.Controls.Add(this.labelDocumentType);
            this.Controls.Add(this.labelDocumentGroup);
            this.MinimumSize = new System.Drawing.Size(295, 2);
            this.Name = "MyUserControlTaskPane";
            this.Size = new System.Drawing.Size(300, 620);
            this.EnabledChanged += new System.EventHandler(this.MyUserControlTaskPane_EnabledChanged);
            this.Resize += new System.EventHandler(this.MyUserControlTaskPane_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDocumentGroup;
        private System.Windows.Forms.Label labelDocumentType;
        private System.Windows.Forms.ComboBox comboBoxDocumentType;
        private System.Windows.Forms.ComboBox comboBoxDocumentGroup;
        private System.Windows.Forms.Panel panelAttributes;
        private System.Windows.Forms.Label labelAttributes;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.Button buttonArchive;
        private System.Windows.Forms.Label labelVersion;
    }
}
