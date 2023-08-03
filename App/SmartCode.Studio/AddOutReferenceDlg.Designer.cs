namespace SmartCode.Studio
{
    partial class AddOutReferenceDlg
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
            this.uiLBParentTable = new System.Windows.Forms.ListBox();
            this.uiLBChildTable = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.uiAddJoin = new System.Windows.Forms.Button();
            this.uiRemoveJoin = new System.Windows.Forms.Button();
            this.uiLVReferenceJoins = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.uiLblParentTable = new System.Windows.Forms.Label();
            this.uiCmbChildTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiTxtReferenceName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uiLBParentTable
            // 
            this.uiLBParentTable.FormattingEnabled = true;
            this.uiLBParentTable.Location = new System.Drawing.Point(23, 63);
            this.uiLBParentTable.Name = "uiLBParentTable";
            this.uiLBParentTable.Size = new System.Drawing.Size(192, 134);
            this.uiLBParentTable.TabIndex = 0;
            // 
            // uiLBChildTable
            // 
            this.uiLBChildTable.FormattingEnabled = true;
            this.uiLBChildTable.Location = new System.Drawing.Point(223, 63);
            this.uiLBChildTable.Name = "uiLBChildTable";
            this.uiLBChildTable.Size = new System.Drawing.Size(192, 134);
            this.uiLBChildTable.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(259, 343);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 343);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // uiAddJoin
            // 
            this.uiAddJoin.Location = new System.Drawing.Point(235, 203);
            this.uiAddJoin.Name = "uiAddJoin";
            this.uiAddJoin.Size = new System.Drawing.Size(89, 23);
            this.uiAddJoin.TabIndex = 4;
            this.uiAddJoin.Text = "Add &Join";
            this.uiAddJoin.UseVisualStyleBackColor = true;
            this.uiAddJoin.Click += new System.EventHandler(this.uiAddJoin_Click);
            // 
            // uiRemoveJoin
            // 
            this.uiRemoveJoin.Location = new System.Drawing.Point(330, 203);
            this.uiRemoveJoin.Name = "uiRemoveJoin";
            this.uiRemoveJoin.Size = new System.Drawing.Size(85, 23);
            this.uiRemoveJoin.TabIndex = 5;
            this.uiRemoveJoin.Text = "&Remove Join";
            this.uiRemoveJoin.UseVisualStyleBackColor = true;
            this.uiRemoveJoin.Click += new System.EventHandler(this.uiRemoveJoin_Click);
            // 
            // uiLVReferenceJoins
            // 
            this.uiLVReferenceJoins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.uiLVReferenceJoins.FullRowSelect = true;
            this.uiLVReferenceJoins.HideSelection = false;
            this.uiLVReferenceJoins.Location = new System.Drawing.Point(23, 232);
            this.uiLVReferenceJoins.MultiSelect = false;
            this.uiLVReferenceJoins.Name = "uiLVReferenceJoins";
            this.uiLVReferenceJoins.Size = new System.Drawing.Size(392, 94);
            this.uiLVReferenceJoins.TabIndex = 6;
            this.uiLVReferenceJoins.UseCompatibleStateImageBehavior = false;
            this.uiLVReferenceJoins.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Parent Column";
            this.columnHeader1.Width = 182;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Child Column";
            this.columnHeader2.Width = 181;
            // 
            // uiLblParentTable
            // 
            this.uiLblParentTable.AutoSize = true;
            this.uiLblParentTable.Location = new System.Drawing.Point(25, 42);
            this.uiLblParentTable.Name = "uiLblParentTable";
            this.uiLblParentTable.Size = new System.Drawing.Size(0, 13);
            this.uiLblParentTable.TabIndex = 7;
            // 
            // uiCmbChildTable
            // 
            this.uiCmbChildTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiCmbChildTable.FormattingEnabled = true;
            this.uiCmbChildTable.Location = new System.Drawing.Point(223, 42);
            this.uiCmbChildTable.Name = "uiCmbChildTable";
            this.uiCmbChildTable.Size = new System.Drawing.Size(192, 21);
            this.uiCmbChildTable.TabIndex = 8;
            this.uiCmbChildTable.SelectedIndexChanged += new System.EventHandler(this.uiCmbChildTable_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name:";
            // 
            // uiTxtReferenceName
            // 
            this.uiTxtReferenceName.Location = new System.Drawing.Point(68, 5);
            this.uiTxtReferenceName.Name = "uiTxtReferenceName";
            this.uiTxtReferenceName.Size = new System.Drawing.Size(224, 20);
            this.uiTxtReferenceName.TabIndex = 10;
            // 
            // AddOutReferenceDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 391);
            this.Controls.Add(this.uiTxtReferenceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiCmbChildTable);
            this.Controls.Add(this.uiLblParentTable);
            this.Controls.Add(this.uiLVReferenceJoins);
            this.Controls.Add(this.uiRemoveJoin);
            this.Controls.Add(this.uiAddJoin);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.uiLBChildTable);
            this.Controls.Add(this.uiLBParentTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOutReferenceDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Out Reference";
            this.Load += new System.EventHandler(this.AddOutReference_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox uiLBParentTable;
        private System.Windows.Forms.ListBox uiLBChildTable;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button uiAddJoin;
        private System.Windows.Forms.Button uiRemoveJoin;
        private System.Windows.Forms.ListView uiLVReferenceJoins;
        private System.Windows.Forms.Label uiLblParentTable;
        private System.Windows.Forms.ComboBox uiCmbChildTable;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiTxtReferenceName;
    }
}