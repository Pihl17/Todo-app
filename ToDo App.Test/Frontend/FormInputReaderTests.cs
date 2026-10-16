using System;
using System.Windows.Forms;
using NUnit.Framework;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App_test.Frontend
{
    public class FormInputReaderTests
    {
        [Test]
        public void ReadText_TextWithSpaces_ReturnsTrimmedText()
        {
            TextBox textBox = new TextBox();
            textBox.Text = " Buy milk ";

            string result = FormInputReader.ReadText(textBox);

            Assert.That(result, Is.EqualTo("Buy milk"));
        }

        [Test]
        public void ReadDate_NotTicked_returnsNull()
        {
            DateTimePicker picker = new DateTimePicker();
            picker.ShowCheckBox = true;
            picker.Checked = false;

            Assert.That(FormInputReader.ReadDate(picker), Is.Null);
        }

        [Test]
        public void ReadDate_Ticked_ReturnsPickedDate()
        {
            DateTimePicker picker = new DateTimePicker();
            picker.ShowCheckBox = true;
            picker.Checked = true;
            picker.Value = new DateTime(2026, 10, 20);

            Assert.That(FormInputReader.ReadDate(picker), Is.EqualTo(new DateTime(2026, 10, 20)));
        }

        [Test]
        public void ReadNumber_ReturnsWholeNumber()
        {
            NumericUpDown numberBox = new NumericUpDown();
            numberBox.Value = 7;

            Assert.That(FormInputReader.ReadNumber(numberBox), Is.EqualTo(7));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ReadYesNo_ReturnsCheckedState(bool isChecked)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Checked = isChecked;

            Assert.That(FormInputReader.ReadYesNo(checkBox), Is.EqualTo(isChecked));
        }

        [Test]
        public void FillEnum_ContainsAllValues()
        {
            ComboBox comboBox = new ComboBox();

            FormInputReader.FillEnum<ToDoStatus>(comboBox);

            Assert.That(comboBox.Items.Count, Is.EqualTo(Enum.GetValues<ToDoStatus>().Length));
        }

        [Test]
        public void ReadEnum_SelectedValue_ReturnsIt()
        {
            ComboBox comboBox = new ComboBox();
            FormInputReader.FillEnum<RepeatInterval>(comboBox);
            comboBox.SelectedItem = RepeatInterval.Weekly;

            Assert.That(FormInputReader.ReadEnum<RepeatInterval>(comboBox), Is.EqualTo(RepeatInterval.Weekly));
        }

        [Test]
        public void ReadEnum_NothingSelected_Throws()
        {
            ComboBox comboBox = new ComboBox();

            Assert.Throws<InvalidOperationException>(() => FormInputReader.ReadEnum<ToDoStatus>(comboBox));
        }

        [Test]
        public void ShowDate_Null_LeavesBoxUnticked()
        {
            DateTimePicker picker = new DateTimePicker();

            FormInputReader.ShowDate(picker, null);

            Assert.That(picker.Checked, Is.False);
        }

        [Test]
        public void ShowDate_Date_TicksBoxAndShowDate()
        {
            DateTimePicker picker = new DateTimePicker();

            FormInputReader.ShowDate(picker, new DateTime(2026, 10, 20));

            Assert.That(picker.Checked, Is.True);
            Assert.That(picker.Value.Date, Is.EqualTo(new DateTime(2026, 10, 20)));
        }
    }
}
