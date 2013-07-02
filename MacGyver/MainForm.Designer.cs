namespace MacGyver
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
            this.cbPids = new System.Windows.Forms.ComboBox();
            this.cbParameters = new System.Windows.Forms.ComboBox();
            this.lbCraftParams = new System.Windows.Forms.ListBox();
            this.lbItems = new System.Windows.Forms.ListBox();
            this.lbTools = new System.Windows.Forms.ListBox();
            this.lbOutput = new System.Windows.Forms.ListBox();
            this.lbViewParams = new System.Windows.Forms.ListBox();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.txtName = new System.Windows.Forms.TextBox();
            this.numExperience = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.btnCraft = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReqItem = new System.Windows.Forms.Button();
            this.btnReqTool = new System.Windows.Forms.Button();
            this.btnOutputItem = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.numNumber = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stripLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.recipeList = new BrightIdeasSoftware.ObjectListView();
            this.col1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.col2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.col3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cbOperator = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExperience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumber)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipeList)).BeginInit();
            this.SuspendLayout();
            // 
            // cbPids
            // 
            this.cbPids.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPids.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPids.DropDownWidth = 300;
            this.cbPids.FormattingEnabled = true;
            this.cbPids.Location = new System.Drawing.Point(419, 159);
            this.cbPids.MaxDropDownItems = 46;
            this.cbPids.Name = "cbPids";
            this.cbPids.Size = new System.Drawing.Size(146, 21);
            this.cbPids.TabIndex = 0;
            // 
            // cbParameters
            // 
            this.cbParameters.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbParameters.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbParameters.DropDownWidth = 300;
            this.cbParameters.FormattingEnabled = true;
            this.cbParameters.Location = new System.Drawing.Point(419, 33);
            this.cbParameters.MaxDropDownItems = 46;
            this.cbParameters.Name = "cbParameters";
            this.cbParameters.Size = new System.Drawing.Size(146, 21);
            this.cbParameters.TabIndex = 1;
            // 
            // lbCraftParams
            // 
            this.lbCraftParams.FormattingEnabled = true;
            this.lbCraftParams.HorizontalScrollbar = true;
            this.lbCraftParams.Location = new System.Drawing.Point(185, 33);
            this.lbCraftParams.Name = "lbCraftParams";
            this.lbCraftParams.Size = new System.Drawing.Size(167, 95);
            this.lbCraftParams.TabIndex = 2;
            this.lbCraftParams.DoubleClick += new System.EventHandler(this.lbCraftParams_DoubleClick);
            // 
            // lbItems
            // 
            this.lbItems.FormattingEnabled = true;
            this.lbItems.HorizontalScrollbar = true;
            this.lbItems.Location = new System.Drawing.Point(12, 159);
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(167, 95);
            this.lbItems.TabIndex = 3;
            this.lbItems.DoubleClick += new System.EventHandler(this.lbItems_DoubleClick);
            this.lbItems.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbItems_MouseWheel);
            // 
            // lbTools
            // 
            this.lbTools.FormattingEnabled = true;
            this.lbTools.HorizontalScrollbar = true;
            this.lbTools.Location = new System.Drawing.Point(185, 159);
            this.lbTools.Name = "lbTools";
            this.lbTools.Size = new System.Drawing.Size(167, 95);
            this.lbTools.TabIndex = 4;
            this.lbTools.DoubleClick += new System.EventHandler(this.lbTools_DoubleClick);
            // 
            // lbOutput
            // 
            this.lbOutput.FormattingEnabled = true;
            this.lbOutput.HorizontalScrollbar = true;
            this.lbOutput.Location = new System.Drawing.Point(185, 277);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(167, 95);
            this.lbOutput.TabIndex = 5;
            this.lbOutput.DoubleClick += new System.EventHandler(this.lbOutput_DoubleClick);
            // 
            // lbViewParams
            // 
            this.lbViewParams.FormattingEnabled = true;
            this.lbViewParams.HorizontalScrollbar = true;
            this.lbViewParams.Location = new System.Drawing.Point(12, 33);
            this.lbViewParams.Name = "lbViewParams";
            this.lbViewParams.Size = new System.Drawing.Size(167, 95);
            this.lbViewParams.TabIndex = 6;
            this.lbViewParams.DoubleClick += new System.EventHandler(this.lbViewParams_DoubleClick);
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(444, 186);
            this.numCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(121, 20);
            this.numCount.TabIndex = 10;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(50, 380);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(88, 20);
            this.txtName.TabIndex = 11;
            // 
            // numExperience
            // 
            this.numExperience.Location = new System.Drawing.Point(317, 381);
            this.numExperience.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numExperience.Name = "numExperience";
            this.numExperience.Size = new System.Drawing.Size(55, 20);
            this.numExperience.TabIndex = 12;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(444, 60);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(121, 20);
            this.numericUpDown3.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Parameters for view";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Parameters to craft";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(358, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Parameter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Value";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(361, 86);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(99, 42);
            this.btnView.TabIndex = 18;
            this.btnView.Text = "For view";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnCraft
            // 
            this.btnCraft.Location = new System.Drawing.Point(466, 86);
            this.btnCraft.Name = "btnCraft";
            this.btnCraft.Size = new System.Drawing.Size(99, 42);
            this.btnCraft.TabIndex = 19;
            this.btnCraft.Text = "For craft";
            this.btnCraft.UseVisualStyleBackColor = true;
            this.btnCraft.Click += new System.EventHandler(this.btnCraft_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Item pid";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(358, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Count";
            // 
            // btnReqItem
            // 
            this.btnReqItem.Location = new System.Drawing.Point(361, 212);
            this.btnReqItem.Name = "btnReqItem";
            this.btnReqItem.Size = new System.Drawing.Size(99, 42);
            this.btnReqItem.TabIndex = 22;
            this.btnReqItem.Text = "Required item";
            this.btnReqItem.UseVisualStyleBackColor = true;
            this.btnReqItem.Click += new System.EventHandler(this.btnReqItem_Click);
            // 
            // btnReqTool
            // 
            this.btnReqTool.Location = new System.Drawing.Point(466, 212);
            this.btnReqTool.Name = "btnReqTool";
            this.btnReqTool.Size = new System.Drawing.Size(99, 42);
            this.btnReqTool.TabIndex = 23;
            this.btnReqTool.Text = "Required tool";
            this.btnReqTool.UseVisualStyleBackColor = true;
            this.btnReqTool.Click += new System.EventHandler(this.btnReqTool_Click);
            // 
            // btnOutputItem
            // 
            this.btnOutputItem.Location = new System.Drawing.Point(361, 277);
            this.btnOutputItem.Name = "btnOutputItem";
            this.btnOutputItem.Size = new System.Drawing.Size(99, 42);
            this.btnOutputItem.TabIndex = 24;
            this.btnOutputItem.Text = "Output item";
            this.btnOutputItem.UseVisualStyleBackColor = true;
            this.btnOutputItem.Click += new System.EventHandler(this.btnOutputItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 383);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(144, 383);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(251, 383);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Experience";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 407);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Script";
            // 
            // txtScript
            // 
            this.txtScript.Location = new System.Drawing.Point(50, 404);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(322, 20);
            this.txtScript.TabIndex = 29;
            // 
            // numNumber
            // 
            this.numNumber.Location = new System.Drawing.Point(194, 381);
            this.numNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numNumber.Name = "numNumber";
            this.numNumber.Size = new System.Drawing.Size(51, 20);
            this.numNumber.TabIndex = 30;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Required items";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(182, 143);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Required tools";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(182, 261);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Output items";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 261);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 35;
            this.label14.Text = "Description";
            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Location = new System.Drawing.Point(416, 344);
            this.btnAddUpdate.Name = "btnAddUpdate";
            this.btnAddUpdate.Size = new System.Drawing.Size(149, 52);
            this.btnAddUpdate.TabIndex = 36;
            this.btnAddUpdate.Text = "Add/update";
            this.btnAddUpdate.UseVisualStyleBackColor = true;
            this.btnAddUpdate.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(418, 399);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(147, 28);
            this.btnClear.TabIndex = 37;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rtbDescription
            // 
            this.rtbDescription.Location = new System.Drawing.Point(12, 277);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(167, 95);
            this.rtbDescription.TabIndex = 39;
            this.rtbDescription.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripLog});
            this.statusStrip1.Location = new System.Drawing.Point(0, 430);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(863, 22);
            this.statusStrip1.TabIndex = 41;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stripLog
            // 
            this.stripLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.stripLog.Name = "stripLog";
            this.stripLog.Size = new System.Drawing.Size(101, 17);
            this.stripLog.Text = "Program opened";
            // 
            // recipeList
            // 
            this.recipeList.AllColumns.Add(this.col1);
            this.recipeList.AllColumns.Add(this.col2);
            this.recipeList.AllColumns.Add(this.col3);
            this.recipeList.AllowColumnReorder = true;
            this.recipeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recipeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1,
            this.col2,
            this.col3});
            this.recipeList.Cursor = System.Windows.Forms.Cursors.Default;
            this.recipeList.Font = new System.Drawing.Font("Tahoma", 8F);
            this.recipeList.FullRowSelect = true;
            this.recipeList.Location = new System.Drawing.Point(571, 9);
            this.recipeList.Name = "recipeList";
            this.recipeList.ShowGroups = false;
            this.recipeList.Size = new System.Drawing.Size(290, 363);
            this.recipeList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.recipeList.TabIndex = 3;
            this.recipeList.UseCompatibleStateImageBehavior = false;
            this.recipeList.View = System.Windows.Forms.View.Details;
            this.recipeList.DoubleClick += new System.EventHandler(this.recipeList_DoubleClick);
            // 
            // col1
            // 
            this.col1.AspectName = "number";
            this.col1.HeaderFont = null;
            this.col1.IsEditable = false;
            this.col1.Text = "Id";
            this.col1.Width = 27;
            // 
            // col2
            // 
            this.col2.AspectName = "name";
            this.col2.HeaderFont = null;
            this.col2.IsEditable = false;
            this.col2.Text = "Name";
            this.col2.Width = 105;
            // 
            // col3
            // 
            this.col3.AspectName = "desc";
            this.col3.HeaderFont = null;
            this.col3.IsEditable = false;
            this.col3.Text = "Description";
            this.col3.Width = 132;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(571, 378);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 49);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "Save list";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(718, 378);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(141, 49);
            this.btnLoad.TabIndex = 43;
            this.btnLoad.Text = "Load list";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbOperator
            // 
            this.cbOperator.AutoSize = true;
            this.cbOperator.Location = new System.Drawing.Point(357, 6);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(169, 17);
            this.cbOperator.TabIndex = 44;
            this.cbOperator.Text = "OR (if not checked then AND)";
            this.cbOperator.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(863, 452);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rtbDescription);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddUpdate);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numNumber);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnOutputItem);
            this.Controls.Add(this.btnReqTool);
            this.Controls.Add(this.btnReqItem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCraft);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numExperience);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.numCount);
            this.Controls.Add(this.lbViewParams);
            this.Controls.Add(this.lbOutput);
            this.Controls.Add(this.lbTools);
            this.Controls.Add(this.lbItems);
            this.Controls.Add(this.lbCraftParams);
            this.Controls.Add(this.cbParameters);
            this.Controls.Add(this.cbPids);
            this.Controls.Add(this.recipeList);
            this.Name = "MainForm";
            this.Text = "MacGyver";
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExperience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumber)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPids;
        private System.Windows.Forms.ComboBox cbParameters;
        private System.Windows.Forms.ListBox lbCraftParams;
        private System.Windows.Forms.ListBox lbItems;
        private System.Windows.Forms.ListBox lbTools;
        private System.Windows.Forms.ListBox lbOutput;
        private System.Windows.Forms.ListBox lbViewParams;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown numExperience;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnCraft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReqItem;
        private System.Windows.Forms.Button btnReqTool;
        private System.Windows.Forms.Button btnOutputItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.NumericUpDown numNumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stripLog;
        private BrightIdeasSoftware.ObjectListView recipeList;
        private BrightIdeasSoftware.OLVColumn col1;
        private BrightIdeasSoftware.OLVColumn col2;
        private BrightIdeasSoftware.OLVColumn col3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.CheckBox cbOperator;
    }
}

