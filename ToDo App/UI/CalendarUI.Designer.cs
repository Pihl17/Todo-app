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
            monthCalendar1 = new MonthCalendar();
            comboBox1 = new ComboBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(monthCalendar1);
            panel1.Location = new Point(3, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(1227, 604);
            panel1.TabIndex = 2;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(274, 378);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 3;
            monthCalendar1.DateChanged += this.monthCalendar1_DateChanged;
            // 
            // comboBox1
            // 
            comboBox1.FlatStyle = FlatStyle.System;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(85, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 3;
            // 
            // CalendarUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(comboBox1);
            Controls.Add(panel1);
            Name = "CalendarUI";
            Size = new Size(1233, 639);
            Load += CalendarUI_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private MonthCalendar monthCalendar1;
        private ComboBox comboBox1;
    }
}
