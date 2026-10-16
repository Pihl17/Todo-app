using System;
using System.Windows.Forms;

namespace ToDo_App.UI
{
    /// <summary>
    /// Helper methods that read and fill the controls used in the ToDo forms.
    /// </summary>
    public static class FormInputReader
    {
        /// <summary>
        /// Returns the text of a TextBox without leading and trailing spaces.
        /// </summary>
        public static string ReadText(TextBox textBox)
        {
            return textBox.Text.Trim();
        }

        /// <summary>
        /// Returns the picked date, or null if the date box is not ticked.
        /// </summary>
        public static DateTime? ReadDate(DateTimePicker picker)
        {
            if (picker.ShowCheckBox && !picker.Checked)
            {
                return null;
            }
            return picker.Value;
        }

        /// <summary>
        /// Returns the whole number in a NumericUpDown.
        /// </summary>
        public static int ReadNumber(NumericUpDown numberBox)
        {
            return (int)numberBox.Value;
        }

        ///<summary>
        /// Returns true if the checkBox is ticked.
        /// </summary>
        public static bool ReadYesNo(CheckBox checkBox)
        {
            return checkBox.Checked;
        }

        /// <summary>
        /// Returns the selected enum value of a ComboBox.
        /// </summary>
        public static T ReadEnum<T>(ComboBox comboBox) where T : struct, Enum
        {
            if (comboBox.SelectedItem is T value)
            {
                return value;
            }
            throw new InvalidOperationException("No value is selected.");
        }

        /// <summary>
        /// Fills a ComboBox with all values of an enum.
        /// </summary>
        public static void FillEnum<T>(ComboBox comboBox) where T : struct, Enum
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox.Items.Clear();

            foreach(T value in Enum.GetValues<T>())
            {
                comboBox.Items.Add(value);
            }
        }

        /// <summary>
        /// Shows a date in a DateTimePicker. A null date leaves the box unticked.
        /// </summary>
        public static void ShowDate(DateTimePicker picker, DateTime? date)
        {
            picker.ShowCheckBox = true;
            picker.Checked = date != null;
            if (date !=null)
            {
                picker.Value = date.Value;
            }
        }

    }
}
