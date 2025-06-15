namespace LinguaHelper
{
    partial class SetOptionsForm
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
            btnOK = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtGroqAPIKey = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnCancel = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(632, 671);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(112, 34);
            btnOK.TabIndex = 0;
            btnOK.Text = "Save";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(22, 84);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(117, 25);
            label1.TabIndex = 1;
            label1.Text = "Groq API Key";
            // 
            // txtGroqAPIKey
            // 
            txtGroqAPIKey.Location = new System.Drawing.Point(172, 80);
            txtGroqAPIKey.Name = "txtGroqAPIKey";
            txtGroqAPIKey.Size = new System.Drawing.Size(620, 31);
            txtGroqAPIKey.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtGroqAPIKey);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(12, 31);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(862, 601);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Options";
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(762, 671);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(112, 34);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SetOptionsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(886, 717);
            Controls.Add(btnCancel);
            Controls.Add(groupBox1);
            Controls.Add(btnOK);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetOptionsForm";
            Text = "SetOptionsForm";
            Shown += SetOptionsForm_Shown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGroqAPIKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
    }
}