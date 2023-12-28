using CefSharp;
using CefSharp.WinForms;
using LinguaHelper.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinguaHelper
{
    [SupportedOSPlatform("windows")]
    public partial class MainForm : Form
    {
        #region ctor 
        private int? _hotKeyGo;
        private int? _hotKeySwitch;
        private int? _hotKeyShow;

        private ListView _lstHistory;
        private History _history;


        List<DictionaryTranslatorUnit> _units;
        private delegate void ClickOnBrowserItemsDelegate(BrowserItem browser);
        private delegate void UpdateItemInHistoryListViewDelegate(HistoryDataItem oldItem, HistoryDataItem updatedItem);
        delegate void HotKeyPressedDelegate(object sender, HotKeyEventArgs e);

        private int _previousDesktopIndex = -1;
        [SupportedOSPlatform("windows")]
        public MainForm()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }
        #endregion

        #region Back up
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
        #endregion

        #region Browsers
        private void ReloadOaldJS()
        {
            throw new NotImplementedException();
            // _dictionaryTranslatorUnits[UnitName.Oald].Dictionary.ReloadJSCode(OaldJS.GetInstance());
        }

        private void StopFinding()
        {
            foreach (var unit in _units)
            {
                if (unit.Dictionary.Browser.IsBrowserInitialized)
                    unit.Dictionary.Browser.StopFinding(true);
            }
        }
        private DictionaryTranslatorUnit GetCurrentUnit()
        {
            return tabControl1.TabPages[tabControl1.SelectedIndex].Tag as DictionaryTranslatorUnit;
        }

        /// <summary>
        /// Continue finding in the current browser
        /// </summary>
        private void ContinueFinding(string text, bool findNext)
        {
            if (GetCurrentUnit().Dictionary.Browser.IsBrowserInitialized)
                GetCurrentUnit().Dictionary.Browser.Find(text, true, false, findNext);
        }
        private void FindInDictionaries(string text, bool findNext)
        {
            if (text.Length <= 0)
            {
                //this will clear all search result
                StopFinding();
            }
            else
            {
                ContinueFinding(text, findNext);
            }
        }

        private void txtFindText_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Enter) != Keys.Enter)
            {
                FindInDictionaries(txtFindText.Text, false);
            }
        }

        private void txtFindText_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Enter) == Keys.Enter)
            {
                FindInDictionaries(txtFindText.Text, true);
            }
        }
        private void LoginToOALD(string gotoAfterLoading = null)
        {
            throw new NotImplementedException();
            // _dictionaryTranslatorUnits[UnitName.Oald].Dictionary.Prepare(gotoAfterLoading);
        }
        void GoBrowsers(string text, bool isUserCommand, bool saveHistory = true, bool force = false, bool silentOALD = false)
        {
            text = PrepareTextToProceed(text);
            if (string.IsNullOrEmpty(text)) return;

            txtToSearch.Text = text;
            if (saveHistory)
                _history.AddHistoryItem(text, GetCategory());

            foreach (var unit in _units)
            {
                unit.Go(text, isUserCommand, force);
            }
        }
        void ReloadAllBrowsers()
        {
            foreach (var unit in _units)
            {
                unit.ReLoad();
            }
        }
        private void InitBrowsers()
        {
            _units = JsonConvert.DeserializeObject<List<DictionaryTranslatorUnit>>(File.ReadAllText("Browser Settings\\Browsers.json"));
            foreach (var unit in _units)
            {
                unit.Dictionary.BoundObject.JScriptErrorOccured += BoundObject_JScriptErrorOccured;
                unit.Translator.BoundObject.JScriptErrorOccured += BoundObject_JScriptErrorOccured;
                unit.Dictionary.BrowerErrorOccured += BrowerErrorOccured;
                unit.Translator.BrowerErrorOccured += BrowerErrorOccured;
            }
        }

        private void BrowerErrorOccured(BrowserItem sender, string message)
        {
            WriteToLog($"Browser name: {sender.BrowserName} Message:{message}", LogRecordCategory.Browser);
        }

        /// <summary>
        ///write the error message to the log file: exact date and time, error message, name, stack
        /// </summary>
        private void BoundObject_JScriptErrorOccured(object sender, JScriptErrorEventArgs e)
        {
            WriteToLog($"{e.Message} {e.Name} {e.Stack}", LogRecordCategory.JScript);
            JScriptStatusError();
        }

        #endregion

        #region Log processing
        enum LogRecordCategory
        {
            Browser,
            JScript,
            General
        }
        private void WriteToLog(string message, LogRecordCategory logRecordCategory)
        {
            var stt = Settings.Default;
            var logRecord = $@"{DateTime.Now.ToString(_dateTimeFormat, CultureInfo.InvariantCulture)}: <{logRecordCategory}> {message}{Environment.NewLine}";
            File.AppendAllText(stt.ErrorLogPath, logRecord);
        }
        #endregion

        #region Color themes
        [SupportedOSPlatform("windows")]
        void SetColorTheme(ColorTheme theme)
        {
            Settings.Default.DarkTheme = theme == ColorTheme.Dark;
            Settings.Default.Save();

            var themeManager = ThemeManagerLoader.LoadThemeManager();
            themeManager.ApplyTheme(this);
            tabControl1.DisplayStyle = theme == ColorTheme.Dark ? TabStyle.Dark : TabStyle.Default;

            SetColorThemeForAllUnits(theme);
            ReloadAllBrowsers();
        }

        private void SetColorThemeForAllUnits(ColorTheme theme)
        {
            foreach (var unit in _units)
            {
                unit.ColorTheme = theme;
            }
        }
        #endregion

        #region Exiting Main form closing
        private bool _promptBeforeClosure = true;
        private void CloseWithoutPrompt()
        {
            _promptBeforeClosure = false;
            Close();
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await ClosingDialogAsync(e);
            await SaveTasksBackupAsync();
            UnregisterHotKeys();
        }

        private async Task ClosingDialogAsync(FormClosingEventArgs e)
        {
            var isPromptNeeded = (e.CloseReason == CloseReason.UserClosing) && _promptBeforeClosure;
            var isHistoryChanged = _history != null && _history.HistoryHasBeenChanged;
            //covering the code: a and b
            if (isPromptNeeded && isHistoryChanged)
            {
                await SaveHistoryDialogAsync(e);
            }
            //covering the code: a and not b
            else if (isPromptNeeded && !isHistoryChanged)
            {
                PromptForAppClosure(e);
            }
            //covering the code: not a and b
            else if (!isPromptNeeded && isHistoryChanged)
            {
                await SaveHistory(true, true, true);
            }
            //covering the code: not a and not b (nothing to do)
        }

        private void PromptForAppClosure(FormClosingEventArgs e)
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

        private async Task SaveHistoryDialogAsync(FormClosingEventArgs e)
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

        private void UnregisterHotKeys()
        {
            if (_hotKeyGo != null)
                HotKeyManager.UnregisterHotKey(_hotKeyGo.Value);
            if (_hotKeySwitch != null)
                HotKeyManager.UnregisterHotKey(_hotKeySwitch.Value);
            if (_hotKeyShow != null)
                HotKeyManager.UnregisterHotKey(_hotKeyShow.Value);
        }
        #endregion

        #region General Methods
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool AskForConfirmationWarning(string message)
        {
            var result = MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            return result == DialogResult.Yes;
        }
        private void StartProcess(string path)
        {
            Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = path });
        }
        private void WindowOnTop()
        {
            Show();
            this.Restore();
            BringToFront();
            Activate();
        }
        private void SearchTextBoxFocus()
        {
            txtToSearch.SelectAll();
            txtToSearch.Focus();
        }
        string GetCategory()
        {
            if (string.IsNullOrEmpty(cbxCategory.Text))
                return "general";
            return cbxCategory.Text;
        }

        string PrepareTextToProceed(string text)
        {
            text = text.Trim(',', '.', '!', ':', ';', '-', '"', '\'', ' ', '\n');
            text = text.Replace(Environment.NewLine, " ");
            text = new Regex("[ ]{2,}", RegexOptions.None).Replace(text, " ");
            return text;
        }

        private void ActivateTabsIfNeeded()
        {
            if (Settings.Default.ActivateTabsAfterAppStarts)
                ActivateTabs();
        }
        private void ActivateTabs()
        {
            var selected = tabControl1.SelectedIndex;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                tabControl1.SelectedIndex = i;
            }
            tabControl1.SelectedIndex = selected;
        }
        #endregion

        #region Global hot keys
        // private CancellationTokenSource _switchDesktopsCts = new CancellationTokenSource();
        async void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
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
                await ShowThisAppDesktop();
                GoBrowsers(Clipboard.GetText(), true);
            }
            else if ((e.Modifiers & KeyModifiers.Control) == KeyModifiers.Control &&
                (e.Modifiers & KeyModifiers.Shift) == KeyModifiers.Shift &&
                (e.Key & Keys.Q) == Keys.Q)
            {
                await SwitchDesktops();
            }
            else if ((e.Modifiers & KeyModifiers.Control) == KeyModifiers.Control &&
                (e.Modifiers & KeyModifiers.Alt) == KeyModifiers.Alt &&
                (e.Key & Keys.Q) == Keys.Q)
            {
                await ShowThisAppDesktop();
            }
        }
        #endregion

        #region History
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
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadHistoryAndRefillListViewDialog();
        }
        private void cbxCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cbx = (ComboBox)sender;
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
        bool AreTheGroupsNeeded()
        {
            return !MM_showChronologically.Checked;
        }

        private void _history_NewCategoryAdded(object sender, NewCategoryAddedEventArgs e)
        {
            AddCategoryCombobox(e.Category);
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
        private void AddCategoryIfNeed(bool isCategoryNew, string category)
        {
            if (isCategoryNew && AreTheGroupsNeeded())
            {
                AddGroupToHistoryListView(category);
            }
        }
        private void UpdateItemInHistoryListView(HistoryDataItem oldItem, HistoryDataItem updatedItem)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateItemInHistoryListViewDelegate(UpdateItemInHistoryListView), oldItem, updatedItem);
                return;
            }
            //this event handler is called when the item is updated in the history, so the item is already in the history.
            //item must be added to the list view if it is not there (because there might be a date filter)
            var isCategoryNew = string.CompareOrdinal(oldItem.Category, updatedItem.Category) != 0;
            var li = FindHistoryListViewItem(oldItem.Phrase, oldItem.Category);
            if (li != null)
            {
                li.SubItems[0].Text = updatedItem.Phrase;
                li.SubItems[1].Text = updatedItem.Category;
                li.SubItems[2].Text = updatedItem.Date.ToString(_dateTimeFormat, CultureInfo.InvariantCulture);
                AddCategoryIfNeed(isCategoryNew, updatedItem.Category);
                ProceedAfterAddOrUpdatedHistoryListView();
            }
            else
            {
                AddItemAndGroupToHistoryListView(updatedItem, isCategoryNew);
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
            AddItemAndGroupToHistoryListView(e.HistoryItem, e.IsCategoryNew);
        }

        private delegate void AddHistoryItemToListViewDelegate(HistoryDataItem historyItem, bool isCategoryNew);

        private void AddItemAndGroupToHistoryListView(HistoryDataItem historyItem, bool isCategoryNew)
        {
            if (InvokeRequired)
            {
                Invoke(new AddHistoryItemToListViewDelegate(AddItemAndGroupToHistoryListView), historyItem, isCategoryNew);
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

        private async Task SaveHistoryBackup()
        {
            var stt = Settings.Default;
            if (!string.IsNullOrEmpty(stt.HistoryBackupPath))
                await _history.SaveHistoryToFile(stt.HistoryBackupPath, true);
        }

        private async Task SaveHistory(bool saveMainFile, bool SaveACopy, bool saveBackup)
        {
            if (saveMainFile) await _history.SaveHistoryToFile();
            if (SaveACopy) await _history.SaveHistoryToFileAsACopy();
            if (saveBackup) await SaveHistoryBackup();
        }

        private async Task SaveTasksBackupAsync()
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
        #endregion

        #region History view
        private void SetDateTimeFilter(DateTime begin, DateTime end)
        {
            dtBegin.Value = begin;
            dtEnd.Value = end;
        }

        private void SetDateTimeFilterNow()
        {
            SetDateTimeFilter(DateTime.Now, DateTime.Now);
        }

        private void SwitchVisibleDateTimeFilter(bool needDateFilter)
        {
            dtBegin.Visible = needDateFilter;
            dtEnd.Visible = needDateFilter;
            lblFrom.Visible = needDateFilter;
            lblTo.Visible = needDateFilter;
            btnByDateFilter.Visible = needDateFilter;
        }
        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MM_showCurrentCategory.Checked && !MM_showChronologically.Checked)
                FillHistoryListView();
        }
        private void InitSorterListView()
        {
            _lvwColumnSorter = new ListViewColumnSorter { DateTimeFormat = _dateTimeFormat };
            _lvwColumnSorter.ColumnTypes.Add(typeof(string));
            _lvwColumnSorter.ColumnTypes.Add(typeof(string));
            _lvwColumnSorter.ColumnTypes.Add(typeof(DateTime));
        }
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
            GoBrowsers(selectedItem.Text, true);
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

        private void AddItemsToHistoryListView(string currentCategory, bool filterByDate, DateTime beginDate, DateTime endDate)
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
            FillHistoryListView(MM_showCurrentCategory.Checked, MM_showChronologically.Checked, IsFilterByDateNeed(), dtBegin.Value, dtEnd.Value);
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
                Invoke(new AddCategoryComboboxDelegate(AddCategoryCombobox), category);
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
        #endregion

        #region HotKeys
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
        private void txtBrowse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoBrowsers(txtToSearch.Text, true, true, MM_ForceLoadFromBrowseField.Checked);
            }
        }
        #endregion

        #region Initializations
        private void CefInit()
        {
            var settingsBrowser = new CefSettings
            {
                Locale = "en-US,en",
                AcceptLanguageList = "en-US,en",
                //PersistSessionCookies = false,
            };
            settingsBrowser.CefCommandLineArgs.Remove("mute-audio");
            settingsBrowser.CefCommandLineArgs.Add("enable-media-stream", "1");
            settingsBrowser.CefCommandLineArgs.Add("allow-universal-access-from-files", "1");
            settingsBrowser.CefCommandLineArgs.Add("allow-file-access-from-files", "1");
            settingsBrowser.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.5672.93 Safari/537.36 CefSharp Browser/" + Cef.CefSharpVersion;
            Cef.Initialize(settingsBrowser);
        }
        private void InitFirstTabSelection()
        {
            FillFirstTabComboBox();
            FirstTabComboboxSelectFromSettings();
        }

        [SupportedOSPlatform("windows")]
        private async Task VirtualDesktopPSInitAsync()
        {
            _previousDesktopIndex = -1;
            try
            {
                if (!await VirtualDesktopPowerShell.IsVirtualDesktopInstalled())
                {
                    var result = MessageBox.Show(@"To continue using this application Virtual Desktop Module is needed to be installed. To read more about Virtual Desktop module for powershell you can go to https://github.com/MScholtes/PSVirtualDesktop. If you decide to deny the installation, the application will be closed. Continue installing the module (answer Yes is recommended)?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        var controls = ShowProgressBar("Please wait while the Virtual Desktop module is being installed...");
                        await VirtualDesktopPowerShell.InstallVirtualDesktopAsync();
                        RemoveControls(controls);
                    }
                    else
                    {
                        CloseWithoutPrompt();
                    }
                }
            }
            catch (Exception)
            {
                ShowErrorMessage("An exception occured during installation of Virtual Desktop module.");
                CloseWithoutPrompt();
            }
        }

        private void RemoveControls(List<Control> controls)
        {
            foreach (var control in controls)
            {
                Controls.Remove(control);
                control.Dispose();
            }
        }

        [SupportedOSPlatform("windows")]
        private List<Control> ShowProgressBar(string message)
        {
            var progressBar = new ProgressBar
            {
                Dock = DockStyle.None,
                Style = ProgressBarStyle.Marquee,
                Size = new Size(500, 50),
                Anchor = AnchorStyles.None
            };
            Controls.Add(progressBar);
            progressBar.Location = new Point((Width - progressBar.Width) / 2, (Height - progressBar.Height) / 2);
            progressBar.BringToFront();
            progressBar.Show();

            var label = new Label
            {
                Text = message,
                Dock = DockStyle.None,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
            };

            Controls.Add(label);
            label.Location = new Point((Width - label.Width) / 2, (Height - label.Height) / 2 - progressBar.Size.Height);
            label.BringToFront();
            label.Show();
            Application.DoEvents();

            return new List<Control> { progressBar, label };
        }

        [SupportedOSPlatform("Windows")]
        private async Task InitMainMenuItems()
        {
            MM_ReturnDesktop.DropDownItems.Clear();
            AddReturnDesktopItem("Auto", null, true);
            var desktops = await VirtualDesktopPowerShell.GetDesktopListAsync();
            foreach (var desktop in desktops)
            {
                var name = desktop.Name;
                AddReturnDesktopItem(string.IsNullOrEmpty(name) ? $"Desktop {desktop.Index + 1}" : name, desktop);
            }
            await SetPreviousDesktopMenuText();
        }
        [SupportedOSPlatform("windows")]
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
        private void InitHotKeys()
        {
            _hotKeyGo = HotKeyManager.RegisterHotKey(Keys.X, KeyModifiers.Control | KeyModifiers.Shift);
            _hotKeySwitch = HotKeyManager.RegisterHotKey(Keys.Q, KeyModifiers.Control | KeyModifiers.Shift);
            _hotKeyShow = HotKeyManager.RegisterHotKey(Keys.Q, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
        }

        #endregion

        #region Main form load and shown
        [SupportedOSPlatform("windows")]
        private void MainForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Text = Application.ProductName;
        }

        [SupportedOSPlatform("windows")]
        private async void MainForm_Shown(object sender, EventArgs e)
        {
            CefInit();
            await VirtualDesktopPSInitAsync();
            LoadSettings();
            InitHotKeys();
            InitBrowsers();
            InitTabs();
            await InitMainMenuItems();
            InitFirstTabSelection();
            ForceTranslateOnSelection();
            InitHistory();
            SetDateTimeFilterNow();
            FillCategoriesComboBox();
            InitHistoryListView();
            FillHistoryListView();
            ActivateUnits();
            foreach (var unit in _units)
            {
                unit.LoadDefaultPage();
            }

            ActivateTabsIfNeeded();
            SetThemeFromSettings();
        }
        #endregion

        #region Main menu click handlers
        private void MM_ResetTranslateOnSelection_Click(object sender, EventArgs e)
        {
            foreach(var unit in _units)
                unit.ResetTranslateOnSelection();
            SetTranslateOnSelectionIndeterminate();
            SetMenuItemTranslateOnSelectionCurrent();
        }

        private void SetTranslateOnSelectionIndeterminate()
        {
            MM_TranslateOnSelection.CheckState = CheckState.Indeterminate;
            SaveTranslateOnSelectionOption();
        }
        private void MM_TranslateOnSelectionCurrent_Click(object sender, EventArgs e)
        {
            var currentUnit = GetCurrentUnit();
            if (currentUnit == null)
            {
                MM_TranslateOnSelectionCurrent.Checked = !MM_TranslateOnSelectionCurrent.Checked;
                return;
            }

            currentUnit.Dictionary.TranslateOnSelection = MM_TranslateOnSelectionCurrent.Checked;
            SetTranslateOnSelectionIndeterminate();
        }

        private void MM_TranslateOnSelection_Click(object sender, EventArgs e)
        {
            SaveTranslateOnSelectionOption();
            ForceTranslateOnSelection();
        }

        private void ForceTranslateOnSelection()
        {
            SetTranslateOnSelectionAllBrowsers();
            SetMenuItemTranslateOnSelectionCurrent();
        }

        private void SetTranslateOnSelectionAllBrowsers()
        {
            var state = GetTranslateOnSelectionOption();
            foreach (var unit in _units)
            {
                switch (state)
                {
                    case CheckState.Unchecked:
                        unit.Dictionary.TranslateOnSelection = false;
                        break;
                    case CheckState.Checked:
                        unit.Dictionary.TranslateOnSelection = true;
                        break;
                }
            }
        }

        private CheckState GetTranslateOnSelectionOption()
        {
            var stt = Settings.Default;
            switch (stt.TranslateOnSelection)
            {
                case 0:
                    return CheckState.Unchecked;
                case 1:
                    return CheckState.Checked;
                case 2:
                    return CheckState.Indeterminate;
                default:
                    throw new Exception("Unknown value of TranslateOnSelection option");
            }
        }


        private void SaveTranslateOnSelectionOption()
        {
            var stt = Settings.Default;
            switch (MM_TranslateOnSelection.CheckState)
            {
                case CheckState.Unchecked:
                    stt.TranslateOnSelection = 0;
                    break;
                case CheckState.Checked:
                    stt.TranslateOnSelection = 1;
                    break;
                case CheckState.Indeterminate:
                    stt.TranslateOnSelection = 2;
                    break;
            }
            stt.Save();
        }

        private void SetMenuItemTranslateOnSelectionCurrent()
        {
            var currentUnit = GetCurrentUnit();
            if (currentUnit != null)
                MM_TranslateOnSelectionCurrent.Checked = currentUnit.Dictionary.TranslateOnSelection;
        }

        private void MM_ReloadAll_Click(object sender, EventArgs e)
        {
            ReloadAllBrowsers();
        }
        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetColorTheme(ColorTheme.Dark);
        }
        private void showJSErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var logPath = Settings.Default.ErrorLogPath;
            if (File.Exists(logPath))
                StartProcess(logPath);
            else
                ShowInfoMessage($"There is no file {logPath}. And probably that's a good sign: most likely there is no errors so far.");
        }
        void SetForceLoadFromBrowseField(bool predicate)
        {
            var stt = Settings.Default;
            stt.ForceLoadFromBrowse = predicate;
            stt.Save();
        }
        [SupportedOSPlatform("windows")]
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
        [SupportedOSPlatform("windows")]
        private void FindClearAndFocus()
        {
            txtFindText.Text = "";
            txtFindText.Focus();
            FindInDictionaries("", false);
        }
        [SupportedOSPlatform("windows")]
        private void btnClearFind_Click(object sender, EventArgs e)
        {
            FindClearAndFocus();
        }
        private void MM_SetBackupFolder_Click(object sender, EventArgs e)
        {
            ChooseBackupFolderDialog();
        }
        [SupportedOSPlatform("windows")]
        private void setOALDCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //var stt = Settings.Default;
            //var dialog = new CredentialsForm
            //{
            //    Credentials = new Credentials(stt.OALDUser, stt.OALDPass)
            //};

            //if (dialog.ShowDialog() != DialogResult.OK) return;
            //stt.OALDUser = dialog.Credentials.UserName;
            //stt.OALDPass = dialog.Credentials.Password;
            //stt.Save();
            //ReloadOaldJS();
        }
        [SupportedOSPlatform("windows")]
        private void MM_ShortcutsHelp_Click(object sender, EventArgs e)
        {
            new ShortcutsForm().ShowDialog();
        }

        void SetLoginToOALDOnStart(bool predicate)
        {
            throw new NotFiniteNumberException();
            //var stt = Settings.Default;
            //stt.OALDLoginOnStart = predicate;
            //stt.Save();
        }
        [SupportedOSPlatform("windows")]
        private void MM_LoginToOALDOnStart_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            SetLoginToOALDOnStart(((ToolStripMenuItem)sender).Checked);
        }
        [SupportedOSPlatform("windows")]
        private void MM_AutoSortByDate_Click(object sender, EventArgs e)
        {
            AutoSortByDateOrderOptionSave(MM_AutoSortByDate.Checked);
            if (MM_AutoSortByDate.Checked)
                SortHistoryListViewByDateIfNeeded();
        }

        private void btnClearInput_Click(object sender, EventArgs e)
        {
            SearchClearAndFocus();
        }
        [SupportedOSPlatform("windows")]
        private void SearchClearAndFocus()
        {
            txtToSearch.Clear();
            txtToSearch.Focus();
        }
        [SupportedOSPlatform("windows")]
        private void MM_ForceLoadFromBrowseField_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            if (item.Checked && !AskForConfirmationWarning("This option allows all the browsers to load at the same time. It is not recommened behaviour. It may cause a ban for some services. Do you agree to enable this option?"))
            {
                item.Checked = false;
            }
            SetForceLoadFromBrowseField(item.Checked);
        }

        bool _previousDesktopAuto = true;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //_dictionaryTranslatorUnits[UnitName.Oald].Dictionary.ClickOnElementByClassName(".sound.audio_play_button.pron-us.icon-audio");
        }
        void SetSpeakOnBrowsingOALD(bool predicate)
        {
            var stt = Settings.Default;
            stt.SpeakOnBrowsingOALD = predicate;
            stt.Save();

            ReloadOaldJS();
        }
        [SupportedOSPlatform("windows")]
        private void MM_SpeakOnBrowsingOALD_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            SetSpeakOnBrowsingOALD(((ToolStripMenuItem)sender).Checked);
        }
        private void MM_UseReverseOrder_Click(object sender, EventArgs e)
        {
            _history.EnumerateForward = !MM_UseReverseOrder.Checked;
            ReverseOrderOptionSave(MM_UseReverseOrder.Checked);
            FillHistoryListView();
        }

        private void MM_ShowTasks_Click(object sender, EventArgs e)
        {
            var stt = Settings.Default;
            if (File.Exists(stt.TaskFilePath) || SetTaskPath())
                StartProcess(Settings.Default.TaskFilePath);
        }

        /// <summary>
        /// Opens the dialog to choose the task file path and saves it to the settings.
        /// </summary>
        private bool SetTaskPath()
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"Task files (*.docx)|*.docx|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = @"Choose the task file"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var stt = Settings.Default;
                stt.TaskFilePath = dialog.FileName;
                stt.Save();
                return true;
            }

            return false;
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
        private void MM_showOnlyTodayEntries_Click(object sender, EventArgs e)
        {
            var stt = Settings.Default;
            stt.ShowOnlyTodaysEntries = MM_showOnlyTodayEntries.Checked;
            stt.Save();
            ToggleShowOnlyTodayMenuItems();
            FillHistoryListView();
        }
        /// <summary>
        /// Depends on the state of the <see cref="MM_showOnlyTodayEntries"/> sets the state of the main menu history items and other filters. Does NOT fill the history list view.
        /// </summary>
        private void ToggleShowOnlyTodayMenuItems()
        {
            if (MM_showOnlyTodayEntries.Checked)
            {
                MM_UseDateFilter.Checked = false;
                SetDateTimeFilterNow();
                SwitchVisibleDateTimeFilter(false);
            }
        }
        private void MM_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
        private void showDatainiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProcess(Settings.Default.DataPath);
        }

        private void showTheAppFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProcess(Application.StartupPath);
        }
        private void showChronoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillHistoryListView();
        }

        private void MM_showChronologically_CheckedChanged(object sender, EventArgs e)
        {
            MM_showCurrentCategory.Enabled = !MM_showChronologically.Checked;
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
        private void MM_NeedUrbanDictionary_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            ShowUrban(MM_NeedUrbanDictionary.Checked);
        }
        private void loginToOALDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            LoginToOALD();
        }
        [SupportedOSPlatform("Windows")]
        private async void MM_ReturnDesktopItem_Click(object sender, EventArgs e)
        {
            var item = ((ToolStripMenuItem)sender);
            if (item.Tag == null)
            {
                _previousDesktopAuto = true;
                await SetPreviousDesktopMenuText();
            }
            else
            {
                _previousDesktopAuto = false;
                // SetPreviousDesktop((VirtualDesktop)item.Tag);
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

        private void activateTabsAfterAppStartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var stt = Settings.Default;
            stt.ActivateTabsAfterAppStarts = ((ToolStripMenuItem)sender).Checked;
            stt.Save();
        }
        #endregion

        #region Status bar
        private void stError_DoubleClick(object sender, EventArgs e)
        {
            JScriptStatusErrorReset();
        }

        private void JScriptStatusErrorReset()
        {
            stError.BackColor = Color.Green;
        }

        private void JScriptStatusError()
        {
            stError.BackColor = Color.Red;
        }

        #endregion

        #region Settings
        private void LoadSettings()
        {
            var stt = Settings.Default;
            MM_ForceLoadFromBrowseField.Checked = stt.ForceLoadFromBrowse;
            MM_SpeakOnBrowsingOALD.Checked = stt.SpeakOnBrowsingOALD;
            MM_ActivateTabsAfterAppStarts.Checked = stt.ActivateTabsAfterAppStarts;
            MM_showOnlyTodayEntries.Checked = stt.ShowOnlyTodaysEntries;
            MM_TranslateOnSelection.CheckState = GetTranslateOnSelectionOption();
            ToggleShowOnlyTodayMenuItems();
        }


        private void SetThemeFromSettings()
        {
            //MessageBox.Show(Settings.Default.GoogleForAsyncEvalJS);
            if (Settings.Default.DarkTheme)
                SetColorTheme(ColorTheme.Dark);
        }
        #endregion

        #region Sound <depriecated> 
        //private void Dictionary_FinishAllTasks(BrowserItem sender)
        //{
        //    ClickOnBrowserItems(sender);
        //}

        //private void ClickOnBrowserItems(BrowserItem browser)
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new ClickOnBrowserItemsDelegate(ClickOnBrowserItems), browser);
        //        return;
        //    }
        //    if (!MM_SpeakOnBrowsingOALD.Checked || _silentOALD) return;
        //    browser.ProcessAllItemsToClickAsync();
        //    //var name = Settings.Default.OALD_AudioButton_ID;
        //    //_dictionaryTranslatorUnits[UnitName.Oald].Dictionary.ClickOnElementByClassNameAsync(@$".{name.Replace(' ', '.')}");
        //}
        #endregion

        #region Tab control
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
            foreach (var unit in _units)
            {
                var table = CreateTableForUnit();
                AutoLayoutUnit(table, unit);

                var dictionaryName = unit.Dictionary.BrowserName;
                tabControl1.TabPages.Add(dictionaryName, dictionaryName);
                tabControl1.TabPages[dictionaryName].Tag = unit;
                tabControl1.TabPages[dictionaryName]?.Controls.Add(table);
            }
            tabControl1.TabPages.Add("tabHistory", "History");

            ShowUrban(MM_NeedUrbanDictionary.Checked);
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

        private void Page_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.ClipRectangle);
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
            if (!tabControl1.TabPages.ContainsKey(name))
                return false;

            tabControl1.SelectTab(name);

            return true;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateUnits();
            SetMenuItemTranslateOnSelectionCurrent();
            StopFinding();
            FindInDictionaries(txtFindText.Text, false);
        }

        /// <summary>
        /// Activate all the units according to the ruels: if the tab is active, the unit.translator is activated, otherwise deactivated. Dictionaries are always activated.
        /// </summary>
        private void ActivateUnits()
        {
            var curUnit = GetCurrentUnit();
            if (curUnit == null)
                return;

            foreach (var unit in _units)
            {
                if (unit == curUnit)
                    unit.Translator.Activate();
                else
                    unit.Translator.Deactivate();

                unit.Dictionary.Activate();
            }
        }

        //enum UnitName
        //{
        //    Oald,
        //    Cambridge,
        //    Wiki,
        //    Google
        //}
        #endregion

        #region Virtual desktops
        [SupportedOSPlatform("windows")]
        private async Task SwitchDesktops()
        {
            //TODO: check if cancelling is needed!
            await VirtualDesktopPowerShell.CancelCurrentOperationAsync();

            try
            {
                var thisAppDesktopIndex = await VirtualDesktopPowerShell.GetDesktopIndexFromHandleAsync(Handle);
                var currentDesktopIndex = await VirtualDesktopPowerShell.GetCurrentDesktopIndexAsync();

                if (thisAppDesktopIndex != currentDesktopIndex || !await ReturnToPreviousDesktop())
                {
                    //This app is not on the current desktop or there is no previous desktop, so need to switch to the desktop where this app is
                    await ShowThisAppDesktop(thisAppDesktopIndex, currentDesktopIndex);
                }
            }
            catch (PipelineStoppedException)
            {

            }

        }

        [SupportedOSPlatform("windows")]
        async Task SavePreviousDesktop(int desktop)
        {
            if (_previousDesktopAuto)
                await SetPreviousDesktop(desktop);
        }

        [SupportedOSPlatform("windows")]
        private async Task ShowThisAppDesktop(int thisAppDesktopIndex, int currentDesktopIndex)
        {
            if (thisAppDesktopIndex != currentDesktopIndex)
            {
                //This app is not on the current desktop, so need to switch to it
                await SavePreviousDesktop(currentDesktopIndex);
                await VirtualDesktopPowerShell.SwitchToDesktopAsync(thisAppDesktopIndex);
            }

            WindowOnTop();
            SearchTextBoxFocus();
        }

        [SupportedOSPlatform("windows")]
        private async Task ShowThisAppDesktop()
        {
            var thisAppDesktopIndex = await VirtualDesktopPowerShell.GetDesktopIndexFromHandleAsync(Handle);
            var currentDesktopIndex = await VirtualDesktopPowerShell.GetCurrentDesktopIndexAsync();

            await ShowThisAppDesktop(thisAppDesktopIndex, currentDesktopIndex);
        }
        [SupportedOSPlatform("Windows")]
        private async Task SetPreviousDesktop(int desktopIndex)
        {
            _previousDesktopIndex = desktopIndex;
            await SetPreviousDesktopMenuText();
        }

        [SupportedOSPlatform("Windows")]
        private async Task SetPreviousDesktopMenuText()
        {
            var name = "None";
            var desktopTag = " desktop";
            if (_previousDesktopIndex != -1)
            {
                name = await VirtualDesktopPowerShell.GetDesktopNameAsync(_previousDesktopIndex);
                desktopTag = "";
            }

            MM_ReturnDesktop.Text = $"Previous{desktopTag}: {(_previousDesktopAuto ? "(Auto) " : "")} {name}";
        }

        [SupportedOSPlatform("Windows")]
        private async Task<bool> ReturnToPreviousDesktop()
        {
            if (_previousDesktopIndex == -1) return false;
            await VirtualDesktopPowerShell.SwitchToDesktopAsync(_previousDesktopIndex);
            return true;
        }

        private async void returnToPreviousDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ReturnToPreviousDesktop();
        }
        #endregion
    }
}
