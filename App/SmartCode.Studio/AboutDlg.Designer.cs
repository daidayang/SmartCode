/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace SmartCode.Studio
{
    partial class AboutDlg
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uiLblCaption = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiLblCopyRight = new System.Windows.Forms.Label();
            this.uiLinkEmail = new System.Windows.Forms.LinkLabel();
            this.uiLinkWebSite = new System.Windows.Forms.LinkLabel();
            this.uiBtnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SmartCode.Studio.Properties.Resources.AboutImg;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::SmartCode.Studio.Properties.Resources.AboutImg;
            this.pictureBox1.InitialImage = global::SmartCode.Studio.Properties.Resources.AboutImg;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 189);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // uiLblCaption
            // 
            this.uiLblCaption.AutoSize = true;
            this.uiLblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLblCaption.Location = new System.Drawing.Point(116, 12);
            this.uiLblCaption.Name = "uiLblCaption";
            this.uiLblCaption.Size = new System.Drawing.Size(118, 15);
            this.uiLblCaption.TabIndex = 1;
            this.uiLblCaption.Text = "Smart Code V 2.0";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(119, 39);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(261, 76);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "Released under both BSD license and Lesser GPL library license.\nWhenever there is" +
                " any discrepancy between the two licenses, the BSD license will take precedence." +
                "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiLinkWebSite);
            this.groupBox1.Controls.Add(this.uiLinkEmail);
            this.groupBox1.Controls.Add(this.uiLblCopyRight);
            this.groupBox1.Location = new System.Drawing.Point(119, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // uiLblCopyRight
            // 
            this.uiLblCopyRight.AutoSize = true;
            this.uiLblCopyRight.Location = new System.Drawing.Point(18, 17);
            this.uiLblCopyRight.Name = "uiLblCopyRight";
            this.uiLblCopyRight.Size = new System.Drawing.Size(191, 13);
            this.uiLblCopyRight.TabIndex = 0;
            this.uiLblCopyRight.Text = "Copyright © 2005-2007 Danilo Mendez";
            // 
            // uiLinkEmail
            // 
            this.uiLinkEmail.AutoSize = true;
            this.uiLinkEmail.Location = new System.Drawing.Point(18, 36);
            this.uiLinkEmail.Name = "uiLinkEmail";
            this.uiLinkEmail.Size = new System.Drawing.Size(137, 13);
            this.uiLinkEmail.TabIndex = 1;
            this.uiLinkEmail.TabStop = true;
            this.uiLinkEmail.Text = "danilo.mendez@kontac.net";
            this.uiLinkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uiLinkEmail_LinkClicked);
            // 
            // uiLinkWebSite
            // 
            this.uiLinkWebSite.AutoSize = true;
            this.uiLinkWebSite.Location = new System.Drawing.Point(18, 57);
            this.uiLinkWebSite.Name = "uiLinkWebSite";
            this.uiLinkWebSite.Size = new System.Drawing.Size(85, 13);
            this.uiLinkWebSite.TabIndex = 2;
            this.uiLinkWebSite.TabStop = true;
            this.uiLinkWebSite.Text = "www.kontac.net";
            this.uiLinkWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uiLinkWebSite_LinkClicked);
            // 
            // uiBtnOK
            // 
            this.uiBtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiBtnOK.Location = new System.Drawing.Point(304, 213);
            this.uiBtnOK.Name = "uiBtnOK";
            this.uiBtnOK.Size = new System.Drawing.Size(75, 23);
            this.uiBtnOK.TabIndex = 4;
            this.uiBtnOK.Text = "&OK";
            this.uiBtnOK.UseVisualStyleBackColor = true;
            // 
            // AboutDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiBtnOK;
            this.ClientSize = new System.Drawing.Size(391, 248);
            this.Controls.Add(this.uiBtnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.uiLblCaption);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Smart Code";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label uiLblCaption;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel uiLinkWebSite;
        private System.Windows.Forms.LinkLabel uiLinkEmail;
        private System.Windows.Forms.Label uiLblCopyRight;
        private System.Windows.Forms.Button uiBtnOK;
    }
}