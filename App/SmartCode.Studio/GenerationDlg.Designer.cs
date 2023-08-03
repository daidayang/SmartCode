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
    partial class GenerationDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerationDlg));
            this.uiClose = new System.Windows.Forms.Button();
            this.uiGenerate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel6 = new System.Windows.Forms.Panel();
            this.uiLVNamedObjects = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel5 = new System.Windows.Forms.Panel();
            this.uiChkViews = new System.Windows.Forms.CheckBox();
            this.uiChkTables = new System.Windows.Forms.CheckBox();
            this.uiLVProjectTemplates = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel4 = new System.Windows.Forms.Panel();
            this.uiBtnRemoveAll = new System.Windows.Forms.Button();
            this.uiBtnRemove = new System.Windows.Forms.Button();
            this.uiBtnAdd = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uiTVEntityTemplates = new SmartCode.Studio.Controls.UserControls.MultiTreeView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiClose
            // 
            this.uiClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiClose.Location = new System.Drawing.Point(150, 11);
            this.uiClose.Name = "uiClose";
            this.uiClose.Size = new System.Drawing.Size(75, 23);
            this.uiClose.TabIndex = 7;
            this.uiClose.Text = "&Close";
            this.uiClose.UseVisualStyleBackColor = true;
            // 
            // uiGenerate
            // 
            this.uiGenerate.Location = new System.Drawing.Point(68, 11);
            this.uiGenerate.Name = "uiGenerate";
            this.uiGenerate.Size = new System.Drawing.Size(75, 23);
            this.uiGenerate.TabIndex = 6;
            this.uiGenerate.Text = "&Generate";
            this.uiGenerate.UseVisualStyleBackColor = true;
            this.uiGenerate.Click += new System.EventHandler(this.uiGenerate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 60);
            this.panel1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Assign/Deassign Tables and Views to Templates";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Smart Code Generation Setting";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 501);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(658, 43);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.uiClose);
            this.panel3.Controls.Add(this.uiGenerate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(425, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 43);
            this.panel3.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "BringToFrontHS.png");
            this.imageList1.Images.SetKeyName(1, "BringForwardHS.png");
            this.imageList1.Images.SetKeyName(2, "open116x16 24-bit.png");
            this.imageList1.Images.SetKeyName(3, "TableHS.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel6);
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(658, 441);
            this.splitContainer1.SplitterDistance = 241;
            this.splitContainer1.TabIndex = 11;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.uiLVNamedObjects);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(241, 399);
            this.panel6.TabIndex = 2;
            // 
            // uiLVNamedObjects
            // 
            this.uiLVNamedObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.uiLVNamedObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLVNamedObjects.HideSelection = false;
            this.uiLVNamedObjects.Location = new System.Drawing.Point(0, 0);
            this.uiLVNamedObjects.Name = "uiLVNamedObjects";
            this.uiLVNamedObjects.Size = new System.Drawing.Size(241, 399);
            this.uiLVNamedObjects.SmallImageList = this.imageList1;
            this.uiLVNamedObjects.TabIndex = 0;
            this.uiLVNamedObjects.UseCompatibleStateImageBehavior = false;
            this.uiLVNamedObjects.View = System.Windows.Forms.View.Details;
            this.uiLVNamedObjects.SelectedIndexChanged += new System.EventHandler(this.uiLVNamedObjects_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Objects";
            this.columnHeader1.Width = 244;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.uiChkViews);
            this.panel5.Controls.Add(this.uiChkTables);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 399);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(241, 42);
            this.panel5.TabIndex = 1;
            // 
            // uiChkViews
            // 
            this.uiChkViews.AutoSize = true;
            this.uiChkViews.Location = new System.Drawing.Point(111, 7);
            this.uiChkViews.Name = "uiChkViews";
            this.uiChkViews.Size = new System.Drawing.Size(54, 17);
            this.uiChkViews.TabIndex = 1;
            this.uiChkViews.Text = "&Views";
            this.uiChkViews.UseVisualStyleBackColor = true;
            this.uiChkViews.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // uiChkTables
            // 
            this.uiChkTables.AutoSize = true;
            this.uiChkTables.Checked = true;
            this.uiChkTables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uiChkTables.Location = new System.Drawing.Point(13, 7);
            this.uiChkTables.Name = "uiChkTables";
            this.uiChkTables.Size = new System.Drawing.Size(58, 17);
            this.uiChkTables.TabIndex = 0;
            this.uiChkTables.Text = "&Tables";
            this.uiChkTables.UseVisualStyleBackColor = true;
            this.uiChkTables.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // uiLVProjectTemplates
            // 
            this.uiLVProjectTemplates.CheckBoxes = true;
            this.uiLVProjectTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.uiLVProjectTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLVProjectTemplates.FullRowSelect = true;
            this.uiLVProjectTemplates.HideSelection = false;
            this.uiLVProjectTemplates.Location = new System.Drawing.Point(3, 16);
            this.uiLVProjectTemplates.Name = "uiLVProjectTemplates";
            this.uiLVProjectTemplates.Size = new System.Drawing.Size(319, 156);
            this.uiLVProjectTemplates.SmallImageList = this.imageList1;
            this.uiLVProjectTemplates.TabIndex = 1;
            this.uiLVProjectTemplates.UseCompatibleStateImageBehavior = false;
            this.uiLVProjectTemplates.View = System.Windows.Forms.View.Details;
            this.uiLVProjectTemplates.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.uiLVProjectTemplates_ItemCheck);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Templates (IsPojectTemplate=True)";
            this.columnHeader2.Width = 290;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.uiBtnRemoveAll);
            this.panel4.Controls.Add(this.uiBtnRemove);
            this.panel4.Controls.Add(this.uiBtnAdd);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(88, 441);
            this.panel4.TabIndex = 0;
            // 
            // uiBtnRemoveAll
            // 
            this.uiBtnRemoveAll.Location = new System.Drawing.Point(7, 209);
            this.uiBtnRemoveAll.Name = "uiBtnRemoveAll";
            this.uiBtnRemoveAll.Size = new System.Drawing.Size(74, 23);
            this.uiBtnRemoveAll.TabIndex = 2;
            this.uiBtnRemoveAll.Text = "Deassign &All";
            this.uiBtnRemoveAll.UseVisualStyleBackColor = true;
            this.uiBtnRemoveAll.Click += new System.EventHandler(this.uiBtnRemoveAll_Click);
            // 
            // uiBtnRemove
            // 
            this.uiBtnRemove.Location = new System.Drawing.Point(8, 135);
            this.uiBtnRemove.Name = "uiBtnRemove";
            this.uiBtnRemove.Size = new System.Drawing.Size(74, 23);
            this.uiBtnRemove.TabIndex = 1;
            this.uiBtnRemove.Text = "&Deassign";
            this.uiBtnRemove.UseVisualStyleBackColor = true;
            this.uiBtnRemove.Click += new System.EventHandler(this.uiBtnRemove_Click);
            // 
            // uiBtnAdd
            // 
            this.uiBtnAdd.Location = new System.Drawing.Point(8, 106);
            this.uiBtnAdd.Name = "uiBtnAdd";
            this.uiBtnAdd.Size = new System.Drawing.Size(74, 23);
            this.uiBtnAdd.TabIndex = 0;
            this.uiBtnAdd.Text = "&Assign -->";
            this.uiBtnAdd.UseVisualStyleBackColor = true;
            this.uiBtnAdd.Click += new System.EventHandler(this.uiBtnAdd_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(88, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(325, 441);
            this.splitContainer2.SplitterDistance = 262;
            this.splitContainer2.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiTVEntityTemplates);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 262);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity Templates";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.uiLVProjectTemplates);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 175);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Project Templates";
            // 
            // uiTVEntityTemplates
            // 
            this.uiTVEntityTemplates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiTVEntityTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTVEntityTemplates.HideSelection = false;
            this.uiTVEntityTemplates.ImageIndex = 0;
            this.uiTVEntityTemplates.ImageList = this.imageList1;
            this.uiTVEntityTemplates.Location = new System.Drawing.Point(3, 16);
            this.uiTVEntityTemplates.Name = "uiTVEntityTemplates";
            this.uiTVEntityTemplates.SelectedImageIndex = 0;
            this.uiTVEntityTemplates.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("uiTVEntityTemplates.SelectedNodes")));
            this.uiTVEntityTemplates.Size = new System.Drawing.Size(319, 243);
            this.uiTVEntityTemplates.Sorted = true;
            this.uiTVEntityTemplates.TabIndex = 8;
            this.uiTVEntityTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.uiTVEntityTemplates_AfterSelect);
            // 
            // GenerationDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 544);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "GenerationDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting Code Generation";
            this.Load += new System.EventHandler(this.GenerationDlg_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uiClose;
        private System.Windows.Forms.Button uiGenerate;
        private SmartCode.Studio.Controls.UserControls.MultiTreeView uiTVEntityTemplates;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView uiLVNamedObjects;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView uiLVProjectTemplates;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button uiBtnRemove;
        private System.Windows.Forms.Button uiBtnAdd;
        private System.Windows.Forms.Button uiBtnRemoveAll;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox uiChkViews;
        private System.Windows.Forms.CheckBox uiChkTables;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}