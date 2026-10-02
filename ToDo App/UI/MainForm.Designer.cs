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
        tableLayoutPanel1 = new TableLayoutPanel();
        flowLayoutPanel1 = new FlowLayoutPanel();
        createToDoButton = new Button();
        toDoList = new ToDoListDisplay();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
        tableLayoutPanel1.Controls.Add(toDoList, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1143, 750);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.AutoSize = true;
        flowLayoutPanel1.Controls.Add(createToDoButton);
        flowLayoutPanel1.Dock = DockStyle.Left;
        flowLayoutPanel1.Location = new Point(4, 5);
        flowLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(219, 740);
        flowLayoutPanel1.TabIndex = 2;
        // 
        // createToDoButton
        // 
        createToDoButton.AutoSize = true;
        createToDoButton.Location = new Point(4, 5);
        createToDoButton.Margin = new Padding(4, 5, 4, 5);
        createToDoButton.Name = "createToDoButton";
        createToDoButton.Size = new Size(211, 58);
        createToDoButton.TabIndex = 0;
        createToDoButton.Text = "New ToDo Note";
        createToDoButton.UseVisualStyleBackColor = true;
        createToDoButton.Click += createToDoButton_Click;
        // 
        // toDoList
        // 
        toDoList.AutoSize = true;
        toDoList.Dock = DockStyle.Fill;
        toDoList.Location = new Point(230, 3);
        toDoList.MinimumSize = new Size(400, 300);
        toDoList.Name = "toDoList";
        toDoList.Size = new Size(910, 744);
        toDoList.TabIndex = 3;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1143, 750);
        Controls.Add(tableLayoutPanel1);
        Margin = new Padding(4, 5, 4, 5);
        Name = "MainForm";
        Text = "ToDo App";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private TableLayoutPanel tableLayoutPanel1;
    private Button createToDoButton;
    private FlowLayoutPanel flowLayoutPanel1;
    private ToDoListDisplay toDoList;
}
