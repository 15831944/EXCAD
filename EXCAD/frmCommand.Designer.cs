namespace EXCAD
{
    partial class frmCommand
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCenterXCell = new System.Windows.Forms.TextBox();
            this.btnDrawCircle = new System.Windows.Forms.Button();
            this.btnWriteObjectInfo = new System.Windows.Forms.Button();
            this.txtObjectInfoSheet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRadiusCell = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCenterYCell = new System.Windows.Forms.TextBox();
            this.btnDrawLine = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Center X:";
            // 
            // txtCenterXCell
            // 
            this.txtCenterXCell.Location = new System.Drawing.Point(12, 24);
            this.txtCenterXCell.Name = "txtCenterXCell";
            this.txtCenterXCell.Size = new System.Drawing.Size(60, 20);
            this.txtCenterXCell.TabIndex = 12;
            this.txtCenterXCell.Text = "Sheet1!A1";
            // 
            // btnDrawCircle
            // 
            this.btnDrawCircle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawCircle.Location = new System.Drawing.Point(12, 50);
            this.btnDrawCircle.Name = "btnDrawCircle";
            this.btnDrawCircle.Size = new System.Drawing.Size(192, 23);
            this.btnDrawCircle.TabIndex = 13;
            this.btnDrawCircle.Text = "Draw Circle";
            this.btnDrawCircle.UseVisualStyleBackColor = true;
            this.btnDrawCircle.Click += new System.EventHandler(this.btnDrawCircle_Click);
            // 
            // btnWriteObjectInfo
            // 
            this.btnWriteObjectInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteObjectInfo.Location = new System.Drawing.Point(12, 169);
            this.btnWriteObjectInfo.Name = "btnWriteObjectInfo";
            this.btnWriteObjectInfo.Size = new System.Drawing.Size(192, 23);
            this.btnWriteObjectInfo.TabIndex = 17;
            this.btnWriteObjectInfo.Text = "Write Selected Objects Info";
            this.btnWriteObjectInfo.UseVisualStyleBackColor = true;
            this.btnWriteObjectInfo.Click += new System.EventHandler(this.btnWriteObjectInfo_Click);
            // 
            // txtObjectInfoSheet
            // 
            this.txtObjectInfoSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjectInfoSheet.Location = new System.Drawing.Point(12, 143);
            this.txtObjectInfoSheet.Name = "txtObjectInfoSheet";
            this.txtObjectInfoSheet.Size = new System.Drawing.Size(192, 20);
            this.txtObjectInfoSheet.TabIndex = 16;
            this.txtObjectInfoSheet.Text = "Sheet1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Sheet to Write Info:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(0, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 1);
            this.label3.TabIndex = 14;
            // 
            // txtRadiusCell
            // 
            this.txtRadiusCell.Location = new System.Drawing.Point(144, 24);
            this.txtRadiusCell.Name = "txtRadiusCell";
            this.txtRadiusCell.Size = new System.Drawing.Size(60, 20);
            this.txtRadiusCell.TabIndex = 18;
            this.txtRadiusCell.Text = "Sheet1!A3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Radius:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Center Y:";
            // 
            // txtCenterYCell
            // 
            this.txtCenterYCell.Location = new System.Drawing.Point(78, 24);
            this.txtCenterYCell.Name = "txtCenterYCell";
            this.txtCenterYCell.Size = new System.Drawing.Size(60, 20);
            this.txtCenterYCell.TabIndex = 20;
            this.txtCenterYCell.Text = "Sheet1!A2";
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawLine.Location = new System.Drawing.Point(12, 89);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(192, 23);
            this.btnDrawLine.TabIndex = 22;
            this.btnDrawLine.Text = "Draw Line (0, 0), (100, 100)";
            this.btnDrawLine.UseVisualStyleBackColor = true;
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(-1, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(218, 1);
            this.label6.TabIndex = 23;
            // 
            // frmCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 203);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnDrawLine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCenterYCell);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRadiusCell);
            this.Controls.Add(this.btnWriteObjectInfo);
            this.Controls.Add(this.txtObjectInfoSheet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDrawCircle);
            this.Controls.Add(this.txtCenterXCell);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmCommand";
            this.ShowInTaskbar = false;
            this.Text = "Command";
            this.Load += new System.EventHandler(this.frmCommand_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCenterXCell;
        private System.Windows.Forms.Button btnDrawCircle;
        private System.Windows.Forms.Button btnWriteObjectInfo;
        private System.Windows.Forms.TextBox txtObjectInfoSheet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRadiusCell;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCenterYCell;
        private System.Windows.Forms.Button btnDrawLine;
        private System.Windows.Forms.Label label6;
    }
}