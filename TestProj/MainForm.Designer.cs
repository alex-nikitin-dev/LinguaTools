﻿namespace TestProj
{
    partial class MainForm
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
            this.txtToSearch = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.chkHotKeyEnabled = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_NeedUrbanDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtToSearch
            // 
            this.txtToSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToSearch.Location = new System.Drawing.Point(1, 32);
            this.txtToSearch.Name = "txtToSearch";
            this.txtToSearch.Size = new System.Drawing.Size(1043, 20);
            this.txtToSearch.TabIndex = 1;
            this.txtToSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToSearch_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(1, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1213, 706);
            this.tabControl1.TabIndex = 2;
            // 
            // chkHotKeyEnabled
            // 
            this.chkHotKeyEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHotKeyEnabled.AutoSize = true;
            this.chkHotKeyEnabled.Checked = true;
            this.chkHotKeyEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHotKeyEnabled.Location = new System.Drawing.Point(1091, 34);
            this.chkHotKeyEnabled.Name = "chkHotKeyEnabled";
            this.chkHotKeyEnabled.Size = new System.Drawing.Size(111, 17);
            this.chkHotKeyEnabled.TabIndex = 3;
            this.chkHotKeyEnabled.Text = "Hot key\'s enabled";
            this.chkHotKeyEnabled.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1214, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_NeedUrbanDictionary});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // MM_NeedUrbanDictionary
            // 
            this.MM_NeedUrbanDictionary.CheckOnClick = true;
            this.MM_NeedUrbanDictionary.Name = "MM_NeedUrbanDictionary";
            this.MM_NeedUrbanDictionary.Size = new System.Drawing.Size(194, 22);
            this.MM_NeedUrbanDictionary.Text = "Need Urban Dictionary";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 773);
            this.Controls.Add(this.chkHotKeyEnabled);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtToSearch);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtToSearch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox chkHotKeyEnabled;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_NeedUrbanDictionary;
    }
}

