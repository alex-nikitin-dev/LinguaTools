using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using TestProj.Properties;

namespace TestProj
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            CefSettings settingsBrowser = new CefSettings
            {
                Locale = "en-US,en",
                AcceptLanguageList = "en-US,en",
                
            };
            Cef.Initialize(settingsBrowser);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
        }

        private void txtToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoBrowsers(txtToSearch.Text);
            }
        }

        string GetCategory()
        {
            if (string.IsNullOrEmpty(cbxCategory.Text))
                return "general";
            return cbxCategory.Text;
        }


        string PrepareTextToProceed(string text)
        {
            text = text.Trim(',', '.', '!', ':', ';','-','"','\'',' ','\n');
            text = text.Replace(Environment.NewLine, " ");
            text = new Regex("[ ]{2,}", RegexOptions.None).Replace( text, " ");
            return text;
        }

        void GoOALD(string text)
        {
            _browsers[0].Load($"https://www.oxfordlearnersdictionaries.com/search/english/?q={text}");
        }

        void GoGoogleTranslate(string text)
        {
            _browsers[1].Load($"https://translate.google.am/?hl=en#view=home&op=translate&sl=en&tl=ru&text={text}");
        }

        void GoUrbanDictionary(string text)
        {
             _browsers[2].Load($"https://www.urbandictionary.com/define.php?term={text}");
        }

        void GoBrowsers(string text, bool saveHistory = true)
        {
            text = PrepareTextToProceed(text);
            if (string.IsNullOrEmpty(text)) return;

            txtToSearch.Text = text;
            if(saveHistory)
                _history.AddHistoryItem(text, GetCategory());
            
            GoOALD(text);
            GoGoogleTranslate(text);
            if(MM_NeedUrbanDictionary.Checked) GoUrbanDictionary(text);
        }

        private List<ChromiumWebBrowser> _browsers;
        private History _history;
        private int? _hotKeyId;

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;

            InitHotKeys();
            InitBrowsers();
            InitTabs();
            InitHistory();
            SetDateTimeFilterNow();
            FillCategoriesComboBox();
            InitHistoryListView();
            FillHistoryListView();
            //LoginToOALD();
            GoOALD("test");
            GoGoogleTranslate("test");
            SetThemeFromSettings();
        }

        private void SetThemeFromSettings()
        {
            if (Settings.Default.DarkTheme)
                SetColorTheme(ColorTheme.Dark);
        }

        private void InitHotKeys()
        {
            _hotKeyId = HotKeyManager.RegisterHotKey(Keys.X, KeyModifiers.Control | KeyModifiers.Shift);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
        }

        private void InitTabs()
        {
            var table = new TableLayoutPanel() { ColumnCount = 2, RowCount = 1, Dock = DockStyle.Fill };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            table.Controls.Add(_browsers[0], 0, 0);
            table.Controls.Add(_browsers[1], 1, 0);

            tabControl1.Font = new Font(tabControl1.Font.FontFamily, 12, FontStyle.Regular);
            tabControl1.TabPages.Add("OALD");
            tabControl1.TabPages.Add("Urban");
            tabControl1.TabPages[0].Controls.Add(table);
            tabControl1.TabPages[1].Controls.Add(_browsers[2]);
        }

        private void InitBrowsers()
        {
            _browsers = new List<ChromiumWebBrowser>();
            for (int i = 0; i < 3; i++)
            {
                _browsers.Add(new ChromiumWebBrowser(""));
                _browsers[i].Dock = DockStyle.Fill;
                _browsers[i].FrameLoadEnd += MainForm_FrameLoadEnd;
            }

            _browsers[0].FrameLoadEnd += OALD_Browser_LoadingStateChanged;
        }

        private void InitHistory()
        {
            _history = new History(_dateTimeFormat,
                _dateTimeFormatForFile,
                Settings.Default.DataPath,
                !Settings.Default.ReverseOrderHistory);

            _history.NewCategoryAdded += _history_NewCategoryAdded;
            _history.NewHistoryItemAdded += _history_NewHistoryItemAdded;
            _history.LoadData(Settings.Default.DataPath);
        }

       private void _history_NewHistoryItemAdded(object sender, NewHistoryItemAddedEventArgs e)
        {
            AddItemAndGroupToHistoryListView(e.HistoryItem,e.IsCategoryNew);
        }


        private delegate void AddHistoryItemToListViewDelegate(HistoryDataItem historyItem,bool isCategoryNew);

        private void AddItemAndGroupToHistoryListView(HistoryDataItem historyItem, bool isCategoryNew)
        {
            if (InvokeRequired)
            {
                Invoke(new AddHistoryItemToListViewDelegate(AddItemAndGroupToHistoryListView));
                return;
            }

            if (isCategoryNew && AreTheGroupsNeeded())
            {
                AddGroupToHistoryListView(historyItem.Category);
            }

            AddItemToHistoryListView(historyItem, _history.EnumerateForward);
            //_lstHistory.Update();
            HistoryListViewResize();
        }

        private void AddGroupToHistoryListView(string groupName)
        {
            var group = new ListViewGroup(groupName, groupName);
            _lstHistory.Groups.Add(group);
        }

        private void AddItemToHistoryListView(HistoryDataItem historyItem,bool insertToTheEnd)
        {
            var li = new ListViewItem() { Text = historyItem.Phrase };
            li.SubItems.Add(historyItem.Category);
            li.SubItems.Add(historyItem.Date.ToString(_dateTimeFormat, CultureInfo.InvariantCulture));

            if (AreTheGroupsNeeded())
                li.Group = _lstHistory.Groups[historyItem.Category];

            var index = insertToTheEnd ? _lstHistory.Items.Count : 0;


            _lstHistory.Items.Insert(index, li);

            //li.Group.Items.Insert(index, li);
            //_lstHistory.Groups[li.Group.Name].Items.Insert(index, li);
        }


        bool AreTheGroupsNeeded()
        {
            return !MM_showChronologically.Checked;
        }

        private void _history_NewCategoryAdded(object sender, NewCategoryAddedEventArgs e)
        {
            AddCategoryCombobox(e.Category);
        }

        private void MainForm_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (_currentColorTheme == ColorTheme.Dark)
            {
                var browser = (ChromiumWebBrowser)sender;
                SetBrowserColors(browser, "#282828", "#dadada");
            }
        }

        enum ColorTheme
        {
            Dark,
            Light
        }

        ColorTheme _currentColorTheme = ColorTheme.Light;

        void ReloadAllBrowsers()
        {
            foreach (var browser in _browsers)
            {
                if (browser.IsBrowserInitialized)
                {
                    browser.Reload();
                }
            }
        }

        void SetColorsRecursively(Control parent,Color backColor, Color foreColor)
        {
            parent.BackColor = backColor;
            parent.ForeColor = foreColor;
            if (!parent.HasChildren) return;

            foreach (Control child in parent.Controls)
            {
                SetColorsRecursively(child, backColor, foreColor);
            }
        }

        void SetColorTheme(ColorTheme theme)
        {
            _currentColorTheme = theme;

            switch (theme)
            {
                case ColorTheme.Dark:
                    SetColorsRecursively(this, ColorTranslator.FromHtml("#282828"), ColorTranslator.FromHtml("#dadada"));
                    break;
                case ColorTheme.Light:
                    SetColorsRecursively(this, SystemColors.Control, SystemColors.ControlText);
                    break;
            }

            ReloadAllBrowsers();

            Settings.Default.DarkTheme = theme == ColorTheme.Dark;
            Settings.Default.Save();
        }

        void DeleteAdOALD(ChromiumWebBrowser browser)
        {
            browser.EvaluateScriptAsync($@"
            polls = document.querySelectorAll('[id ^= ""ad_""]');
            Array.prototype.forEach.call(polls, callback);

            function callback(element, iterator)
            {{
                element.remove();
            }}
            ");
        }

        void SetBrowserColors(ChromiumWebBrowser browser, string backColor,string foreColor)
        {
            browser.EvaluateScriptAsync($@"function SetBackgroundForAll(backColor, foreColor){{
                var elements = document.querySelectorAll('*');
                for (var i = 0; i < elements.length; i++) {{
                elements[i].style.backgroundColor=backColor;
                elements[i].style.color = foreColor;
                }}
            }}
            SetBackgroundForAll('{backColor}','{foreColor}');

            var style1 = document.createElement('style');
            style1.innerHTML = `
                * {{
                    color: {foreColor}!important;
                    background-color: {backColor};
                 }}
              `;
            document.head.appendChild(style1);
            ");
        }

        private ListView _lstHistory;
        private void InitHistoryListView()
        {
            tabControl1.TabPages.Add("tabHistory", "History");
            _lstHistory = new ListView()
            {
                View = View.Details, 
                Dock = DockStyle.Fill, 
                Font = new Font(Font.FontFamily, 12, FontStyle.Regular),
                FullRowSelect = true
            };
          
            tabControl1.TabPages["tabHistory"].Controls.Add(_lstHistory);
            _lstHistory.Columns.Add("phrase", "phrase");
            _lstHistory.Columns.Add("category", "category");
            _lstHistory.Columns.Add("date", "date");
            
            var menuItems = new List<MenuItem>
            {
                new MenuItem("Update", UpdateHistoryListView),
                new MenuItem("Delete", DeleteHistoryItem)
            };

            _lstHistory.ContextMenu = new ContextMenu(menuItems.ToArray());
            _lstHistory.MouseDoubleClick += LstHistoryOnMouseDoubleClick;
        }

        private void LstHistoryOnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_lstHistory.SelectedItems.Count == 0) return;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            cbxCategory.Text = _lstHistory.SelectedItems[0].SubItems[1].Text;
            GoBrowsers(_lstHistory.SelectedItems[0].Text);
        }

        private void DeleteHistoryItem(object sender, EventArgs e)
        {
            if (_lstHistory.SelectedItems.Count == 0) return;
            var sure = MessageBox.Show(@"The selected entry will be deleted from the history. Are you sure?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sure == DialogResult.Yes)
            {
                foreach (ListViewItem lstItem in _lstHistory.SelectedItems)
                {
                    var phrase = lstItem.Text;
                    var category = lstItem.SubItems[1].Text;
                    _history.DeleteHistoryItem(phrase, category);
                }

                FillHistoryListView();
            }
        }

        private void UpdateHistoryListView(object sender, EventArgs e)
        {
            UpdateHistoryListView();
        }

        void UpdateHistoryListView()
        {
            _history.LoadData(Settings.Default.DataPath);
            FillHistoryListView();
            FillCategoriesComboBox();
        }

        private void AddGroupsToHistoryListView(string currentCategory)
        {
            foreach (var category in _history.Categories)
            {
                if (currentCategory != null && string.CompareOrdinal(currentCategory, category) != 0) continue;
                AddGroupToHistoryListView(category);
            }
        }

        private void AddItemsToHistoryListView(string currentCategory,bool filterByDate, DateTime beginDate, DateTime endDate)
        {
            if (beginDate > endDate) throw new ArgumentException("beginDate > endDate");
            foreach (var item in _history)
            {
                if (currentCategory != null && string.CompareOrdinal(currentCategory, item.Category) != 0) continue;

                if (filterByDate && (item.Date < beginDate || item.Date > endDate)) continue;

                AddItemToHistoryListView(item, true);
            }
            HistoryListViewResize();
        }

        private string GetCurrentCategoryForListView(bool showCurrentCategoryOnly, bool chronologically)
        {
            string currentCategory = null;
            if (showCurrentCategoryOnly && !chronologically)
                currentCategory = GetCategory();

            return currentCategory;
        }


        private bool IsFilterByDateNeed()
        {
            return MM_UseDateFilter.Checked || MM_showOnlyTodayEntries.Checked;
        }

        private void FillHistoryListView()
        {
            FillHistoryListView(MM_showCurrentCategory.Checked,MM_showChronologically.Checked,IsFilterByDateNeed(),dtBegin.Value, dtEnd.Value);
        }

        private void FillHistoryListView(bool showCurrentCategoryOnly, bool chronologically, bool filterByDate, DateTime beginDate, DateTime endDate)
        {
            _lstHistory.Items.Clear();
            var currentCategory = GetCurrentCategoryForListView(showCurrentCategoryOnly, chronologically);
            
            if (AreTheGroupsNeeded()) 
                AddGroupsToHistoryListView(currentCategory);

            AddItemsToHistoryListView(currentCategory, filterByDate, beginDate, endDate);
        }

        private void HistoryListViewResize()
        {
            _lstHistory.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            _lstHistory.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            _lstHistory.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
       
        private string _dateTimeFormat = @"dd.MMMM.yyyy HH:mm:ss";
        private string _dateTimeFormatForFile = @"dd.MMMM.yyyy_HH.mm.ss";

        private delegate void AddCategoryComboboxDelegate(string category);

        private void AddCategoryCombobox(string category)
        {
            if (InvokeRequired)
            {
                Invoke(new AddCategoryComboboxDelegate(AddCategoryCombobox));
                return;
            }

            var index = cbxCategory.Items.Add(category);
            cbxCategory.SelectedIndex = index;
        }

        private void FillCategoriesComboBox()
        {
            cbxCategory.Items.Clear();

            foreach (var category in _history.Categories)
            {
                cbxCategory.Items.Add(category);
            }

            cbxCategory.Sorted = true;
        }

        private void LoginToOALD()
        {
            _preparingOALD = true;
            _browsers[0].Load("https://www.oxfordlearnersdictionaries.com/account/login");
        }
       
        private void OALD_Browser_LoadingStateChanged(object sender, FrameLoadEndEventArgs e)
        {
            if (_preparingOALD && e.Frame.IsMain)
            {
                _preparingOALD = false;
                PrepareOALD((ChromiumWebBrowser)sender);
            }

            DeleteAdOALD((ChromiumWebBrowser)sender);
        }

        private bool _preparingOALD;
        static void PrepareOALD(ChromiumWebBrowser browser)
        {
            browser.EvaluateScriptAsync(@"function setItem(itemId,itemValue){
            document.getElementById(itemId).value = itemValue;
            }
            
            setItem('j_username','alex.nikitin.gm@gmail.com');
            setItem('j_password','txFfNTfAvgXDQ8q');
           // document.getElementsByTagName('form')[0].submit();
           document.getElementsByClassName('mdl-btn mdl-btn-main mdl-btn-left')[0].click();
            ");
        }

        delegate void HotKeyPressedDelegate(object sender, HotKeyEventArgs e);

        void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new HotKeyPressedDelegate(HotKeyManager_HotKeyPressed), sender, e);
                return;
            }

            if (!chkHotKeyEnabled.Checked) return;
            GoBrowsers(Clipboard.GetText());
        }

        private async Task SaveHistoryDialog()
        {
            var wannaSave = MessageBox.Show(@"Do you want to save data before exit?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (wannaSave == DialogResult.Yes)
            {
                await _history.SaveHistoryToFile();
            }
            else
            {
                await _history.SaveHistoryToFileAsACopy();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_history.HistoryHasBeenChanged) 
                _ = SaveHistoryDialog();
            
            if (_hotKeyId != null)
                HotKeyManager.UnregisterHotKey(_hotKeyId.Value);
        }

        private void loginToOALDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginToOALD();
        }

        private void cbxCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cbx = (ComboBox) sender;
                if (!cbx.Items.Contains(cbx.Text))
                {
                    cbx.Items.Add(cbx.Text);
                }
            }
        }

        private void ReloadHistoryAndRefillListViewDialog()
        {
            var sure = MessageBox.Show(@"You will lose all unsaved data. Are you sure?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sure == DialogResult.Yes)
            {
                _history.LoadData(Settings.Default.DataPath);
                FillHistoryListView();
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadHistoryAndRefillListViewDialog();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetColorTheme(ColorTheme.Dark);
        }

        private void MM_NeedUrbanDictionary_Click(object sender, EventArgs e)
        {
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetColorTheme(ColorTheme.Light);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = _history.SaveHistoryToFile();
        }

        private void saveAsCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = _history.SaveHistoryToFileAsACopy();
        }

        private void showCurrentCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillHistoryListView();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MM_showCurrentCategory.Checked && !MM_showChronologically.Checked)
                FillHistoryListView();
        }

        private void showChronoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillHistoryListView();
        }

        private void MM_showChronologically_CheckedChanged(object sender, EventArgs e)
        {
            MM_showCurrentCategory.Enabled = !MM_showChronologically.Checked;
            //_history.EnumerateForward = !MM_showChronologically.Checked;
        }

        private void showDatainiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.Default.DataPath);
        }

        private void showTheAppFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath);
        }

        private void SetDateTimeFilter(DateTime begin, DateTime end)
        {
            dtBegin.Value = begin;
            dtEnd.Value = end;
        }

        private void SetDateTimeFilterNow()
        {
            SetDateTimeFilter(DateTime.Now, DateTime.Now);
        }

        private void MM_showOnlyTodayEntries_Click(object sender, EventArgs e)
        {
            if (MM_showOnlyTodayEntries.Checked)
            {
                MM_UseDateFilter.Checked = false;
                SetDateTimeFilterNow();
                SwitchVisibleDateTimeFilter(false);
            }

            FillHistoryListView();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MM_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }


        private void SwitchVisibleDateTimeFilter(bool needDateFilter)
        {
            dtBegin.Visible = needDateFilter;
            dtEnd.Visible = needDateFilter;
            lblFrom.Visible = needDateFilter;
            lblTo.Visible = needDateFilter;
            btnByDateFilter.Visible = needDateFilter;
        }

        private void MM_UseDateFilter_Click(object sender, EventArgs e)
        {
            SwitchVisibleDateTimeFilter(MM_UseDateFilter.Checked);

            if (MM_UseDateFilter.Checked)
            {
                MM_showOnlyTodayEntries.Checked = false;
            }
            else
            {
                FillHistoryListView();
            }
                
        }

        private void btnByDateFilter_Click(object sender, EventArgs e)
        {
            FillHistoryListView();
        }

        private void ReverseOrderOptionSave(bool reverseOrder)
        {
            Settings.Default.ReverseOrderHistory = reverseOrder;
            Settings.Default.Save();
        }

        private void MM_UseReverseOrder_Click(object sender, EventArgs e)
        {
            _history.EnumerateForward = !MM_UseReverseOrder.Checked;
            ReverseOrderOptionSave(MM_UseReverseOrder.Checked);
            FillHistoryListView();
        }

        private void MM_ShowTasks_Click(object sender, EventArgs e)
        {
            Process.Start("Tasks.docx");
        }

        private void txtToSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
