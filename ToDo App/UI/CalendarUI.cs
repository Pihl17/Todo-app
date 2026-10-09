using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Input;
using ToDo_App.Calendar;

namespace ToDo_App.UI
{
    public partial class CalendarUI : UserControl
    {
        private IInputHandling inputHandler;

        public CalendarUI()
        {
            InitializeComponent();
        }

        public CalendarUI(IInputHandling inputHandler) : this()
        {
            this.inputHandler = inputHandler;
            inputHandler.Calendar().InitializeSelectedMonth();
            selectedMonth.Text = inputHandler.Calendar().selectedMonthString;

            PopulateMonthPanel();
        }

        private void CalendarUI_Load(object sender, EventArgs e)
        {

        }

        private void selectedMonth_Click(object sender, EventArgs e)
        {
            monthSelectPanel.Visible = !monthSelectPanel.Visible;
        }

        private void previousMonth_Click(object sender, EventArgs e)
        {
            inputHandler.Calendar().PreviousMonth();
            selectedMonth.Text = inputHandler.Calendar().selectedMonthString;
        }

        private void nextMonth_Click(object sender, EventArgs e)
        {
            inputHandler.Calendar().NextMonth();
            selectedMonth.Text = inputHandler.Calendar().selectedMonthString;
        }

            private void PopulateMonthPanel()
        {
            monthSelectPanel.Controls.Clear();

            int columns = 3;
            int spacing = 3;

            int buttonWidth =
                (monthSelectPanel.ClientSize.Width - (columns - 1) * spacing)
                / columns;

            for (int i = 0; i < 12; i++)
            {
                int column = i % columns;
                int row = i / columns;

                Button button = new Button();
                button.Text = $"Month {i + 1}";
                button.Width = buttonWidth;
                button.Height = 40;

                button.Left = column * (buttonWidth + spacing);
                button.Top = row * (button.Height + spacing);

                button.Click += (sender, e) =>
                {
                    MessageBox.Show($"You clicked {button.Text}");
                };

                monthSelectPanel.Controls.Add(button);
            }
        
    }
    }
}
