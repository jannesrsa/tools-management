namespace SourceCode.Tools.Management
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStripButton connectionPropertiesToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem connectionPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripMenuItem syncFromServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton syncFromServerToolStripButton;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
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
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Workflows");
            this.workflowsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteWorkflowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRecentServers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncFromServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.timingMessageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssGapFiller = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssServer = new System.Windows.Forms.ToolStripDropDownButton();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.connectionPropertiesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.syncFromServerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mainTreeView = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.procSetContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteProcessFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workflowsContextMenuStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.procSetContextMenuStrip.SuspendLayout();
            this.processFolderContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // workflowsContextMenuStrip
            // 
            this.workflowsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteWorkflowsToolStripMenuItem});
            this.workflowsContextMenuStrip.Name = "cmsCreate";
            this.workflowsContextMenuStrip.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteWorkflowsToolStripMenuItem
            // 
            this.deleteWorkflowsToolStripMenuItem.Name = "deleteWorkflowsToolStripMenuItem";
            this.deleteWorkflowsToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteWorkflowsToolStripMenuItem.Text = "&Delete";
            this.deleteWorkflowsToolStripMenuItem.Click += new System.EventHandler(this.deleteWorkflowsToolStripMenuItem_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(812, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRecentServers,
            this.toolStripSeparator5,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // tsmRecentServers
            // 
            this.tsmRecentServers.Image = ((System.Drawing.Image)(resources.GetObject("tsmRecentServers.Image")));
            this.tsmRecentServers.Name = "tsmRecentServers";
            this.tsmRecentServers.Size = new System.Drawing.Size(150, 22);
            this.tsmRecentServers.Text = "Recent Servers";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(147, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripMenuItem.Image")));
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionPropertiesToolStripMenuItem,
            this.syncFromServerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // connectionPropertiesToolStripMenuItem
            // 
            this.connectionPropertiesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("connectionPropertiesToolStripMenuItem.Image")));
            this.connectionPropertiesToolStripMenuItem.Name = "connectionPropertiesToolStripMenuItem";
            this.connectionPropertiesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.connectionPropertiesToolStripMenuItem.Text = "Connection Properties";
            this.connectionPropertiesToolStripMenuItem.Click += new System.EventHandler(this.connectionPropertiesToolStripButton_Click);
            // 
            // syncFromServerToolStripMenuItem
            // 
            this.syncFromServerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("syncFromServerToolStripMenuItem.Image")));
            this.syncFromServerToolStripMenuItem.Name = "syncFromServerToolStripMenuItem";
            this.syncFromServerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.syncFromServerToolStripMenuItem.Text = "Sync From Server";
            this.syncFromServerToolStripMenuItem.Click += new System.EventHandler(this.syncFromServerToolStripButton_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timingMessageToolStripStatusLabel,
            this.tssGapFiller,
            this.tssServer});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 661);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(812, 22);
            this.mainStatusStrip.TabIndex = 1;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // timingMessageToolStripStatusLabel
            // 
            this.timingMessageToolStripStatusLabel.Name = "timingMessageToolStripStatusLabel";
            this.timingMessageToolStripStatusLabel.Size = new System.Drawing.Size(142, 17);
            this.timingMessageToolStripStatusLabel.Text = "                                             ";
            // 
            // tssGapFiller
            // 
            this.tssGapFiller.Name = "tssGapFiller";
            this.tssGapFiller.Size = new System.Drawing.Size(554, 17);
            this.tssGapFiller.Spring = true;
            // 
            // tssServer
            // 
            this.tssServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssServer.Image = ((System.Drawing.Image)(resources.GetObject("tssServer.Image")));
            this.tssServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssServer.Name = "tssServer";
            this.tssServer.Size = new System.Drawing.Size(101, 20);
            this.tssServer.Text = "Not Connected";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionPropertiesToolStripButton,
            this.toolStripSeparator2,
            this.syncFromServerToolStripButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(812, 25);
            this.mainToolStrip.TabIndex = 3;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // connectionPropertiesToolStripButton
            // 
            this.connectionPropertiesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("connectionPropertiesToolStripButton.Image")));
            this.connectionPropertiesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectionPropertiesToolStripButton.Name = "connectionPropertiesToolStripButton";
            this.connectionPropertiesToolStripButton.Size = new System.Drawing.Size(140, 22);
            this.connectionPropertiesToolStripButton.Text = "K2 Server Connection";
            this.connectionPropertiesToolStripButton.ToolTipText = "Connection Properties";
            this.connectionPropertiesToolStripButton.Click += new System.EventHandler(this.connectionPropertiesToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // syncFromServerToolStripButton
            // 
            this.syncFromServerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("syncFromServerToolStripButton.Image")));
            this.syncFromServerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.syncFromServerToolStripButton.Name = "syncFromServerToolStripButton";
            this.syncFromServerToolStripButton.Size = new System.Drawing.Size(118, 22);
            this.syncFromServerToolStripButton.Text = "Sync From Server";
            this.syncFromServerToolStripButton.ToolTipText = "Refresh";
            this.syncFromServerToolStripButton.Click += new System.EventHandler(this.syncFromServerToolStripButton_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FolderClose");
            this.imageList1.Images.SetKeyName(1, "Service");
            this.imageList1.Images.SetKeyName(2, "Image3");
            this.imageList1.Images.SetKeyName(3, "Directory");
            this.imageList1.Images.SetKeyName(4, "Image4");
            this.imageList1.Images.SetKeyName(5, "Package");
            this.imageList1.Images.SetKeyName(6, "Image1");
            this.imageList1.Images.SetKeyName(7, "SPIntegration");
            this.imageList1.Images.SetKeyName(8, "Tenant");
            this.imageList1.Images.SetKeyName(9, "K2Instance");
            this.imageList1.Images.SetKeyName(10, "Portal");
            this.imageList1.Images.SetKeyName(11, "Server");
            this.imageList1.Images.SetKeyName(12, "DeploymentInputs");
            this.imageList1.Images.SetKeyName(13, "K2Process");
            this.imageList1.Images.SetKeyName(14, "K2Activity");
            this.imageList1.Images.SetKeyName(15, "DeploymentInputValues");
            this.imageList1.Images.SetKeyName(16, "ProcessInstance");
            this.imageList1.Images.SetKeyName(17, "Process");
            this.imageList1.Images.SetKeyName(18, "ProcSet");
            this.imageList1.Images.SetKeyName(19, "ProcessFolder");
            // 
            // mainTreeView
            // 
            this.mainTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainTreeView.ImageIndex = 0;
            this.mainTreeView.ImageList = this.imageList1;
            this.mainTreeView.Location = new System.Drawing.Point(0, 49);
            this.mainTreeView.Name = "mainTreeView";
            treeNode1.ContextMenuStrip = this.workflowsContextMenuStrip;
            treeNode1.Name = "WorkflowsTreeNode";
            treeNode1.Text = "Workflows";
            this.mainTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.mainTreeView.SelectedImageIndex = 0;
            this.mainTreeView.Size = new System.Drawing.Size(248, 612);
            this.mainTreeView.TabIndex = 4;
            this.mainTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.mainTreeView_NodeMouseClick);
            this.mainTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainTreeView_MouseDown);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(248, 49);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 612);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // procSetContextMenuStrip
            // 
            this.procSetContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteProcessToolStripMenuItem});
            this.procSetContextMenuStrip.Name = "cmsCreate";
            this.procSetContextMenuStrip.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteProcessToolStripMenuItem
            // 
            this.deleteProcessToolStripMenuItem.Name = "deleteProcessToolStripMenuItem";
            this.deleteProcessToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteProcessToolStripMenuItem.Text = "&Delete";
            this.deleteProcessToolStripMenuItem.Click += new System.EventHandler(this.deleteProcessToolStripMenuItem_Click);
            // 
            // processFolderContextMenuStrip
            // 
            this.processFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteProcessFolderToolStripMenuItem});
            this.processFolderContextMenuStrip.Name = "cmsCreate";
            this.processFolderContextMenuStrip.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteProcessFolderToolStripMenuItem
            // 
            this.deleteProcessFolderToolStripMenuItem.Name = "deleteProcessFolderToolStripMenuItem";
            this.deleteProcessFolderToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteProcessFolderToolStripMenuItem.Text = "&Delete";
            this.deleteProcessFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteProcessFolderToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 683);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.mainTreeView);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "K2 Management Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MdiChildActivate += new System.EventHandler(this.MainForm_MdiChildActivate);
            this.workflowsContextMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.procSetContextMenuStrip.ResumeLayout(false);
            this.processFolderContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripStatusLabel tssGapFiller;
        private System.Windows.Forms.ToolStripDropDownButton tssServer;
        private System.Windows.Forms.ToolStripMenuItem tsmRecentServers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel timingMessageToolStripStatusLabel;
        private System.Windows.Forms.TreeView mainTreeView;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ContextMenuStrip procSetContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteProcessToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip processFolderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteProcessFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip workflowsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteWorkflowsToolStripMenuItem;
    }
}

