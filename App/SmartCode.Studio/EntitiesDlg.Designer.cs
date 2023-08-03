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
    partial class EntitiesDlg
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
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uiUnselectAllTables = new System.Windows.Forms.Button();
            this.uiSelectAllTables = new System.Windows.Forms.Button();
            this.uiTables = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uiUnselectAllViews = new System.Windows.Forms.Button();
            this.uiSelectAllViews = new System.Windows.Forms.Button();
            this.uiViews = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiCancel
            // 
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(383, 306);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 5;
            this.uiCancel.Text = "&Cancel";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // uiOK
            // 
            this.uiOK.Location = new System.Drawing.Point(301, 306);
            this.uiOK.Name = "uiOK";
            this.uiOK.Size = new System.Drawing.Size(75, 23);
            this.uiOK.TabIndex = 4;
            this.uiOK.Text = "&OK";
            this.uiOK.UseVisualStyleBackColor = true;
            this.uiOK.Click += new System.EventHandler(this.uiOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(446, 275);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uiUnselectAllTables);
            this.tabPage1.Controls.Add(this.uiSelectAllTables);
            this.tabPage1.Controls.Add(this.uiTables);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(438, 249);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tables";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // uiUnselectAllTables
            // 
            this.uiUnselectAllTables.Location = new System.Drawing.Point(325, 43);
            this.uiUnselectAllTables.Name = "uiUnselectAllTables";
            this.uiUnselectAllTables.Size = new System.Drawing.Size(92, 23);
            this.uiUnselectAllTables.TabIndex = 6;
            this.uiUnselectAllTables.Text = "Select &None";
            this.uiUnselectAllTables.UseVisualStyleBackColor = true;
            this.uiUnselectAllTables.Click += new System.EventHandler(this.uiUnselectAllTables_Click);
            // 
            // uiSelectAllTables
            // 
            this.uiSelectAllTables.Location = new System.Drawing.Point(325, 14);
            this.uiSelectAllTables.Name = "uiSelectAllTables";
            this.uiSelectAllTables.Size = new System.Drawing.Size(92, 23);
            this.uiSelectAllTables.TabIndex = 5;
            this.uiSelectAllTables.Text = "Select &All";
            this.uiSelectAllTables.UseVisualStyleBackColor = true;
            this.uiSelectAllTables.Click += new System.EventHandler(this.uiSelectAllTables_Click);
            // 
            // uiTables
            // 
            this.uiTables.CheckBoxes = true;
            this.uiTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.uiTables.Location = new System.Drawing.Point(16, 14);
            this.uiTables.Name = "uiTables";
            this.uiTables.Size = new System.Drawing.Size(286, 204);
            this.uiTables.TabIndex = 0;
            this.uiTables.UseCompatibleStateImageBehavior = false;
            this.uiTables.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 276;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.uiUnselectAllViews);
            this.tabPage2.Controls.Add(this.uiSelectAllViews);
            this.tabPage2.Controls.Add(this.uiViews);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(438, 249);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Views";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // uiUnselectAllViews
            // 
            this.uiUnselectAllViews.Location = new System.Drawing.Point(320, 45);
            this.uiUnselectAllViews.Name = "uiUnselectAllViews";
            this.uiUnselectAllViews.Size = new System.Drawing.Size(92, 23);
            this.uiUnselectAllViews.TabIndex = 8;
            this.uiUnselectAllViews.Text = "Select &None";
            this.uiUnselectAllViews.UseVisualStyleBackColor = true;
            this.uiUnselectAllViews.Click += new System.EventHandler(this.uiUnselectAllViews_Click);
            // 
            // uiSelectAllViews
            // 
            this.uiSelectAllViews.Location = new System.Drawing.Point(320, 16);
            this.uiSelectAllViews.Name = "uiSelectAllViews";
            this.uiSelectAllViews.Size = new System.Drawing.Size(92, 23);
            this.uiSelectAllViews.TabIndex = 7;
            this.uiSelectAllViews.Text = "Select &All";
            this.uiSelectAllViews.UseVisualStyleBackColor = true;
            this.uiSelectAllViews.Click += new System.EventHandler(this.uiSelectAllViews_Click);
            // 
            // uiViews
            // 
            this.uiViews.CheckBoxes = true;
            this.uiViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.uiViews.Location = new System.Drawing.Point(16, 16);
            this.uiViews.Name = "uiViews";
            this.uiViews.Size = new System.Drawing.Size(286, 204);
            this.uiViews.TabIndex = 1;
            this.uiViews.UseCompatibleStateImageBehavior = false;
            this.uiViews.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 276;
            // 
            // EntitiesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(481, 341);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.uiCancel);
            this.Controls.Add(this.uiOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntitiesDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Entities";
            this.Load += new System.EventHandler(this.EntitiesDlg_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.Button uiOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView uiTables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button uiUnselectAllTables;
        private System.Windows.Forms.Button uiSelectAllTables;
        private System.Windows.Forms.Button uiUnselectAllViews;
        private System.Windows.Forms.Button uiSelectAllViews;
        private System.Windows.Forms.ListView uiViews;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}