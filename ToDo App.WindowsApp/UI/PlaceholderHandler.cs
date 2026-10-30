using System.Drawing;
using System.Windows.Forms;

namespace ToDo_App.WindowsApp.UI
{
    public class PlaceholderHandler
    {
        private readonly TextBoxBase inputControl;
        private readonly string placeholder;
        private bool isPlaceholder;

        public PlaceholderHandler(TextBox textBox, string placeholder)
            : this((TextBoxBase)textBox, placeholder)
        {
        }

        public PlaceholderHandler(RichTextBox richTextBox, string placeholder)
            : this((TextBoxBase)richTextBox, placeholder)
        {
        }

        private PlaceholderHandler(TextBoxBase inputControl, string placeholder)
        {
            this.inputControl = inputControl
                ?? throw new ArgumentNullException(nameof(inputControl));

            this.placeholder = placeholder;

            ShowPlaceholder();

            inputControl.Enter += RemovePlaceholder;
            inputControl.Leave += AddPlaceholder;
        }

        private void ShowPlaceholder()
        {
            inputControl.Text = placeholder;
            inputControl.ForeColor = Color.Gray;
            isPlaceholder = true;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (isPlaceholder)
            {
                inputControl.Clear();
                inputControl.ForeColor = Color.Black;
                isPlaceholder = false;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputControl.Text))
            {
                ShowPlaceholder();
            }
        }

        public override string? ToString()
        {
            if (isPlaceholder && inputControl.Text == placeholder)
            {
                return null;
            }

            return inputControl.Text;
        }
    }
}