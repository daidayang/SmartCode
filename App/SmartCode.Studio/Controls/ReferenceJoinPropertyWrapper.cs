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
    internal class ReferenceJoinPropertyWrapper : NamedPropertyWrapper
    {
        public ReferenceJoinPropertyWrapper(ExplorerTreeNode treeNode, ReferenceJoin referenceJoin)
            : base(treeNode, referenceJoin)
        {
        }

        internal ReferenceJoin CurrentReferenceJoin
        {
            get
            {
                return NamedObject as ReferenceJoin;
            }
        }

        [DisplayText("PropParentColumn", "PropParentColumnDesc", "JoinCategory")]
        public string ParentColumn
        {
            get
            {
                return CurrentReferenceJoin.ParentColumn.Name;
            }
        }

        [DisplayText("PropChildColumn", "PropChildColumnDesc", "JoinCategory")]
        public string ChildColumn
        {
            get
            {
                return CurrentReferenceJoin.ChildColumn.Name;
            }
        }
        [DisplayText("PropLOV", "PropLOVDesc", "JoinCategory")]
        [Editor(typeof(LOVEditor), typeof(UITypeEditor))]
        public string LOV
        {
            get
            {
                string results = "";
                foreach (ColumnSchema column in CurrentReferenceJoin.LOV)
                {
                    results += column.Name + ",";
                }
                return  string.IsNullOrEmpty(results) ? "": results.Substring(0, results.Length-1);
            }
            set
            {
                CurrentReferenceJoin.LOV.Clear();
                if (!string.IsNullOrEmpty(value))
                {
                    TableSchema parentTable = CurrentReferenceJoin.ParentReference.ParentTable;
                    string[] columns = value.Split(',');
                    foreach (string colName in columns)
                    {
                        ColumnSchema column = parentTable.FindColumn(colName);
                        if (column != null && CurrentReferenceJoin.LOV.IndexOf(column) == -1)
                        {
                            CurrentReferenceJoin.LOV.Add(column);
                        }
                    }
                }
            }
        }
    }
}