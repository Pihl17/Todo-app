using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ToDo_App.Tests")]
namespace ToDo_App.WindowsApp.UI
{
    public partial class ChecklistItemUI : UserControl
    {
        public TextBox ChecklistTextBox => ChecklistItemNameTB;

        public void SetTextBoxText(string text)
        {
            ChecklistItemNameTB.Text = text;
        }

        public void TriggerItemLeave()
        {
            ItemLeave?.Invoke(this, EventArgs.Empty);
        }

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
        internal void ChecklistItemNameTB_Leave(object sender, EventArgs e)
        {
            ItemLeave?.Invoke(this, EventArgs.Empty);
        }

        internal void ChecklistItemNameTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                ItemLeave?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
