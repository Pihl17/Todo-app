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
            calendarDisplayPanel.Location = new Point(3, 32);
            calendarDisplayPanel.Name = "calendarDisplayPanel";
            calendarDisplayPanel.Size = new Size(1226, 604);
            calendarDisplayPanel.TabIndex = 2;
            // 
            // previousMonth
            // 
            previousMonth.Location = new Point(3, 5);
            previousMonth.Margin = new Padding(3, 2, 3, 2);
            previousMonth.Name = "previousMonth";
            previousMonth.Size = new Size(25, 22);
            previousMonth.TabIndex = 3;
            previousMonth.Text = "◄";
            previousMonth.UseVisualStyleBackColor = true;
            previousMonth.Click += previousMonth_Click;
            // 
            // nextMonth
            // 
            nextMonth.Location = new Point(230, 5);
            nextMonth.Margin = new Padding(3, 2, 3, 2);
            nextMonth.Name = "nextMonth";
            nextMonth.Size = new Size(25, 22);
            nextMonth.TabIndex = 4;
            nextMonth.Text = "►";
            nextMonth.UseVisualStyleBackColor = true;
            nextMonth.Click += nextMonth_Click;
            // 
            // selectedMonth
            // 
            selectedMonth.Location = new Point(34, 5);
            selectedMonth.Margin = new Padding(3, 2, 3, 2);
            selectedMonth.Name = "selectedMonth";
            selectedMonth.Size = new Size(192, 22);
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
            monthSelectPanel.Location = new Point(34, 31);
            monthSelectPanel.Margin = new Padding(3, 2, 3, 2);
            monthSelectPanel.Name = "monthSelectPanel";
            monthSelectPanel.Size = new Size(261, 228);
            monthSelectPanel.TabIndex = 0;
            monthSelectPanel.Visible = false;
            // 
            // yearBtn
            // 
            yearBtn.Enabled = false;
            yearBtn.Location = new Point(33, 3);
            yearBtn.Margin = new Padding(3, 2, 3, 2);
            yearBtn.Name = "yearBtn";
            yearBtn.Size = new Size(192, 22);
            yearBtn.TabIndex = 6;
            yearBtn.UseVisualStyleBackColor = true;
            // 
            // nextYearBtn
            // 
            nextYearBtn.Location = new Point(230, 3);
            nextYearBtn.Margin = new Padding(3, 2, 3, 2);
            nextYearBtn.Name = "nextYearBtn";
            nextYearBtn.Size = new Size(25, 22);
            nextYearBtn.TabIndex = 6;
            nextYearBtn.Text = "►";
            nextYearBtn.UseVisualStyleBackColor = true;
            nextYearBtn.Click += nextYearBtn_Click;
            // 
            // previousYearBtn
            // 
            previousYearBtn.Location = new Point(3, 3);
            previousYearBtn.Margin = new Padding(3, 2, 3, 2);
            previousYearBtn.Name = "previousYearBtn";
            previousYearBtn.Size = new Size(25, 22);
            previousYearBtn.TabIndex = 6;
            previousYearBtn.Text = "◄";
            previousYearBtn.UseVisualStyleBackColor = true;
            previousYearBtn.Click += previousYearBtn_Click;
            // 
            // CalendarUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(monthSelectPanel);
            Controls.Add(selectedMonth);
            Controls.Add(nextMonth);
            Controls.Add(previousMonth);
            Controls.Add(calendarDisplayPanel);
            Name = "CalendarUI";
            Size = new Size(1233, 639);
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
