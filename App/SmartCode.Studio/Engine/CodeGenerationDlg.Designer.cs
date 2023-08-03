/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace SmartCode.Studio.Engine
{
    partial class CodeGenerationDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeGenerationDlg));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiLiResume = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.uiClose = new System.Windows.Forms.Button();
            this.uiProgressBar = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.831418F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96.16858F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.uiLiResume, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.uiClose, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.uiProgressBar, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(543, 349);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // uiLiResume
            // 
            this.uiLiResume.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.uiLiResume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLiResume.FullRowSelect = true;
            this.uiLiResume.GridLines = true;
            this.uiLiResume.Location = new System.Drawing.Point(23, 64);
            this.uiLiResume.Name = "uiLiResume";
            this.uiLiResume.Size = new System.Drawing.Size(496, 234);
            this.uiLiResume.SmallImageList = this.imageList;
            this.uiLiResume.TabIndex = 0;
            this.uiLiResume.UseCompatibleStateImageBehavior = false;
            this.uiLiResume.View = System.Windows.Forms.View.Details;
            this.uiLiResume.DoubleClick += new System.EventHandler(this.uiLiResume_DoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 32;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 128;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.Width = 327;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "ok.png");
            this.imageList.Images.SetKeyName(1, "error2.png");
            // 
            // uiClose
            // 
            this.uiClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiClose.Location = new System.Drawing.Point(444, 322);
            this.uiClose.Name = "uiClose";
            this.uiClose.Size = new System.Drawing.Size(75, 24);
            this.uiClose.TabIndex = 1;
            this.uiClose.Text = "&Close";
            this.uiClose.UseVisualStyleBackColor = true;
            // 
            // uiProgressBar
            // 
            this.uiProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiProgressBar.Location = new System.Drawing.Point(23, 23);
            this.uiProgressBar.Name = "uiProgressBar";
            this.uiProgressBar.Size = new System.Drawing.Size(496, 19);
            this.uiProgressBar.TabIndex = 2;
            // 
            // CodeGenerationDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiClose;
            this.ClientSize = new System.Drawing.Size(543, 349);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CodeGenerationDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generation Resumen";
            this.Load += new System.EventHandler(this.CodeGenerationDlg_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView uiLiResume;
        private System.Windows.Forms.Button uiClose;
        private System.Windows.Forms.ProgressBar uiProgressBar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList;

    }
}