using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace LinguaHelper
{
    [SupportedOSPlatform("Windows")]
    public partial class SetOptionsForm : Form
    {
        public SetOptionsForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var stt = Properties.Settings.Default;
            if (stt != null)
            {
                stt.GroqAIKey = txtGroqAPIKey.Text.Trim();
            }
            stt.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SetOptionsForm_Shown(object sender, EventArgs e)
        {
            var stt = Properties.Settings.Default;
            if (stt != null)
            {
                txtGroqAPIKey.Text = stt.GroqAIKey ?? string.Empty;
            }
        }
    }
}
