namespace SourceCode.Tools.Management.Forms
{
    partial class StartNewProcessForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartNewProcessForm));
            this.startOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.folioTextBox = new System.Windows.Forms.TextBox();
            this.asynchronousCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.executionTypeLabel = new System.Windows.Forms.Label();
            this.dataFieldsGroupBox = new System.Windows.Forms.GroupBox();
            this.dataFieldsScrollableControl = new System.Windows.Forms.ScrollableControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.instanceToolStrip = new System.Windows.Forms.ToolStrip();
            this.instanceRefreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.startOptionsGroupBox.SuspendLayout();
            this.dataFieldsGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.instanceToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // startOptionsGroupBox
            // 
            this.startOptionsGroupBox.Controls.Add(this.folioTextBox);
            this.startOptionsGroupBox.Controls.Add(this.asynchronousCheckBox);
            this.startOptionsGroupBox.Controls.Add(this.label2);
            this.startOptionsGroupBox.Controls.Add(this.executionTypeLabel);
            this.startOptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.startOptionsGroupBox.Location = new System.Drawing.Point(0, 25);
            this.startOptionsGroupBox.Name = "startOptionsGroupBox";
            this.startOptionsGroupBox.Size = new System.Drawing.Size(493, 83);
            this.startOptionsGroupBox.TabIndex = 0;
            this.startOptionsGroupBox.TabStop = false;
            this.startOptionsGroupBox.Text = "Start Options";
            // 
            // folioTextBox
            // 
            this.folioTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.folioTextBox.Location = new System.Drawing.Point(104, 48);
            this.folioTextBox.Name = "folioTextBox";
            this.folioTextBox.Size = new System.Drawing.Size(383, 20);
            this.folioTextBox.TabIndex = 3;
            // 
            // asynchronousCheckBox
            // 
            this.asynchronousCheckBox.AutoSize = true;
            this.asynchronousCheckBox.Checked = true;
            this.asynchronousCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.asynchronousCheckBox.Location = new System.Drawing.Point(104, 20);
            this.asynchronousCheckBox.Name = "asynchronousCheckBox";
            this.asynchronousCheckBox.Size = new System.Drawing.Size(93, 17);
            this.asynchronousCheckBox.TabIndex = 2;
            this.asynchronousCheckBox.Text = "Asynchronous";
            this.asynchronousCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Folio:";
            // 
            // executionTypeLabel
            // 
            this.executionTypeLabel.AutoSize = true;
            this.executionTypeLabel.Location = new System.Drawing.Point(13, 20);
            this.executionTypeLabel.Name = "executionTypeLabel";
            this.executionTypeLabel.Size = new System.Drawing.Size(84, 13);
            this.executionTypeLabel.TabIndex = 0;
            this.executionTypeLabel.Text = "Execution Type:";
            // 
            // dataFieldsGroupBox
            // 
            this.dataFieldsGroupBox.Controls.Add(this.dataFieldsScrollableControl);
            this.dataFieldsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataFieldsGroupBox.Location = new System.Drawing.Point(0, 108);
            this.dataFieldsGroupBox.Name = "dataFieldsGroupBox";
            this.dataFieldsGroupBox.Size = new System.Drawing.Size(493, 269);
            this.dataFieldsGroupBox.TabIndex = 1;
            this.dataFieldsGroupBox.TabStop = false;
            this.dataFieldsGroupBox.Text = "Data and XML Fields";
            // 
            // dataFieldsScrollableControl
            // 
            this.dataFieldsScrollableControl.AutoScroll = true;
            this.dataFieldsScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataFieldsScrollableControl.Location = new System.Drawing.Point(3, 16);
            this.dataFieldsScrollableControl.Name = "dataFieldsScrollableControl";
            this.dataFieldsScrollableControl.Size = new System.Drawing.Size(487, 250);
            this.dataFieldsScrollableControl.TabIndex = 2;
            this.dataFieldsScrollableControl.Text = "dataFieldsScrollableControl";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 377);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 50);
            this.panel1.TabIndex = 4;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(325, 11);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 27);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(406, 11);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 27);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // instanceToolStrip
            // 
            this.instanceToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instanceRefreshToolStripButton});
            this.instanceToolStrip.Location = new System.Drawing.Point(0, 0);
            this.instanceToolStrip.Name = "instanceToolStrip";
            this.instanceToolStrip.Size = new System.Drawing.Size(493, 25);
            this.instanceToolStrip.TabIndex = 5;
            this.instanceToolStrip.Text = "toolStrip1";
            // 
            // instanceRefreshToolStripButton
            // 
            this.instanceRefreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("instanceRefreshToolStripButton.Image")));
            this.instanceRefreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.instanceRefreshToolStripButton.Name = "instanceRefreshToolStripButton";
            this.instanceRefreshToolStripButton.Size = new System.Drawing.Size(66, 22);
            this.instanceRefreshToolStripButton.Text = "Refresh";
            this.instanceRefreshToolStripButton.Click += new System.EventHandler(this.instanceRefreshToolStripButton_Click);
            // 
            // StartNewProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 427);
            this.Controls.Add(this.dataFieldsGroupBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.startOptionsGroupBox);
            this.Controls.Add(this.instanceToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartNewProcessForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Start New Process";
            this.startOptionsGroupBox.ResumeLayout(false);
            this.startOptionsGroupBox.PerformLayout();
            this.dataFieldsGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.instanceToolStrip.ResumeLayout(false);
            this.instanceToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox startOptionsGroupBox;
        private System.Windows.Forms.TextBox folioTextBox;
        private System.Windows.Forms.CheckBox asynchronousCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label executionTypeLabel;
        private System.Windows.Forms.GroupBox dataFieldsGroupBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ScrollableControl dataFieldsScrollableControl;
        private System.Windows.Forms.ToolStrip instanceToolStrip;
        private System.Windows.Forms.ToolStripButton instanceRefreshToolStripButton;
    }
}