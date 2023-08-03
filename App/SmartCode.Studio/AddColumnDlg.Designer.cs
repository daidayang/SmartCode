namespace SmartCode.Studio
{
    partial class AddColumnDlg
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiNull = new System.Windows.Forms.CheckBox();
            this.uiUnique = new System.Windows.Forms.CheckBox();
            this.uiPK = new System.Windows.Forms.CheckBox();
            this.uiName = new System.Windows.Forms.TextBox();
            this.uiCode = new System.Windows.Forms.TextBox();
            this.uiType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(248, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(167, 180);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Code:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiNull);
            this.groupBox1.Controls.Add(this.uiUnique);
            this.groupBox1.Controls.Add(this.uiPK);
            this.groupBox1.Location = new System.Drawing.Point(16, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 61);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Constraints";
            // 
            // uiNull
            // 
            this.uiNull.AutoSize = true;
            this.uiNull.Location = new System.Drawing.Point(216, 20);
            this.uiNull.Name = "uiNull";
            this.uiNull.Size = new System.Drawing.Size(44, 17);
            this.uiNull.TabIndex = 2;
            this.uiNull.Text = "&Null";
            this.uiNull.UseVisualStyleBackColor = true;
            // 
            // uiUnique
            // 
            this.uiUnique.AutoSize = true;
            this.uiUnique.Location = new System.Drawing.Point(124, 20);
            this.uiUnique.Name = "uiUnique";
            this.uiUnique.Size = new System.Drawing.Size(60, 17);
            this.uiUnique.TabIndex = 1;
            this.uiUnique.Text = "&Unique";
            this.uiUnique.UseVisualStyleBackColor = true;
            // 
            // uiPK
            // 
            this.uiPK.AutoSize = true;
            this.uiPK.Location = new System.Drawing.Point(20, 19);
            this.uiPK.Name = "uiPK";
            this.uiPK.Size = new System.Drawing.Size(80, 17);
            this.uiPK.TabIndex = 0;
            this.uiPK.Text = "&Primary key";
            this.uiPK.UseVisualStyleBackColor = true;
            // 
            // uiName
            // 
            this.uiName.Location = new System.Drawing.Point(58, 12);
            this.uiName.Name = "uiName";
            this.uiName.Size = new System.Drawing.Size(265, 20);
            this.uiName.TabIndex = 6;
            this.uiName.TextChanged += new System.EventHandler(this.uiName_TextChanged);
            // 
            // uiCode
            // 
            this.uiCode.Location = new System.Drawing.Point(58, 35);
            this.uiCode.Name = "uiCode";
            this.uiCode.Size = new System.Drawing.Size(265, 20);
            this.uiCode.TabIndex = 7;
            // 
            // uiType
            // 
            this.uiType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiType.FormattingEnabled = true;
            this.uiType.Location = new System.Drawing.Point(58, 60);
            this.uiType.Name = "uiType";
            this.uiType.Size = new System.Drawing.Size(265, 21);
            this.uiType.TabIndex = 8;
            // 
            // AddColumnDlg
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(339, 226);
            this.Controls.Add(this.uiType);
            this.Controls.Add(this.uiCode);
            this.Controls.Add(this.uiName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Column";
            this.Load += new System.EventHandler(this.AddColumnDlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox uiNull;
        private System.Windows.Forms.CheckBox uiUnique;
        private System.Windows.Forms.CheckBox uiPK;
        private System.Windows.Forms.TextBox uiName;
        private System.Windows.Forms.TextBox uiCode;
        private System.Windows.Forms.ComboBox uiType;
    }
}