namespace OutlookVSTOAddIn
{
    partial class ManageTaskPaneRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ManageTaskPaneRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.groupTaskPaneManager = this.Factory.CreateRibbonGroup();
            this.toggleButtonShowTaskPane = this.Factory.CreateRibbonToggleButton();
            this.groupProperties = this.Factory.CreateRibbonGroup();
            this.toggleButtonSettings = this.Factory.CreateRibbonToggleButton();
            this.tab1.SuspendLayout();
            this.groupTaskPaneManager.SuspendLayout();
            this.groupProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.groupTaskPaneManager);
            this.tab1.Groups.Add(this.groupProperties);
            this.tab1.Label = "IDM (DAF)";
            this.tab1.Name = "tab1";
            // 
            // groupTaskPaneManager
            // 
            this.groupTaskPaneManager.Items.Add(this.toggleButtonShowTaskPane);
            this.groupTaskPaneManager.Label = "Document Archive";
            this.groupTaskPaneManager.Name = "groupTaskPaneManager";
            // 
            // toggleButtonShowTaskPane
            // 
            this.toggleButtonShowTaskPane.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleButtonShowTaskPane.Label = "Archive Email";
            this.toggleButtonShowTaskPane.Name = "toggleButtonShowTaskPane";
            this.toggleButtonShowTaskPane.OfficeImageId = "AutoArchiveSettings";
            this.toggleButtonShowTaskPane.ShowImage = true;
            this.toggleButtonShowTaskPane.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButton_Click);
            // 
            // groupProperties
            // 
            this.groupProperties.Items.Add(this.toggleButtonSettings);
            this.groupProperties.Label = "Properties";
            this.groupProperties.Name = "groupProperties";
            // 
            // toggleButtonSettings
            // 
            this.toggleButtonSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleButtonSettings.Label = "Settings";
            this.toggleButtonSettings.Name = "toggleButtonSettings";
            this.toggleButtonSettings.OfficeImageId = "CurrentViewSettings";
            this.toggleButtonSettings.ShowImage = true;
            this.toggleButtonSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButton_Click);
            // 
            // ManageTaskPaneRibbon
            // 
            this.Name = "ManageTaskPaneRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ManageTaskPaneRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupTaskPaneManager.ResumeLayout(false);
            this.groupTaskPaneManager.PerformLayout();
            this.groupProperties.ResumeLayout(false);
            this.groupProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupTaskPaneManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonShowTaskPane;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupProperties;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSettings;
    }

    partial class ThisRibbonCollection
    {
        internal ManageTaskPaneRibbon ManageTaskPaneRibbon
        {
            get { return this.GetRibbon<ManageTaskPaneRibbon>(); }
        }
    }
}
