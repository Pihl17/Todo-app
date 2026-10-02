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

    public void AddToList(ToDoDisplay todo)
    {
        listContainer.Controls.Add(todo);
    }

    public void RemoveFromList(ToDoDisplay todo)
    {
        throw new NotImplementedException();
    }
}
