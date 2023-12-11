namespace LinguaHelper
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
            txtToSearch = new System.Windows.Forms.TextBox();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showDatainiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_SetBackupFolder = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            showTheAppFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            MM_ShowTasks = new System.Windows.Forms.ToolStripMenuItem();
            showJSErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_FirstTab = new System.Windows.Forms.ToolStripMenuItem();
            MM_cbxFirstTab = new System.Windows.Forms.ToolStripComboBox();
            mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_NeedUrbanDictionary = new System.Windows.Forms.ToolStripMenuItem();
            loginToOALDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_LoginToOALDOnStart = new System.Windows.Forms.ToolStripMenuItem();
            setOALDCredentialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_ForceLoadFromBrowseField = new System.Windows.Forms.ToolStripMenuItem();
            MM_SpeakOnBrowsingOALD = new System.Windows.Forms.ToolStripMenuItem();
            MM_ActivateTabsAfterAppStarts = new System.Windows.Forms.ToolStripMenuItem();
            historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_save = new System.Windows.Forms.ToolStripMenuItem();
            MM_saveAsCopy = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            MM_showCurrentCategory = new System.Windows.Forms.ToolStripMenuItem();
            MM_showChronologically = new System.Windows.Forms.ToolStripMenuItem();
            MM_showOnlyTodayEntries = new System.Windows.Forms.ToolStripMenuItem();
            MM_UseDateFilter = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            MM_UseReverseOrder = new System.Windows.Forms.ToolStripMenuItem();
            MM_AutoSortByDate = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            MM_Update = new System.Windows.Forms.ToolStripMenuItem();
            styleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_HotKeyEnabled = new System.Windows.Forms.ToolStripMenuItem();
            windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MM_ReturnDesktop = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            MM_Help = new System.Windows.Forms.ToolStripMenuItem();
            MM_About = new System.Windows.Forms.ToolStripMenuItem();
            MM_ShortcutsHelp = new System.Windows.Forms.ToolStripMenuItem();
            cbxCategory = new System.Windows.Forms.ComboBox();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            stError = new System.Windows.Forms.ToolStripStatusLabel();
            mainProgress = new System.Windows.Forms.ToolStripProgressBar();
            stHistoryCountShown = new System.Windows.Forms.ToolStripStatusLabel();
            dtBegin = new System.Windows.Forms.DateTimePicker();
            dtEnd = new System.Windows.Forms.DateTimePicker();
            lblFrom = new System.Windows.Forms.Label();
            lblTo = new System.Windows.Forms.Label();
            btnByDateFilter = new System.Windows.Forms.Button();
            btnClearInput = new System.Windows.Forms.Button();
            txtFindText = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            controlPanel = new System.Windows.Forms.TableLayoutPanel();
            btnClearFind = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.CustomTabControl();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            controlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // txtToSearch
            // 
            txtToSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtToSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            txtToSearch.Location = new System.Drawing.Point(4, 31);
            txtToSearch.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            txtToSearch.Name = "txtToSearch";
            txtToSearch.Size = new System.Drawing.Size(692, 35);
            txtToSearch.TabIndex = 1;
            txtToSearch.KeyDown += txtBrowse_KeyDown;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, mainToolStripMenuItem, historyToolStripMenuItem, styleToolStripMenuItem, optionsToolStripMenuItem, windowToolStripMenuItem, MM_Help });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1902, 33);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { showDatainiToolStripMenuItem, MM_SetBackupFolder, toolStripSeparator5, showTheAppFolderToolStripMenuItem, toolStripSeparator4, MM_ShowTasks, showJSErrorsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // showDatainiToolStripMenuItem
            // 
            showDatainiToolStripMenuItem.Name = "showDatainiToolStripMenuItem";
            showDatainiToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            showDatainiToolStripMenuItem.Text = "Show data.ini";
            showDatainiToolStripMenuItem.Click += showDatainiToolStripMenuItem_Click;
            // 
            // MM_SetBackupFolder
            // 
            MM_SetBackupFolder.Name = "MM_SetBackupFolder";
            MM_SetBackupFolder.Size = new System.Drawing.Size(276, 34);
            MM_SetBackupFolder.Text = "Set Backup folder";
            MM_SetBackupFolder.Click += MM_SetBackupFolder_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.BackColor = System.Drawing.Color.Black;
            toolStripSeparator5.ForeColor = System.Drawing.Color.Black;
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(273, 6);
            // 
            // showTheAppFolderToolStripMenuItem
            // 
            showTheAppFolderToolStripMenuItem.Name = "showTheAppFolderToolStripMenuItem";
            showTheAppFolderToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            showTheAppFolderToolStripMenuItem.Text = "Show the app folder";
            showTheAppFolderToolStripMenuItem.Click += showTheAppFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(273, 6);
            // 
            // MM_ShowTasks
            // 
            MM_ShowTasks.Name = "MM_ShowTasks";
            MM_ShowTasks.Size = new System.Drawing.Size(276, 34);
            MM_ShowTasks.Text = "Show Tasks";
            MM_ShowTasks.Click += MM_ShowTasks_Click;
            // 
            // showJSErrorsToolStripMenuItem
            // 
            showJSErrorsToolStripMenuItem.Name = "showJSErrorsToolStripMenuItem";
            showJSErrorsToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            showJSErrorsToolStripMenuItem.Text = "Show Errors Log";
            showJSErrorsToolStripMenuItem.Click += showJSErrorsToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MM_FirstTab, MM_cbxFirstTab });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            viewToolStripMenuItem.Text = "View";
            // 
            // MM_FirstTab
            // 
            MM_FirstTab.Name = "MM_FirstTab";
            MM_FirstTab.Size = new System.Drawing.Size(290, 34);
            MM_FirstTab.Text = "The First Tab:";
            // 
            // MM_cbxFirstTab
            // 
            MM_cbxFirstTab.DropDownWidth = 200;
            MM_cbxFirstTab.Name = "MM_cbxFirstTab";
            MM_cbxFirstTab.Size = new System.Drawing.Size(200, 33);
            MM_cbxFirstTab.SelectedIndexChanged += MM_cbxFirstTab_SelectedIndexChanged;
            // 
            // mainToolStripMenuItem
            // 
            mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MM_NeedUrbanDictionary, loginToOALDToolStripMenuItem, MM_LoginToOALDOnStart, setOALDCredentialsToolStripMenuItem, MM_ForceLoadFromBrowseField, MM_SpeakOnBrowsingOALD, MM_ActivateTabsAfterAppStarts });
            mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            mainToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            mainToolStripMenuItem.Text = "Browser";
            // 
            // MM_NeedUrbanDictionary
            // 
            MM_NeedUrbanDictionary.CheckOnClick = true;
            MM_NeedUrbanDictionary.Name = "MM_NeedUrbanDictionary";
            MM_NeedUrbanDictionary.Size = new System.Drawing.Size(502, 34);
            MM_NeedUrbanDictionary.Text = "Need Urban Dictionary [Deprecated]";
            MM_NeedUrbanDictionary.Visible = false;
            MM_NeedUrbanDictionary.Click += MM_NeedUrbanDictionary_Click;
            // 
            // loginToOALDToolStripMenuItem
            // 
            loginToOALDToolStripMenuItem.Name = "loginToOALDToolStripMenuItem";
            loginToOALDToolStripMenuItem.Size = new System.Drawing.Size(502, 34);
            loginToOALDToolStripMenuItem.Text = "Login to OALD  [Deprecated]";
            loginToOALDToolStripMenuItem.Visible = false;
            loginToOALDToolStripMenuItem.Click += loginToOALDToolStripMenuItem_Click;
            // 
            // MM_LoginToOALDOnStart
            // 
            MM_LoginToOALDOnStart.CheckOnClick = true;
            MM_LoginToOALDOnStart.Name = "MM_LoginToOALDOnStart";
            MM_LoginToOALDOnStart.Size = new System.Drawing.Size(502, 34);
            MM_LoginToOALDOnStart.Text = "Login to OALD on Start  [Deprecated]";
            MM_LoginToOALDOnStart.Visible = false;
            MM_LoginToOALDOnStart.Click += MM_LoginToOALDOnStart_Click;
            // 
            // setOALDCredentialsToolStripMenuItem
            // 
            setOALDCredentialsToolStripMenuItem.Name = "setOALDCredentialsToolStripMenuItem";
            setOALDCredentialsToolStripMenuItem.Size = new System.Drawing.Size(502, 34);
            setOALDCredentialsToolStripMenuItem.Text = "Set OALD Credentials  [Deprecated]";
            setOALDCredentialsToolStripMenuItem.Visible = false;
            setOALDCredentialsToolStripMenuItem.Click += setOALDCredentialsToolStripMenuItem_Click;
            // 
            // MM_ForceLoadFromBrowseField
            // 
            MM_ForceLoadFromBrowseField.Name = "MM_ForceLoadFromBrowseField";
            MM_ForceLoadFromBrowseField.Size = new System.Drawing.Size(502, 34);
            MM_ForceLoadFromBrowseField.Text = "Force Load From Browse Field";
            MM_ForceLoadFromBrowseField.Click += MM_ForceLoadFromBrowseField_Click;
            // 
            // MM_SpeakOnBrowsingOALD
            // 
            MM_SpeakOnBrowsingOALD.Checked = true;
            MM_SpeakOnBrowsingOALD.CheckOnClick = true;
            MM_SpeakOnBrowsingOALD.CheckState = System.Windows.Forms.CheckState.Checked;
            MM_SpeakOnBrowsingOALD.Name = "MM_SpeakOnBrowsingOALD";
            MM_SpeakOnBrowsingOALD.Size = new System.Drawing.Size(502, 34);
            MM_SpeakOnBrowsingOALD.Text = "Click on All Items (Current Browser)  [Deprecated]";
            MM_SpeakOnBrowsingOALD.Visible = false;
            MM_SpeakOnBrowsingOALD.Click += MM_SpeakOnBrowsingOALD_Click;
            // 
            // MM_ActivateTabsAfterAppStarts
            // 
            MM_ActivateTabsAfterAppStarts.Name = "MM_ActivateTabsAfterAppStarts";
            MM_ActivateTabsAfterAppStarts.Size = new System.Drawing.Size(502, 34);
            MM_ActivateTabsAfterAppStarts.Text = "Activate Tabs After App Starts";
            MM_ActivateTabsAfterAppStarts.Click += activateTabsAfterAppStartsToolStripMenuItem_Click;
            // 
            // historyToolStripMenuItem
            // 
            historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MM_save, MM_saveAsCopy, toolStripSeparator2, MM_showCurrentCategory, MM_showChronologically, MM_showOnlyTodayEntries, MM_UseDateFilter, toolStripSeparator3, MM_UseReverseOrder, MM_AutoSortByDate, toolStripSeparator1, MM_Update });
            historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            historyToolStripMenuItem.Size = new System.Drawing.Size(85, 29);
            historyToolStripMenuItem.Text = "History";
            // 
            // MM_save
            // 
            MM_save.Name = "MM_save";
            MM_save.Size = new System.Drawing.Size(340, 34);
            MM_save.Text = "Save";
            MM_save.Click += saveToolStripMenuItem_Click;
            // 
            // MM_saveAsCopy
            // 
            MM_saveAsCopy.Name = "MM_saveAsCopy";
            MM_saveAsCopy.Size = new System.Drawing.Size(340, 34);
            MM_saveAsCopy.Text = "Save As Copy";
            MM_saveAsCopy.Click += saveAsCopyToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(337, 6);
            // 
            // MM_showCurrentCategory
            // 
            MM_showCurrentCategory.Checked = true;
            MM_showCurrentCategory.CheckOnClick = true;
            MM_showCurrentCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            MM_showCurrentCategory.Name = "MM_showCurrentCategory";
            MM_showCurrentCategory.Size = new System.Drawing.Size(340, 34);
            MM_showCurrentCategory.Text = "Show Current Category Only";
            MM_showCurrentCategory.Click += showCurrentCategoryToolStripMenuItem_Click;
            // 
            // MM_showChronologically
            // 
            MM_showChronologically.CheckOnClick = true;
            MM_showChronologically.Name = "MM_showChronologically";
            MM_showChronologically.Size = new System.Drawing.Size(340, 34);
            MM_showChronologically.Text = "Show Chronologically";
            MM_showChronologically.CheckedChanged += MM_showChronologically_CheckedChanged;
            MM_showChronologically.Click += showChronoToolStripMenuItem_Click;
            // 
            // MM_showOnlyTodayEntries
            // 
            MM_showOnlyTodayEntries.CheckOnClick = true;
            MM_showOnlyTodayEntries.Name = "MM_showOnlyTodayEntries";
            MM_showOnlyTodayEntries.Size = new System.Drawing.Size(340, 34);
            MM_showOnlyTodayEntries.Text = "Show Only Today Entries";
            MM_showOnlyTodayEntries.Click += MM_showOnlyTodayEntries_Click;
            // 
            // MM_UseDateFilter
            // 
            MM_UseDateFilter.CheckOnClick = true;
            MM_UseDateFilter.Name = "MM_UseDateFilter";
            MM_UseDateFilter.Size = new System.Drawing.Size(340, 34);
            MM_UseDateFilter.Text = "Use Date filter";
            MM_UseDateFilter.Click += MM_UseDateFilter_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(337, 6);
            // 
            // MM_UseReverseOrder
            // 
            MM_UseReverseOrder.Checked = true;
            MM_UseReverseOrder.CheckOnClick = true;
            MM_UseReverseOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            MM_UseReverseOrder.Name = "MM_UseReverseOrder";
            MM_UseReverseOrder.Size = new System.Drawing.Size(340, 34);
            MM_UseReverseOrder.Text = "Use Reverse Order";
            MM_UseReverseOrder.Click += MM_UseReverseOrder_Click;
            // 
            // MM_AutoSortByDate
            // 
            MM_AutoSortByDate.Checked = true;
            MM_AutoSortByDate.CheckOnClick = true;
            MM_AutoSortByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            MM_AutoSortByDate.Name = "MM_AutoSortByDate";
            MM_AutoSortByDate.Size = new System.Drawing.Size(340, 34);
            MM_AutoSortByDate.Text = "Auto Sort By Date";
            MM_AutoSortByDate.Click += MM_AutoSortByDate_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(337, 6);
            // 
            // MM_Update
            // 
            MM_Update.Name = "MM_Update";
            MM_Update.Size = new System.Drawing.Size(340, 34);
            MM_Update.Text = "Update";
            MM_Update.Click += updateToolStripMenuItem_Click;
            // 
            // styleToolStripMenuItem
            // 
            styleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { darkToolStripMenuItem, lightToolStripMenuItem });
            styleToolStripMenuItem.Name = "styleToolStripMenuItem";
            styleToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            styleToolStripMenuItem.Text = "Style";
            // 
            // darkToolStripMenuItem
            // 
            darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            darkToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            darkToolStripMenuItem.Text = "Dark";
            darkToolStripMenuItem.Click += darkToolStripMenuItem_Click;
            // 
            // lightToolStripMenuItem
            // 
            lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            lightToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            lightToolStripMenuItem.Text = "Light";
            lightToolStripMenuItem.Click += lightToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MM_HotKeyEnabled });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // MM_HotKeyEnabled
            // 
            MM_HotKeyEnabled.Checked = true;
            MM_HotKeyEnabled.CheckOnClick = true;
            MM_HotKeyEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            MM_HotKeyEnabled.Name = "MM_HotKeyEnabled";
            MM_HotKeyEnabled.Size = new System.Drawing.Size(270, 34);
            MM_HotKeyEnabled.Text = "Hot Key Enabled";
            // 
            // windowToolStripMenuItem
            // 
            windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MM_ReturnDesktop, toolStripMenuItem1 });
            windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            windowToolStripMenuItem.Size = new System.Drawing.Size(94, 29);
            windowToolStripMenuItem.Text = "Window";
            // 
            // MM_ReturnDesktop
            // 
            MM_ReturnDesktop.Name = "MM_ReturnDesktop";
            MM_ReturnDesktop.Size = new System.Drawing.Size(332, 34);
            MM_ReturnDesktop.Text = "Return To Previous Desktop";
            MM_ReturnDesktop.Click += returnToPreviousDesktopToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(332, 34);
            toolStripMenuItem1.Text = "test";
            toolStripMenuItem1.Visible = false;
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // MM_Help
            // 
            MM_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MM_About, MM_ShortcutsHelp });
            MM_Help.Name = "MM_Help";
            MM_Help.Size = new System.Drawing.Size(65, 29);
            MM_Help.Text = "Help";
            // 
            // MM_About
            // 
            MM_About.Name = "MM_About";
            MM_About.Size = new System.Drawing.Size(270, 34);
            MM_About.Text = "About";
            MM_About.Click += MM_About_Click;
            // 
            // MM_ShortcutsHelp
            // 
            MM_ShortcutsHelp.Name = "MM_ShortcutsHelp";
            MM_ShortcutsHelp.Size = new System.Drawing.Size(270, 34);
            MM_ShortcutsHelp.Text = "Shortcuts";
            MM_ShortcutsHelp.Click += MM_ShortcutsHelp_Click;
            // 
            // cbxCategory
            // 
            cbxCategory.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cbxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            cbxCategory.FormattingEnabled = true;
            cbxCategory.IntegralHeight = false;
            cbxCategory.Location = new System.Drawing.Point(1428, 31);
            cbxCategory.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            cbxCategory.MaxDropDownItems = 20;
            cbxCategory.Name = "cbxCategory";
            cbxCategory.Size = new System.Drawing.Size(464, 37);
            cbxCategory.TabIndex = 5;
            cbxCategory.TabStop = false;
            cbxCategory.SelectedIndexChanged += cbxCategory_SelectedIndexChanged;
            cbxCategory.KeyDown += cbxCategory_KeyDown;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { stError, mainProgress, stHistoryCountShown });
            statusStrip1.Location = new System.Drawing.Point(0, 1146);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 23, 0);
            statusStrip1.Size = new System.Drawing.Size(1902, 32);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // stError
            // 
            stError.BackColor = System.Drawing.Color.Green;
            stError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            stError.DoubleClickEnabled = true;
            stError.ForeColor = System.Drawing.Color.White;
            stError.Name = "stError";
            stError.Size = new System.Drawing.Size(33, 25);
            stError.Text = "JS ";
            stError.DoubleClick += stError_DoubleClick;
            // 
            // mainProgress
            // 
            mainProgress.Name = "mainProgress";
            mainProgress.Size = new System.Drawing.Size(167, 24);
            mainProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            mainProgress.Visible = false;
            // 
            // stHistoryCountShown
            // 
            stHistoryCountShown.Name = "stHistoryCountShown";
            stHistoryCountShown.Size = new System.Drawing.Size(122, 25);
            stHistoryCountShown.Text = "History Status";
            // 
            // dtBegin
            // 
            dtBegin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            dtBegin.Location = new System.Drawing.Point(912, 6);
            dtBegin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            dtBegin.Name = "dtBegin";
            dtBegin.Size = new System.Drawing.Size(331, 31);
            dtBegin.TabIndex = 7;
            dtBegin.TabStop = false;
            dtBegin.Visible = false;
            // 
            // dtEnd
            // 
            dtEnd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            dtEnd.Location = new System.Drawing.Point(1291, 6);
            dtEnd.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            dtEnd.Name = "dtEnd";
            dtEnd.Size = new System.Drawing.Size(331, 31);
            dtEnd.TabIndex = 8;
            dtEnd.TabStop = false;
            dtEnd.Visible = false;
            // 
            // lblFrom
            // 
            lblFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lblFrom.AutoSize = true;
            lblFrom.Location = new System.Drawing.Point(852, 11);
            lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new System.Drawing.Size(54, 25);
            lblFrom.TabIndex = 9;
            lblFrom.Text = "From";
            lblFrom.Visible = false;
            // 
            // lblTo
            // 
            lblTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lblTo.AutoSize = true;
            lblTo.Location = new System.Drawing.Point(1252, 9);
            lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new System.Drawing.Size(30, 25);
            lblTo.TabIndex = 10;
            lblTo.Text = "To";
            lblTo.Visible = false;
            // 
            // btnByDateFilter
            // 
            btnByDateFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            btnByDateFilter.Location = new System.Drawing.Point(1631, 0);
            btnByDateFilter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnByDateFilter.Name = "btnByDateFilter";
            btnByDateFilter.Size = new System.Drawing.Size(124, 44);
            btnByDateFilter.TabIndex = 11;
            btnByDateFilter.TabStop = false;
            btnByDateFilter.Text = "Show";
            btnByDateFilter.UseVisualStyleBackColor = true;
            btnByDateFilter.Visible = false;
            btnByDateFilter.Click += btnByDateFilter_Click;
            // 
            // btnClearInput
            // 
            btnClearInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearInput.Location = new System.Drawing.Point(704, 31);
            btnClearInput.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnClearInput.Name = "btnClearInput";
            btnClearInput.Size = new System.Drawing.Size(64, 37);
            btnClearInput.TabIndex = 12;
            btnClearInput.TabStop = false;
            btnClearInput.Text = "Clear";
            btnClearInput.UseVisualStyleBackColor = true;
            btnClearInput.Click += btnClearInput_Click;
            // 
            // txtFindText
            // 
            txtFindText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtFindText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            txtFindText.Location = new System.Drawing.Point(776, 31);
            txtFindText.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            txtFindText.Name = "txtFindText";
            txtFindText.Size = new System.Drawing.Size(572, 35);
            txtFindText.TabIndex = 2;
            txtFindText.KeyDown += txtFindText_KeyDown;
            txtFindText.KeyUp += txtFindText_KeyUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Left;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label1.Location = new System.Drawing.Point(4, 0);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(83, 25);
            label1.TabIndex = 14;
            label1.Text = "Browse:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Left;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label2.Location = new System.Drawing.Point(776, 0);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 25);
            label2.TabIndex = 15;
            label2.Text = "Find:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label3.Location = new System.Drawing.Point(1428, 0);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(98, 25);
            label3.TabIndex = 16;
            label3.Text = "Category:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(controlPanel, 0, 0);
            tableLayoutPanel1.Controls.Add(tabControl1, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 33);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.479964F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.52003F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1902, 1113);
            tableLayoutPanel1.TabIndex = 17;
            // 
            // controlPanel
            // 
            controlPanel.AutoSize = true;
            controlPanel.ColumnCount = 5;
            controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.66871F));
            controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.3313F));
            controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 471F));
            controlPanel.Controls.Add(btnClearFind, 3, 1);
            controlPanel.Controls.Add(label1, 0, 0);
            controlPanel.Controls.Add(txtToSearch, 0, 1);
            controlPanel.Controls.Add(label2, 2, 0);
            controlPanel.Controls.Add(btnClearInput, 1, 1);
            controlPanel.Controls.Add(txtFindText, 2, 1);
            controlPanel.Controls.Add(cbxCategory, 4, 1);
            controlPanel.Controls.Add(label3, 4, 0);
            controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            controlPanel.Location = new System.Drawing.Point(3, 4);
            controlPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            controlPanel.Name = "controlPanel";
            controlPanel.RowCount = 2;
            controlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            controlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            controlPanel.Size = new System.Drawing.Size(1896, 75);
            controlPanel.TabIndex = 3;
            // 
            // btnClearFind
            // 
            btnClearFind.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearFind.Location = new System.Drawing.Point(1356, 31);
            btnClearFind.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnClearFind.Name = "btnClearFind";
            btnClearFind.Size = new System.Drawing.Size(64, 35);
            btnClearFind.TabIndex = 18;
            btnClearFind.TabStop = false;
            btnClearFind.Text = "Clear";
            btnClearFind.UseVisualStyleBackColor = true;
            btnClearFind.Click += btnClearFind_Click;
            // 
            // tabControl1
            // 
            tabControl1.DisplayStyleProvider.BackGroundColor = System.Drawing.Color.FromArgb(207, 207, 207);
            tabControl1.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            tabControl1.DisplayStyleProvider.FocusTrack = true;
            tabControl1.DisplayStyleProvider.HotTrack = true;
            tabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            tabControl1.DisplayStyleProvider.Opacity = 1F;
            tabControl1.DisplayStyleProvider.Overlap = 0;
            tabControl1.DisplayStyleProvider.Padding = new System.Drawing.Point(6, 3);
            tabControl1.DisplayStyleProvider.Radius = 2;
            tabControl1.DisplayStyleProvider.ShowTabCloser = false;
            tabControl1.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            tabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.HotTrack = true;
            tabControl1.Location = new System.Drawing.Point(3, 86);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1896, 1024);
            tabControl1.TabIndex = 4;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1902, 1178);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnByDateFilter);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            Controls.Add(dtEnd);
            Controls.Add(dtBegin);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            MinimumSize = new System.Drawing.Size(1889, 56);
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            controlPanel.ResumeLayout(false);
            controlPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem MM_SpeakOnBrowsingOALD;
        private System.Windows.Forms.ToolStripMenuItem showJSErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel stError;
        private System.Windows.Forms.ToolStripMenuItem MM_ActivateTabsAfterAppStarts;
    }
}

