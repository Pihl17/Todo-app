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
            panel1 = new Panel();
            previousMonth = new Button();
            nextMonth = new Button();
            selectedMonth = new Button();
            monthSelectPanel = new Panel();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(3, 43);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1402, 805);
            panel1.TabIndex = 2;
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
            selectedMonth.Location = new Point(38, 7);
            selectedMonth.Name = "selectedMonth";
            selectedMonth.Size = new Size(219, 29);
            selectedMonth.TabIndex = 5;
            selectedMonth.UseVisualStyleBackColor = true;
            selectedMonth.Click += selectedMonth_Click;
            // 
            // monthSelectPanel
            // 
            monthSelectPanel.BorderStyle = BorderStyle.FixedSingle;
            monthSelectPanel.Location = new Point(38, 42);
            monthSelectPanel.Name = "monthSelectPanel";
            monthSelectPanel.Size = new Size(400, 300);
            monthSelectPanel.TabIndex = 0;
            monthSelectPanel.Visible = false;
            // 
            // CalendarUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(monthSelectPanel);
            Controls.Add(selectedMonth);
            Controls.Add(nextMonth);
            Controls.Add(previousMonth);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "CalendarUI";
            Size = new Size(1409, 852);
            Load += CalendarUI_Load;
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Button previousMonth;
        private Button nextMonth;
        private Button selectedMonth;
        private Panel monthSelectPanel;
    }
}
