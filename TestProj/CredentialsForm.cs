using System;
using System.Windows.Forms;

namespace TestProj
{
    public partial class CredentialsForm : Form
    {
        public CredentialsForm()
        {
            InitializeComponent();
            Credentials = new Credentials();
        }

        private readonly char _passwordChar = '*';
        public Credentials Credentials { get; set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Credentials = new Credentials(txtUser.Text, txtPass.Text);
            DialogResult = DialogResult.OK;
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = chkShowPass.Checked ? '\0' : _passwordChar;
        }

        private void CredentialsForm_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            txtPass.PasswordChar = _passwordChar;

            txtUser.Text = Credentials.UserName;
            txtPass.Text = Credentials.Password;
        }
    }
}
