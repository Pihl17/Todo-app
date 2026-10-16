using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App_test.Frontend
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

            inputMock.Setup(x => x.GetToDoList())
                .Returns(new List<ToDo>());

            inputMock.Setup(x => x.GetToDosInGivenMonth(It.IsAny<List<ToDo>>()))
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
                Assert.That(calendarUI.MonthButtonCount, Is.EqualTo(12));
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
                    Assert.That(button.Text, Is.EqualTo("Month " + i));
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
            Assert.DoesNotThrow(() =>
                calendarUI.MonthButton_Click(
                    calendarUI.MonthButtons[0],
                    EventArgs.Empty));
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

            calendarUI.MonthButton_Click(
                calendarUI.MonthButtons[0],
                EventArgs.Empty);

            Assert.That(eventRaised, Is.True);
            Assert.That(selectedMonth, Is.EqualTo("Month 1"));
        }

        [Test]
        public void CalendarUI_Dispose_DisposesControl()
        {
            Assert.DoesNotThrow(() => calendarUI.Dispose());
        }

        [TearDown]
        public void TearDown()
        {
            calendarUI?.Dispose();
        }
    }
}
