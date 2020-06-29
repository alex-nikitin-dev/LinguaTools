using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            foreach (var item in _data)
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

        void GoBrowsers(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            AddToFile(text);
            
            _browsers[0].Load($"https://www.oxfordlearnersdictionaries.com/search/english/?q={text}");
            _browsers[1].Load($"https://translate.google.com/#view=home&op=translate&sl=en&tl=ru&text={text}");
            if(MM_NeedUrbanDictionary.Checked) _browsers[2].Load($"https://www.urbandictionary.com/define.php?term={text}");
        }

        private void AddToFile(string text)
        {
            if (!IsThereItem(text, GetCategory()))
            {
                File.AppendAllText(Settings.Default.DataPath,
                    $@"{text};;{GetCategory()};;{DateTime.Now.ToString(_dateTimeFormat, CultureInfo.InvariantCulture)}{Environment.NewLine}"
                );
                UpdateHistory();
            }
        }

        private List<ChromiumWebBrowser> _browsers;
       // private readonly List<string> _tabNames = new List<string>() {"OALD", "Translate", "Urban"};

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
            }

            var table = new TableLayoutPanel() {ColumnCount = 2, RowCount = 1, Dock = DockStyle.Fill};
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            table.Controls.Add(_browsers[0], 0, 0);
            table.Controls.Add(_browsers[1], 1, 0);
            
            
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
        }

        private ListView _lstHistory;
        private void InitHistory()
        {
            tabControl1.TabPages.Add("tabHistory", "History");
            _lstHistory = new ListView() { View = View.Details, Dock = DockStyle.Fill };
            tabControl1.TabPages["tabHistory"].Controls.Add(_lstHistory);
            _lstHistory.Columns.Add(new ColumnHeader("phrase"));
            _lstHistory.Columns.Add(new ColumnHeader("category"));
            _lstHistory.Columns.Add(new ColumnHeader("date"));
            _lstHistory.FullRowSelect = true;


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
            foreach (var dataItem in _data)
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
            foreach (var dataItem in _data)
            {
                cbxCategory.Items.Add(dataItem.Key);
            }
        }


        class DataItem
        {
            internal readonly string Phrase;
            internal DateTime Date;

            public DataItem(string phrase, DateTime date)
            {
                Phrase = phrase;
                Date = date;
            }
        }

        private Dictionary<string,List<DataItem>> _data = new Dictionary<string, List<DataItem>>();

        private void LoadData(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                var rows = File.ReadAllLines(path);
                _data.Clear();

                foreach (var row in rows)
                {
                    if(string.IsNullOrEmpty(row)) continue;
                    var items = row.Split(new[] {";;"}, StringSplitOptions.RemoveEmptyEntries);
                    var category = items[1];
                    if (!_data.ContainsKey(category))
                    {
                        _data.Add(category, new List<DataItem>());
                    }

                    _data[category].Add(new DataItem(items[0],
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            LoadData(Settings.Default.DataPath);
            FillHistory();
        }
    }
}
