namespace ToDo_App
{
    partial class ToDoNoteUserControl
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
            tableLayoutPanel.Location = new Point(2, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(294, 94);
            tableLayoutPanel.TabIndex = 0;
            // 
            // toDoTitle
            // 
            toDoTitle.AutoSize = true;
            toDoTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            toDoTitle.Location = new Point(3, 3);
            toDoTitle.Margin = new Padding(3);
            toDoTitle.Name = "toDoTitle";
            toDoTitle.Size = new Size(50, 25);
            toDoTitle.TabIndex = 0;
            toDoTitle.Text = "Title";
            // 
            // toDoDescription
            // 
            toDoDescription.AutoSize = true;
            toDoDescription.Location = new Point(5, 36);
            toDoDescription.Margin = new Padding(5);
            toDoDescription.Name = "toDoDescription";
            toDoDescription.Size = new Size(67, 15);
            toDoDescription.TabIndex = 1;
            toDoDescription.Text = "Description";
            // 
            // ToDoNoteUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(tableLayoutPanel);
            MaximumSize = new Size(500, 0);
            MinimumSize = new Size(300, 100);
            Name = "ToDoNoteUserControl";
            Padding = new Padding(2);
            Size = new Size(298, 98);
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
}
