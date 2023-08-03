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
using System.Runtime.InteropServices;
using SmartCode.Studio.Utils;
using SmartCode.Model;
using SmartCode.Studio.Controls.UserControls.XPListView;

namespace SmartCode.Studio
{
    public partial class RefreshProjectDlg : Form
    {
        enum ObjectType
        {
            Table,
            View,
            Column,
        }

        //[DllImport("user32.dll")]
        //private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        //private const int WM_SCROLL = 276; // Horizontal scroll
        //private const int WM_VSCROLL = 277; // Vertical scroll
        //private const int WM_MOUSEWHEEL = 522;
        //private const int WM_COMMAND = 273;
        //private const int WM_USER = 1024;
        private Domain projectDomain;
        private Domain dbDomain;


        //Mirror Treeview?
 
        public RefreshProjectDlg()
        {
            InitializeComponent();
        }

        public RefreshProjectDlg(Domain projectDomain, Domain dbDomain)
            :this()
        {
            this.projectDomain = projectDomain;
            this.dbDomain = dbDomain;
            this.uiLVResults.ShowInGroups = true;
        }

        private void RefreshProjectDlg_Load(object sender, EventArgs e)
        {
            Analize();
        }

        private void Analize()
        {
            foreach (TableSchema dbTable in dbDomain.DatabaseSchema.Tables)
            {
                TableSchema projectTable = projectDomain.DatabaseSchema.FindTable(dbTable.Name);
                if (projectTable == null)
                {
                    XPListViewItem li = new XPListViewItem(new string[] { "Table", dbTable.Name, "-", "Add" });
                    li.Tag = dbTable;
                    this.uiLVResults.Items.Add(li);
                }
                else
                {
                    foreach (ColumnSchema dbColumn in dbTable.Columns())
                    {
                        ColumnSchema projColumn = projectTable.FindColumn(dbColumn.Name);
                        //Exist??
                        if (projColumn == null)
                        {
                            XPListViewItem li = new XPListViewItem(new string[] { "Column", dbTable.Name, dbColumn.Name, "Add" });
                            li.Tag = dbColumn;
                            this.uiLVResults.Items.Add(li);
                        }
                        else if (projColumn.DefaultValue != dbColumn.DefaultValue ||
                            projColumn.IsIdentity != dbColumn.IsIdentity ||
                            projColumn.IsPrimaryKey != dbColumn.IsPrimaryKey ||
                            projColumn.IsRequired != dbColumn.IsRequired ||
                            projColumn.NetDataType != dbColumn.NetDataType ||
                            projColumn.SqlType != dbColumn.SqlType ||
                            projColumn.Scale != dbColumn.Scale )
                        {
                            XPListViewItem li = new XPListViewItem(new string[] { "Column", dbTable.Name, dbColumn.Name, "Update" });
                            li.Tag = dbColumn;
                            this.uiLVResults.Items.Add(li);
                        }
                    }
                }
            }
            this.uiLVResults.AutoGroupByColumn(1);

        }

        private void uiOK_Click(object sender, EventArgs e)
        {
            UpdateProject();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void UpdateProject()
        {
            foreach (XPListViewItem li in this.uiLVResults.CheckedItems)
            {
                TableSchema table = li.Tag as TableSchema;
                if (table != null && li.SubItems[3].Text == "Add")
                {
                    //Add table to Domain
                    TableSchema newtable = table.Clone() as TableSchema;
                    newtable.Domain = this.projectDomain;
                    this.projectDomain.DatabaseSchema.Tables.Add(newtable);
                    continue;
                }

                ColumnSchema dbColumn = li.Tag as ColumnSchema;
                if (dbColumn != null && li.SubItems[3].Text == "Add")
                {
                    TableSchema projectTable = projectDomain.DatabaseSchema.FindTable(li.SubItems[1].Text);
                    if (projectTable != null)
                    {
                        ColumnSchema newColumn = (ColumnSchema)dbColumn.Clone();
                        newColumn.Table = projectTable;
                        projectTable.ColumnSchemaCollection.Add(newColumn.Name, newColumn);
                    }
                }
                else if (dbColumn != null && li.SubItems[3].Text == "Update")
                {
                    TableSchema projectTable = projectDomain.DatabaseSchema.FindTable(li.SubItems[1].Text);
                    if (projectTable != null)
                    {
                        ColumnSchema projectColumn = projectTable.FindColumn(dbColumn.Name);
                        if (projectColumn != null)
                        {
                            projectColumn.DefaultValue = dbColumn.DefaultValue;
                            projectColumn.IsIdentity = dbColumn.IsIdentity ;
                            projectColumn.IsPrimaryKey = dbColumn.IsPrimaryKey;
                            projectColumn.IsRequired = dbColumn.IsRequired ;
                            projectColumn.NetDataType = dbColumn.NetDataType;
                            projectColumn.SqlType = dbColumn.SqlType ;
                            projectColumn.Scale = dbColumn.Scale;
                        }

                    }
                }
            }
        }
  
    }
}