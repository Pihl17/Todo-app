using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToDo_App.WindowsApp.UI
{
    public partial class ChecklistItemUI : UserControl
    {
        private PlaceholderHandler itemNamePlaceholder;
        public event EventHandler ItemLeave;
        public ChecklistItemUI()
        {
            InitializeComponent();
            itemNamePlaceholder = new PlaceholderHandler(ChecklistItemNameTB, "New checklist item here.");
        }
        public string GetTextBoxText()
        {
            return itemNamePlaceholder.ToString();
        }
        public bool GetCheckBoxValue()
        {
            return ChecklistCB.Checked;
        }
        private void ChecklistItemNameTB_Leave(object sender, EventArgs e)
        {
            ItemLeave?.Invoke(this, EventArgs.Empty);
        }

        private void ChecklistItemNameTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                ItemLeave?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
