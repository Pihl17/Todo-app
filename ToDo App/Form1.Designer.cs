namespace ToDo_App
{
    partial class ToDoForm
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
            saveFileDialog1 = new SaveFileDialog();
            tableLayoutPanel1 = new TableLayoutPanel();
            toDoListLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            createToDoButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(toDoListLayoutPanel, 1, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // toDoListLayoutPanel
            // 
            toDoListLayoutPanel.AutoScroll = true;
            toDoListLayoutPanel.Dock = DockStyle.Fill;
            toDoListLayoutPanel.Location = new Point(116, 3);
            toDoListLayoutPanel.Name = "toDoListLayoutPanel";
            toDoListLayoutPanel.Size = new Size(681, 444);
            toDoListLayoutPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(createToDoButton);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(107, 444);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // createToDoButton
            // 
            createToDoButton.AutoSize = true;
            createToDoButton.Location = new Point(3, 3);
            createToDoButton.Name = "createToDoButton";
            createToDoButton.Size = new Size(101, 25);
            createToDoButton.TabIndex = 0;
            createToDoButton.Text = "New ToDo Note";
            createToDoButton.UseVisualStyleBackColor = true;
            createToDoButton.Click += createToDoButton_Click;
            // 
            // ToDoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "ToDoForm";
            Text = "ToDo App";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SaveFileDialog saveFileDialog1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button createToDoButton;
        private FlowLayoutPanel toDoListLayoutPanel;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
