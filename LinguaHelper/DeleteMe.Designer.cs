namespace LinguaHelper
{
    partial class DeleteMe
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
            chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            SuspendLayout();
            // 
            // chromiumWebBrowser1
            // 
            chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            chromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            chromiumWebBrowser1.Location = new System.Drawing.Point(0, 0);
            chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            chromiumWebBrowser1.Size = new System.Drawing.Size(1870, 1247);
            chromiumWebBrowser1.TabIndex = 0;
            // 
            // DeleteMe
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1870, 1247);
            Controls.Add(chromiumWebBrowser1);
            Name = "DeleteMe";
            Text = "DeleteMe";
            ResumeLayout(false);
        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
    }
}