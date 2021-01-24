﻿namespace TestProj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtToSearch = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.chkHotKeyEnabled = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDatainiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTheAppFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MM_ShowTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_NeedUrbanDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToOALDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_save = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_saveAsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MM_showCurrentCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_showChronologically = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_showOnlyTodayEntries = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_UseDateFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MM_UseReverseOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_AutoSortByDate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MM_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.styleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_ShortcutsHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.cbxCategory = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.stHistoryCountShown = new System.Windows.Forms.ToolStripStatusLabel();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnByDateFilter = new System.Windows.Forms.Button();
            this.btnClearInput = new System.Windows.Forms.Button();
            this.MM_Test = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtToSearch
            // 
            this.txtToSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToSearch.Location = new System.Drawing.Point(1, 32);
            this.txtToSearch.Name = "txtToSearch";
            this.txtToSearch.Size = new System.Drawing.Size(742, 26);
            this.txtToSearch.TabIndex = 1;
            this.txtToSearch.TextChanged += new System.EventHandler(this.txtToSearch_TextChanged);
            this.txtToSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToSearch_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(1, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1213, 706);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // chkHotKeyEnabled
            // 
            this.chkHotKeyEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHotKeyEnabled.AutoSize = true;
            this.chkHotKeyEnabled.Checked = true;
            this.chkHotKeyEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHotKeyEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHotKeyEnabled.Location = new System.Drawing.Point(1048, 34);
            this.chkHotKeyEnabled.Name = "chkHotKeyEnabled";
            this.chkHotKeyEnabled.Size = new System.Drawing.Size(154, 24);
            this.chkHotKeyEnabled.TabIndex = 3;
            this.chkHotKeyEnabled.Text = "Hot key\'s enabled";
            this.chkHotKeyEnabled.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mainToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.styleToolStripMenuItem,
            this.MM_Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1214, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDatainiToolStripMenuItem,
            this.showTheAppFolderToolStripMenuItem,
            this.toolStripSeparator4,
            this.MM_ShowTasks});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // showDatainiToolStripMenuItem
            // 
            this.showDatainiToolStripMenuItem.Name = "showDatainiToolStripMenuItem";
            this.showDatainiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showDatainiToolStripMenuItem.Text = "Show data.ini";
            this.showDatainiToolStripMenuItem.Click += new System.EventHandler(this.showDatainiToolStripMenuItem_Click);
            // 
            // showTheAppFolderToolStripMenuItem
            // 
            this.showTheAppFolderToolStripMenuItem.Name = "showTheAppFolderToolStripMenuItem";
            this.showTheAppFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showTheAppFolderToolStripMenuItem.Text = "Show the app folder";
            this.showTheAppFolderToolStripMenuItem.Click += new System.EventHandler(this.showTheAppFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // MM_ShowTasks
            // 
            this.MM_ShowTasks.Name = "MM_ShowTasks";
            this.MM_ShowTasks.Size = new System.Drawing.Size(180, 22);
            this.MM_ShowTasks.Text = "Show Tasks";
            this.MM_ShowTasks.Click += new System.EventHandler(this.MM_ShowTasks_Click);
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_NeedUrbanDictionary,
            this.loginToOALDToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.mainToolStripMenuItem.Text = "Browser";
            // 
            // MM_NeedUrbanDictionary
            // 
            this.MM_NeedUrbanDictionary.CheckOnClick = true;
            this.MM_NeedUrbanDictionary.Name = "MM_NeedUrbanDictionary";
            this.MM_NeedUrbanDictionary.Size = new System.Drawing.Size(194, 22);
            this.MM_NeedUrbanDictionary.Text = "Need Urban Dictionary";
            this.MM_NeedUrbanDictionary.Click += new System.EventHandler(this.MM_NeedUrbanDictionary_Click);
            // 
            // loginToOALDToolStripMenuItem
            // 
            this.loginToOALDToolStripMenuItem.Name = "loginToOALDToolStripMenuItem";
            this.loginToOALDToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.loginToOALDToolStripMenuItem.Text = "Login to OALD";
            this.loginToOALDToolStripMenuItem.Click += new System.EventHandler(this.loginToOALDToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_save,
            this.MM_saveAsCopy,
            this.toolStripSeparator2,
            this.MM_showCurrentCategory,
            this.MM_showChronologically,
            this.MM_showOnlyTodayEntries,
            this.MM_UseDateFilter,
            this.toolStripSeparator3,
            this.MM_UseReverseOrder,
            this.MM_AutoSortByDate,
            this.toolStripSeparator1,
            this.MM_Update});
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.historyToolStripMenuItem.Text = "History";
            // 
            // MM_save
            // 
            this.MM_save.Name = "MM_save";
            this.MM_save.Size = new System.Drawing.Size(225, 22);
            this.MM_save.Text = "Save";
            this.MM_save.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // MM_saveAsCopy
            // 
            this.MM_saveAsCopy.Name = "MM_saveAsCopy";
            this.MM_saveAsCopy.Size = new System.Drawing.Size(225, 22);
            this.MM_saveAsCopy.Text = "Save As Copy";
            this.MM_saveAsCopy.Click += new System.EventHandler(this.saveAsCopyToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(222, 6);
            // 
            // MM_showCurrentCategory
            // 
            this.MM_showCurrentCategory.Checked = true;
            this.MM_showCurrentCategory.CheckOnClick = true;
            this.MM_showCurrentCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MM_showCurrentCategory.Name = "MM_showCurrentCategory";
            this.MM_showCurrentCategory.Size = new System.Drawing.Size(225, 22);
            this.MM_showCurrentCategory.Text = "Show Current Category Only";
            this.MM_showCurrentCategory.Click += new System.EventHandler(this.showCurrentCategoryToolStripMenuItem_Click);
            // 
            // MM_showChronologically
            // 
            this.MM_showChronologically.CheckOnClick = true;
            this.MM_showChronologically.Name = "MM_showChronologically";
            this.MM_showChronologically.Size = new System.Drawing.Size(225, 22);
            this.MM_showChronologically.Text = "Show Chronologically";
            this.MM_showChronologically.CheckedChanged += new System.EventHandler(this.MM_showChronologically_CheckedChanged);
            this.MM_showChronologically.Click += new System.EventHandler(this.showChronoToolStripMenuItem_Click);
            // 
            // MM_showOnlyTodayEntries
            // 
            this.MM_showOnlyTodayEntries.CheckOnClick = true;
            this.MM_showOnlyTodayEntries.Name = "MM_showOnlyTodayEntries";
            this.MM_showOnlyTodayEntries.Size = new System.Drawing.Size(225, 22);
            this.MM_showOnlyTodayEntries.Text = "Show Only Today Entries";
            this.MM_showOnlyTodayEntries.Click += new System.EventHandler(this.MM_showOnlyTodayEntries_Click);
            // 
            // MM_UseDateFilter
            // 
            this.MM_UseDateFilter.CheckOnClick = true;
            this.MM_UseDateFilter.Name = "MM_UseDateFilter";
            this.MM_UseDateFilter.Size = new System.Drawing.Size(225, 22);
            this.MM_UseDateFilter.Text = "Use Date filter";
            this.MM_UseDateFilter.Click += new System.EventHandler(this.MM_UseDateFilter_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // MM_UseReverseOrder
            // 
            this.MM_UseReverseOrder.Checked = true;
            this.MM_UseReverseOrder.CheckOnClick = true;
            this.MM_UseReverseOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MM_UseReverseOrder.Name = "MM_UseReverseOrder";
            this.MM_UseReverseOrder.Size = new System.Drawing.Size(225, 22);
            this.MM_UseReverseOrder.Text = "Use Reverse Order";
            this.MM_UseReverseOrder.Click += new System.EventHandler(this.MM_UseReverseOrder_Click);
            // 
            // MM_AutoSortByDate
            // 
            this.MM_AutoSortByDate.Checked = true;
            this.MM_AutoSortByDate.CheckOnClick = true;
            this.MM_AutoSortByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MM_AutoSortByDate.Name = "MM_AutoSortByDate";
            this.MM_AutoSortByDate.Size = new System.Drawing.Size(225, 22);
            this.MM_AutoSortByDate.Text = "Auto Sort By Date";
            this.MM_AutoSortByDate.Click += new System.EventHandler(this.MM_AutoSortByDate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // MM_Update
            // 
            this.MM_Update.Name = "MM_Update";
            this.MM_Update.Size = new System.Drawing.Size(225, 22);
            this.MM_Update.Text = "Update";
            this.MM_Update.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // styleToolStripMenuItem
            // 
            this.styleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkToolStripMenuItem,
            this.lightToolStripMenuItem});
            this.styleToolStripMenuItem.Name = "styleToolStripMenuItem";
            this.styleToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.styleToolStripMenuItem.Text = "Style";
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // MM_Help
            // 
            this.MM_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_About,
            this.MM_ShortcutsHelp,
            this.MM_Test});
            this.MM_Help.Name = "MM_Help";
            this.MM_Help.Size = new System.Drawing.Size(44, 20);
            this.MM_Help.Text = "Help";
            // 
            // MM_About
            // 
            this.MM_About.Name = "MM_About";
            this.MM_About.Size = new System.Drawing.Size(180, 22);
            this.MM_About.Text = "About";
            this.MM_About.Click += new System.EventHandler(this.MM_About_Click);
            // 
            // MM_ShortcutsHelp
            // 
            this.MM_ShortcutsHelp.Name = "MM_ShortcutsHelp";
            this.MM_ShortcutsHelp.Size = new System.Drawing.Size(180, 22);
            this.MM_ShortcutsHelp.Text = "Shortcuts";
            this.MM_ShortcutsHelp.Click += new System.EventHandler(this.MM_ShortcutsHelp_Click);
            // 
            // cbxCategory
            // 
            this.cbxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.Location = new System.Drawing.Point(794, 31);
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(248, 28);
            this.cbxCategory.TabIndex = 5;
            this.cbxCategory.SelectedIndexChanged += new System.EventHandler(this.cbxCategory_SelectedIndexChanged);
            this.cbxCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxCategory_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainProgress,
            this.stHistoryCountShown});
            this.statusStrip1.Location = new System.Drawing.Point(0, 751);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1214, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainProgress
            // 
            this.mainProgress.Name = "mainProgress";
            this.mainProgress.Size = new System.Drawing.Size(100, 16);
            this.mainProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mainProgress.Visible = false;
            // 
            // stHistoryCountShown
            // 
            this.stHistoryCountShown.Name = "stHistoryCountShown";
            this.stHistoryCountShown.Size = new System.Drawing.Size(0, 17);
            // 
            // dtBegin
            // 
            this.dtBegin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtBegin.Location = new System.Drawing.Point(361, 6);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(200, 20);
            this.dtBegin.TabIndex = 7;
            this.dtBegin.Visible = false;
            // 
            // dtEnd
            // 
            this.dtEnd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtEnd.Location = new System.Drawing.Point(609, 6);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(200, 20);
            this.dtEnd.TabIndex = 8;
            this.dtEnd.Visible = false;
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(325, 11);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(30, 13);
            this.lblFrom.TabIndex = 9;
            this.lblFrom.Text = "From";
            this.lblFrom.Visible = false;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(583, 12);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 13);
            this.lblTo.TabIndex = 10;
            this.lblTo.Text = "To";
            this.lblTo.Visible = false;
            // 
            // btnByDateFilter
            // 
            this.btnByDateFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnByDateFilter.Location = new System.Drawing.Point(835, 4);
            this.btnByDateFilter.Name = "btnByDateFilter";
            this.btnByDateFilter.Size = new System.Drawing.Size(75, 23);
            this.btnByDateFilter.TabIndex = 11;
            this.btnByDateFilter.Text = "Show";
            this.btnByDateFilter.UseVisualStyleBackColor = true;
            this.btnByDateFilter.Visible = false;
            this.btnByDateFilter.Click += new System.EventHandler(this.btnByDateFilter_Click);
            // 
            // btnClearInput
            // 
            this.btnClearInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearInput.Location = new System.Drawing.Point(749, 32);
            this.btnClearInput.Name = "btnClearInput";
            this.btnClearInput.Size = new System.Drawing.Size(39, 26);
            this.btnClearInput.TabIndex = 12;
            this.btnClearInput.Text = "Clear";
            this.btnClearInput.UseVisualStyleBackColor = true;
            this.btnClearInput.Click += new System.EventHandler(this.btnClearInput_Click);
            // 
            // MM_Test
            // 
            this.MM_Test.Name = "MM_Test";
            this.MM_Test.Size = new System.Drawing.Size(180, 22);
            this.MM_Test.Text = "Test";
            this.MM_Test.Click += new System.EventHandler(this.MM_Test_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 773);
            this.Controls.Add(this.btnClearInput);
            this.Controls.Add(this.btnByDateFilter);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtBegin);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cbxCategory);
            this.Controls.Add(this.chkHotKeyEnabled);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtToSearch);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1200, 39);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtToSearch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox chkHotKeyEnabled;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_NeedUrbanDictionary;
        private System.Windows.Forms.ToolStripMenuItem loginToOALDToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_Update;
        private System.Windows.Forms.ToolStripMenuItem styleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MM_saveAsCopy;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar mainProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MM_showCurrentCategory;
        private System.Windows.Forms.ToolStripMenuItem MM_showChronologically;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDatainiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTheAppFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_showOnlyTodayEntries;
        private System.Windows.Forms.ToolStripMenuItem MM_UseDateFilter;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.ToolStripMenuItem MM_Help;
        private System.Windows.Forms.ToolStripMenuItem MM_About;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnByDateFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MM_UseReverseOrder;
        private System.Windows.Forms.ToolStripMenuItem MM_ShowTasks;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MM_AutoSortByDate;
        private System.Windows.Forms.ToolStripStatusLabel stHistoryCountShown;
        private System.Windows.Forms.Button btnClearInput;
        private System.Windows.Forms.ToolStripMenuItem MM_ShortcutsHelp;
        private System.Windows.Forms.ToolStripMenuItem MM_Test;
    }
}

