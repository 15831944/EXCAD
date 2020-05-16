namespace EXCAD
{
    partial class frmAppHost
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
            this.pnlHost = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pnlHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHost
            // 
            this.pnlHost.Controls.Add(this.lblLoading);
            this.pnlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHost.Location = new System.Drawing.Point(0, 0);
            this.pnlHost.Name = "pnlHost";
            this.pnlHost.Size = new System.Drawing.Size(684, 411);
            this.pnlHost.TabIndex = 3;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.ForeColor = System.Drawing.Color.DimGray;
            this.lblLoading.Location = new System.Drawing.Point(12, 9);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(153, 17);
            this.lblLoading.TabIndex = 2;
            this.lblLoading.Text = "Loading external app...";
            // 
            // frmAppHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.pnlHost);
            this.Name = "frmAppHost";
            this.Text = "Application Host";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAppHost_FormClosing);
            this.Load += new System.EventHandler(this.frmAppHost_Load);
            this.ResizeEnd += new System.EventHandler(this.frmAppHost_ResizeEnd);
            this.pnlHost.ResumeLayout(false);
            this.pnlHost.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHost;
        private System.Windows.Forms.Label lblLoading;
    }
}