using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WindowsApp.UI
{
    public partial class CreateToDo : UserControl
    {
        private PlaceholderHandler titlePlaceholder;
        private PlaceholderHandler descriptionPlaceholder;
        private PlaceholderHandler tagPlaceholder;
        private DateTime? selectedDT;
        private IInputHandling inputHandler;
        List<ChecklistItemUI> checklistItems = new List<ChecklistItemUI>();

        public CreateToDo()
        {
        }
        /// <summary>
        /// Initialize the panel.
        /// </summary>
        public CreateToDo(IInputHandling inputHandler) : this()
        {
            InitializeComponent();
            this.inputHandler = inputHandler;
            repeatCB.SelectedIndex = 0;
            titlePlaceholder = new PlaceholderHandler(titleTB, "Title here");
            descriptionPlaceholder = new PlaceholderHandler(descriptionTB, "Write your description here.");
            tagPlaceholder = new PlaceholderHandler(tagInputTB, "Write your tags here.");
            AddChecklistItem();
        }
        /// <summary>
        /// Close button that closes the panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void deadlineSelectBtn_Click(object sender, EventArgs e)
        {
            AlternateCalendarVisibility();
        }
        private void clearDateBtn_Click(object sender, EventArgs e)
        {
            selectedDT = null;
            deadlineTB.Text = "Deadline: None";
            AlternateCalendarVisibility();
        }
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            selectedDT = monthCalendar.SelectionStart;
            deadlineTB.Text = $"Deadline: {selectedDT.ToString()}";
            AlternateCalendarVisibility();
        }
        private void AlternateCalendarVisibility()
        {
            monthCalendar.Visible = !monthCalendar.Visible;
            clearDateBtn.Visible = !clearDateBtn.Visible;
        }
        private void AddChecklistItem()
        {
            ChecklistItemUI item = new ChecklistItemUI();

            item.Dock = DockStyle.Top;
            item.ItemLeave += ChecklistItem_ItemLeave;

            checklistItems.Add(item);
            checklistItemPanel.Controls.Add(item);
            checklistItemPanel.Controls.SetChildIndex(item, 0);
            UpdateChecklistHeight();
        }
        private void ChecklistItem_ItemLeave(object sender, EventArgs e)
        {
            if (sender is not ChecklistItemUI item)
                return;

            // Add a new row when the last row has text.
            if (checklistItems.Last() == item &&
                !string.IsNullOrWhiteSpace(item.GetTextBoxText()))
            {
                AddChecklistItem();
                return;
            }

            if (checklistItems.Count > 1 &&
                string.IsNullOrWhiteSpace(item.GetTextBoxText()))
            {
                BeginInvoke((Action)(() =>
                {
                    // Verify it hasn't already been removed.
                    if (checklistItems.Contains(item))
                    {
                        checklistItems.Remove(item);
                        checklistItemPanel.Controls.Remove(item);
                        item.Dispose();
                        UpdateChecklistHeight();
                    }
                }));
            }
        }
        private void UpdateChecklistHeight()
        {
            int totalHeight = checklistItemPanel.Controls
                .Cast<Control>()
                .Sum(control => control.Height);

            checklistItemPanel.Height = totalHeight;
            checklistPanel.Height = totalHeight + 30;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(!Enum.TryParse<RepeatInterval>(repeatCB.Text,true,out RepeatInterval repeat))
            {
                repeat = RepeatInterval.None;
            }
            List<string> tags = tagPlaceholder.ToString()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            List<ChecklistItem> checklist = new List<ChecklistItem>();
            foreach (var item in checklistItems)
            {
                var cli = new ChecklistItem();
                cli.IsDone = item.GetCheckBoxValue();
                cli.Description = item.GetTextBoxText();
                checklist.Add(cli);
            }
            inputHandler.CreateNewToDo(
                title: titlePlaceholder.ToString(),
                choosenDeadLine: selectedDT,
                repeat: repeat,
                priority: int.Parse(priorityUpDown.Text),
                description: descriptionPlaceholder.ToString(),
                tags: tags,
                checklist: null);

            this.Dispose();
        }
    }
}

