namespace CodeAnalyzer
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCallTree = new System.Windows.Forms.TabPage();
            this.callView = new BrightIdeasSoftware.TreeListView();
            this.callViewColName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.callViewColIncl = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.callViewColExcl = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabModules = new System.Windows.Forms.TabPage();
            this.moduleView = new BrightIdeasSoftware.TreeListView();
            this.moduleViewColName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.moduleViewColExcl = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabFunction = new System.Windows.Forms.TabPage();
            this.rtbFunctionDetails = new System.Windows.Forms.RichTextBox();
            this.cmbFunctions = new System.Windows.Forms.ComboBox();
            this.cmbModules = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCallTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.callView)).BeginInit();
            this.tabModules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moduleView)).BeginInit();
            this.tabFunction.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.aToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.loadToolStripMenuItem.Text = "Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCallTree);
            this.tabControl1.Controls.Add(this.tabModules);
            this.tabControl1.Controls.Add(this.tabFunction);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 516);
            this.tabControl1.TabIndex = 1;
            // 
            // tabCallTree
            // 
            this.tabCallTree.Controls.Add(this.callView);
            this.tabCallTree.Location = new System.Drawing.Point(4, 22);
            this.tabCallTree.Name = "tabCallTree";
            this.tabCallTree.Size = new System.Drawing.Size(776, 490);
            this.tabCallTree.TabIndex = 0;
            this.tabCallTree.Text = "Call Tree";
            this.tabCallTree.UseVisualStyleBackColor = true;
            // 
            // callView
            // 
            this.callView.AllColumns.Add(this.callViewColName);
            this.callView.AllColumns.Add(this.callViewColIncl);
            this.callView.AllColumns.Add(this.callViewColExcl);
            this.callView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.callViewColName,
            this.callViewColIncl,
            this.callViewColExcl});
            this.callView.Cursor = System.Windows.Forms.Cursors.Default;
            this.callView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.callView.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.callView.FullRowSelect = true;
            this.callView.GridLines = true;
            this.callView.Location = new System.Drawing.Point(0, 0);
            this.callView.Name = "callView";
            this.callView.OwnerDraw = true;
            this.callView.ShowGroups = false;
            this.callView.Size = new System.Drawing.Size(776, 490);
            this.callView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.callView.TabIndex = 3;
            this.callView.UseCompatibleStateImageBehavior = false;
            this.callView.View = System.Windows.Forms.View.Details;
            this.callView.VirtualMode = true;
            // 
            // callViewColName
            // 
            this.callViewColName.AspectName = "Name";
            this.callViewColName.IsEditable = false;
            this.callViewColName.Text = "Function";
            this.callViewColName.Width = 590;
            // 
            // callViewColIncl
            // 
            this.callViewColIncl.AspectName = "InclStr";
            this.callViewColIncl.IsEditable = false;
            this.callViewColIncl.Text = "Inclusive %";
            this.callViewColIncl.Width = 90;
            // 
            // callViewColExcl
            // 
            this.callViewColExcl.AspectName = "ExclStr";
            this.callViewColExcl.FillsFreeSpace = true;
            this.callViewColExcl.IsEditable = false;
            this.callViewColExcl.Text = "Exclusive %";
            this.callViewColExcl.Width = 90;
            // 
            // tabModules
            // 
            this.tabModules.Controls.Add(this.moduleView);
            this.tabModules.Location = new System.Drawing.Point(4, 22);
            this.tabModules.Name = "tabModules";
            this.tabModules.Size = new System.Drawing.Size(776, 512);
            this.tabModules.TabIndex = 2;
            this.tabModules.Text = "Modules";
            this.tabModules.UseVisualStyleBackColor = true;
            // 
            // moduleView
            // 
            this.moduleView.AllColumns.Add(this.moduleViewColName);
            this.moduleView.AllColumns.Add(this.moduleViewColExcl);
            this.moduleView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.moduleViewColName,
            this.moduleViewColExcl});
            this.moduleView.Cursor = System.Windows.Forms.Cursors.Default;
            this.moduleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moduleView.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.moduleView.FullRowSelect = true;
            this.moduleView.GridLines = true;
            this.moduleView.Location = new System.Drawing.Point(0, 0);
            this.moduleView.Name = "moduleView";
            this.moduleView.OwnerDraw = true;
            this.moduleView.ShowGroups = false;
            this.moduleView.Size = new System.Drawing.Size(776, 512);
            this.moduleView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.moduleView.TabIndex = 3;
            this.moduleView.UseCompatibleStateImageBehavior = false;
            this.moduleView.View = System.Windows.Forms.View.Details;
            this.moduleView.VirtualMode = true;
            // 
            // moduleViewColName
            // 
            this.moduleViewColName.AspectName = "Name";
            this.moduleViewColName.IsEditable = false;
            this.moduleViewColName.Text = "Function";
            this.moduleViewColName.Width = 680;
            // 
            // moduleViewColExcl
            // 
            this.moduleViewColExcl.AspectName = "ExclStr";
            this.moduleViewColExcl.FillsFreeSpace = true;
            this.moduleViewColExcl.IsEditable = false;
            this.moduleViewColExcl.Text = "Exclusive %";
            this.moduleViewColExcl.Width = 90;
            // 
            // tabFunction
            // 
            this.tabFunction.Controls.Add(this.rtbFunctionDetails);
            this.tabFunction.Controls.Add(this.cmbFunctions);
            this.tabFunction.Controls.Add(this.cmbModules);
            this.tabFunction.Location = new System.Drawing.Point(4, 22);
            this.tabFunction.Name = "tabFunction";
            this.tabFunction.Size = new System.Drawing.Size(776, 490);
            this.tabFunction.TabIndex = 3;
            this.tabFunction.Text = "Function Details";
            this.tabFunction.UseVisualStyleBackColor = true;
            // 
            // rtbFunctionDetails
            // 
            this.rtbFunctionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbFunctionDetails.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbFunctionDetails.Location = new System.Drawing.Point(0, 40);
            this.rtbFunctionDetails.Name = "rtbFunctionDetails";
            this.rtbFunctionDetails.ReadOnly = true;
            this.rtbFunctionDetails.Size = new System.Drawing.Size(776, 450);
            this.rtbFunctionDetails.TabIndex = 3;
            this.rtbFunctionDetails.Text = "";
            this.rtbFunctionDetails.WordWrap = false;
            // 
            // cmbFunctions
            // 
            this.cmbFunctions.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctions.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbFunctions.FormattingEnabled = true;
            this.cmbFunctions.IntegralHeight = false;
            this.cmbFunctions.Location = new System.Drawing.Point(0, 20);
            this.cmbFunctions.MaxDropDownItems = 50;
            this.cmbFunctions.Name = "cmbFunctions";
            this.cmbFunctions.Size = new System.Drawing.Size(776, 20);
            this.cmbFunctions.TabIndex = 2;
            this.cmbFunctions.SelectedIndexChanged += new System.EventHandler(this.cmbFunctions_SelectedIndexChanged);
            // 
            // cmbModules
            // 
            this.cmbModules.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbModules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModules.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbModules.FormattingEnabled = true;
            this.cmbModules.IntegralHeight = false;
            this.cmbModules.Location = new System.Drawing.Point(0, 0);
            this.cmbModules.MaxDropDownItems = 50;
            this.cmbModules.Name = "cmbModules";
            this.cmbModules.Size = new System.Drawing.Size(776, 20);
            this.cmbModules.TabIndex = 0;
            this.cmbModules.SelectedIndexChanged += new System.EventHandler(this.cmbModules_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(88, 17);
            this.lblStatus.Text = "Reading stacks:";
            this.lblStatus.Visible = false;
            // 
            // pbStatus
            // 
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 16);
            this.pbStatus.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "CodeAnalyzer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabCallTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.callView)).EndInit();
            this.tabModules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moduleView)).EndInit();
            this.tabFunction.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCallTree;
        private BrightIdeasSoftware.TreeListView callView;
        private BrightIdeasSoftware.TreeListView moduleView;
        private BrightIdeasSoftware.OLVColumn callViewColName;
        private BrightIdeasSoftware.OLVColumn callViewColIncl;
        private BrightIdeasSoftware.OLVColumn callViewColExcl;
        private BrightIdeasSoftware.OLVColumn moduleViewColName;
        private BrightIdeasSoftware.OLVColumn moduleViewColExcl;
        private System.Windows.Forms.TabPage tabModules;
        private System.Windows.Forms.TabPage tabFunction;
        private System.Windows.Forms.ComboBox cmbModules;
        private System.Windows.Forms.ComboBox cmbFunctions;
        private System.Windows.Forms.RichTextBox rtbFunctionDetails;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
    }
}

