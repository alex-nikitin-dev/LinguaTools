using System;
using System.Windows.Forms;

namespace TestProj
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
