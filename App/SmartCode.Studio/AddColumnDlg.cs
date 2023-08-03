/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SmartCode.Studio.Database;
using SmartCode.Model;

namespace SmartCode.Studio
{
    public partial class AddColumnDlg : Form
    {
        public AddColumnDlg()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddColumnDlg_Load(object sender, EventArgs e)
        {
            IDictionary<String, SqlType> sqlTypes = TypesFactory.GetSQLTypes(SmartCode.Studio.SmartStudio.MainForm.CurrentProject.Domain.DatabaseSchema.ConnectionInfo.Provider);
            IEnumerator typesEnum = sqlTypes.GetEnumerator();

            foreach (KeyValuePair<String, SqlType> pair in sqlTypes)
            {
                this.uiType.Items.Add(new ListItemType(pair.Key, pair.Value));
            }
        
        }

        internal  class ListItemType
        {
            string m_key;
            SqlType m_value;

            internal ListItemType(string key, SqlType value)
            {
                this.m_key = key;
                this.m_value = value;
            }

            public string Key
            {
                get { return this.m_key; }
            }

            public SqlType Value
            {
                get { return m_value; }
            }

            public override string ToString()
            {
                return m_key;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        internal string Name
        {
            get
            {
               return  this.uiName.Text;
            }
        }
        internal string Code
        {
            get
            {
                return this.uiCode.Text;
            }
        }
        internal bool IsNull
        {
            get
            {
                return this.uiNull.Checked;
            }
        }
        internal bool IsPK
        {
            get
            {
                return this.uiPK.Checked;
            }
        }
        internal bool IsUnique
        {
            get
            {
                return this.uiUnique.Checked;
            }
        }

        internal SqlType SQLType
        {
            get
            {
                ListItemType itemType = this.uiType.SelectedItem as ListItemType;
                if (itemType != null)
                {
                    return itemType.Value;
                }
                else
                {
                    return   SqlType.AnsiChar;
                }
            }
        }

        internal String OriginalType
        {
            get
            {
                ListItemType itemType = this.uiType.SelectedItem as ListItemType;
                if (itemType != null)
                {
                    return itemType.Key;
                }
                else
                {
                    return "";
                }
            }
        }

        private void uiName_TextChanged(object sender, EventArgs e)
        {
            this.uiCode.Text  = this.uiName.Text.Replace("_", "").TrimEnd();
        }
    }
}