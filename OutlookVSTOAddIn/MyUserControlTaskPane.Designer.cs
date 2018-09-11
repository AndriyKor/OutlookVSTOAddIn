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
            this.labelDivision = new System.Windows.Forms.Label();
            this.labelItemType = new System.Windows.Forms.Label();
            this.comboBoxItemType = new System.Windows.Forms.ComboBox();
            this.comboBoxDivision = new System.Windows.Forms.ComboBox();
            this.panelAttributes = new System.Windows.Forms.Panel();
            this.labelAttributes = new System.Windows.Forms.Label();
            this.labelFilter = new System.Windows.Forms.Label();
            this.buttonArchive = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDivision
            // 
            this.labelDivision.AutoSize = true;
            this.labelDivision.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDivision.Location = new System.Drawing.Point(28, 35);
            this.labelDivision.Name = "labelDivision";
            this.labelDivision.Size = new System.Drawing.Size(53, 15);
            this.labelDivision.TabIndex = 0;
            this.labelDivision.Text = "Division";
            // 
            // labelItemType
            // 
            this.labelItemType.AutoSize = true;
            this.labelItemType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelItemType.Location = new System.Drawing.Point(28, 94);
            this.labelItemType.Name = "labelItemType";
            this.labelItemType.Size = new System.Drawing.Size(58, 15);
            this.labelItemType.TabIndex = 2;
            this.labelItemType.Text = "Document Type";
            // 
            // comboBoxItemType
            // 
            this.comboBoxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxItemType.FormattingEnabled = true;
            this.comboBoxItemType.Location = new System.Drawing.Point(28, 115);
            this.comboBoxItemType.Name = "comboBoxItemType";
            this.comboBoxItemType.Size = new System.Drawing.Size(240, 23);
            this.comboBoxItemType.TabIndex = 5;
            this.comboBoxItemType.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemType_SelectedIndexChanged);
            // 
            // comboBoxDivision
            // 
            this.comboBoxDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDivision.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDivision.FormattingEnabled = true;
            this.comboBoxDivision.Items.AddRange(new object[] {
            "Merged [EXT]",
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
            "608"});
            this.comboBoxDivision.Location = new System.Drawing.Point(28, 56);
            this.comboBoxDivision.Name = "comboBoxDivision";
            this.comboBoxDivision.Size = new System.Drawing.Size(240, 23);
            this.comboBoxDivision.TabIndex = 6;
            this.comboBoxDivision.SelectedIndexChanged += new System.EventHandler(this.comboBoxDivision_SelectedIndexChanged);
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
            // 
            // MyUserControlTaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.comboBoxDivision);
            this.Controls.Add(this.comboBoxItemType);
            this.Controls.Add(this.buttonArchive);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.labelAttributes);
            this.Controls.Add(this.panelAttributes);
            this.Controls.Add(this.labelItemType);
            this.Controls.Add(this.labelDivision);
            this.Enabled = false;
            this.MinimumSize = new System.Drawing.Size(295, 2);
            this.Name = "MyUserControlTaskPane";
            this.Size = new System.Drawing.Size(300, 620);
            this.EnabledChanged += new System.EventHandler(this.MyUserControlTaskPane_EnabledChanged);
            this.Resize += new System.EventHandler(this.MyUserControlTaskPane_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDivision;
        private System.Windows.Forms.Label labelItemType;
        private System.Windows.Forms.ComboBox comboBoxItemType;
        private System.Windows.Forms.ComboBox comboBoxDivision;
        private System.Windows.Forms.Panel panelAttributes;
        private System.Windows.Forms.Label labelAttributes;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.Button buttonArchive;
        private System.Windows.Forms.Label labelVersion;
    }
}
