using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToDo_App.UI;

public partial class ToDoListDisplay : UserControl
{
    public ToDoListDisplay()
    {
        InitializeComponent();
    }

    public void AddToList(UserControl control)
    {
        listContainer.Controls.Add(control);
    }

    public void RemoveFromList(UserControl control)
    {
        throw new NotImplementedException();
    }
}
