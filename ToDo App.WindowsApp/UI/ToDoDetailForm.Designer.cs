namespace ToDo_App.UI
{
    partial class ToDoDetailForm
    {
       
        private System.ComponentModel.IContainer components = null;

       
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            table = new TableLayoutPanel();
            idCaption = new Label();
            idLabel = new Label();
            titleCaption = new Label();
            titleTextBox = new TextBox();
            descriptionCaption = new Label();
            descriptionTextBox = new TextBox();
            createdCaption = new Label();
            creationDateLabel = new Label();
            completedCaption = new Label();
            completionDateLabel = new Label();
            deadlineCaption = new Label();
            deadlinePicker = new DateTimePicker();
            priorityCaption = new Label();
            priorityNumberBox = new NumericUpDown();
            statusCaption = new Label();
            statusComboBox = new ComboBox();
            repeatCaption = new Label();
            repeatComboBox = new ComboBox();
            buttonPanel = new FlowLayoutPanel();
            cancelButton = new Button();
            saveButton = new Button();
            table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)priorityNumberBox).BeginInit();
            SuspendLayout();
            // 
            // table
            // 
            table.ColumnCount = 2;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            table.Controls.Add(idCaption, 0, 0);
            table.Controls.Add(idLabel, 1, 0);
            table.Controls.Add(titleCaption, 0, 1);
            table.Controls.Add(titleTextBox, 1, 1);
            table.Controls.Add(descriptionCaption, 0, 2);
            table.Controls.Add(descriptionTextBox, 1, 2);
            table.Controls.Add(createdCaption, 0, 3);
            table.Controls.Add(creationDateLabel, 1, 3);
            table.Controls.Add(completedCaption, 0, 4);
            table.Controls.Add(completionDateLabel, 1, 4);
            table.Controls.Add(deadlineCaption, 0, 5);
            table.Controls.Add(deadlinePicker, 1, 5);
            table.Controls.Add(priorityCaption, 0, 6);
            table.Controls.Add(priorityNumberBox, 1, 6);
            table.Controls.Add(statusCaption, 0, 7);
            table.Controls.Add(repeatCaption, 0, 8);
            table.Controls.Add(repeatComboBox, 1, 8);
            table.Controls.Add(buttonPanel, 0, 9);
            table.Controls.Add(statusComboBox, 1, 7);
            table.Dock = DockStyle.Fill;
            table.Location = new Point(0, 0);
            table.Name = "table";
            table.Padding = new Padding(12);
            table.RowCount = 10;
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.RowStyles.Add(new RowStyle());
            table.Size = new Size(480, 440);
            table.TabIndex = 0;
            // 
            // idCaption
            // 
            idCaption.Anchor = AnchorStyles.Left;
            idCaption.AutoSize = true;
            idCaption.Location = new Point(15, 12);
            idCaption.Name = "idCaption";
            idCaption.Size = new Size(20, 15);
            idCaption.TabIndex = 0;
            idCaption.Text = "id:";
            // 
            // idLabel
            // 
            idLabel.Anchor = AnchorStyles.Left;
            idLabel.AutoSize = true;
            idLabel.Location = new Point(135, 12);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(0, 15);
            idLabel.TabIndex = 1;
            // 
            // titleCaption
            // 
            titleCaption.Anchor = AnchorStyles.Left;
            titleCaption.AutoSize = true;
            titleCaption.Location = new Point(15, 34);
            titleCaption.Name = "titleCaption";
            titleCaption.Size = new Size(33, 15);
            titleCaption.TabIndex = 2;
            titleCaption.Text = "Title:";
            // 
            // titleTextBox
            // 
            titleTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            titleTextBox.Location = new Point(135, 30);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(330, 23);
            titleTextBox.TabIndex = 3;
            // 
            // descriptionCaption
            // 
            descriptionCaption.Anchor = AnchorStyles.Left;
            descriptionCaption.AutoSize = true;
            descriptionCaption.Location = new Point(15, 91);
            descriptionCaption.Name = "descriptionCaption";
            descriptionCaption.Size = new Size(70, 15);
            descriptionCaption.TabIndex = 4;
            descriptionCaption.Text = "Description:";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            descriptionTextBox.Location = new Point(135, 59);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.Size = new Size(330, 80);
            descriptionTextBox.TabIndex = 5;
            // 
            // createdCaption
            // 
            createdCaption.Anchor = AnchorStyles.Left;
            createdCaption.AutoSize = true;
            createdCaption.Location = new Point(15, 142);
            createdCaption.Name = "createdCaption";
            createdCaption.Size = new Size(51, 15);
            createdCaption.TabIndex = 6;
            createdCaption.Text = "Created:";
            // 
            // creationDateLabel
            // 
            creationDateLabel.Anchor = AnchorStyles.Left;
            creationDateLabel.AutoSize = true;
            creationDateLabel.Location = new Point(135, 142);
            creationDateLabel.Name = "creationDateLabel";
            creationDateLabel.Size = new Size(0, 15);
            creationDateLabel.TabIndex = 7;
            // 
            // completedCaption
            // 
            completedCaption.Anchor = AnchorStyles.Left;
            completedCaption.AutoSize = true;
            completedCaption.Location = new Point(15, 157);
            completedCaption.Name = "completedCaption";
            completedCaption.Size = new Size(69, 15);
            completedCaption.TabIndex = 8;
            completedCaption.Text = "Completed:";
            // 
            // completionDateLabel
            // 
            completionDateLabel.Anchor = AnchorStyles.Left;
            completionDateLabel.AutoSize = true;
            completionDateLabel.Location = new Point(135, 157);
            completionDateLabel.Name = "completionDateLabel";
            completionDateLabel.Size = new Size(0, 15);
            completionDateLabel.TabIndex = 9;
            // 
            // deadlineCaption
            // 
            deadlineCaption.Anchor = AnchorStyles.Left;
            deadlineCaption.AutoSize = true;
            deadlineCaption.Location = new Point(15, 179);
            deadlineCaption.Name = "deadlineCaption";
            deadlineCaption.Size = new Size(56, 15);
            deadlineCaption.TabIndex = 10;
            deadlineCaption.Text = "Deadline:";
            // 
            // deadlinePicker
            // 
            deadlinePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            deadlinePicker.Location = new Point(135, 175);
            deadlinePicker.Name = "deadlinePicker";
            deadlinePicker.ShowCheckBox = true;
            deadlinePicker.Size = new Size(330, 23);
            deadlinePicker.TabIndex = 11;
            // 
            // priorityCaption
            // 
            priorityCaption.Anchor = AnchorStyles.Left;
            priorityCaption.AutoSize = true;
            priorityCaption.Location = new Point(15, 208);
            priorityCaption.Name = "priorityCaption";
            priorityCaption.Size = new Size(48, 15);
            priorityCaption.TabIndex = 12;
            priorityCaption.Text = "Priority:";
            // 
            // priorityNumberBox
            // 
            priorityNumberBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            priorityNumberBox.Location = new Point(135, 204);
            priorityNumberBox.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            priorityNumberBox.Name = "priorityNumberBox";
            priorityNumberBox.Size = new Size(330, 23);
            priorityNumberBox.TabIndex = 13;
            // 
            // statusCaption
            // 
            statusCaption.Anchor = AnchorStyles.Left;
            statusCaption.AutoSize = true;
            statusCaption.Location = new Point(15, 237);
            statusCaption.Name = "statusCaption";
            statusCaption.Size = new Size(42, 15);
            statusCaption.TabIndex = 14;
            statusCaption.Text = "Status:";
            // 
            // statusComboBox
            // 
            statusComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusComboBox.Location = new Point(135, 233);
            statusComboBox.Name = "statusComboBox";
            statusComboBox.Size = new Size(330, 23);
            statusComboBox.TabIndex = 15;
            // 
            // repeatCaption
            // 
            repeatCaption.Anchor = AnchorStyles.Left;
            repeatCaption.AutoSize = true;
            repeatCaption.Location = new Point(15, 266);
            repeatCaption.Name = "repeatCaption";
            repeatCaption.Size = new Size(46, 15);
            repeatCaption.TabIndex = 16;
            repeatCaption.Text = "Repeat:";
            // 
            // repeatComboBox
            // 
            repeatComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            repeatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            repeatComboBox.Location = new Point(135, 262);
            repeatComboBox.Name = "repeatComboBox";
            repeatComboBox.Size = new Size(330, 23);
            repeatComboBox.TabIndex = 17;
            // 
            // buttonPanel
            // 
            buttonPanel.Location = new Point(15, 291);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(114, 100);
            buttonPanel.TabIndex = 18;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(0, 0);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(0, 0);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 0;
            saveButton.Text = "Save";
            saveButton.Click += saveButton_Click;
            // 
            // ToDoDetailForm
            // 
            AcceptButton = saveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(480, 440);
            Controls.Add(table);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ToDoDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ToDo details";
            table.ResumeLayout(false);
            table.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)priorityNumberBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label idCaption;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label titleCaption;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label descriptionCaption;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label createdCaption;
        private System.Windows.Forms.Label creationDateLabel;
        private System.Windows.Forms.Label completedCaption;
        private System.Windows.Forms.Label completionDateLabel;
        private System.Windows.Forms.Label deadlineCaption;
        private System.Windows.Forms.DateTimePicker deadlinePicker;
        private System.Windows.Forms.Label priorityCaption;
        private System.Windows.Forms.NumericUpDown priorityNumberBox;
        private System.Windows.Forms.Label statusCaption;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Label repeatCaption;
        private System.Windows.Forms.ComboBox repeatComboBox;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;

    }
}