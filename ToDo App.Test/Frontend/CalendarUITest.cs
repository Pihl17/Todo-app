using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App.Test.Frontend
{
    internal class CalendarUITest
    {

        CalendarUI calendarUI;
        private Mock<IInputHandling> inputMock;
        private DateTime currentMonth;

        [SetUp]
        public void Setup()
        {
            currentMonth = new DateTime(2026, 10, 1);

            inputMock = new Mock<IInputHandling>();

            inputMock.Setup(x => x.InitializeSelectedMonth())
                .Callback(() => currentMonth = new DateTime(2026, 10, 1));

            inputMock.Setup(x => x.GetSelectedMonthString())
                .Returns(() => currentMonth.ToString("MMMM yyyy"));

            inputMock.Setup(x => x.NextMonth())
                .Callback(() => currentMonth = currentMonth.AddMonths(1));

            inputMock.Setup(x => x.PreviousMonth())
                .Callback(() => currentMonth = currentMonth.AddMonths(-1));

            inputMock.Setup(x => x.NextYear())
                .Callback(() => currentMonth = currentMonth.AddYears(1));

            inputMock.Setup(x => x.PreviousYear())
                .Callback(() => currentMonth = currentMonth.AddYears(-1));

            inputMock.Setup(x => x.GetToDoList())
                .Returns(new List<ToDo>());

            inputMock.Setup(x => x.GetToDosOnDate(It.IsAny<DateTime>())).Returns(new List<ToDo>());


            inputMock.Setup(x => x.GetToDosInGivenMonth())
                .Returns(() =>
                {
                    int days = DateTime.DaysInMonth(
                        currentMonth.Year, currentMonth.Month);

                    return Enumerable.Range(1, days)
                        .Select(day => new CalendarDay
                        {
                            Date = new DateTime(
                                currentMonth.Year, currentMonth.Month, day),
                            ToDosOfTheDay = new List<ToDo>()
                        })
                        .ToList();
                });

            // Construct the UI only after configuring the mock.
            calendarUI = new CalendarUI(inputMock.Object);
        }


        [Test]
        public void TestCalendarUIInitialization()
        {
            Assert.IsNotNull(calendarUI);
        }


        [Test]
        public void CalendarUI_InitializesWithInputHandlerTest()
        {
            using var ui = new CalendarUI(inputMock.Object);

            Assert.That(ui, Is.Not.Null);
        }
        [Test]
        public void CalendarUI_CreatesTwelveMonthButtons()
        {
            try
            {
                Assert.That(calendarUI.MonthButtonCount, Is.EqualTo(15));
            }
            finally
            {
                calendarUI.Dispose();
            }
        }
        
        [Test]
        public void CalendarUI_ClickTwelveMonthButtons()
        {
            try
            {
                int i = 0;
                foreach (Button button in calendarUI.MonthButtons)
                {
                    i++;
                    Assert.That(button.Text, !Is.Null);
                }
            }
            finally
            {
                calendarUI.Dispose();
            }
        }
        [Test]
        public void MonthSelectorButton_TogglesPanelVisibilityTest()
        {

            bool initialVisibility = calendarUI.MonthPanelVisible;

            calendarUI.MonthSelectorButton.PerformClick();

            Assert.That(
                calendarUI.MonthPanelVisible,
                Is.EqualTo(!initialVisibility));

        }
        [Test]
        public void MonthButton_Click_NoSubscribers_DoesNotThrow()
        {
            var button = new Button { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(1) };

            Assert.DoesNotThrow(() =>
                calendarUI.MonthButton_Click(button, EventArgs.Empty));
        }
        [Test]
        public void NextMonthButton_GivesNextMonthTest()
        {
            DateTime initialMonth = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            calendarUI.NextMonthButton.PerformClick();
            DateTime nextMonth = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            Assert.That(nextMonth, Is.EqualTo(initialMonth.AddMonths(1)));
        }
        [Test]
        public void PreviousMonthButton_GivesPreviousMonthTest()
        {
            DateTime initialMonth = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            calendarUI.PreviousMonthButton.PerformClick();
            DateTime previousMonth = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            Assert.That(previousMonth, Is.EqualTo(initialMonth.AddMonths(-1)));
        }
        [Test]
        public void NextYearButton_GivesNextYearTest()
        {
            bool initialVisibility = calendarUI.MonthPanelVisible;
            calendarUI.MonthSelectorButton.PerformClick();
            DateTime initialYear = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            calendarUI.NextYearButton.PerformClick();
            inputMock.Verify(x => x.NextYear(), Times.Once);
            DateTime nextYear = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            Assert.That(nextYear, Is.EqualTo(initialYear.AddYears(1)));
        }
        [Test]
        public void PreviousYearButton_GivesPreviousYearTest()
        {
            bool initialVisibility = calendarUI.MonthPanelVisible;
            calendarUI.MonthSelectorButton.PerformClick();
            DateTime initialYear = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            calendarUI.PreviousYearButton.PerformClick();
            DateTime previousYear = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            Assert.That(previousYear, Is.EqualTo(initialYear.AddYears(-1)));
        }
        [Test]
        public void DisplayedMonth_UpdatesSelectedMonthButtonTextTest()
        {
            DateTime initialMonth = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            calendarUI.NextMonthButton.PerformClick();
            DateTime updatedMonth = DateTime.Parse(calendarUI.MonthSelectorButton.Text);
            Assert.That(updatedMonth, Is.EqualTo(initialMonth.AddMonths(1)));
        }
        [Test]
        public void MonthButton_Click_RaisesMonthSelectedEvent()
        {
            bool eventRaised = false;
            string selectedMonth = null;

            calendarUI.MonthSelected += month =>
            {
                eventRaised = true;
                selectedMonth = month;
            };
            string firstMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(1);
            calendarUI.MonthButtons[3].Text = firstMonth;

            calendarUI.MonthButton_Click(
                calendarUI.MonthButtons[3],
                EventArgs.Empty);

            Assert.That(eventRaised, Is.True);
            Assert.That(selectedMonth, Is.EqualTo(firstMonth));
        }

        [Test]
        public void CalendarUI_Dispose_DisposesControl()
        {
            Assert.DoesNotThrow(() => calendarUI.Dispose());
        }
        [Test]
        public void PopulateCalendarDisplayPanel_DayHasToDos_DisplaysToDoCount()
        {
            // Arrange
            var days = inputMock.Object.GetToDosInGivenMonth();

            days[0].ToDosOfTheDay.Add(new ToDo());
            days[0].ToDosOfTheDay.Add(new ToDo());

            inputMock.Setup(x => x.GetToDosInGivenMonth())
                .Returns(days);

            // Act
            calendarUI.PopulateCalendarDisplayPanel();

            // Assert
            Button firstDayButton = calendarUI.CalendarDayButtons.First();

            Assert.That(firstDayButton.Text, Does.Contain("To Dos today: 2"));
        }
        [Test]
        public void PopulateMonthPanel_CreatesMonthButtons()
        {
            var method = typeof(CalendarUI).GetMethod(
                "PopulateMonthPanel",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            Assert.That(method, Is.Not.Null);

            method.Invoke(calendarUI, null);
            Assert.That(calendarUI.MonthButtons.Count(), Is.EqualTo(15));
            Assert.That(
                calendarUI.MonthButtons.All(b => !string.IsNullOrWhiteSpace(b.Text)),
                Is.True);
        }
        [Test]
        public void CalendarDayButton_Click_CreatesDisplayForSelectedDate()
        {

            var button = new Button { Text = "15" };

            calendarUI.CalendarDayButton_Click(button, EventArgs.Empty);

            Assert.That(calendarUI.DayDisplay, Is.Not.Null);
            Assert.That(calendarUI.DayDisplay.Dock, Is.EqualTo(DockStyle.Fill));
        }
        [Test]
        public void CalendarDayButton_Click_RaisesMonthSelectedEvent()
        {
            // Arrange
            bool eventRaised = false;
            string selectedDay = null;

            calendarUI.MonthSelected += value =>
            {
                eventRaised = true;
                selectedDay = value;
            };

            var button = new Button { Text = "15" };

            // Act
            calendarUI.CalendarDayButton_Click(button, EventArgs.Empty);

            // Assert
            Assert.That(eventRaised, Is.True);
            Assert.That(selectedDay, Is.EqualTo("15"));
        }
        [Test]
        public void CalendarUI_Click_Test()
        {
            bool initialVisibility = calendarUI.MonthPanelVisible;

            calendarUI.MonthSelectorButton.PerformClick();
            Assert.That(calendarUI.MonthPanelVisible, Is.EqualTo(!initialVisibility));

            typeof(Control)
                .GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic)!
                .Invoke(calendarUI, new object[] { EventArgs.Empty });
            Assert.That(calendarUI.MonthPanelVisible, Is.EqualTo(initialVisibility));
        }

        [TearDown]
        public void TearDown()
        {
            calendarUI?.Dispose();
        }
    }
}
