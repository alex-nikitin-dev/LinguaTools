namespace TestProj
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDatainiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_SetBackupFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.showTheAppFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MM_ShowTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_FirstTab = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_cbxFirstTab = new System.Windows.Forms.ToolStripComboBox();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_NeedUrbanDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToOALDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_LoginToOALDOnStart = new System.Windows.Forms.ToolStripMenuItem();
            this.setOALDCredentialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_ForceLoadFromBrowseField = new System.Windows.Forms.ToolStripMenuItem();
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
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_HotKeyEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_ReturnDesktop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.txtFindText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.controlPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearFind = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.CustomTabControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtToSearch
            // 
            this.txtToSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtToSearch.Location = new System.Drawing.Point(4, 31);
            this.txtToSearch.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtToSearch.Name = "txtToSearch";
            this.txtToSearch.Size = new System.Drawing.Size(692, 35);
            this.txtToSearch.TabIndex = 1;
            this.txtToSearch.TextChanged += new System.EventHandler(this.txtToSearch_TextChanged);
            this.txtToSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrowse_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mainToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.styleToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.MM_Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1902, 33);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDatainiToolStripMenuItem,
            this.MM_SetBackupFolder,
            this.toolStripSeparator5,
            this.showTheAppFolderToolStripMenuItem,
            this.toolStripSeparator4,
            this.MM_ShowTasks});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // showDatainiToolStripMenuItem
            // 
            this.showDatainiToolStripMenuItem.Name = "showDatainiToolStripMenuItem";
            this.showDatainiToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            this.showDatainiToolStripMenuItem.Text = "Show data.ini";
            this.showDatainiToolStripMenuItem.Click += new System.EventHandler(this.showDatainiToolStripMenuItem_Click);
            // 
            // MM_SetBackupFolder
            // 
            this.MM_SetBackupFolder.Name = "MM_SetBackupFolder";
            this.MM_SetBackupFolder.Size = new System.Drawing.Size(276, 34);
            this.MM_SetBackupFolder.Text = "Set Backup folder";
            this.MM_SetBackupFolder.Click += new System.EventHandler(this.MM_SetBackupFolder_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.BackColor = System.Drawing.Color.Black;
            this.toolStripSeparator5.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(273, 6);
            // 
            // showTheAppFolderToolStripMenuItem
            // 
            this.showTheAppFolderToolStripMenuItem.Name = "showTheAppFolderToolStripMenuItem";
            this.showTheAppFolderToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            this.showTheAppFolderToolStripMenuItem.Text = "Show the app folder";
            this.showTheAppFolderToolStripMenuItem.Click += new System.EventHandler(this.showTheAppFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(273, 6);
            // 
            // MM_ShowTasks
            // 
            this.MM_ShowTasks.Name = "MM_ShowTasks";
            this.MM_ShowTasks.Size = new System.Drawing.Size(276, 34);
            this.MM_ShowTasks.Text = "Show Tasks";
            this.MM_ShowTasks.Click += new System.EventHandler(this.MM_ShowTasks_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_FirstTab,
            this.MM_cbxFirstTab});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // MM_FirstTab
            // 
            this.MM_FirstTab.Name = "MM_FirstTab";
            this.MM_FirstTab.Size = new System.Drawing.Size(290, 34);
            this.MM_FirstTab.Text = "The First Tab:";
            // 
            // MM_cbxFirstTab
            // 
            this.MM_cbxFirstTab.DropDownWidth = 200;
            this.MM_cbxFirstTab.Name = "MM_cbxFirstTab";
            this.MM_cbxFirstTab.Size = new System.Drawing.Size(200, 33);
            this.MM_cbxFirstTab.SelectedIndexChanged += new System.EventHandler(this.MM_cbxFirstTab_SelectedIndexChanged);
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_NeedUrbanDictionary,
            this.loginToOALDToolStripMenuItem,
            this.MM_LoginToOALDOnStart,
            this.setOALDCredentialsToolStripMenuItem,
            this.MM_ForceLoadFromBrowseField});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            this.mainToolStripMenuItem.Text = "Browser";
            // 
            // MM_NeedUrbanDictionary
            // 
            this.MM_NeedUrbanDictionary.CheckOnClick = true;
            this.MM_NeedUrbanDictionary.Name = "MM_NeedUrbanDictionary";
            this.MM_NeedUrbanDictionary.Size = new System.Drawing.Size(352, 34);
            this.MM_NeedUrbanDictionary.Text = "Need Urban Dictionary";
            this.MM_NeedUrbanDictionary.Click += new System.EventHandler(this.MM_NeedUrbanDictionary_Click);
            // 
            // loginToOALDToolStripMenuItem
            // 
            this.loginToOALDToolStripMenuItem.Name = "loginToOALDToolStripMenuItem";
            this.loginToOALDToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.loginToOALDToolStripMenuItem.Text = "Login to OALD";
            this.loginToOALDToolStripMenuItem.Click += new System.EventHandler(this.loginToOALDToolStripMenuItem_Click);
            // 
            // MM_LoginToOALDOnStart
            // 
            this.MM_LoginToOALDOnStart.CheckOnClick = true;
            this.MM_LoginToOALDOnStart.Name = "MM_LoginToOALDOnStart";
            this.MM_LoginToOALDOnStart.Size = new System.Drawing.Size(352, 34);
            this.MM_LoginToOALDOnStart.Text = "Login to OALD on Start";
            this.MM_LoginToOALDOnStart.Click += new System.EventHandler(this.MM_LoginToOALDOnStart_Click);
            // 
            // setOALDCredentialsToolStripMenuItem
            // 
            this.setOALDCredentialsToolStripMenuItem.Name = "setOALDCredentialsToolStripMenuItem";
            this.setOALDCredentialsToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.setOALDCredentialsToolStripMenuItem.Text = "Set OALD Credentials";
            this.setOALDCredentialsToolStripMenuItem.Click += new System.EventHandler(this.setOALDCredentialsToolStripMenuItem_Click);
            // 
            // MM_ForceLoadFromBrowseField
            // 
            this.MM_ForceLoadFromBrowseField.Name = "MM_ForceLoadFromBrowseField";
            this.MM_ForceLoadFromBrowseField.Size = new System.Drawing.Size(352, 34);
            this.MM_ForceLoadFromBrowseField.Text = "Force Load From Browse Field";
            this.MM_ForceLoadFromBrowseField.Click += new System.EventHandler(this.MM_ForceLoadFromBrowseField_Click);
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
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(85, 29);
            this.historyToolStripMenuItem.Text = "History";
            // 
            // MM_save
            // 
            this.MM_save.Name = "MM_save";
            this.MM_save.Size = new System.Drawing.Size(340, 34);
            this.MM_save.Text = "Save";
            this.MM_save.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // MM_saveAsCopy
            // 
            this.MM_saveAsCopy.Name = "MM_saveAsCopy";
            this.MM_saveAsCopy.Size = new System.Drawing.Size(340, 34);
            this.MM_saveAsCopy.Text = "Save As Copy";
            this.MM_saveAsCopy.Click += new System.EventHandler(this.saveAsCopyToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(337, 6);
            // 
            // MM_showCurrentCategory
            // 
            this.MM_showCurrentCategory.Checked = true;
            this.MM_showCurrentCategory.CheckOnClick = true;
            this.MM_showCurrentCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MM_showCurrentCategory.Name = "MM_showCurrentCategory";
            this.MM_showCurrentCategory.Size = new System.Drawing.Size(340, 34);
            this.MM_showCurrentCategory.Text = "Show Current Category Only";
            this.MM_showCurrentCategory.Click += new System.EventHandler(this.showCurrentCategoryToolStripMenuItem_Click);
            // 
            // MM_showChronologically
            // 
            this.MM_showChronologically.CheckOnClick = true;
            this.MM_showChronologically.Name = "MM_showChronologically";
            this.MM_showChronologically.Size = new System.Drawing.Size(340, 34);
            this.MM_showChronologically.Text = "Show Chronologically";
            this.MM_showChronologically.CheckedChanged += new System.EventHandler(this.MM_showChronologically_CheckedChanged);
            this.MM_showChronologically.Click += new System.EventHandler(this.showChronoToolStripMenuItem_Click);
            // 
            // MM_showOnlyTodayEntries
            // 
            this.MM_showOnlyTodayEntries.CheckOnClick = true;
            this.MM_showOnlyTodayEntries.Name = "MM_showOnlyTodayEntries";
            this.MM_showOnlyTodayEntries.Size = new System.Drawing.Size(340, 34);
            this.MM_showOnlyTodayEntries.Text = "Show Only Today Entries";
            this.MM_showOnlyTodayEntries.Click += new System.EventHandler(this.MM_showOnlyTodayEntries_Click);
            // 
            // MM_UseDateFilter
            // 
            this.MM_UseDateFilter.CheckOnClick = true;
            this.MM_UseDateFilter.Name = "MM_UseDateFilter";
            this.MM_UseDateFilter.Size = new System.Drawing.Size(340, 34);
            this.MM_UseDateFilter.Text = "Use Date filter";
            this.MM_UseDateFilter.Click += new System.EventHandler(this.MM_UseDateFilter_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(337, 6);
            // 
            // MM_UseReverseOrder
            // 
            this.MM_UseReverseOrder.Checked = true;
            this.MM_UseReverseOrder.CheckOnClick = true;
            this.MM_UseReverseOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MM_UseReverseOrder.Name = "MM_UseReverseOrder";
            this.MM_UseReverseOrder.Size = new System.Drawing.Size(340, 34);
            this.MM_UseReverseOrder.Text = "Use Reverse Order";
            this.MM_UseReverseOrder.Click += new System.EventHandler(this.MM_UseReverseOrder_Click);
            // 
            // MM_AutoSortByDate
            // 
            this.MM_AutoSortByDate.Checked = true;
            this.MM_AutoSortByDate.CheckOnClick = true;
            this.MM_AutoSortByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MM_AutoSortByDate.Name = "MM_AutoSortByDate";
            this.MM_AutoSortByDate.Size = new System.Drawing.Size(340, 34);
            this.MM_AutoSortByDate.Text = "Auto Sort By Date";
            this.MM_AutoSortByDate.Click += new System.EventHandler(this.MM_AutoSortByDate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(337, 6);
            // 
            // MM_Update
            // 
            this.MM_Update.Name = "MM_Update";
            this.MM_Update.Size = new System.Drawing.Size(340, 34);
            this.MM_Update.Text = "Update";
            this.MM_Update.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // styleToolStripMenuItem
            // 
            this.styleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkToolStripMenuItem,
            this.lightToolStripMenuItem});
            this.styleToolStripMenuItem.Name = "styleToolStripMenuItem";
            this.styleToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.styleToolStripMenuItem.Text = "Style";
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_HotKeyEnabled});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // MM_HotKeyEnabled
            // 
            this.MM_HotKeyEnabled.Checked = true;
            this.MM_HotKeyEnabled.CheckOnClick = true;
            this.MM_HotKeyEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MM_HotKeyEnabled.Name = "MM_HotKeyEnabled";
            this.MM_HotKeyEnabled.Size = new System.Drawing.Size(245, 34);
            this.MM_HotKeyEnabled.Text = "Hot Key Enabled";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_ReturnDesktop,
            this.toolStripMenuItem1});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(94, 29);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // MM_ReturnDesktop
            // 
            this.MM_ReturnDesktop.Name = "MM_ReturnDesktop";
            this.MM_ReturnDesktop.Size = new System.Drawing.Size(332, 34);
            this.MM_ReturnDesktop.Text = "Return To Previous Desktop";
            this.MM_ReturnDesktop.Click += new System.EventHandler(this.returnToPreviousDesktopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(332, 34);
            this.toolStripMenuItem1.Text = "test";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // MM_Help
            // 
            this.MM_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_About,
            this.MM_ShortcutsHelp});
            this.MM_Help.Name = "MM_Help";
            this.MM_Help.Size = new System.Drawing.Size(65, 29);
            this.MM_Help.Text = "Help";
            // 
            // MM_About
            // 
            this.MM_About.Name = "MM_About";
            this.MM_About.Size = new System.Drawing.Size(270, 34);
            this.MM_About.Text = "About";
            this.MM_About.Click += new System.EventHandler(this.MM_About_Click);
            // 
            // MM_ShortcutsHelp
            // 
            this.MM_ShortcutsHelp.Name = "MM_ShortcutsHelp";
            this.MM_ShortcutsHelp.Size = new System.Drawing.Size(270, 34);
            this.MM_ShortcutsHelp.Text = "Shortcuts";
            this.MM_ShortcutsHelp.Click += new System.EventHandler(this.MM_ShortcutsHelp_Click);
            // 
            // cbxCategory
            // 
            this.cbxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.IntegralHeight = false;
            this.cbxCategory.Location = new System.Drawing.Point(1428, 31);
            this.cbxCategory.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cbxCategory.MaxDropDownItems = 20;
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(464, 37);
            this.cbxCategory.TabIndex = 5;
            this.cbxCategory.TabStop = false;
            this.cbxCategory.SelectedIndexChanged += new System.EventHandler(this.cbxCategory_SelectedIndexChanged);
            this.cbxCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxCategory_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainProgress,
            this.stHistoryCountShown});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1156);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 23, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1902, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainProgress
            // 
            this.mainProgress.Name = "mainProgress";
            this.mainProgress.Size = new System.Drawing.Size(167, 18);
            this.mainProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mainProgress.Visible = false;
            // 
            // stHistoryCountShown
            // 
            this.stHistoryCountShown.Name = "stHistoryCountShown";
            this.stHistoryCountShown.Size = new System.Drawing.Size(0, 15);
            // 
            // dtBegin
            // 
            this.dtBegin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtBegin.Location = new System.Drawing.Point(912, 6);
            this.dtBegin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(331, 31);
            this.dtBegin.TabIndex = 7;
            this.dtBegin.TabStop = false;
            this.dtBegin.Visible = false;
            // 
            // dtEnd
            // 
            this.dtEnd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtEnd.Location = new System.Drawing.Point(1291, 6);
            this.dtEnd.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(331, 31);
            this.dtEnd.TabIndex = 8;
            this.dtEnd.TabStop = false;
            this.dtEnd.Visible = false;
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(852, 11);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(54, 25);
            this.lblFrom.TabIndex = 9;
            this.lblFrom.Text = "From";
            this.lblFrom.Visible = false;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(1252, 9);
            this.lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(30, 25);
            this.lblTo.TabIndex = 10;
            this.lblTo.Text = "To";
            this.lblTo.Visible = false;
            // 
            // btnByDateFilter
            // 
            this.btnByDateFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnByDateFilter.Location = new System.Drawing.Point(1631, 0);
            this.btnByDateFilter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnByDateFilter.Name = "btnByDateFilter";
            this.btnByDateFilter.Size = new System.Drawing.Size(124, 44);
            this.btnByDateFilter.TabIndex = 11;
            this.btnByDateFilter.TabStop = false;
            this.btnByDateFilter.Text = "Show";
            this.btnByDateFilter.UseVisualStyleBackColor = true;
            this.btnByDateFilter.Visible = false;
            this.btnByDateFilter.Click += new System.EventHandler(this.btnByDateFilter_Click);
            // 
            // btnClearInput
            // 
            this.btnClearInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearInput.Location = new System.Drawing.Point(704, 31);
            this.btnClearInput.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClearInput.Name = "btnClearInput";
            this.btnClearInput.Size = new System.Drawing.Size(64, 37);
            this.btnClearInput.TabIndex = 12;
            this.btnClearInput.TabStop = false;
            this.btnClearInput.Text = "Clear";
            this.btnClearInput.UseVisualStyleBackColor = true;
            this.btnClearInput.Click += new System.EventHandler(this.btnClearInput_Click);
            // 
            // txtFindText
            // 
            this.txtFindText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFindText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFindText.Location = new System.Drawing.Point(776, 31);
            this.txtFindText.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtFindText.Name = "txtFindText";
            this.txtFindText.Size = new System.Drawing.Size(572, 35);
            this.txtFindText.TabIndex = 2;
            this.txtFindText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFindText_KeyDown);
            this.txtFindText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFindText_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Browse:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(776, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Find:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(1428, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 25);
            this.label3.TabIndex = 16;
            this.label3.Text = "Category:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.controlPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 33);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.479964F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.52003F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1902, 1123);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // controlPanel
            // 
            this.controlPanel.AutoSize = true;
            this.controlPanel.ColumnCount = 5;
            this.controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.66871F));
            this.controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.3313F));
            this.controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 471F));
            this.controlPanel.Controls.Add(this.btnClearFind, 3, 1);
            this.controlPanel.Controls.Add(this.label1, 0, 0);
            this.controlPanel.Controls.Add(this.txtToSearch, 0, 1);
            this.controlPanel.Controls.Add(this.label2, 2, 0);
            this.controlPanel.Controls.Add(this.btnClearInput, 1, 1);
            this.controlPanel.Controls.Add(this.txtFindText, 2, 1);
            this.controlPanel.Controls.Add(this.cbxCategory, 4, 1);
            this.controlPanel.Controls.Add(this.label3, 4, 0);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(3, 4);
            this.controlPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.RowCount = 2;
            this.controlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.controlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.controlPanel.Size = new System.Drawing.Size(1896, 76);
            this.controlPanel.TabIndex = 3;
            // 
            // btnClearFind
            // 
            this.btnClearFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFind.Location = new System.Drawing.Point(1356, 31);
            this.btnClearFind.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClearFind.Name = "btnClearFind";
            this.btnClearFind.Size = new System.Drawing.Size(64, 35);
            this.btnClearFind.TabIndex = 18;
            this.btnClearFind.TabStop = false;
            this.btnClearFind.Text = "Clear";
            this.btnClearFind.UseVisualStyleBackColor = true;
            this.btnClearFind.Click += new System.EventHandler(this.btnClearFind_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.DisplayStyleProvider.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.tabControl1.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.tabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.tabControl1.DisplayStyleProvider.FocusTrack = true;
            this.tabControl1.DisplayStyleProvider.HotTrack = true;
            this.tabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabControl1.DisplayStyleProvider.Opacity = 1F;
            this.tabControl1.DisplayStyleProvider.Overlap = 0;
            this.tabControl1.DisplayStyleProvider.Padding = new System.Drawing.Point(6, 3);
            this.tabControl1.DisplayStyleProvider.Radius = 2;
            this.tabControl1.DisplayStyleProvider.ShowTabCloser = false;
            this.tabControl1.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.tabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(3, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1896, 1033);
            this.tabControl1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1178);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnByDateFilter);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtBegin);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MinimumSize = new System.Drawing.Size(1889, 56);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtToSearch;
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
        private System.Windows.Forms.ToolStripMenuItem MM_LoginToOALDOnStart;
        private System.Windows.Forms.TextBox txtFindText;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_HotKeyEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem MM_SetBackupFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem setOALDCredentialsToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel controlPanel;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_FirstTab;
        private System.Windows.Forms.ToolStripComboBox MM_cbxFirstTab;
        private System.Windows.Forms.Button btnClearFind;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_ReturnDesktop;
        private System.Windows.Forms.CustomTabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem MM_ForceLoadFromBrowseField;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

