using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using DesktopHelper;
using TestProj.Properties;


namespace TestProj
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            
        }

        private void txtBrowse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoBrowsers(txtToSearch.Text, MM_ForceLoadFromBrowseField.Checked);
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


        bool _firstGoBrowsers = true;
        bool _silentOALD = false;
        void GoBrowsers(string text, bool saveHistory = true, bool force = false, bool silentOALD = false)
        {
            text = PrepareTextToProceed(text);
            if (string.IsNullOrEmpty(text)) return;

            txtToSearch.Text = text;
            if(saveHistory)
                _history.AddHistoryItem(text, GetCategory());

            _silentOALD = silentOALD;

            foreach (var unit in _dictionaryTranslatorUnits.Values)
            { 
                unit.Go(text, force);
            }

            //if(MM_NeedUrbanDictionary.Checked) GoUrbanDictionary(text);
            if (_firstGoBrowsers)
            {
                _firstGoBrowsers = false;
                ActivateTabs();
            }
        }

        private void ActivateTabs()
        {
            var selected = tabControl1.SelectedIndex;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                //tabControl1.SelectedTab = tabControl1.TabPages[i];
                tabControl1.SelectedIndex= i;
            }

            //tabControl1.SelectedTab = tabControl1.TabPages[selected];
            tabControl1.SelectedIndex = selected;
        }

        private History _history;
        private int? _hotKeyGo;
        private int? _hotKeyReturn;

        private Color _foreColor;
        private Color _backColor;

        private void CefInit()
        {
            var settingsBrowser = new CefSettings
            {
                Locale = "en-US,en",
                AcceptLanguageList = "en-US,en",
                PersistSessionCookies = false,
            };
            settingsBrowser.CefCommandLineArgs.Remove("mute-audio");
            settingsBrowser.CefCommandLineArgs.Add("enable-media-stream", "1");
            Cef.Initialize(settingsBrowser);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CefInit();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;

            Text = Application.ProductName;
            _foreColor = ForeColor;
            _backColor = BackColor;
            _colorThemes = new();

            LoadSettings();
            InitHotKeys();
            InitBrowsers();
            InitTabs();
            InitMainMenuItems();
            InitFirstTabSelection();
            InitHistory();
            SetDateTimeFilterNow();
            FillCategoriesComboBox();
            InitHistoryListView();
            FillHistoryListView();

            if (MM_LoginToOALDOnStart.Checked)
                LoginToOALD("test");
            
            GoBrowsers("test", false, false, true);
            SetThemeFromSettings();
        }

        private void AddReturnDesktopItem(string text, object tag, bool @checked = false)
        {
            var item = new ToolStripMenuItem
            {
                Text = text,
                Tag = tag,
                CheckOnClick = true,
                Checked = @checked
            };
            item.Click += MM_ReturnDesktopItem_Click;
            MM_ReturnDesktop.DropDownItems.Add(item);
        }

        private void MM_ReturnDesktopItem_Click(object sender, EventArgs e)
        {
            var item = ((ToolStripMenuItem)sender);
            if (item.Tag == null)
            {
                _previousDesktopAuto = true;
                SetPreviousDesktopMenuText();
            }
            else
            {
                _previousDesktopAuto = false;
                SetPreviousDesktop((Desktop)item.Tag);
            }

            
            MM_ReturnDesktopCheckItems(item);
        }

        private void MM_ReturnDesktopCheckItems(ToolStripMenuItem checkedItem)
        {
            foreach (ToolStripMenuItem item in MM_ReturnDesktop.DropDownItems)
            { 
                item.Checked = item == checkedItem;
            }
        }

        private void InitMainMenuItems()
        { 
            MM_ReturnDesktop.DropDownItems.Clear();
            AddReturnDesktopItem("Auto", null,true);

            for (int i = 0; i < Desktop.Count; i++)
            {
                AddReturnDesktopItem(Desktop.DesktopNameFromIndex(i), Desktop.FromIndex(i));
            }

            SetPreviousDesktopMenuText();
        }

        private void LoadSettings()
        {
            var stt = Settings.Default;
            MM_LoginToOALDOnStart.Checked = stt.OALDLoginOnStart;
            MM_ForceLoadFromBrowseField.Checked = stt.ForceLoadFromBrowse;
            MM_SpeakOnBrowsingOALD.Checked = stt.SpeakOnBrowsingOALD;

            _colorThemes.Clear();
            _colorThemes.Add(ColorTheme.Dark, new(ColorTheme.Dark, stt.DarkForeground, stt.DarkBackground));
            _colorThemes.Add(ColorTheme.Light, new(ColorTheme.Light, _foreColor, _backColor));

            _currentColorTheme = stt.DarkTheme ? ColorTheme.Dark : ColorTheme.Light;
        }

        Dictionary<ColorTheme, ColorThemeProvider> _colorThemes;
       

        private void SetThemeFromSettings()
        { 
            if (Settings.Default.DarkTheme)
                SetColorTheme(ColorTheme.Dark);
        }

        private void InitHotKeys()
        {
            _hotKeyGo = HotKeyManager.RegisterHotKey(Keys.X, KeyModifiers.Control | KeyModifiers.Shift);
            _hotKeyReturn = HotKeyManager.RegisterHotKey(Keys.Q, KeyModifiers.Control | KeyModifiers.Shift);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
        }

        private TableLayoutPanel CreateTableForUnit()
        {
            var table = new TableLayoutPanel() { ColumnCount = 2, RowCount = 1, Dock = DockStyle.Fill };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            return table;
        }

        private void AutoLayoutUnit(TableLayoutPanel panel, DictionaryTranslatorUnit unit)
        {
            panel.Controls.Add(unit.Dictionary.Browser, 0, 0);
            panel.Controls.Add(unit.Translator.Browser, 1, 0);
        }

        private void InitTabs()
        {
            tabControl1.Font = new Font(family: tabControl1.Font.FontFamily, 12, FontStyle.Regular);
            foreach (var unit in _dictionaryTranslatorUnits.Values)
            { 
                var table = CreateTableForUnit();
                AutoLayoutUnit(table, unit);

                var dictionaryName = unit.Dictionary.BrowserName;
                tabControl1.TabPages.Add(dictionaryName, dictionaryName);
                tabControl1.TabPages[dictionaryName]?.Controls.Add(table);
            }
            tabControl1.TabPages.Add("tabHistory", "History");

            ShowUrban(MM_NeedUrbanDictionary.Checked);
        }

        private void Page_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.ClipRectangle);
        }

        private void InitFirstTabSelection()
        {
            FillFirstTabComboBox();
            FirstTabComboboxSelectFromSettings();
        }

        private void FirstTabComboboxSelectFromSettings()
        {
            var stt = Settings.Default;

            for (int i = 0; i < MM_cbxFirstTab.Items.Count; i++)
            {
                var item = MM_cbxFirstTab.Items[i] as string;
                if (string.CompareOrdinal(item, stt.firstTabName) == 0)
                {
                    MM_cbxFirstTab.SelectedIndex = i;
                }
            }
        }

        private void FillFirstTabComboBox()
        {
            MM_cbxFirstTab.Items.Clear();
            foreach (TabPage tab in tabControl1.TabPages)
            {
                MM_cbxFirstTab.Items.Add(tab.Name);
            }
        }

        private bool SetFirstTab(string name)
        {
            if(!tabControl1.TabPages.ContainsKey(name))
                return false;

            tabControl1.SelectTab(name);

            return true;
        }

        enum UnitName
        {
            Oald,
            Cambridge,
            Wiki,
            Google
        }

        Dictionary<UnitName, DictionaryTranslatorUnit> _dictionaryTranslatorUnits;

        private void InitBrowsers()
        {
            var stt = Settings.Default;
            DictionaryTranslatorUnit.DefaultTranslatorUrl = stt.GT_URL;
            DictionaryTranslatorUnit.DefaultTranslatorName = stt.GT_Name;
            _dictionaryTranslatorUnits = new ();

            var cssDarkColorTheme = File.ReadAllText(stt.DarkCSSColorThemePath);
            var cssDarkGTranslator = File.ReadAllText(stt.DarkCSSGTranslator);

            var oald = new BrowserItem(stt.OALD_URL,
                                       "OALD",
                                       cssDarkColorTheme,
                                       _currentColorTheme,
                                       OaldJS.GetInstance(),
                                       stt.OALDPrepareURL);
            oald.FinishAllTasks += Oald_FinishAllTasks;

            var cambridge = new BrowserItem(stt.Cambridge_URL,
                                            "Cambridge en-rus",
                                            cssDarkColorTheme,
                                            _currentColorTheme,
                                            CambridgeJS.GetInstance());
            var wiki = new BrowserItem(stt.Wiki_URL,
                                       "Wikipedia en",
                                       cssDarkColorTheme,
                                       _currentColorTheme,
                                       GenericJS.GetInstance());
            var google = new BrowserItem(stt.GoogleSearchURL,
                                         "Google",
                                         null/*cssDarkColorTheme*/,
                                         _currentColorTheme,
                                         GoogleJS.GetInstance(),
                                         null,
                                         stt.GoogleSearchRequestParams);

            _dictionaryTranslatorUnits.Add(UnitName.Oald ,new (oald, cssDarkGTranslator));
            _dictionaryTranslatorUnits.Add(UnitName.Cambridge ,new (cambridge, cssDarkGTranslator));
            _dictionaryTranslatorUnits.Add(UnitName.Wiki , new (wiki, cssDarkGTranslator));
            _dictionaryTranslatorUnits.Add(UnitName.Google, new (google, cssDarkGTranslator));
        }

        private void Oald_FinishAllTasks(BrowserItem sender)
        {
            OaldAutoSound();
        }

        private delegate void OaldAutoSoundDelegate();

        private void OaldAutoSound()
        {
            if (InvokeRequired) {
                Invoke(new OaldAutoSoundDelegate(OaldAutoSound));
                return;
            }
            if (!MM_SpeakOnBrowsingOALD.Checked || _silentOALD) return;

            var name = Settings.Default.OALD_AudioButton_ID;
            _dictionaryTranslatorUnits[UnitName.Oald].Dictionary.ClickOnElementByClassName(@$".{name.Replace(' ', '.')}");
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

       
        

        ColorTheme _currentColorTheme = ColorTheme.Light;

        void ReloadAllBrowsers()
        {
            foreach (var unit in _dictionaryTranslatorUnits.Values)
            {
                unit.ReLoad();
            }
        }

        void SetColorsRecursively(Control parent,Color backColor, Color foreColor)
        {
            parent.BackColor = backColor;
            parent.ForeColor = foreColor;

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

                var highLightColors = GetHighLightColor(backColor, foreColor);
                (parent as MenuStrip).Renderer = new MyRenderer(highLightColors.backColor, backColor);
            }
        }
        void SetMenuStripColorsRecursively(ToolStripItem parent, Color backColor, Color foreColor)
        {
            parent.BackColor = backColor;
            parent.ForeColor = foreColor;


            if (parent is ToolStripMenuItem)
            {
                var parentsDropDown = (parent as ToolStripMenuItem).DropDown;
                ((parentsDropDown as ToolStripDropDownMenu)!).ShowCheckMargin = true;
                ((parentsDropDown as ToolStripDropDownMenu)!).ShowImageMargin = false;
                parentsDropDown.BackColor = backColor;
                parentsDropDown.ForeColor = foreColor;
                parentsDropDown.Invalidate(true);
            }

            if (parent is ToolStripDropDownItem)
            {
                foreach (ToolStripItem childItem in (parent as ToolStripDropDownItem).DropDownItems)
                {
                    SetMenuStripColorsRecursively(childItem, backColor, foreColor);
                }
            }
        }

       
        private (Color backColor, Color foreColor) GetHighLightColor(Color backColor, Color foreColor)
        {
            switch (_currentColorTheme)
            {
                case ColorTheme.Dark:
                    return (ControlPaint.Light(backColor),ControlPaint.Dark(foreColor));
                case ColorTheme.Light:
                    return (ControlPaint.Dark(backColor), ControlPaint.Light(foreColor));
                default:
                    throw new NotImplementedException();
            }
        }

        public class MyProfessionalColorTable : ProfessionalColorTable
        {

            Color _backColor;
            public MyProfessionalColorTable(Color backColor)
            {
                _backColor = backColor;
            }

            public override Color ImageMarginGradientBegin => _backColor;
            public override Color ImageMarginGradientMiddle => _backColor;
            public override Color ImageMarginGradientEnd => _backColor;
        }
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            private Color _backColorOfHighlightedItem;
            //private Color _foreColorOfHighlightedItem;
            public MyRenderer(Color backColorOfHighlightedItem, Color backColor/*,Color foreColor*/)
                :base(new MyProfessionalColorTable(backColor))
            {
                _backColorOfHighlightedItem = backColorOfHighlightedItem;
                //_foreColorOfHighlightedItem = foreColor;
            }
            
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color backColor = e.Item.Selected ? _backColorOfHighlightedItem : e.Item.BackColor;
                using SolidBrush brush = new(backColor);
                e.Graphics.FillRectangle(brush, rc);
                // base.OnRenderMenuItemBackground(e);
            }

            //protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            //{
            //    e.TextColor = e.Item.Selected ? _foreColorOfHighlightedItem : e.Item.ForeColor;
            //    base.OnRenderItemText(e);
            //}
        }

        void SetColorTheme(ColorTheme theme)
        {
            _currentColorTheme = theme;
            SetColorsRecursively(this, _colorThemes[theme].Background, _colorThemes[theme].Foreground);
            UseImmersiveDarkMode(Handle, theme == ColorTheme.Dark);
            tabControl1.DisplayStyle = theme == ColorTheme.Dark ? TabStyle.Dark : TabStyle.Default;
          

            SetColorThemeForAllUnits();
            ReloadAllBrowsers();

            Settings.Default.DarkTheme = theme == ColorTheme.Dark;
            Settings.Default.Save();
        }

        private void SetColorThemeForAllUnits()
        {
            foreach (var unit in _dictionaryTranslatorUnits.Values)
            {
                unit.ColorTheme = _currentColorTheme;
            }
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

            tabControl1.TabPages["tabHistory"]?.Controls.Add(_lstHistory);
            _lstHistory.Columns.Add("phrase", "phrase");
            _lstHistory.Columns.Add("category", "category");
            _lstHistory.Columns.Add("date", "date");

            var menuItems = new List<ToolStripMenuItem>
            {
                new("Update",null, UpdateHistoryListView),
                new("Delete", null, DeleteHistoryItem)
            };

            _lstHistory.ContextMenuStrip = new ContextMenuStrip();
            // ReSharper disable once CoVariantArrayConversion
            _lstHistory.ContextMenuStrip?.Items.AddRange(menuItems.ToArray());
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

        private void LoginToOALD(string gotoAfterLoading = null)
        {
            _dictionaryTranslatorUnits[UnitName.Oald].Dictionary.Prepare(gotoAfterLoading);
        }
        

        delegate void HotKeyPressedDelegate(object sender, HotKeyEventArgs e);

        private Desktop _previousDesktop;

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
            {
                ShowThisAppDesktop();
                GoBrowsers(Clipboard.GetText());
            }
            else if ((e.Modifiers & KeyModifiers.Control) == KeyModifiers.Control &&
                (e.Modifiers & KeyModifiers.Shift) == KeyModifiers.Shift &&
                (e.Key & Keys.Q) == Keys.Q)
            {
                SwitchDesktops();
            }
        }

        private void SwitchDesktops()
        {
            var thisAppDesktop = Desktop.FromWindow(Handle);
            if (thisAppDesktop.IsVisible)
            {
                ReturnToPreviousDesktop();
            }
            else
            {
                ShowThisAppDesktop();
            }
        }

        private void SearchTextBoxFocus()
        {
            txtToSearch.SelectAll();
            txtToSearch.Focus();
        }

        void SavePreviousDesktop()
        {
            if (_previousDesktopAuto)
                SetPreviousDesktop (Desktop.Current);
        }

        private void ShowThisAppDesktop()
        {
            var thisAppDesktop = Desktop.FromWindow(Handle);
            if (!thisAppDesktop.IsVisible)
            {
                SavePreviousDesktop();
                thisAppDesktop.MakeVisible();
            }

            SearchTextBoxFocus();
        }

        private void SetPreviousDesktop(Desktop desktop)
        {
            _previousDesktop = desktop;
            SetPreviousDesktopMenuText();
        }
        private void SetPreviousDesktopMenuText()
        {
            var name = _previousDesktop != null? Desktop.DesktopNameFromDesktop(_previousDesktop): "None";
            var desktopTag = _previousDesktop == null ? " desktop" : "";

            MM_ReturnDesktop.Text = $"Previous{desktopTag}: {(_previousDesktopAuto ? "(Auto) " : "")} {name}";
        }
        private void ReturnToPreviousDesktop()
        {
            if (_previousDesktop == null)
                return;//DesktopHelper.Desktop.FromIndex(0).MakeVisible();
            if (!_previousDesktop.IsVisible)
                _previousDesktop.MakeVisible();
        }

        private void returnToPreviousDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ReturnToPreviousDesktop();
        }

        private async Task SaveHistoryBackup()
        {
            var stt = Settings.Default;
            if (!string.IsNullOrEmpty(stt.HistoryBackupPath))
                await _history.SaveHistoryToFile(stt.HistoryBackupPath, true);
        }

        private async Task SaveHistory(bool saveMainFile, bool SaveACopy, bool saveBackup)
        {
            
            if (saveMainFile) await _history.SaveHistoryToFile();
            if(SaveACopy) await _history.SaveHistoryToFileAsACopy();
            if(saveBackup) await SaveHistoryBackup();
        }

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

                File.Copy(stt.TaskFilePath, Path.Combine(dir, Path.GetFileName(stt.TaskFilePath) ?? throw new InvalidOperationException()), true);
            });
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var isUserClosing = e.CloseReason == CloseReason.UserClosing;
            var isHistoryChanged = _history.HistoryHasBeenChanged;

            if (isUserClosing && isHistoryChanged)
            {
                var dialogResult = MessageBox.Show(@"Do you want to save data before exit?", Application.ProductName,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        await SaveHistory(true, false, true);
                        break;
                    case DialogResult.No:
                        await SaveHistory(false, true, false);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true; 
                        return;
                }
            }
            if (isUserClosing && !isHistoryChanged)
            {
                var dialogResult = MessageBox.Show(@"Do you want to close the app?", Application.ProductName,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                switch (dialogResult)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true; 
                        return;
                }
            }
            else if(!isUserClosing && isHistoryChanged)
            {
                await SaveHistory(false, true, true);
            }

            await SaveTasksBackup();
            UnregisterHotKeys();
        }

        private void UnregisterHotKeys()
        {
            if (_hotKeyGo != null && _hotKeyReturn != null)
            {
                HotKeyManager.UnregisterHotKey(_hotKeyGo.Value);
                HotKeyManager.UnregisterHotKey(_hotKeyReturn.Value);
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
                }
            }
            else
            {
                if (tabControl1.TabPages.ContainsKey("Urban"))
                {
                    tabControl1.TabPages.Remove(tabControl1.TabPages["Urban"]!);
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

        private void StartProcess(string path)
        {
            Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = path });
        }

        private void showDatainiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProcess(Settings.Default.DataPath);
        }

        private void showTheAppFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProcess(Application.StartupPath);
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
            StartProcess(Settings.Default.TaskFilePath);
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
            SearchClearAndFocus();
        }

        private void SearchClearAndFocus()
        {
            txtToSearch.Clear();
            txtToSearch.Focus();
        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.C))
            {
                SearchClearAndFocus();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Shift | Keys.F))
            {
                FindClearAndFocus();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C))
            //{
            //    txtToSearch.Clear();
            //    txtToSearch.Focus();
            //}
        }

        private void MM_ShortcutsHelp_Click(object sender, EventArgs e)
        {
            new ShortcutsForm().ShowDialog();
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

        private void StopFinding()
        {
            foreach (var unit in _dictionaryTranslatorUnits.Values)
            {
                unit.Dictionary.Browser.StopFinding(true);
            }
        }

        private void StartFinding(string text)
        {
            foreach (var unit in _dictionaryTranslatorUnits.Values)
            {
                unit.Dictionary.Browser.Find(txtFindText.Text, true, false, false);
            }
        }
        private void FindInDictionaries(string text)
        {
            if (text.Length <= 0)
            {
                //this will clear all search result
                StopFinding();
            }
            else
            {
                StartFinding(text);
            }
        }

        private void txtFindText_KeyUp(object sender, KeyEventArgs e)
        {
            FindInDictionaries(txtFindText.Text);
        }

        private void txtFindText_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Enter) == Keys.Enter)
            {
                StopFinding();
                FindInDictionaries(txtFindText.Text);
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
                dlg.InitialDirectory = Path.GetDirectoryName(stt.HistoryBackupPath) ?? throw new InvalidOperationException();
                dlg.FileName = Path.GetFileName(stt.HistoryBackupPath) ?? throw new InvalidOperationException();
                var ext = Path.GetExtension(stt.HistoryBackupPath) ?? throw new InvalidOperationException();
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

        private void setOALDCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var stt = Settings.Default;
            var dialog = new CredentialsForm
            {
                Credentials = new Credentials(stt.OALDUser, stt.OALDPass)
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;
            stt.OALDUser = dialog.Credentials.UserName;
            stt.OALDPass = dialog.Credentials.Password;
            stt.Save();
            ReloadOaldJS();
        }

        private void ReloadOaldJS()
        {
            _dictionaryTranslatorUnits[UnitName.Oald].Dictionary.ReloadJSCode(OaldJS.GetInstance());
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MM_cbxFirstTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var stt = Settings.Default;
            if (SetFirstTab(MM_cbxFirstTab.Text))
            {
                stt.firstTabName = MM_cbxFirstTab.Text;
                stt.Save();
            }
            else
            {
                ShowErrorMessage($"There is no tab which has name {MM_cbxFirstTab.Text}");
            }
        }

        private void FindClearAndFocus()
        {
            txtFindText.Text = "";
            txtFindText.Focus();
            FindInDictionaries("");
        }

        private void btnClearFind_Click(object sender, EventArgs e)
        {
            FindClearAndFocus();
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
    ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        internal static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }


        void SetForceLoadFromBrowseField(bool predicate)
        {
            var stt = Settings.Default;
            stt.ForceLoadFromBrowse = predicate;
            stt.Save();
        }
        private void MM_ForceLoadFromBrowseField_Click(object sender, EventArgs e)
        {
            SetForceLoadFromBrowseField(((ToolStripMenuItem)sender).Checked);
        }

        bool _previousDesktopAuto = true;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _dictionaryTranslatorUnits[UnitName.Oald].Dictionary.ClickOnElementByClassName(".sound.audio_play_button.pron-us.icon-audio");
        }
        void SetSpeakOnBrowsingOALD(bool predicate)
        {
            var stt = Settings.Default;
            stt.SpeakOnBrowsingOALD = predicate;
            stt.Save();

            ReloadOaldJS();
        }
        private void MM_SpeakOnBrowsingOALD_Click(object sender, EventArgs e)
        {
            SetSpeakOnBrowsingOALD(((ToolStripMenuItem)sender).Checked);
        }
    }
}
