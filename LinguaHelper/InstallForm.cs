using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace LinguaHelper
{
    [SupportedOSPlatform("windows")]
    public partial class InstallForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool InstallSucceeded { get; private set; }

        private readonly ProgressBar _progressBar;

        public InstallForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            Text = "Installing...";
            Width = 400;
            Height = 100;

            _progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Dock = DockStyle.Fill
            };

            Controls.Add(_progressBar);
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                await VirtualDesktopPowerShell.CleanInstallVirtualDesktopAsync();
                InstallSucceeded = true;
            }
            catch
            {
                InstallSucceeded = false;
            }

            // Allow form to finish rendering before closing
            await Task.Delay(200); // Optional, gives UI a breath

            Close();
        }
    }


}
