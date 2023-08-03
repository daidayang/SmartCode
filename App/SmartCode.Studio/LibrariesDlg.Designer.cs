/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace SmartCode.Studio
{
    partial class LibrariesDlg
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibrariesDlg));
            this.uiClose = new System.Windows.Forms.Button();
            this.uiTVLibraries = new System.Windows.Forms.TreeView();
            this.uiIcons = new System.Windows.Forms.ImageList(this.components);
            this.uiAddFromGAC = new System.Windows.Forms.Button();
            this.uiRemove = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiClose
            // 
            this.uiClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiClose.Location = new System.Drawing.Point(246, 288);
            this.uiClose.Name = "uiClose";
            this.uiClose.Size = new System.Drawing.Size(75, 23);
            this.uiClose.TabIndex = 7;
            this.uiClose.Text = "&Close";
            this.uiClose.UseVisualStyleBackColor = true;
            // 
            // uiTVLibraries
            // 
            this.uiTVLibraries.ImageIndex = 0;
            this.uiTVLibraries.ImageList = this.uiIcons;
            this.uiTVLibraries.Location = new System.Drawing.Point(21, 21);
            this.uiTVLibraries.Name = "uiTVLibraries";
            this.uiTVLibraries.SelectedImageIndex = 0;
            this.uiTVLibraries.Size = new System.Drawing.Size(300, 214);
            this.uiTVLibraries.TabIndex = 8;
            this.uiTVLibraries.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.uiTVLibraries_AfterSelect);
            // 
            // uiIcons
            // 
            this.uiIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("uiIcons.ImageStream")));
            this.uiIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.uiIcons.Images.SetKeyName(0, "LibrariesHS.png");
            this.uiIcons.Images.SetKeyName(1, "Library.png");
            this.uiIcons.Images.SetKeyName(2, "Template.png");
            // 
            // uiAddFromGAC
            // 
            this.uiAddFromGAC.Location = new System.Drawing.Point(128, 241);
            this.uiAddFromGAC.Name = "uiAddFromGAC";
            this.uiAddFromGAC.Size = new System.Drawing.Size(88, 23);
            this.uiAddFromGAC.TabIndex = 9;
            this.uiAddFromGAC.Text = "AddFrom&GAC";
            this.uiAddFromGAC.UseVisualStyleBackColor = true;
            this.uiAddFromGAC.Visible = false;
            this.uiAddFromGAC.Click += new System.EventHandler(this.uiAddFromGAC_Click);
            // 
            // uiRemove
            // 
            this.uiRemove.Enabled = false;
            this.uiRemove.Location = new System.Drawing.Point(222, 241);
            this.uiRemove.Name = "uiRemove";
            this.uiRemove.Size = new System.Drawing.Size(88, 23);
            this.uiRemove.TabIndex = 10;
            this.uiRemove.Text = "&Remove";
            this.uiRemove.UseVisualStyleBackColor = true;
            this.uiRemove.Click += new System.EventHandler(this.uiRemove_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Add Library";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.UiAddFromFolder_Click);
            // 
            // LibrariesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiClose;
            this.ClientSize = new System.Drawing.Size(345, 323);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uiRemove);
            this.Controls.Add(this.uiAddFromGAC);
            this.Controls.Add(this.uiTVLibraries);
            this.Controls.Add(this.uiClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LibrariesDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Libraries";
            this.Load += new System.EventHandler(this.LibrariesDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uiClose;
        private System.Windows.Forms.TreeView uiTVLibraries;
        private System.Windows.Forms.Button uiAddFromGAC;
        private System.Windows.Forms.Button uiRemove;
        private System.Windows.Forms.ImageList uiIcons;
        private System.Windows.Forms.Button button1;
    }
}