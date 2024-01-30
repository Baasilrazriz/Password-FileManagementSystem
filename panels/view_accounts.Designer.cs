namespace Pms.panels
{
    partial class view_accounts
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.info = new Guna.UI2.WinForms.Guna2Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.info.SuspendLayout();
            this.SuspendLayout();
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.White;
            this.info.Controls.Add(this.label8);
            this.info.Location = new System.Drawing.Point(3, 3);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(926, 769);
            this.info.TabIndex = 69;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(429, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "view";
            // 
            // view_accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.info);
            this.Name = "view_accounts";
            this.Size = new System.Drawing.Size(1508, 908);
            this.info.ResumeLayout(false);
            this.info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel info;
        private System.Windows.Forms.Label label8;
    }
}
