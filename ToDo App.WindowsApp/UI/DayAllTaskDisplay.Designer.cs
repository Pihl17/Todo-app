namespace ToDo_App.UI
{
    partial class DayAllTaskDisplay
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
            closeBtn = new Button();
            taskDisplayPanel = new Panel();
            NewToDoBtn = new Button();
            taskDisplayPanel.SuspendLayout();
            SuspendLayout();
            // 
            // closeBtn
            // 
            closeBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeBtn.BackColor = Color.Firebrick;
            closeBtn.ForeColor = SystemColors.Control;
            closeBtn.Location = new Point(571, 3);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(29, 29);
            closeBtn.TabIndex = 0;
            closeBtn.Text = "X";
            closeBtn.UseVisualStyleBackColor = false;
            closeBtn.Click += closeBtn_Click;
            // 
            // taskDisplayPanel
            // 
            taskDisplayPanel.Controls.Add(closeBtn);
            taskDisplayPanel.Dock = DockStyle.Top;
            taskDisplayPanel.Location = new Point(0, 0);
            taskDisplayPanel.Name = "taskDisplayPanel";
            taskDisplayPanel.Size = new Size(603, 125);
            taskDisplayPanel.TabIndex = 1;
            // 
            // NewToDoBtn
            // 
            NewToDoBtn.Dock = DockStyle.Bottom;
            NewToDoBtn.Location = new Point(0, 412);
            NewToDoBtn.Name = "NewToDoBtn";
            NewToDoBtn.Size = new Size(603, 51);
            NewToDoBtn.TabIndex = 2;
            NewToDoBtn.Text = "Create new to do";
            NewToDoBtn.UseVisualStyleBackColor = true;
            // 
            // DayAllTaskDisplay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NewToDoBtn);
            Controls.Add(taskDisplayPanel);
            Name = "DayAllTaskDisplay";
            RightToLeft = RightToLeft.No;
            Size = new Size(603, 463);
            taskDisplayPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button closeBtn;
        private Panel taskDisplayPanel;
        private Button NewToDoBtn;
    }
}
