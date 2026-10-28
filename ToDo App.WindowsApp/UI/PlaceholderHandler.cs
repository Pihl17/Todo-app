using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo_App.WindowsApp.UI
{
    using System.Drawing;
    using System.Windows.Forms;

    public class PlaceholderHandler
    {
        private readonly RichTextBox richTextBox;
        private readonly TextBox textBox;
        private readonly string placeholder;
        private bool isPlaceholder;
        /// <summary>
        /// Constructor that takes a TextBox. Text will then update automatically with the placeholder text.
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="placeholder"></param>
        public PlaceholderHandler(TextBox textBox, string placeholder)
        {
            this.textBox = textBox;
            this.placeholder = placeholder;

            ShowPlaceholder();

            textBox.Enter += RemovePlaceholder;
            textBox.Leave += AddPlaceholder;
        }
        /// <summary>
        /// Constructor that takes a RichTextBox. Text will then update automatically with the placeholder text.
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="placeholder"></param>
        public PlaceholderHandler(RichTextBox richTextBox, string placeholder)
        {
            this.richTextBox = richTextBox;
            this.placeholder = placeholder;

            ShowPlaceholder();

            richTextBox.Enter += RemovePlaceholder;
            richTextBox.Leave += AddPlaceholder;
        }
        /// <summary>
        /// Displays the placeholder text in gray.
        /// </summary>
        private void ShowPlaceholder()
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            isPlaceholder = true;
        }
        /// <summary>
        /// Removes the placeholder text and restores the color to black.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (isPlaceholder)
            {
                textBox.Clear();
                textBox.ForeColor = Color.Black;
                isPlaceholder = false;
            }
        }
        /// <summary>
        /// Only shows the placeholder if the field is blank.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                ShowPlaceholder();
            }
        }
        public override string? ToString()
        {
            string s = null;
            if (!isPlaceholder)
            {
                if (richTextBox != null)
                {
                    s = richTextBox.Text;
                } else
                {
                    s = textBox.Text;
                }
            }
            return s;
        }
    }
}
