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
        public event Action<string> MonthSelected;
        public Button[] MonthButtons => monthSelectPanel.Controls.OfType<Button>().ToArray();
        public bool MonthPanelVisible => monthSelectPanel.Visible;
        public int MonthButtonCount => monthSelectPanel.Controls.Count;
        public Button NextMonthButton => nextMonth;
        public Button PreviousMonthButton => previousMonth;
        public Button MonthSelectorButton => selectedMonth;
        private IInputHandling inputHandler;
        /// <summary>
        /// UI for the calendar, allowing users to view and select months.
        /// </summary>
        public CalendarUI()
        {
            InitializeComponent();
            PopulateMonthPanel();
            this.components = new System.ComponentModel.Container();
        }
        /// <summary>
        /// Initializes the CalendarUI with an input handler, sets the selected month, and populates the month selection panel.
        /// </summary>
        /// <param name="inputHandler"></param>
        public CalendarUI(IInputHandling inputHandler) : this()
        {
            this.inputHandler = inputHandler;
            inputHandler.InitializeSelectedMonth();
            selectedMonth.Text = inputHandler.GetSelectedMonthString();

        }
        /// <summary>
        /// Event handler for the CalendarUI load event. Currently, it does not perform any actions when the control is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarUI_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Event handler for the selectedMonth button click event. Toggles the visibility of the monthSelectPanel when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectedMonth_Click(object sender, EventArgs e)
        {
            monthSelectPanel.Visible = !monthSelectPanel.Visible;
        }
        /// <summary>
        /// Event handler for the previousMonth button click event. Moves to the previous month in the calendar and updates the selectedMonth button text accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousMonth_Click(object sender, EventArgs e)
        {
            inputHandler.PreviousMonth();
            selectedMonth.Text = inputHandler.GetSelectedMonthString();
        }
        /// <summary>
        /// Event handler for the nextMonth button click event. Moves to the next month in the calendar and updates the selectedMonth button text accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextMonth_Click(object sender, EventArgs e)
        {
            inputHandler.NextMonth();
            selectedMonth.Text = inputHandler.GetSelectedMonthString();
        }
        /// <summary>
        /// Populates the monthSelectPanel with buttons representing each month of the year. Each button is labeled with the corresponding month number and has a click event that displays a message box indicating which month was clicked.
        /// </summary>
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

                button.Click += MonthButton_Click;

                monthSelectPanel.Controls.Add(button);
            }

        }

        public void MonthButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MonthSelected?.Invoke(button.Text);
        }
    }
}
