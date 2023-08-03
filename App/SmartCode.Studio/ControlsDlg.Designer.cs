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
    partial class ControlsDlg
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiControlName = new System.Windows.Forms.TextBox();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiOK = new System.Windows.Forms.Button();
            this.uiControlType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Control type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Control name:";
            // 
            // uiControlName
            // 
            this.uiControlName.Location = new System.Drawing.Point(30, 25);
            this.uiControlName.Name = "uiControlName";
            this.uiControlName.Size = new System.Drawing.Size(308, 20);
            this.uiControlName.TabIndex = 11;
            // 
            // uiCancel
            // 
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(263, 169);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(75, 23);
            this.uiCancel.TabIndex = 10;
            this.uiCancel.Text = "&Cancel";
            this.uiCancel.UseVisualStyleBackColor = true;
            // 
            // uiOK
            // 
            this.uiOK.Location = new System.Drawing.Point(181, 169);
            this.uiOK.Name = "uiOK";
            this.uiOK.Size = new System.Drawing.Size(75, 23);
            this.uiOK.TabIndex = 9;
            this.uiOK.Text = "&OK";
            this.uiOK.UseVisualStyleBackColor = true;
            this.uiOK.Click += new System.EventHandler(this.uiOK_Click);
            // 
            // uiControlType
            // 
            this.uiControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiControlType.FormattingEnabled = true;
            this.uiControlType.Items.AddRange(new object[] {
            "TextBox",
            "CheckBox",
            "ComboBox",
            "Image",
            "ListBox",
            "OptionButton",
            "Popup"});
            this.uiControlType.Location = new System.Drawing.Point(30, 64);
            this.uiControlType.Name = "uiControlType";
            this.uiControlType.Size = new System.Drawing.Size(308, 21);
            this.uiControlType.TabIndex = 8;
            // 
            // ControlsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(370, 218);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiControlName);
            this.Controls.Add(this.uiCancel);
            this.Controls.Add(this.uiOK);
            this.Controls.Add(this.uiControlType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlsDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Controls";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiControlName;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.Button uiOK;
        private System.Windows.Forms.ComboBox uiControlType;
    }
}