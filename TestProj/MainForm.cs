using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        bool IsThereItem(string phrase, string category)
        {
            foreach (var item in _historyData)
            {
                foreach (var row in item.Value)
                {
                    if (string.CompareOrdinal(row.Phrase, phrase) == 0 &&
                        string.CompareOrdinal(item.Key, category) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
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
                AddHistoryItem(text, GetCategory());
            
            GoOALD(text);
            GoGoogleTranslate(text);
            if(MM_NeedUrbanDictionary.Checked) GoUrbanDictionary(text);
        }

        //[Obsolete]
        //private void AddToFile(string text)
        //{
        //    if (!IsThereItem(text, GetCategory()))
        //    {
        //        File.AppendAllText(Settings.Default.DataPath,
        //            $@"{text};;{GetCategory()};;{GetTimeStampNow()}{Environment.NewLine}"
        //        );
        //        UpdateHistory();
        //    }
        //}

        private string GetTimeStampNow()
        {
            return GetDataTimeFormatted(DateTime.Now);
        }

        private string GetDataTimeFormatted(DateTime dt)
        {
            return dt.ToString(_dateTimeFormat, CultureInfo.InvariantCulture);
        }

        private string GetCopyPath()
        {
            var path = Settings.Default.DataPath;
            var copyName = Path.GetFileNameWithoutExtension(path) + "_copy_" + GetTimeStampNow();
            var copyExt = Path.GetExtension(path);
            var copyPath = Path.GetDirectoryName(path);

            return Path.Combine(copyPath ?? throw new InvalidOperationException(), copyName, copyExt);
        }

        void CreateHistoryFileCopy()
        {
            File.Copy(Settings.Default.DataPath, GetCopyPath());
        }

        private void SaveHistoryToFile(string path)
        {
            foreach (var (category, data) in _historyData.Select(x => (x.Key, x.Value)))
            {
                foreach (var dataItem in data)
                {
                    File.AppendAllText(path,
                        $@"{dataItem.Phrase};;{category};;{GetDataTimeFormatted(dataItem.Date)}{Environment.NewLine}");
                }
            }
        }

        private void SaveHistoryToFileAsACopy()
        {
            SaveHistoryToFile(GetCopyPath());
        }

        private void SaveHistoryToFile(bool saveTheCopy=true)
        {
            if (saveTheCopy)
                CreateHistoryFileCopy(); 

            SaveHistoryToFile(Settings.Default.DataPath);
        }

        private void AddHistoryItem(string text,string category)
        {
            if (!IsThereItem(text, category))
            {
                if (!_historyData.ContainsKey(category))
                    _historyData.Add(category, new List<HistoryDataItem>());
                if (_historyData[category] == null)
                    _historyData[category] = new List<HistoryDataItem>();

                _historyData[category].Add(new HistoryDataItem(text, DateTime.Now));
            }
        }

        private List<ChromiumWebBrowser> _browsers;

        private int? _hotKeyId;
        private void MainForm_Load(object sender, EventArgs e)
        {
            _hotKeyId = HotKeyManager.RegisterHotKey(Keys.X, KeyModifiers.Control | KeyModifiers.Shift);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
            _browsers = new List<ChromiumWebBrowser>();
            for (int i = 0; i < 3; i++)
            {
                _browsers.Add(new ChromiumWebBrowser(""));
                _browsers[i].Dock = DockStyle.Fill;
                _browsers[i].FrameLoadEnd += MainForm_FrameLoadEnd;
            }

            var table = new TableLayoutPanel() {ColumnCount = 2, RowCount = 1, Dock = DockStyle.Fill};
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            table.Controls.Add(_browsers[0], 0, 0);
            table.Controls.Add(_browsers[1], 1, 0);

            //tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.Font = new Font(tabControl1.Font.FontFamily,12,FontStyle.Regular);
            tabControl1.TabPages.Add("OALD");
            tabControl1.TabPages.Add("Urban");
            tabControl1.TabPages[0].Controls.Add(table);
            tabControl1.TabPages[1].Controls.Add(_browsers[2]);

            _browsers[0].FrameLoadEnd += OALD_Browser_LoadingStateChanged;

            LoadData(Settings.Default.DataPath);
            FillCategories();
            InitHistory();
            FillHistory();
            LoginToOALD();
            GoGoogleTranslate("test");
            SetColorTheme(ColorTheme.Dark);
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
        private void InitHistory()
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
                new MenuItem("Update", UpdateHistory),
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
            throw new NotImplementedException();
        }

        private void UpdateHistory(object sender, EventArgs e)
        {
            UpdateHistory();
        }

        void UpdateHistory()
        {
            LoadData(Settings.Default.DataPath);
            FillHistory();
            FillCategories();
        }

        private void FillHistory()
        {
            _lstHistory.Items.Clear();
            foreach (var dataItem in _historyData)
            {
                var group = new ListViewGroup(dataItem.Key, dataItem.Key);
                _lstHistory.Groups.Add(group);
                
                foreach (var item in dataItem.Value)
                {
                    var li = new ListViewItem { Text = item.Phrase };
                    li.SubItems.Add(dataItem.Key);
                    li.SubItems.Add(item.Date.ToString(_dateTimeFormat, CultureInfo.InvariantCulture));
                    li.Group = group;
                    _lstHistory.Items.Add(li);
                }
            }

            _lstHistory.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            _lstHistory.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            _lstHistory.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
       
        private string _dateTimeFormat = @"dd.MMMM.yyyy HH:mm:ss";

        private void FillCategories()
        {
            cbxCategory.Items.Clear();
            foreach (var dataItem in _historyData)
            {
                cbxCategory.Items.Add(dataItem.Key);
            }
        }

        class HistoryDataItem
        {
            internal readonly string Phrase;
            internal DateTime Date;

            public HistoryDataItem(string phrase, DateTime date)
            {
                Phrase = phrase;
                Date = date;
            }
        }

        private readonly Dictionary<string,List<HistoryDataItem>> _historyData = new Dictionary<string, List<HistoryDataItem>>();

        private void LoadData(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                var rows = File.ReadAllLines(path);
                _historyData.Clear();

                foreach (var row in rows)
                {
                    if(string.IsNullOrEmpty(row)) continue;
                    var items = row.Split(new[] {";;"}, StringSplitOptions.RemoveEmptyEntries);
                    var category = items[1];
                    if (!_historyData.ContainsKey(category))
                    {
                        _historyData.Add(category, new List<HistoryDataItem>());
                    }

                    _historyData[category].Add(new HistoryDataItem(items[0],
                        DateTime.ParseExact(items[2], _dateTimeFormat, CultureInfo.InvariantCulture)));
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Error loading data from file");
            }
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

        private void SaveHistoryDialog()
        {
            var wannaSave = MessageBox.Show(@"Do you want to save data before exit?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (wannaSave == DialogResult.Yes)
            {
                SaveHistoryToFile();
            }
            else
            {
                SaveHistoryToFileAsACopy();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveHistoryDialog();

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

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sure = MessageBox.Show(@"You will lose all unsaved data. Are you sure?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sure == DialogResult.Yes)
            {
                LoadData(Settings.Default.DataPath);
                FillHistory();
            }
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
            SaveHistoryToFile();
        }
    }
}
