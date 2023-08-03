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
using System.Text;
using SmartCode.Model;
using SmartCode.Studio.Controls.EditorWrapper;
using System.Drawing.Design;
using System.ComponentModel;

namespace SmartCode.Studio.Controls
{
    internal class TablePropertyWrapper : NamedPropertyWrapper
    {
        public TablePropertyWrapper(ExplorerTreeNode treeNode, TableSchema table)
            : base(treeNode, table)
        {
        }

        internal TableSchema CurrentTable
        {
            get
            {
                return NamedObject as TableSchema;
            }
        }

        [DisplayText("PropSchema", "PropSchemaDesc", "DatabaseCategory")]
        public String Schema
        {
            get { return CurrentTable.Schema; }
            set { CurrentTable.Schema = value; }
        }

        [DisplayText("PropCollectionName", "PropCollectionNameDesc", "ClientProfileCategory")]
        public string CollectionName
        {
            get
            {
                return CurrentTable.ClientProfile.CollectionName;
            }
            set
            {
                CurrentTable.ClientProfile.CollectionName = value;
            }
        }

        [DisplayText("PropIsPersistent", "PropIsPersistentDesc", "ClientProfileCategory")]
        public bool IsPersistent
        {
            get
            {
                return CurrentTable.ClientProfile.IsPersistent;
            }
            set
            {
                CurrentTable.ClientProfile.IsPersistent = value;
            }
        }

        [DisplayText("PropAllowUpdate", "PropAllowUpdateDesc", "ClientProfileCategory")]
        public bool AllowUpdate
        {
            get
            {
                return CurrentTable.ClientProfile.AllowUpdate;
            }
            set
            {
                CurrentTable.ClientProfile.AllowUpdate = value;
            }
        }

        [DisplayText("PropAllowDelete", "PropAllowDeleteDesc", "ClientProfileCategory")]
        public bool AllowDelete
        {
            get
            {
                return CurrentTable.ClientProfile.AllowDelete;
            }
            set
            {
                CurrentTable.ClientProfile.AllowDelete = value;
            }
        }

        [DisplayText("PropAllowCopy", "PropAllowCopyDesc", "ClientProfileCategory")]
        public bool AllowCopy
        {
            get
            {
                return CurrentTable.ClientProfile.AllowCopy;
            }
            set
            {
                CurrentTable.ClientProfile.AllowCopy = value;
            }
        }

        [DisplayText("PropAllowInsert", "PropAllowInsertDesc", "ClientProfileCategory")]
        public bool AllowInsert
        {
            get
            {
                return CurrentTable.ClientProfile.AllowInsert;
            }
            set
            {
                CurrentTable.ClientProfile.AllowInsert = value;
            }
        }

        [DisplayText("PropOrderBy", "PropOrderByDesc", "ClientProfileCategory")]
        [Editor(typeof(MultiColumnNameEditor), typeof(UITypeEditor))]
        public string OrderBy
        {
            get
            {
                string results = "";
                foreach (String column in CurrentTable.ClientProfile.OrderBy)
                {
                    results += column + ",";
                }
                return string.IsNullOrEmpty(results) ? "" : results.Substring(0, results.Length - 1);
            }
            set
            {
                CurrentTable.ClientProfile.OrderBy.Clear();
                string[] columns = value.Split(',');
                foreach (string column in columns)
                {
                    CurrentTable.ClientProfile.OrderBy.Add(column);
                }
            }
        }
    }
}
