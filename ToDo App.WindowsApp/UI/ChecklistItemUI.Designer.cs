namespace ToDo_App.WindowsApp.UI
{
    partial class ChecklistItemUI
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
            ChecklistItemNameTB = new TextBox();
            ChecklistCB = new CheckBox();
            SuspendLayout();
            // 
            // ChecklistItemNameTB
            // 
            ChecklistItemNameTB.Dock = DockStyle.Fill;
            ChecklistItemNameTB.Location = new Point(18, 0);
            ChecklistItemNameTB.Name = "ChecklistItemNameTB";
            ChecklistItemNameTB.Size = new Size(461, 27);
            ChecklistItemNameTB.TabIndex = 1;
            ChecklistItemNameTB.KeyPress += ChecklistItemNameTB_KeyPress;
            ChecklistItemNameTB.Leave += ChecklistItemNameTB_Leave;
            // 
            // ChecklistCB
            // 
            ChecklistCB.AutoSize = true;
            ChecklistCB.Dock = DockStyle.Left;
            ChecklistCB.Location = new Point(0, 0);
            ChecklistCB.Name = "ChecklistCB";
            ChecklistCB.Size = new Size(18, 31);
            ChecklistCB.TabIndex = 2;
            ChecklistCB.UseVisualStyleBackColor = true;
            // 
            // ChecklistItem
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ChecklistItemNameTB);
            Controls.Add(ChecklistCB);
            Name = "ChecklistItem";
            Size = new Size(479, 31);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox ChecklistItemNameTB;
        private CheckBox ChecklistCB;
    }
}
