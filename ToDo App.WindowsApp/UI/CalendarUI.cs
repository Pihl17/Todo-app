using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.WindowsApp.UI;

namespace ToDo_App.UI
{
    public partial class CalendarUI : UserControl
    {
        private DayAllTaskDisplay datDisplay;
        public event Action<string> MonthSelected;
        public Button[] MonthButtons => monthSelectPanel.Controls.OfType<Button>().ToArray();
        public IEnumerable<Button> CalendarDayButtons => calendarDisplayPanel.Controls.OfType<Button>().Skip(7);
        public bool MonthPanelVisible => monthSelectPanel.Visible;
        public int MonthButtonCount => monthSelectPanel.Controls.Count;
        public Button NextMonthButton => nextMonth;
        public Button PreviousMonthButton => previousMonth;
        public Button MonthSelectorButton => selectedMonth;
        public Button PreviousYearButton => previousYearBtn;
        public Button NextYearButton => nextYearBtn;
        public DayAllTaskDisplay DayDisplay => datDisplay;

        private IInputHandling inputHandler;
        private readonly List<Button> generatedMonthButtons = new();
        /// <summary>
        /// UI for the calendar, allowing users to view and select months.
        /// </summary>
        public CalendarUI()
        {
        }
        /// <summary>
        /// Initializes the CalendarUI with an input handler, sets the selected month, and populates the month selection panel.
        /// </summary>
        /// <param name="inputHandler"></param>
        public CalendarUI(IInputHandling inputHandler) : this()
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container();
            this.inputHandler = inputHandler;
            this.Click += CalendarUI_Click;
            inputHandler.InitializeSelectedMonth();

            PopulateMonthPanel();
            UpdateCalendarDisplay();
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
            UpdateCalendarDisplay();
        }
        /// <summary>
        /// Event handler for the nextMonth button click event. Moves to the next month in the calendar and updates the selectedMonth button text accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextMonth_Click(object sender, EventArgs e)
        {
            inputHandler.NextMonth();
            UpdateCalendarDisplay();

        }
        /// <summary>
        /// Updates the display to apply the selected month and year.
        /// </summary>
        private void UpdateCalendarDisplay()
        {
            yearBtn.Text = DateTime.Parse(inputHandler.GetSelectedMonthString()).Year.ToString();
            selectedMonth.Text =
                string.Create(inputHandler.GetSelectedMonthString().Length, inputHandler.GetSelectedMonthString(), static (chars, str) => { chars[0] = char.ToUpperInvariant(str[0]); str.AsSpan(1).CopyTo(chars[1..]); });
            PopulateCalendarDisplayPanel();
        }
        /// <summary>
        /// Populates the monthSelectPanel with buttons representing each month of the year. Each button is labeled with the corresponding month number and has a click event that displays a message box indicating which month was clicked.
        /// </summary>
        private void PopulateMonthPanel()
        {
            foreach (Button button in generatedMonthButtons)
                monthSelectPanel.Controls.Remove(button);

            generatedMonthButtons.Clear();

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
                string month = CultureInfo.CurrentCulture.DateTimeFormat
                    .GetMonthName(i + 1);

                month = string.Create(month.Length, month, static (chars, str) =>
                {
                    chars[0] = char.ToUpperInvariant(str[0]);
                    str.AsSpan(1).CopyTo(chars[1..]);
                });

                button.Text = month;
                button.Width = buttonWidth;
                button.Height = 40;
                button.Left = column * (buttonWidth + spacing);
                button.Top = (row + 1) * (button.Height + spacing);
                button.Click += MonthButton_Click;

                monthSelectPanel.Controls.Add(button);
                generatedMonthButtons.Add(button);
            }
        }

        public void MonthButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int month = DateTime.ParseExact(
                button.Text,
                "MMMM",
                CultureInfo.CurrentCulture
            ).Month;

            inputHandler.ChangeToMonth(month);
            UpdateCalendarDisplay();

            MonthSelected?.Invoke(button.Text);
        }
        public void CalendarDayButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int day = int.Parse(button.Text.Split('\n')[0]);

            DateTime selectedMonth = DateTime.Parse(
                inputHandler.GetSelectedMonthString()
            );

            DateTime selectedDay = new DateTime(
                selectedMonth.Year,
                selectedMonth.Month,
                day
            );

            datDisplay = new DayAllTaskDisplay(inputHandler, selectedDay)
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(datDisplay);
            datDisplay.BringToFront();

            MonthSelected?.Invoke(button.Text);
        }
        public void PopulateCalendarDisplayPanel()
        {

            DateTime selectedMonth =
                DateTime.Parse(inputHandler.GetSelectedMonthString());

            int columns = 7;
            int spacing = 3;
            int days = DateTime.DaysInMonth(
                selectedMonth.Year, selectedMonth.Month);

            int buttonWidth =
                (calendarDisplayPanel.ClientSize.Width
                - (columns - 1) * spacing) / columns;

            var relevantToDos = inputHandler.GetToDosInGivenMonth(inputHandler.GetToDoList());

            calendarDisplayPanel.Controls.Clear();

            for (int i = 0; i < 7; i++)
            {
                int column = i % columns;

                Button button = new Button();
                button.Text = $"{selectedMonth.AddDays(i).DayOfWeek}";
                button.Width = buttonWidth;
                button.Height = 40;
                button.Enabled = false;

                button.Left = column * (buttonWidth + spacing);

                button.Anchor = AnchorStyles.Top | AnchorStyles.Left
                              | AnchorStyles.Right;

                calendarDisplayPanel.Controls.Add(button);
            }

            for (int i = 0; i < days; i++)
            {
                int column = i % columns;
                int row = i / columns;

                Button button = new Button();
                button.Text = $"{i + 1}";
                button.Width = buttonWidth;
                button.Height = 80;
                button.TextAlign = ContentAlignment.TopLeft;
                button.Enabled = true;

                button.Left = column * (buttonWidth + spacing);
                button.Top = row * (button.Height + spacing) + 43;

                if (relevantToDos[i].ToDosOfTheDay.Count > 0)
                {
                    button.Text += "\nTo Dos today: " + relevantToDos[i].ToDosOfTheDay.Count.ToString();
                }

                button.Anchor = AnchorStyles.Top | AnchorStyles.Left
                              | AnchorStyles.Right;

                button.Click += CalendarDayButton_Click;

                calendarDisplayPanel.Controls.Add(button);
            }
        }
        private void previousYearBtn_Click(object sender, EventArgs e)
        {
            inputHandler.PreviousYear();
            yearBtn.Text = DateTime.Parse(inputHandler.GetSelectedMonthString()).Year.ToString();
            selectedMonth.Text =
    string.Create(inputHandler.GetSelectedMonthString().Length, inputHandler.GetSelectedMonthString(), static (chars, str) => { chars[0] = char.ToUpperInvariant(str[0]); str.AsSpan(1).CopyTo(chars[1..]); });
            PopulateCalendarDisplayPanel();
        }
        private void nextYearBtn_Click(object sender, EventArgs e)
        {
            inputHandler.NextYear();
            yearBtn.Text = DateTime.Parse(inputHandler.GetSelectedMonthString()).Year.ToString();
            selectedMonth.Text =
    string.Create(inputHandler.GetSelectedMonthString().Length, inputHandler.GetSelectedMonthString(), static (chars, str) => { chars[0] = char.ToUpperInvariant(str[0]); str.AsSpan(1).CopyTo(chars[1..]); });
            PopulateCalendarDisplayPanel();
        }
        private void CalendarUI_Click(object sender, EventArgs e)
        {
            monthSelectPanel.Visible = false;
        }
    }
}
