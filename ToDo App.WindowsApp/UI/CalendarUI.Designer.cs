namespace ToDo_App.UI
{
    partial class CalendarUI
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
            calendarDisplayPanel = new Panel();
            previousMonth = new Button();
            nextMonth = new Button();
            selectedMonth = new Button();
            monthSelectPanel = new Panel();
            yearBtn = new Button();
            nextYearBtn = new Button();
            previousYearBtn = new Button();
            monthSelectPanel.SuspendLayout();
            SuspendLayout();
            // 
            // calendarDisplayPanel
            // 
            calendarDisplayPanel.Location = new Point(3, 43);
            calendarDisplayPanel.Margin = new Padding(3, 4, 3, 4);
            calendarDisplayPanel.Name = "calendarDisplayPanel";
            calendarDisplayPanel.Size = new Size(1401, 805);
            calendarDisplayPanel.TabIndex = 2;
            // 
            // previousMonth
            // 
            previousMonth.Location = new Point(3, 7);
            previousMonth.Name = "previousMonth";
            previousMonth.Size = new Size(29, 29);
            previousMonth.TabIndex = 3;
            previousMonth.Text = "◄";
            previousMonth.UseVisualStyleBackColor = true;
            previousMonth.Click += previousMonth_Click;
            // 
            // nextMonth
            // 
            nextMonth.Location = new Point(263, 7);
            nextMonth.Name = "nextMonth";
            nextMonth.Size = new Size(29, 29);
            nextMonth.TabIndex = 4;
            nextMonth.Text = "►";
            nextMonth.UseVisualStyleBackColor = true;
            nextMonth.Click += nextMonth_Click;
            // 
            // selectedMonth
            // 
            selectedMonth.Location = new Point(39, 7);
            selectedMonth.Name = "selectedMonth";
            selectedMonth.Size = new Size(219, 29);
            selectedMonth.TabIndex = 5;
            selectedMonth.UseVisualStyleBackColor = true;
            selectedMonth.Click += selectedMonth_Click;
            // 
            // monthSelectPanel
            // 
            monthSelectPanel.BorderStyle = BorderStyle.FixedSingle;
            monthSelectPanel.Controls.Add(yearBtn);
            monthSelectPanel.Controls.Add(nextYearBtn);
            monthSelectPanel.Controls.Add(previousYearBtn);
            monthSelectPanel.Location = new Point(39, 41);
            monthSelectPanel.Name = "monthSelectPanel";
            monthSelectPanel.Size = new Size(298, 240);
            monthSelectPanel.TabIndex = 0;
            monthSelectPanel.Visible = false;
            // 
            // yearBtn
            // 
            yearBtn.Enabled = false;
            yearBtn.Location = new Point(38, 4);
            yearBtn.Name = "yearBtn";
            yearBtn.Size = new Size(219, 29);
            yearBtn.TabIndex = 6;
            yearBtn.UseVisualStyleBackColor = true;
            // 
            // nextYearBtn
            // 
            nextYearBtn.Location = new Point(263, 4);
            nextYearBtn.Name = "nextYearBtn";
            nextYearBtn.Size = new Size(29, 29);
            nextYearBtn.TabIndex = 6;
            nextYearBtn.Text = "►";
            nextYearBtn.UseVisualStyleBackColor = true;
            nextYearBtn.Click += nextYearBtn_Click;
            // 
            // previousYearBtn
            // 
            previousYearBtn.Location = new Point(3, 4);
            previousYearBtn.Name = "previousYearBtn";
            previousYearBtn.Size = new Size(29, 29);
            previousYearBtn.TabIndex = 6;
            previousYearBtn.Text = "◄";
            previousYearBtn.UseVisualStyleBackColor = true;
            previousYearBtn.Click += previousYearBtn_Click;
            // 
            // CalendarUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(monthSelectPanel);
            Controls.Add(selectedMonth);
            Controls.Add(nextMonth);
            Controls.Add(previousMonth);
            Controls.Add(calendarDisplayPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "CalendarUI";
            Size = new Size(1409, 852);
            Click += CalendarUI_Click;
            monthSelectPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel calendarDisplayPanel;
        private Button previousMonth;
        private Button nextMonth;
        private Button selectedMonth;
        private Panel monthSelectPanel;
        private Button yearBtn;
        private Button nextYearBtn;
        private Button previousYearBtn;
    }
}
