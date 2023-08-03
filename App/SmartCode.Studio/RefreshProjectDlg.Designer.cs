/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

using SmartCode.Studio.Controls.UserControls.XPListView ;
namespace SmartCode.Studio
{
    partial class RefreshProjectDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefreshProjectDlg));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiLVResults = new SmartCode.Studio.Controls.UserControls.XPListView.XPListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiOK = new System.Windows.Forms.Button();
            this.uiDBImages = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.059041F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95.94096F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.uiLVResults, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.176471F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 341);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // uiLVResults
            // 
            this.uiLVResults.CheckBoxes = true;
            this.uiLVResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.uiLVResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLVResults.FullRowSelect = true;
            this.uiLVResults.GridLines = true;
            this.uiLVResults.Location = new System.Drawing.Point(20, 23);
            this.uiLVResults.Name = "uiLVResults";
            this.uiLVResults.Size = new System.Drawing.Size(397, 255);
            this.uiLVResults.TabIndex = 0;
            this.uiLVResults.UseCompatibleStateImageBehavior = false;
            this.uiLVResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 72;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Table Name";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Object name";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Action";
            this.columnHeader4.Width = 100;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiCancel);
            this.panel1.Controls.Add(this.uiOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(217, 303);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 23);
            this.panel1.TabIndex = 1;
            // 
            // uiCancel
            // 
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(123, 0);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 7;
            this.uiCancel.Text = "&Cancel";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // uiOK
            // 
            this.uiOK.Location = new System.Drawing.Point(41, 0);
            this.uiOK.Name = "uiOK";
            this.uiOK.Size = new System.Drawing.Size(75, 23);
            this.uiOK.TabIndex = 6;
            this.uiOK.Text = "&OK";
            this.uiOK.UseVisualStyleBackColor = true;
            this.uiOK.Click += new System.EventHandler(this.uiOK_Click);
            // 
            // uiDBImages
            // 
            this.uiDBImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("uiDBImages.ImageStream")));
            this.uiDBImages.TransparentColor = System.Drawing.Color.Transparent;
            this.uiDBImages.Images.SetKeyName(0, "folder_closed_16_h.gif");
            this.uiDBImages.Images.SetKeyName(1, "Table.gif");
            this.uiDBImages.Images.SetKeyName(2, "Columns.gif");
            this.uiDBImages.Images.SetKeyName(3, "WorkSpace.gif");
            this.uiDBImages.Images.SetKeyName(4, "RelationshipsHS.png");
            this.uiDBImages.Images.SetKeyName(5, "Join.png");
            this.uiDBImages.Images.SetKeyName(6, "DatasetView_Column.png");
            // 
            // RefreshProjectDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(442, 341);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RefreshProjectDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Refresh Project From Database";
            this.Load += new System.EventHandler(this.RefreshProjectDlg_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ImageList uiDBImages;
        private XPListView uiLVResults;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.Button uiOK;


    }
}