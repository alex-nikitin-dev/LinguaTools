namespace TestProj
{
    partial class CredentialsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSave = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            chkShowPass = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            txtPass = new System.Windows.Forms.TextBox();
            txtUser = new System.Windows.Forms.TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            btnSave.Location = new System.Drawing.Point(493, 346);
            btnSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(125, 44);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chkShowPass);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtPass);
            groupBox1.Controls.Add(txtUser);
            groupBox1.Location = new System.Drawing.Point(20, 23);
            groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            groupBox1.Size = new System.Drawing.Size(598, 312);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Your credentials";
            // 
            // chkShowPass
            // 
            chkShowPass.AutoSize = true;
            chkShowPass.Location = new System.Drawing.Point(10, 250);
            chkShowPass.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            chkShowPass.Name = "chkShowPass";
            chkShowPass.Size = new System.Drawing.Size(164, 29);
            chkShowPass.TabIndex = 4;
            chkShowPass.Text = "Show password";
            chkShowPass.UseVisualStyleBackColor = true;
            chkShowPass.CheckedChanged += chkShowPass_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(397, 165);
            label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 25);
            label2.TabIndex = 3;
            label2.Text = "password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(397, 87);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 25);
            label1.TabIndex = 2;
            label1.Text = "login";
            // 
            // txtPass
            // 
            txtPass.Location = new System.Drawing.Point(10, 160);
            txtPass.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            txtPass.Name = "txtPass";
            txtPass.Size = new System.Drawing.Size(374, 31);
            txtPass.TabIndex = 1;
            // 
            // txtUser
            // 
            txtUser.Location = new System.Drawing.Point(10, 73);
            txtUser.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            txtUser.Name = "txtUser";
            txtUser.Size = new System.Drawing.Size(374, 31);
            txtUser.TabIndex = 0;
            // 
            // CredentialsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(638, 413);
            Controls.Add(btnSave);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CredentialsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Credentials Form";
            Load += CredentialsForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
    }
}