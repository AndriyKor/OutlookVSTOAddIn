﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using Microsoft.Office.Tools;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using OutlookVSTOAddIn.Global.CustomConfigurationManager;

namespace OutlookVSTOAddIn
{
    public partial class ThisAddIn
    {
        private Dictionary<Outlook.Inspector, InspectorWrapper> inspectorWrappersValue =
            new Dictionary<Outlook.Inspector, InspectorWrapper>();
        private Outlook.Inspectors inspectors;


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            inspectors = this.Application.Inspectors;
            inspectors.NewInspector +=
                new Outlook.InspectorsEvents_NewInspectorEventHandler(
                Inspectors_NewInspector);

            foreach (Outlook.Inspector inspector in inspectors)
            {
                Inspectors_NewInspector(inspector);
            }

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
            inspectors.NewInspector -=
                new Outlook.InspectorsEvents_NewInspectorEventHandler(
                Inspectors_NewInspector);
            inspectors = null;
            inspectorWrappersValue = null;
        }

        void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            if (Inspector.CurrentItem is Outlook.MailItem)
            {
                inspectorWrappersValue.Add(Inspector, new InspectorWrapper(Inspector));                
            }
        }

        public Dictionary<Outlook.Inspector, InspectorWrapper> InspectorWrappers
        {
            get
            {
                return inspectorWrappersValue;
            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }

    public class InspectorWrapper
    {
        private Outlook.Inspector inspector;
        private CustomTaskPane taskPane;
        private CustomTaskPane taskPaneSettings;

        public InspectorWrapper(Outlook.Inspector Inspector)
        {
            inspector = Inspector;
            ((Outlook.InspectorEvents_Event)inspector).Close +=
                new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);

            // Main Task Pane
            taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(
                new MyUserControlTaskPane(), "Archive Email", inspector);

            taskPane.VisibleChanged += new EventHandler(TaskPane_VisibleChanged);

            // Settings Task Pane
            taskPaneSettings = Globals.ThisAddIn.CustomTaskPanes.Add(
                new SettingsPane(), "Settings", inspector);

            taskPaneSettings.VisibleChanged += new EventHandler(TaskPaneSettings_VisibleChanged);

            // Add properties
            taskPane.Width = 320;
            taskPane.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
            taskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;

            taskPaneSettings.Width = 320;
            taskPaneSettings.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
            taskPaneSettings.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
        }

        void TaskPane_VisibleChanged(object sender, EventArgs e)
        {

            Globals.Ribbons[inspector].ManageTaskPaneRibbon.toggleButtonShowTaskPane.Checked =
                taskPane.Visible;
        }

        void TaskPaneSettings_VisibleChanged(object sender, EventArgs e)
        {
            Globals.Ribbons[inspector].ManageTaskPaneRibbon.toggleButtonSettings.Checked =
                taskPaneSettings.Visible;
        }

        void InspectorWrapper_Close()
        {
            if (taskPane != null)
            {
                Globals.ThisAddIn.CustomTaskPanes.Remove(taskPane);
            }

            if (taskPaneSettings != null)
            {
                Globals.ThisAddIn.CustomTaskPanes.Remove(taskPaneSettings);
            }

            taskPane = null;
            taskPaneSettings = null;

            Globals.ThisAddIn.InspectorWrappers.Remove(inspector);
            ((Outlook.InspectorEvents_Event)inspector).Close -=
                new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);
            inspector = null;
        }

        public CustomTaskPane CustomTaskPane
        {
            get
            {
                return taskPane;
            }
        }

        public CustomTaskPane CustomTaskPaneSettings
        {
            get
            {
                return taskPaneSettings;
            }
        }
    }
}
