
namespace LinguaHelper
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            logoPictureBox = new System.Windows.Forms.PictureBox();
            labelProductName = new System.Windows.Forms.Label();
            labelVersion = new System.Windows.Forms.Label();
            labelCopyright = new System.Windows.Forms.Label();
            labelCompanyName = new System.Windows.Forms.Label();
            okButton = new System.Windows.Forms.Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.0952377F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.9047623F));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(labelCompanyName, 1, 3);
            tableLayoutPanel.Controls.Add(okButton, 1, 5);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(15, 17);
            tableLayoutPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.85285F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.50750732F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new System.Drawing.Size(965, 645);
            tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            logoPictureBox.ErrorImage = null;
            logoPictureBox.Image = (System.Drawing.Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new System.Drawing.Point(5, 6);
            logoPictureBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 6);
            logoPictureBox.Size = new System.Drawing.Size(357, 633);
            logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProductName.Location = new System.Drawing.Point(377, 0);
            labelProductName.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            labelProductName.MaximumSize = new System.Drawing.Size(0, 33);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new System.Drawing.Size(583, 33);
            labelProductName.TabIndex = 19;
            labelProductName.Text = "Product Name";
            labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVersion.Location = new System.Drawing.Point(377, 64);
            labelVersion.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            labelVersion.MaximumSize = new System.Drawing.Size(0, 33);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new System.Drawing.Size(583, 33);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "Version";
            labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCopyright.Location = new System.Drawing.Point(377, 128);
            labelCopyright.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            labelCopyright.MaximumSize = new System.Drawing.Size(0, 33);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new System.Drawing.Size(583, 33);
            labelCopyright.TabIndex = 21;
            labelCopyright.Text = "Copyright © Oleksii Nikitin (alex.nikitin.dev@gmail.com), 2023";
            labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCompanyName.Location = new System.Drawing.Point(377, 192);
            labelCompanyName.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            labelCompanyName.MaximumSize = new System.Drawing.Size(0, 33);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new System.Drawing.Size(583, 33);
            labelCompanyName.TabIndex = 22;
            labelCompanyName.Text = "Company Name";
            labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            okButton.Location = new System.Drawing.Point(835, 601);
            okButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(125, 38);
            okButton.TabIndex = 24;
            okButton.Text = "&OK";
            // 
            // AboutForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(995, 679);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            Padding = new System.Windows.Forms.Padding(15, 17, 15, 17);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "AboutForm";
            Shown += AboutForm_Shown;
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Button okButton;

    }
}
