using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools;

namespace OutlookVSTOAddIn
{
    public partial class ManageTaskPaneRibbon
    {
        private void ManageTaskPaneRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void toggleButton_Click(object sender, RibbonControlEventArgs e)
        {
            Outlook.Inspector inspector = (Outlook.Inspector)e.Control.Context;
            InspectorWrapper inspectorWrapper = Globals.ThisAddIn.InspectorWrappers[inspector];
            CustomTaskPane taskPane = inspectorWrapper.CustomTaskPane;
            CustomTaskPane taskPaneSettings = inspectorWrapper.CustomTaskPaneSettings;

            if (((RibbonToggleButton)sender).Name == "toggleButtonShowTaskPane")
            {
                if (taskPaneSettings != null)
                {
                    taskPaneSettings.Visible = false;
                }

                if (taskPane != null)
                {
                    taskPane.Visible = ((RibbonToggleButton)sender).Checked;
                }
            }
            else if (((RibbonToggleButton)sender).Name == "toggleButtonSettings")
            {
                if (taskPane != null)
                {
                    taskPane.Visible = false;
                }

                if (taskPaneSettings != null)
                {
                    taskPaneSettings.Visible = ((RibbonToggleButton)sender).Checked;
                }
            }
        }
    }
}
