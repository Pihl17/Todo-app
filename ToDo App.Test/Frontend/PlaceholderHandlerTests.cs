using NUnit.Framework;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ToDo_App.WindowsApp.UI;

namespace ToDo_App.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class PlaceholderHandlerTests
    {
        [Test]
        public void TextBoxConstructor_DisplaysPlaceholderInGray()
        {
            using var textBox = new TextBox();

            var handler = new PlaceholderHandler(
                textBox, "Enter title");

            Assert.That(textBox.Text, Is.EqualTo("Enter title"));
            Assert.That(textBox.ForeColor, Is.EqualTo(Color.Gray));
            Assert.That(handler.ToString(), Is.Null);
        }

        [Test]
        public void RichTextBoxConstructor_DisplaysPlaceholderInGray()
        {
            using var richTextBox = new RichTextBox();

            var handler = new PlaceholderHandler(
                richTextBox, "Enter description");

            Assert.That(
                richTextBox.Text,
                Is.EqualTo("Enter description"));

            Assert.That(
                richTextBox.ForeColor,
                Is.EqualTo(Color.Gray));

            Assert.That(handler.ToString(), Is.Null);
        }

        [Test]
        public void TextBox_EnterAndLeave_RestoresPlaceholderWhenEmpty()
        {
            TestPlaceholderBehaviour(
                new TextBox(),
                control => new PlaceholderHandler(
                    (TextBox)control, "Enter title"));
        }

        [Test]
        public void RichTextBox_EnterAndLeave_RestoresPlaceholderWhenEmpty()
        {
            TestPlaceholderBehaviour(
                new RichTextBox(),
                control => new PlaceholderHandler(
                    (RichTextBox)control, "Enter description"));
        }

        [Test]
        public void ToString_WhenUserHasEnteredText_ReturnsText()
        {
            using var textBox = new TextBox();
            var handler = new PlaceholderHandler(
                textBox, "Enter title");

            // Simulate the user entering text.
            textBox.Text = "Buy groceries";

            // The placeholder flag is still true until Enter fires,
            // so trigger the actual focus event in a form.
            using var form = new Form();
            using var otherControl = new Button();

            form.Controls.Add(textBox);
            form.Controls.Add(otherControl);
            form.Show();
            Application.DoEvents();

            otherControl.Focus();
            Application.DoEvents();

            textBox.Focus();
            Application.DoEvents();

            textBox.Text = "Buy groceries";
            otherControl.Focus();
            Application.DoEvents();

            Assert.That(handler.ToString(), Is.EqualTo("Buy groceries"));
        }

        private static void TestPlaceholderBehaviour(
            Control input,
            Func<Control, PlaceholderHandler> createHandler)
        {
            using (input)
            using (var form = new Form())
            using (var otherControl = new Button())
            {
                var handler = createHandler(input);

                // Initially, the placeholder should be displayed.
                Assert.That(handler.ToString(), Is.Null);

                form.Controls.Add(input);
                form.Controls.Add(otherControl);

                form.Show();
                Application.DoEvents();

                // Focus another control first.
                otherControl.Focus();
                Application.DoEvents();

                // Enter the input control.
                input.Focus();
                Application.DoEvents();

                Assert.That(input.Text, Is.Empty);
                Assert.That(input.ForeColor, Is.EqualTo(Color.Black));

                // Leave while empty.
                otherControl.Focus();
                Application.DoEvents();

                Assert.That(
                    input.Text,
                    Is.EqualTo(input is RichTextBox
                        ? "Enter description"
                        : "Enter title"));

                Assert.That(input.ForeColor, Is.EqualTo(Color.Gray));
                Assert.That(handler.ToString(), Is.Null);
            }
        }
        [Test]
        public void TextBoxConstructor_NullTextBox_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PlaceholderHandler((TextBox)null!, "Enter title"));
        }
        [Test]
        public void RichTextBoxConstructor_NullRichTextBox_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PlaceholderHandler((RichTextBox)null!, "Enter description"));
        }
    }
}