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
        calendarTabPage = new TabPage();
        topToolStrip = new ToolStrip();
        createNewToDotoolStripButton = new ToolStripButton();
        toolStripSeparator1 = new ToolStripSeparator();
        SelectSavePath = new ToolStripButton();
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
        toolStripContainer.ContentPanel.Margin = new Padding(3, 4, 3, 4);
        toolStripContainer.ContentPanel.Size = new Size(914, 569);
        toolStripContainer.Dock = DockStyle.Fill;
        toolStripContainer.Location = new Point(0, 0);
        toolStripContainer.Margin = new Padding(3, 4, 3, 4);
        toolStripContainer.Name = "toolStripContainer";
        toolStripContainer.Size = new Size(914, 600);
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
        tabControl.Controls.Add(calendarTabPage);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Location = new Point(0, 0);
        tabControl.Margin = new Padding(3, 4, 3, 4);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(914, 569);
        tabControl.TabIndex = 0;
        // 
        // listTabPage
        // 
        listTabPage.Controls.Add(toDoList);
        listTabPage.Location = new Point(4, 29);
        listTabPage.Margin = new Padding(3, 4, 3, 4);
        listTabPage.Name = "listTabPage";
        listTabPage.Padding = new Padding(3, 4, 3, 4);
        listTabPage.Size = new Size(906, 536);
        listTabPage.TabIndex = 0;
        listTabPage.Text = "List";
        listTabPage.UseVisualStyleBackColor = true;
        // 
        // toDoList
        // 
        toDoList.AutoSize = true;
        toDoList.Dock = DockStyle.Fill;
        toDoList.Location = new Point(3, 4);
        toDoList.Margin = new Padding(1);
        toDoList.MinimumSize = new Size(320, 240);
        toDoList.Name = "toDoList";
        toDoList.Size = new Size(900, 528);
        toDoList.TabIndex = 0;
        // 
        // calendarTabPage
        // 
        calendarTabPage.Location = new Point(4, 29);
        calendarTabPage.Margin = new Padding(3, 4, 3, 4);
        calendarTabPage.Name = "calendarTabPage";
        calendarTabPage.Padding = new Padding(3, 4, 3, 4);
        calendarTabPage.Size = new Size(906, 526);
        calendarTabPage.TabIndex = 1;
        calendarTabPage.Text = "calendar";
        calendarTabPage.UseVisualStyleBackColor = true;
        // 
        // topToolStrip
        // 
        topToolStrip.Dock = DockStyle.None;
        topToolStrip.GripStyle = ToolStripGripStyle.Hidden;
        topToolStrip.ImageScalingSize = new Size(24, 24);
        topToolStrip.Items.AddRange(new ToolStripItem[] { createNewToDotoolStripButton, toolStripSeparator1, SelectSavePath });
        topToolStrip.Location = new Point(4, 0);
        topToolStrip.Name = "topToolStrip";
        topToolStrip.Size = new Size(317, 31);
        topToolStrip.TabIndex = 0;
        // 
        // createNewToDotoolStripButton
        // 
        createNewToDotoolStripButton.Image = (Image)resources.GetObject("createNewToDotoolStripButton.Image");
        createNewToDotoolStripButton.ImageTransparentColor = Color.Magenta;
        createNewToDotoolStripButton.Name = "createNewToDotoolStripButton";
        createNewToDotoolStripButton.Size = new Size(149, 28);
        createNewToDotoolStripButton.Text = "Create new Todo";
        createNewToDotoolStripButton.Click += createNewToDotoolStripButton_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(6, 31);
        // 
        // SelectSavePath
        // 
        SelectSavePath.DisplayStyle = ToolStripItemDisplayStyle.Text;
        SelectSavePath.Image = (Image)resources.GetObject("SelectSavePath.Image");
        SelectSavePath.ImageTransparentColor = Color.Magenta;
        SelectSavePath.Name = "SelectSavePath";
        SelectSavePath.Size = new Size(120, 28);
        SelectSavePath.Text = "Select save path";
        SelectSavePath.Click += SelectSavePath_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(914, 600);
        Controls.Add(toolStripContainer);
        Margin = new Padding(3, 4, 3, 4);
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
    private CalendarUI calendarUI;
    private ToDoListDisplay toDoList;
    private ToolStripContainer toolStripContainer;
    private ToolStrip topToolStrip;
    private TabControl tabControl;
    private TabPage listTabPage;
    private TabPage calendarTabPage;
    private ToolStripButton createNewToDotoolStripButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton SelectSavePath;
}
