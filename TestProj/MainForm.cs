using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
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
            var settingsBrowser = new CefSettings
            {
                Locale = "en-US,en",
                AcceptLanguageList = "en-US,en",
                
            };
            Cef.Initialize(settingsBrowser);
            CefSharpSettings.WcfEnabled = true;
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
        }

        private void txtBrowse_KeyDown(object sender, KeyEventArgs e)
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

        private Color _foreColor;
        private Color _backColor;

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;
            _foreColor = ForeColor;
            _backColor = BackColor;

            LoadSettings();
            InitHotKeys();
            InitBrowsers();
            InitTabs();
            InitHistory();
            SetDateTimeFilterNow();
            FillCategoriesComboBox();
            InitHistoryListView();
            FillHistoryListView();
            GoGoogleTranslate("");
            if(MM_LoginToOALDOnStart.Checked)
                LoginToOALD();
            else
                GoOALD("");

            SetThemeFromSettings();
        }



        private void LoadSettings()
        {
            var stt = Settings.Default;
            MM_LoginToOALDOnStart.Checked = stt.OALDLoginOnStart;
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
            tabControl1.TabPages.Add("OALD","OALD");
            tabControl1.TabPages["OALD"].Controls.Add(table);
            tabControl1.TabPages.Add("tabHistory", "History");

            ShowUrban(MM_NeedUrbanDictionary.Checked);
        }

        public class BoundObject
        {
            public string Text;
            public event EventHandler<BrowserTextSelectedEventArgs> BrowserTextSelected;

            protected virtual void OnBrowserTextSelected(BrowserTextSelectedEventArgs e)
            {
                var handler = BrowserTextSelected;
                handler?.Invoke(this, e);
            }
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once IdentifierTypo
            public void onselect(string msg)
            {
                if (!string.IsNullOrEmpty(msg))
                    OnBrowserTextSelected(new BrowserTextSelectedEventArgs(msg));
            }
        }

        private readonly BoundObject _boundObject = new BoundObject();

        private void InitBrowsers()
        {
            _browsers = new List<ChromiumWebBrowser>();
            _boundObject.BrowserTextSelected += _boundObject_BrowserTextSelected;
            for (int i = 0; i < 3; i++)
            {
                _browsers.Add(new ChromiumWebBrowser(""));
                _browsers[i].Dock = DockStyle.Fill;
            }

            _browsers[0].JavascriptObjectRepository.Register("b1", _boundObject, false);
            _browsers[0].FrameLoadEnd += OALD_Browser_FrameLoadEnd;
            _browsers[0].LoadingStateChanged += OALD_Browser_LoadingStateChanged;
            _browsers[1].LoadingStateChanged += GTranslator_LoadingStateChanged;

            _browsers[0].KeyDown += OALD_Browser_KeyDown;
            _browsers[0].JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;

            _browsers[2].LoadingStateChanged += Urban_LoadingStateChanged;
        }

       

        private void OALD_Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C))
            {
                ClearTxtSearchAndFocus();
            }
        }

        private delegate void ClearTxtSearchAndFocusDelegate();

        private void ClearTxtSearchAndFocus()
        {
            if(InvokeRequired)
            {
                Invoke(new ClearTxtSearchAndFocusDelegate(ClearTxtSearchAndFocus));
                return;
            }

            txtToSearch.Clear();
            txtToSearch.Focus();
        }

        private delegate void BrowserTextSelectedDelegate(object sender, BrowserTextSelectedEventArgs e);

        private void _boundObject_BrowserTextSelected(object sender, BrowserTextSelectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new BrowserTextSelectedDelegate(_boundObject_BrowserTextSelected), sender, e);
                return;
            }

            GoGoogleTranslate(e.Text);
        }

        private void InitHistory()
        {
            _history = new History(_dateTimeFormat,
                _dateTimeFormatForFile,
                Settings.Default.DataPath,
                !Settings.Default.ReverseOrderHisroty);

            _history.NewCategoryAdded += _history_NewCategoryAdded;
            _history.NewHistoryItemAdded += _history_NewHistoryItemAdded;
            _history.HistoryItemHasBeenUpdated += _history_HistoryItemHasBeenUpdated;
            _history.LoadData(Settings.Default.DataPath);
        }

        private delegate void UpdateItemInHistoryListViewDelegate(HistoryDataItem oldItem, HistoryDataItem updatedItem);

        private void UpdateItemInHistoryListView(HistoryDataItem oldItem, HistoryDataItem updatedItem)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateItemInHistoryListViewDelegate(UpdateItemInHistoryListView), oldItem, updatedItem);
                return;
            }

            var li = FindHistoryListViewItem(oldItem.Phrase, oldItem.Category);
            if (li != null)
            {
                li.SubItems[0].Text = updatedItem.Phrase;
                li.SubItems[1].Text = updatedItem.Category;
                li.SubItems[2].Text = updatedItem.Date.ToString(_dateTimeFormat, CultureInfo.InvariantCulture);

                var isCategoryNew = string.CompareOrdinal(oldItem.Category,updatedItem.Category) != 0;
                AddCategoryIfNeed(isCategoryNew, updatedItem.Category);
                ProceedAfterAddOrUpdatedHistoryListView();
            }
        }

        private ListViewItem FindHistoryListViewItem(string phrase, string category)
        {
            foreach (ListViewItem item in _lstHistory.Items)
            {
                if (string.CompareOrdinal(item.SubItems[0].Text, phrase) == 0 &&
                   string.CompareOrdinal(item.SubItems[1].Text, category) == 0)
                {
                   return item;
                }
            }

            return null;
        }

        private void _history_HistoryItemHasBeenUpdated(object sender, HistoryItemHasBeenUpdatedArgs e)
        {
            UpdateItemInHistoryListView(e.OldItem, e.UpdatedItem);
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
                Invoke(new AddHistoryItemToListViewDelegate(AddItemAndGroupToHistoryListView),historyItem,isCategoryNew);
                return;
            }


            AddCategoryIfNeed(isCategoryNew, historyItem.Category);
            AddItemToHistoryListView(historyItem);
            ProceedAfterAddOrUpdatedHistoryListView();
        }

        private void ProceedAfterAddOrUpdatedHistoryListView()
        {
            SortHistoryListViewByDateIfNeeded();
            HistoryListViewResize();
            ShowHistoryCount();
        }

        private void AddCategoryIfNeed(bool isCategoryNew,string category)
        {
            if (isCategoryNew && AreTheGroupsNeeded())
            {
                AddGroupToHistoryListView(category);
            }
        }


        private SortOrder GetHistorySortOrderByDate()
        { 
            return MM_UseReverseOrder.Checked ? SortOrder.Descending : SortOrder.Ascending;
        }

        private void AddGroupToHistoryListView(string groupName)
        {
            var group = new ListViewGroup(groupName, groupName);
            _lstHistory.Groups.Add(group);
        }

        private void AddItemToHistoryListView(HistoryDataItem historyItem)
        {
            var li = new ListViewItem() { Text = historyItem.Phrase };
            li.SubItems.Add(historyItem.Category);
            li.SubItems.Add(historyItem.Date.ToString(_dateTimeFormat, CultureInfo.InvariantCulture));

            if (AreTheGroupsNeeded())
                li.Group = _lstHistory.Groups[historyItem.Category];
            
            _lstHistory.Items.Add(li);
        }

        private void SortHistoryListViewByDateIfNeeded()
        {
            if (MM_AutoSortByDate.Checked)
                SortHistoryListView(GetHistorySortOrderByDate(), 2);
        }

        private void SortHistoryListView(SortOrder sortOrder, int sortColumn)
        {
            _lvwColumnSorter.SortColumn = sortColumn;
            _lvwColumnSorter.Order = sortOrder;
            SortHistoryListView();
        }

        bool AreTheGroupsNeeded()
        {
            return !MM_showChronologically.Checked;
        }

        private void _history_NewCategoryAdded(object sender, NewCategoryAddedEventArgs e)
        {
            AddCategoryCombobox(e.Category);
        }

        private void GTranslator_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
                return;
            if(_currentColorTheme == ColorTheme.Dark)
            {
                var browser = (ChromiumWebBrowser)sender;
                SetBrowserColorsCSS(browser, _curCSSTheme);
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
            //if (!parent.HasChildren) return;

            foreach (Control child in parent.Controls)
            {
                SetColorsRecursively(child, backColor, foreColor);
            }

            if(parent is MenuStrip)
            {
                foreach (ToolStripItem menuItem in ((MenuStrip)parent).Items)
                {
                    SetMenuStripColorsRecursively(menuItem, backColor, foreColor);
                }
            }
        }
        void SetMenuStripColorsRecursively(ToolStripItem parent, Color backColor, Color foreColor)
        {
            parent.BackColor = backColor;
            parent.ForeColor = foreColor;

            if (parent is ToolStripMenuItem)
            {
                foreach (object menuItem in ((ToolStripMenuItem)parent).DropDownItems)
                {
                    if (menuItem is ToolStripMenuItem)
                        SetMenuStripColorsRecursively((ToolStripMenuItem)menuItem, backColor, foreColor);
                }
            }

        }

        string _curCSSTheme;

        string LoadCSSColorTheme(string path)
        {
            return File.ReadAllText(path);
        }

        void SetColorTheme(ColorTheme theme)
        {
            _currentColorTheme = theme;

            switch (theme)
            {
                case ColorTheme.Dark:
                    SetColorsRecursively(this, ColorTranslator.FromHtml("#282828"), ColorTranslator.FromHtml("#dadada"));
                    _curCSSTheme = LoadCSSColorTheme(Settings.Default.DarkCSSColorThemePath);
                    break;
                case ColorTheme.Light:
                    SetColorsRecursively(this, _backColor, _foreColor);
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

        //void SetBrowserColorsForAllElements(ChromiumWebBrowser browser, string backColor,string foreColor)
        //{
        //    browser.ExecuteScriptAsyncWhenPageLoaded($@"function SetBackgroundForAll(backColor, foreColor){{
        //        var elements = document.querySelectorAll('*');
        //        for (var i = 0; i < elements.length; i++) {{
        //        if(elements[i] instanceof HTMLImageElement) continue;
        //        //if(elements[i] instanceof HTMLFrameSetElement) continue;
        //        if(elements[i].id == 'lightbox-nav') continue;
        //        elements[i].style.backgroundColor=backColor;
        //        elements[i].style.color = foreColor;
        //        }}
        //    }}
        //    SetBackgroundForAll('{backColor}','{foreColor}');
        //    ");
        //}

        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        //[DllImport("User32.dll")]
        //private static extern IntPtr GetWindowDC(IntPtr hWnd);

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    const int WM_NCPAINT = 0x85;
        //    if (m.Msg == WM_NCPAINT)
        //    {
        //        IntPtr hdc = GetWindowDC(m.HWnd);
        //        if ((int)hdc != 0)
        //        {
        //            Graphics g = Graphics.FromHdc(hdc);
        //            g.FillRectangle(Brushes.Green, new Rectangle(0, 0, 4800, 23));
        //            g.Flush();
        //            ReleaseDC(m.HWnd, hdc);
        //        }
        //    }
        //}

        //delegate void SetBrowserColorsWithDocumentHeadStyleDelegate(ChromiumWebBrowser browser, string backColor, string foreColor);
        //void SetBrowserColorsWithDocumentHeadStyle(ChromiumWebBrowser browser, string backColor, string foreColor)
        //{
        //    if(InvokeRequired)
        //    {
        //        Invoke(new SetBrowserColorsWithDocumentHeadStyleDelegate(SetBrowserColorsWithDocumentHeadStyle), browser, backColor, foreColor);
        //        return;
        //    }

        //    browser.ExecuteScriptAsync($@"
        //    var style1 = document.createElement('style');
        //    style1.innerText = `
        //        html, html * {{
        //        color: #eeeeee !important;
        //        border-color: #555555 !important;
        //        background-color: #292929 !important;
        //        }}

        //        html, body, html::before, body::before {{
        //        background-image: none !important;
        //        }}

        //        img, video {{z-index: 1}}
        //        cite, cite * {{color: #029833 !important}}
        //        video {{background-color: transparent !important}}
        //        input, textarea {{background-color: #333333 !important}}
        //        input, select, button {{background-image: none !important}}
        //        a {{background-color: rgba(255, 255, 255, 0.01) !important}}

        //        :before {{color: #eeeeee !important}}
        //        :link, :link * {{color: #8db2e5 !important}}
        //        :visited, :visited * {{color: rgb(211, 138, 138) !important}}
        //      `;
        //    document.head.appendChild(style1);
        //    ");




        //    //MessageBox.Show(" SetBrowserColorsWithDocumentHeadStyle");


        //    //var style1 = document.createElement('style');
        //    //style1.innerHTML = `html * {
        //    //    {
        //    //    color: { foreColor}
        //    //        !important;
        //    //        background - color: { backColor}
        //    //        !important
        //    //     }
        //    //}
        //    //  `;
        //    //document.head.appendChild(style1);
        //    //// document.body.classList.toggle(style1);
        //}



        delegate void SetBrowserColorsCSSDelegate(ChromiumWebBrowser browser, string css);
        void SetBrowserColorsCSS(ChromiumWebBrowser browser, string css)
        {
            if (InvokeRequired)
            {
                Invoke(new SetBrowserColorsCSSDelegate(SetBrowserColorsCSS), browser, css);
                return;
            }

            browser.ExecuteScriptAsync($@"
            var style1 = document.createElement('style');
            style1.innerText = `{css}`;
            document.head.appendChild(style1);
            ");
        }

        private void InitSorterListView()
        {
            _lvwColumnSorter = new ListViewColumnSorter {DateTimeFormat = _dateTimeFormat};
            _lvwColumnSorter.ColumnTypes.Add(typeof(string));
            _lvwColumnSorter.ColumnTypes.Add(typeof(string));
            _lvwColumnSorter.ColumnTypes.Add(typeof(DateTime));
        }

        private ListView _lstHistory;
        private void InitHistoryListView()
        { 
            InitSorterListView();

            _lstHistory = new ListView()
            {
                View = View.Details, 
                Dock = DockStyle.Fill, 
                Font = new Font(Font.FontFamily, 12, FontStyle.Regular),
                FullRowSelect = true,
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
            _lstHistory.ColumnClick += _lstHistory_ColumnClick;
        }

        private ListViewColumnSorter _lvwColumnSorter;
       
        private void _lstHistory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            SortHistoryListView();
        }

        private void SortHistoryListView()
        {
            _lstHistory.ListViewItemSorter = _lvwColumnSorter;
            _lstHistory.Sort();
            _lstHistory.ListViewItemSorter = null;
        }

        private void LstHistoryOnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_lstHistory.SelectedItems.Count == 0) return;
            var selectedItem = _lstHistory.SelectedItems[0];
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            cbxCategory.Text = selectedItem.SubItems[1].Text;
            GoBrowsers(selectedItem.Text);
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

                if (filterByDate && (item.Date.Date < beginDate.Date || item.Date.Date > endDate.Date)) continue;

                AddItemToHistoryListView(item);
            }

            SortHistoryListViewByDateIfNeeded();
            HistoryListViewResize();
            ShowHistoryCount();
        }

        private void ShowHistoryCount()
        {
            stHistoryCountShown.Text = $@"{_lstHistory.Items.Count} in the History View";
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

        private void FillHistoryListView(bool showCurrentCategoryOnly, bool chronologically, bool filterByDate,
            DateTime beginDate, DateTime endDate)
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
                Invoke(new AddCategoryComboboxDelegate(AddCategoryCombobox),category);
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

        private void Urban_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
                return;

            var browser = (ChromiumWebBrowser)sender;

            if (_currentColorTheme == ColorTheme.Dark && MM_NeedUrbanDictionary.Checked)
            {
                SetBrowserColorsCSS(browser, _curCSSTheme);
            }
        }

        private void OALD_Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
                return;

            var browser = (ChromiumWebBrowser)sender;

            if (_currentColorTheme == ColorTheme.Dark)
            {
                SetBrowserColorsCSS(browser, _curCSSTheme);
            }
        }
        private void OALD_Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (_preparingOALD && e.Frame.IsMain)
            {
                _preparingOALD = false;
                PrepareOALD((ChromiumWebBrowser)sender);
            }

            if (e.Frame.IsMain)
                InsertOtherJavaScript((ChromiumWebBrowser)sender);

            DeleteAdOALD((ChromiumWebBrowser)sender);
        }

        private void InsertOtherJavaScript(ChromiumWebBrowser browser)
        {
            browser.ExecuteScriptAsyncWhenPageLoaded($@"
              document.body.onmouseup = function()
              {{
                    b1.onselect(document.getSelection().toString());
              }};
            ");
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

            if (!MM_HotKeyEnabled.Checked) return;

            if ((e.Modifiers & KeyModifiers.Control) == KeyModifiers.Control &&
               (e.Modifiers & KeyModifiers.Shift) == KeyModifiers.Shift &&
               (e.Key & Keys.X) == Keys.X)
                GoBrowsers(Clipboard.GetText());
        }

        private async Task SaveHistoryBackup()
        {
            var stt = Settings.Default;
            if (!string.IsNullOrEmpty(stt.HistoryBackupPath))
                await _history.SaveHistoryToFile(stt.HistoryBackupPath, true);
        }

        private async Task SaveHistory(bool showDialog)
        {
            var wannaSave = true;

            if (showDialog)
                wannaSave = MessageBox.Show(@"Do you want to save data before exit?", Application.ProductName,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;

            if (wannaSave)
            {
                await _history.SaveHistoryToFile();
            }
            else
            {
                await _history.SaveHistoryToFileAsACopy();
            }

            await SaveHistoryBackup();
        }

/*
        private string GetFileNameWithTimeStamp(string path)
        {
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(path);
            var fileExt = Path.GetExtension(path);
            return $"{fileNameWithoutExt}_{_history.GetTimeStampNowForFile()}_{fileExt}";
        }
*/

        private async Task SaveTasksBackup()
        {
            await Task.Run(() =>
            {
                var stt = Settings.Default;
                if (string.IsNullOrEmpty(stt.HistoryBackupPath))
                    return;
                var dir = Path.GetDirectoryName(stt.HistoryBackupPath);
                if (!Directory.Exists(dir))
                    return;
                if (!File.Exists(stt.TaskFilePath))
                    return;

                File.Copy(stt.TaskFilePath, Path.Combine(dir, Path.GetFileName(stt.TaskFilePath)), true);
            });
        }

        bool _readyToClosing;

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_readyToClosing;

            if (!_readyToClosing)
            {
                if (_history.HistoryHasBeenChanged)
                    await SaveHistory(e.CloseReason == CloseReason.UserClosing);

                await SaveTasksBackup();

                if (_hotKeyId != null)
                    HotKeyManager.UnregisterHotKey(_hotKeyId.Value);

                _readyToClosing = true;
                Close();
            }
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

        private void ShowUrban(bool show)
        {
            if (show)
            {
                if (!tabControl1.TabPages.ContainsKey("Urban"))
                {
                    tabControl1.TabPages.Add("Urban", "Urban");
                    tabControl1.TabPages["Urban"].Controls.Add(_browsers[2]);
                }
                    
            }
            else
            {
                if (tabControl1.TabPages.ContainsKey("Urban"))
                {
                    tabControl1.TabPages.Remove(tabControl1.TabPages["Urban"]);
                }
            }
        }

        private void MM_NeedUrbanDictionary_Click(object sender, EventArgs e)
        {
            ShowUrban(MM_NeedUrbanDictionary.Checked);
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
            Settings.Default.ReverseOrderHisroty = reverseOrder;
            Settings.Default.Save();
        }

        private void AutoSortByDateOrderOptionSave(bool reverseOrder)
        {
            Settings.Default.AutoSortByDate = reverseOrder;
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
            Process.Start(Settings.Default.TaskFilePath);
        }

        private void txtToSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void MM_AutoSortByDate_Click(object sender, EventArgs e)
        {
            AutoSortByDateOrderOptionSave(MM_AutoSortByDate.Checked);
            if(MM_AutoSortByDate.Checked)
                SortHistoryListViewByDateIfNeeded();
        }

        private void btnClearInput_Click(object sender, EventArgs e)
        {
            txtToSearch.Clear();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.C))
            {
                    txtToSearch.Clear();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C))
            {
                txtToSearch.Clear();
            }
        }

        private void MM_ShortcutsHelp_Click(object sender, EventArgs e)
        {
            new ShortcutsForm().ShowDialog();
        }

        private void MM_Test_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_boundObject.Text);
        }

        void SetLoginToOALDOnStart(bool predicate)
        {
            var stt = Settings.Default;
            stt.OALDLoginOnStart = predicate;
            stt.Save();
        }

        private void MM_LoginToOALDOnStart_Click(object sender, EventArgs e)
        {
            SetLoginToOALDOnStart(((ToolStripMenuItem)sender).Checked);
        }
        private void txtFindText_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFindText.Text.Length <= 0)
            {
                //this will clear all search result
                _browsers[0].StopFinding(true);
            }
            else
            {
                _browsers[0].Find(0, txtFindText.Text, true, false, false);
            }
        }

        private void txtFindText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _browsers[0].Find(0, txtFindText.Text, true, false, true);
            }
        }
        private void ChooseBackupFolderDialog()
        {
            var dlg = new SaveFileDialog();
            var stt = Settings.Default;
            
            if (string.IsNullOrEmpty(stt.HistoryBackupPath))
            {
                dlg.InitialDirectory = Application.StartupPath;
                dlg.FileName = "LinguaHelper_data_backup.ini";
                dlg.Filter = @"Ini Files (*.ini)|*.ini";
            }
            else
            {
                dlg.InitialDirectory = Path.GetDirectoryName(stt.HistoryBackupPath);
                dlg.FileName = Path.GetFileName(stt.HistoryBackupPath);
                var ext = Path.GetExtension(stt.HistoryBackupPath);
                dlg.Filter = $@"(*{ext})|*{ext}";
            }

            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            if (string.Compare(Path.GetDirectoryName(dlg.FileName), Application.StartupPath, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                MessageBox.Show(@"Choose a folder other than the startup folder", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ChooseBackupFolderDialog();
                return;
            }

            stt.HistoryBackupPath = dlg.FileName;
            stt.Save();
        }
        private void MM_SetBackupFolder_Click(object sender, EventArgs e)
        {
            ChooseBackupFolderDialog();
        }

       
    }
}
