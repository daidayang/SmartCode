/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace SmartCode.Studio.AssemblyCache
{
    partial class GACDlg
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
            this.uiLvGAC = new System.Windows.Forms.ListView();
            this.uiHeaderName = new System.Windows.Forms.ColumnHeader();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiOK = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // uiLvGAC
            // 
            this.uiLvGAC.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.uiHeaderName});
            this.uiLvGAC.FullRowSelect = true;
            this.uiLvGAC.Location = new System.Drawing.Point(10, 41);
            this.uiLvGAC.MultiSelect = false;
            this.uiLvGAC.Name = "uiLvGAC";
            this.uiLvGAC.Size = new System.Drawing.Size(344, 254);
            this.uiLvGAC.TabIndex = 0;
            this.uiLvGAC.UseCompatibleStateImageBehavior = false;
            this.uiLvGAC.View = System.Windows.Forms.View.Details;
            this.uiLvGAC.SelectedIndexChanged += new System.EventHandler(this._lvGAC_SelectedIndexChanged);
            // 
            // uiHeaderName
            // 
            this.uiHeaderName.Text = "Name";
            this.uiHeaderName.Width = 334;
            // 
            // uiCancel
            // 
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(280, 315);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 5;
            this.uiCancel.Text = "&Cancel";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // uiOK
            // 
            this.uiOK.Location = new System.Drawing.Point(198, 315);
            this.uiOK.Name = "uiOK";
            this.uiOK.Size = new System.Drawing.Size(75, 23);
            this.uiOK.TabIndex = 4;
            this.uiOK.Text = "&OK";
            this.uiOK.UseVisualStyleBackColor = true;
            this.uiOK.Click += new System.EventHandler(this.uiOK_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 11);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(275, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "You cannot find the Library?, add your library to the GAC!";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // GACDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(366, 350);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.uiCancel);
            this.Controls.Add(this.uiOK);
            this.Controls.Add(this.uiLvGAC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GACDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GAC Dialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView uiLvGAC;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.Button uiOK;
        private System.Windows.Forms.ColumnHeader uiHeaderName;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}