namespace ToDo_App.UI;

partial class ToDoDisplay
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
        tableLayoutPanel = new TableLayoutPanel();
        toDoTitle = new Label();
        toDoDescription = new Label();
        tableLayoutPanel.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel
        // 
        tableLayoutPanel.AutoSize = true;
        tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel.ColumnCount = 1;
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel.Controls.Add(toDoTitle, 0, 0);
        tableLayoutPanel.Controls.Add(toDoDescription, 0, 1);
        tableLayoutPanel.Dock = DockStyle.Fill;
        tableLayoutPanel.Location = new Point(3, 3);
        tableLayoutPanel.Margin = new Padding(4, 5, 4, 5);
        tableLayoutPanel.Name = "tableLayoutPanel";
        tableLayoutPanel.RowCount = 2;
        tableLayoutPanel.RowStyles.Add(new RowStyle());
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel.Size = new Size(392, 142);
        tableLayoutPanel.TabIndex = 0;
        // 
        // toDoTitle
        // 
        toDoTitle.AutoSize = true;
        toDoTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        toDoTitle.Location = new Point(4, 5);
        toDoTitle.Margin = new Padding(4, 5, 4, 5);
        toDoTitle.Name = "toDoTitle";
        toDoTitle.Size = new Size(75, 38);
        toDoTitle.TabIndex = 0;
        toDoTitle.Text = "Title";
        // 
        // toDoDescription
        // 
        toDoDescription.AutoSize = true;
        toDoDescription.Location = new Point(7, 56);
        toDoDescription.Margin = new Padding(7, 8, 7, 8);
        toDoDescription.Name = "toDoDescription";
        toDoDescription.Size = new Size(102, 25);
        toDoDescription.TabIndex = 1;
        toDoDescription.Text = "Description";
        // 
        // ToDoDisplay
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(tableLayoutPanel);
        Margin = new Padding(4, 5, 4, 5);
        MaximumSize = new Size(713, 2);
        MinimumSize = new Size(400, 150);
        Name = "ToDoDisplay";
        Padding = new Padding(3, 3, 3, 3);
        Size = new Size(398, 148);
        tableLayoutPanel.ResumeLayout(false);
        tableLayoutPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel;
    private Label toDoTitle;
    private Label toDoDescription;
}
