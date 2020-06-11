using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

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
                AcceptLanguageList = "en-US,en"
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

        void GoBrowsers(string text)
        {
            if (String.IsNullOrEmpty(text)) return;
            _browsers[0].Load($"https://www.oxfordlearnersdictionaries.com/search/english/?q={text}");
            _browsers[1].Load($"https://translate.google.com/#view=home&op=translate&sl=en&tl=ru&text={text}");
            if(MM_NeedUrbanDictionary.Checked) _browsers[2].Load($"https://www.urbandictionary.com/define.php?term={text}");
        }

        private List<ChromiumWebBrowser> _browsers;
        readonly List<string> _tabNames = new List<string>() {"OALD", "Translate", "Urban"};

        private int? _hotKeyId = null;
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
    }
}
