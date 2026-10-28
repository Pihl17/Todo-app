namespace ToDo_App.WindowsApp.UI
{
    partial class CreateToDo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            closeBtn = new Button();
            toDoPanel = new Panel();
            clearDateBtn = new Button();
            monthCalendar = new MonthCalendar();
            savePanel = new Panel();
            saveBtn = new Button();
            checklistPanel = new Panel();
            checklistItemPanel = new Panel();
            checklistTB = new TextBox();
            tagInputTB = new TextBox();
            tagTB = new TextBox();
            priorityPanel = new Panel();
            repeatCB = new ComboBox();
            repeatTB = new TextBox();
            deadlineSelectBtn = new Button();
            deadlineTB = new TextBox();
            priorityUpDown = new DomainUpDown();
            priorityTB = new TextBox();
            descriptionTB = new TextBox();
            titleTB = new TextBox();
            toDoPanel.SuspendLayout();
            savePanel.SuspendLayout();
            checklistPanel.SuspendLayout();
            priorityPanel.SuspendLayout();
            SuspendLayout();
            // 
            // closeBtn
            // 
            closeBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeBtn.BackColor = Color.Firebrick;
            closeBtn.ForeColor = SystemColors.Control;
            closeBtn.Location = new Point(692, 3);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(29, 29);
            closeBtn.TabIndex = 1;
            closeBtn.Text = "X";
            closeBtn.UseVisualStyleBackColor = false;
            closeBtn.Click += closeBtn_Click;
            // 
            // toDoPanel
            // 
            toDoPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            toDoPanel.AutoScroll = true;
            toDoPanel.BackColor = SystemColors.Control;
            toDoPanel.Controls.Add(clearDateBtn);
            toDoPanel.Controls.Add(monthCalendar);
            toDoPanel.Controls.Add(savePanel);
            toDoPanel.Controls.Add(checklistPanel);
            toDoPanel.Controls.Add(tagInputTB);
            toDoPanel.Controls.Add(tagTB);
            toDoPanel.Controls.Add(priorityPanel);
            toDoPanel.Controls.Add(descriptionTB);
            toDoPanel.Controls.Add(titleTB);
            toDoPanel.Location = new Point(38, 38);
            toDoPanel.Name = "toDoPanel";
            toDoPanel.Size = new Size(648, 430);
            toDoPanel.TabIndex = 3;
            // 
            // clearDateBtn
            // 
            clearDateBtn.Location = new Point(176, 401);
            clearDateBtn.Name = "clearDateBtn";
            clearDateBtn.Size = new Size(199, 29);
            clearDateBtn.TabIndex = 4;
            clearDateBtn.Text = "Remove Deadline";
            clearDateBtn.UseVisualStyleBackColor = true;
            clearDateBtn.Visible = false;
            clearDateBtn.Click += clearDateBtn_Click;
            // 
            // monthCalendar
            // 
            monthCalendar.Location = new Point(176, 196);
            monthCalendar.Name = "monthCalendar";
            monthCalendar.TabIndex = 11;
            monthCalendar.Visible = false;
            monthCalendar.DateChanged += monthCalendar_DateChanged;
            // 
            // savePanel
            // 
            savePanel.Controls.Add(saveBtn);
            savePanel.Dock = DockStyle.Top;
            savePanel.Location = new Point(0, 373);
            savePanel.Name = "savePanel";
            savePanel.Size = new Size(648, 30);
            savePanel.TabIndex = 10;
            // 
            // saveBtn
            // 
            saveBtn.BackColor = Color.LightBlue;
            saveBtn.Dock = DockStyle.Right;
            saveBtn.Location = new Point(554, 0);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 30);
            saveBtn.TabIndex = 9;
            saveBtn.Text = "Save to do";
            saveBtn.UseVisualStyleBackColor = false;
            saveBtn.Click += saveBtn_Click;
            // 
            // checklistPanel
            // 
            checklistPanel.Controls.Add(checklistItemPanel);
            checklistPanel.Controls.Add(checklistTB);
            checklistPanel.Dock = DockStyle.Top;
            checklistPanel.Location = new Point(0, 248);
            checklistPanel.Name = "checklistPanel";
            checklistPanel.Size = new Size(648, 125);
            checklistPanel.TabIndex = 8;
            // 
            // checklistItemPanel
            // 
            checklistItemPanel.Dock = DockStyle.Fill;
            checklistItemPanel.Location = new Point(0, 25);
            checklistItemPanel.Name = "checklistItemPanel";
            checklistItemPanel.Size = new Size(648, 100);
            checklistItemPanel.TabIndex = 12;
            // 
            // checklistTB
            // 
            checklistTB.Dock = DockStyle.Top;
            checklistTB.Enabled = false;
            checklistTB.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checklistTB.Location = new Point(0, 0);
            checklistTB.Name = "checklistTB";
            checklistTB.Size = new Size(648, 25);
            checklistTB.TabIndex = 11;
            checklistTB.Text = "Checklist:";
            // 
            // tagInputTB
            // 
            tagInputTB.Dock = DockStyle.Top;
            tagInputTB.Font = new Font("Arial Narrow", 10.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            tagInputTB.Location = new Point(0, 221);
            tagInputTB.Name = "tagInputTB";
            tagInputTB.Size = new Size(648, 27);
            tagInputTB.TabIndex = 7;
            // 
            // tagTB
            // 
            tagTB.Dock = DockStyle.Top;
            tagTB.Enabled = false;
            tagTB.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tagTB.Location = new Point(0, 196);
            tagTB.Name = "tagTB";
            tagTB.Size = new Size(648, 25);
            tagTB.TabIndex = 6;
            tagTB.Text = "Tags (tags are separated with space):";
            // 
            // priorityPanel
            // 
            priorityPanel.Controls.Add(repeatCB);
            priorityPanel.Controls.Add(repeatTB);
            priorityPanel.Controls.Add(deadlineSelectBtn);
            priorityPanel.Controls.Add(deadlineTB);
            priorityPanel.Controls.Add(priorityUpDown);
            priorityPanel.Controls.Add(priorityTB);
            priorityPanel.Dock = DockStyle.Top;
            priorityPanel.Location = new Point(0, 168);
            priorityPanel.Name = "priorityPanel";
            priorityPanel.Size = new Size(648, 28);
            priorityPanel.TabIndex = 4;
            // 
            // repeatCB
            // 
            repeatCB.Dock = DockStyle.Left;
            repeatCB.DropDownStyle = ComboBoxStyle.DropDownList;
            repeatCB.FormattingEnabled = true;
            repeatCB.Items.AddRange(new object[] { "None", "Daily", "Weekly", "Weekdays", "Monthly", "Yearly" });
            repeatCB.Location = new Point(444, 0);
            repeatCB.Name = "repeatCB";
            repeatCB.Size = new Size(151, 28);
            repeatCB.TabIndex = 6;
            // 
            // repeatTB
            // 
            repeatTB.Dock = DockStyle.Left;
            repeatTB.Enabled = false;
            repeatTB.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            repeatTB.Location = new Point(375, 0);
            repeatTB.Name = "repeatTB";
            repeatTB.ReadOnly = true;
            repeatTB.Size = new Size(69, 27);
            repeatTB.TabIndex = 7;
            repeatTB.Text = "Repeat:";
            repeatTB.TextAlign = HorizontalAlignment.Right;
            // 
            // deadlineSelectBtn
            // 
            deadlineSelectBtn.Dock = DockStyle.Left;
            deadlineSelectBtn.Location = new Point(347, 0);
            deadlineSelectBtn.Name = "deadlineSelectBtn";
            deadlineSelectBtn.Size = new Size(28, 28);
            deadlineSelectBtn.TabIndex = 6;
            deadlineSelectBtn.Text = "▼";
            deadlineSelectBtn.UseVisualStyleBackColor = true;
            deadlineSelectBtn.Click += deadlineSelectBtn_Click;
            // 
            // deadlineTB
            // 
            deadlineTB.Dock = DockStyle.Left;
            deadlineTB.Enabled = false;
            deadlineTB.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            deadlineTB.Location = new Point(176, 0);
            deadlineTB.Name = "deadlineTB";
            deadlineTB.ReadOnly = true;
            deadlineTB.Size = new Size(171, 27);
            deadlineTB.TabIndex = 5;
            deadlineTB.Text = "Deadline: None";
            deadlineTB.TextAlign = HorizontalAlignment.Right;
            // 
            // priorityUpDown
            // 
            priorityUpDown.Dock = DockStyle.Left;
            priorityUpDown.Items.Add("10");
            priorityUpDown.Items.Add("9");
            priorityUpDown.Items.Add("8");
            priorityUpDown.Items.Add("7");
            priorityUpDown.Items.Add("6");
            priorityUpDown.Items.Add("5");
            priorityUpDown.Items.Add("4");
            priorityUpDown.Items.Add("3");
            priorityUpDown.Items.Add("2");
            priorityUpDown.Items.Add("1");
            priorityUpDown.Location = new Point(123, 0);
            priorityUpDown.Name = "priorityUpDown";
            priorityUpDown.ReadOnly = true;
            priorityUpDown.Size = new Size(53, 27);
            priorityUpDown.TabIndex = 3;
            priorityUpDown.Wrap = true;
            // 
            // priorityTB
            // 
            priorityTB.Dock = DockStyle.Left;
            priorityTB.Enabled = false;
            priorityTB.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            priorityTB.Location = new Point(0, 0);
            priorityTB.Name = "priorityTB";
            priorityTB.ReadOnly = true;
            priorityTB.RightToLeft = RightToLeft.No;
            priorityTB.Size = new Size(123, 27);
            priorityTB.TabIndex = 4;
            priorityTB.Text = "Priority (1-10):";
            priorityTB.TextAlign = HorizontalAlignment.Right;
            // 
            // descriptionTB
            // 
            descriptionTB.BorderStyle = BorderStyle.None;
            descriptionTB.Dock = DockStyle.Top;
            descriptionTB.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descriptionTB.Location = new Point(0, 32);
            descriptionTB.Multiline = true;
            descriptionTB.Name = "descriptionTB";
            descriptionTB.Size = new Size(648, 136);
            descriptionTB.TabIndex = 5;
            // 
            // titleTB
            // 
            titleTB.BorderStyle = BorderStyle.None;
            titleTB.Dock = DockStyle.Top;
            titleTB.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleTB.Location = new Point(0, 0);
            titleTB.Name = "titleTB";
            titleTB.Size = new Size(648, 32);
            titleTB.TabIndex = 1;
            titleTB.Text = "Title";
            // 
            // CreateToDo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            Controls.Add(toDoPanel);
            Controls.Add(closeBtn);
            Name = "CreateToDo";
            Size = new Size(724, 506);
            toDoPanel.ResumeLayout(false);
            toDoPanel.PerformLayout();
            savePanel.ResumeLayout(false);
            checklistPanel.ResumeLayout(false);
            checklistPanel.PerformLayout();
            priorityPanel.ResumeLayout(false);
            priorityPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button closeBtn;
        private Panel toDoPanel;
        private TextBox titleTB;
        private Panel priorityPanel;
        private DomainUpDown priorityUpDown;
        private TextBox priorityTB;
        private TextBox descriptionTB;
        private TextBox deadlineTB;
        private Button deadlineSelectBtn;
        private TextBox repeatTB;
        private ComboBox repeatCB;
        private TextBox tagTB;
        private TextBox tagInputTB;
        private Panel checklistPanel;
        private Button saveBtn;
        private Panel savePanel;
        private TextBox checklistTB;
        private Button clearDateBtn;
        private MonthCalendar monthCalendar;
        private Panel checklistItemPanel;
    }
}
