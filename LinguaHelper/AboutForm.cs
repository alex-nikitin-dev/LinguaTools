using RichControls;
using System;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace LinguaHelper
{
    [SupportedOSPlatform("windows")]
    partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Text = string.Format("About {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = string.Format("Version {0}", AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;

            CustomInitialization();
        }
        DataGridView _dataGridPackages;
        RichTabControl _richTabControl;
        TextBox _txtDescription;
        private void CustomInitialization()
        {
            _txtDescription = new TextBox()
            {
                Multiline = true,
                Dock = DockStyle.Fill,
            };

            _dataGridPackages = new DataGridView()
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ShowCellErrors = false,
                ShowCellToolTips = false,
                ShowEditingIcon = false,
                ShowRowErrors = false,
            };

            _richTabControl = new RichTabControl()
            {
                Dock = DockStyle.Fill,
            };

            tableLayoutPanel.Controls.Add(_richTabControl, 1, 4);

            _richTabControl.TabPages.Add(new TabPage()
            {
                Text = "Description",
                Controls = { _txtDescription },
            });
            _richTabControl.TabPages.Add(new TabPage()
            {
                Text = "Packages",
                Controls = { _dataGridPackages },
            });
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            _txtDescription.Text = AssemblyDescription;
            _dataGridPackages.Rows.Clear();
            _dataGridPackages.Columns.Clear();
            _dataGridPackages.Columns.Add("Name", "Name");
            _dataGridPackages.Columns.Add("Version", "Version");

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var assemblyName = assembly.GetName();
                if (!assemblyName.Name.StartsWith("System") && !assemblyName.Name.StartsWith("Microsoft"))
                {
                    _dataGridPackages.Rows.Add(assemblyName.Name, assemblyName.Version);
                }
            }

            var themeManager = ThemeManagerLoader.LoadThemeManager();
            _richTabControl.ThemeManager.SetTotalColors(themeManager);
            if (themeManager.CurrentTheme == ThemeManagement.Theme.Dark)
            {
                themeManager.ApplyTheme(this);
            }
        }
    }
}
