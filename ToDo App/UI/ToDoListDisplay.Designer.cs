namespace ToDo_App.UI;

partial class ToDoListDisplay
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
        listContainer = new FlowLayoutPanel();
        SuspendLayout();
        // 
        // listContainer
        // 
        listContainer.AutoScroll = true;
        listContainer.AutoSize = true;
        listContainer.Dock = DockStyle.Fill;
        listContainer.Location = new Point(0, 0);
        listContainer.Name = "listContainer";
        listContainer.Size = new Size(400, 300);
        listContainer.TabIndex = 0;
        // 
        // ToDoListDisplay
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        Controls.Add(listContainer);
        MinimumSize = new Size(400, 300);
        Name = "ToDoListDisplay";
        Size = new Size(400, 300);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private FlowLayoutPanel listContainer;
}
