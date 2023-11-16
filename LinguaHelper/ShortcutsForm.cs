using System;
using System.Windows.Forms;

namespace LinguaHelper
{
    public partial class ShortcutsForm : Form
    {
        public ShortcutsForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
