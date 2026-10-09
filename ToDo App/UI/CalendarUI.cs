using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToDo_App.UI
{
    public partial class CalendarUI : UserControl
    {
        public CalendarUI()
        {
            InitializeComponent();

            selectedMonth.Text = DateTime.Now.ToString("MMMM yyyy");
        }

        private void CalendarUI_Load(object sender, EventArgs e)
        {

        }

        private void selectedMonth_Click(object sender, EventArgs e)
        {
        }

        private void previousMonth_Click(object sender, EventArgs e)
        {

        }

        private void nextMonth_Click(object sender, EventArgs e)
        {

        }
    }
}
