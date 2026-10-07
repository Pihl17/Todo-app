namespace ToDo_App.UI;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        toolStripContainer = new ToolStripContainer();
        tabControl = new TabControl();
        listTabPage = new TabPage();
        toDoList = new ToDoListDisplay();
        topToolStrip = new ToolStrip();
        createNewToDotoolStripButton = new ToolStripButton();
        toolStripSeparator1 = new ToolStripSeparator();
        todosToolStripDropDown = new ToolStripDropDownButton();
        createNewTodoToolStripMenuItem = new ToolStripMenuItem();
        toolStripContainer.ContentPanel.SuspendLayout();
        toolStripContainer.TopToolStripPanel.SuspendLayout();
        toolStripContainer.SuspendLayout();
        tabControl.SuspendLayout();
        listTabPage.SuspendLayout();
        topToolStrip.SuspendLayout();
        SuspendLayout();
        // 
        // toolStripContainer
        // 
        // 
        // toolStripContainer.ContentPanel
        // 
        toolStripContainer.ContentPanel.Controls.Add(tabControl);
        toolStripContainer.ContentPanel.Size = new Size(800, 419);
        toolStripContainer.Dock = DockStyle.Fill;
        toolStripContainer.Location = new Point(0, 0);
        toolStripContainer.Name = "toolStripContainer";
        toolStripContainer.Size = new Size(800, 450);
        toolStripContainer.TabIndex = 0;
        toolStripContainer.Text = "toolStripContainer1";
        // 
        // toolStripContainer.TopToolStripPanel
        // 
        toolStripContainer.TopToolStripPanel.Controls.Add(topToolStrip);
        // 
        // tabControl
        // 
        tabControl.Controls.Add(listTabPage);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Location = new Point(0, 0);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(800, 419);
        tabControl.TabIndex = 0;
        // 
        // listTabPage
        // 
        listTabPage.Controls.Add(toDoList);
        listTabPage.Location = new Point(4, 24);
        listTabPage.Name = "listTabPage";
        listTabPage.Padding = new Padding(3, 3, 3, 3);
        listTabPage.Size = new Size(792, 391);
        listTabPage.TabIndex = 0;
        listTabPage.Text = "List";
        listTabPage.UseVisualStyleBackColor = true;
        // 
        // toDoList
        // 
        toDoList.AutoSize = true;
        toDoList.Dock = DockStyle.Fill;
        toDoList.Location = new Point(3, 3);
        toDoList.Margin = new Padding(1, 1, 1, 1);
        toDoList.MinimumSize = new Size(280, 180);
        toDoList.Name = "toDoList";
        toDoList.Size = new Size(786, 385);
        toDoList.TabIndex = 0;
        // 
        // topToolStrip
        // 
        topToolStrip.Dock = DockStyle.None;
        topToolStrip.GripStyle = ToolStripGripStyle.Hidden;
        topToolStrip.ImageScalingSize = new Size(24, 24);
        topToolStrip.Items.AddRange(new ToolStripItem[] { createNewToDotoolStripButton, toolStripSeparator1, todosToolStripDropDown });
        topToolStrip.Location = new Point(4, 0);
        topToolStrip.Name = "topToolStrip";
        topToolStrip.Size = new Size(120, 31);
        topToolStrip.TabIndex = 0;
        // 
        // createNewToDotoolStripButton
        // 
        createNewToDotoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        createNewToDotoolStripButton.Image = (Image)resources.GetObject("createNewToDotoolStripButton.Image");
        createNewToDotoolStripButton.ImageTransparentColor = Color.Magenta;
        createNewToDotoolStripButton.Name = "createNewToDotoolStripButton";
        createNewToDotoolStripButton.Size = new Size(28, 28);
        createNewToDotoolStripButton.Text = "Create new Todo";
        createNewToDotoolStripButton.Click += createNewToDotoolStripButton_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(6, 31);
        // 
        // todosToolStripDropDown
        // 
        todosToolStripDropDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
        todosToolStripDropDown.DropDownItems.AddRange(new ToolStripItem[] { createNewTodoToolStripMenuItem });
        todosToolStripDropDown.Image = (Image)resources.GetObject("todosToolStripDropDown.Image");
        todosToolStripDropDown.ImageTransparentColor = Color.Magenta;
        todosToolStripDropDown.Name = "todosToolStripDropDown";
        todosToolStripDropDown.Size = new Size(52, 28);
        todosToolStripDropDown.Text = "Todos";
        // 
        // createNewTodoToolStripMenuItem
        // 
        createNewTodoToolStripMenuItem.Name = "createNewTodoToolStripMenuItem";
        createNewTodoToolStripMenuItem.Size = new Size(180, 22);
        createNewTodoToolStripMenuItem.Text = "Create new Todo";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(toolStripContainer);
        Name = "MainForm";
        Text = "ToDo App";
        toolStripContainer.ContentPanel.ResumeLayout(false);
        toolStripContainer.TopToolStripPanel.ResumeLayout(false);
        toolStripContainer.TopToolStripPanel.PerformLayout();
        toolStripContainer.ResumeLayout(false);
        toolStripContainer.PerformLayout();
        tabControl.ResumeLayout(false);
        listTabPage.ResumeLayout(false);
        listTabPage.PerformLayout();
        topToolStrip.ResumeLayout(false);
        topToolStrip.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private ToDoListDisplay toDoList;
    private ToolStripContainer toolStripContainer;
    private ToolStrip topToolStrip;
    private ToolStripDropDownButton todosToolStripDropDown;
    private ToolStripMenuItem createNewTodoToolStripMenuItem;
    private TabControl tabControl;
    private TabPage listTabPage;
    private ToolStripButton createNewToDotoolStripButton;
    private ToolStripSeparator toolStripSeparator1;
}
