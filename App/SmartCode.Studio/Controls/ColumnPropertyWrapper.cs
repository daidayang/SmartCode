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
using SmartCode.Model.Profile;
using SmartCode.Studio.Controls.EditorWrapper;
using System.ComponentModel;
using System.Drawing.Design;

namespace SmartCode.Studio.Controls
{
    internal class ColumnPropertyWrapper : NamedPropertyWrapper
    {
        public ColumnPropertyWrapper(ExplorerTreeNode treeNode, ColumnSchema column)
            : base(treeNode, column)
        {
        }

        internal  ColumnSchema CurrentColumn
        {
            get
            {
                return NamedObject as ColumnSchema;
            }
        }

        [DisplayText("PropSqlType", "PropSqlTypeDesc", "DatabaseCategory")]
        public SqlType SqlType
        {
            get { return CurrentColumn.SqlType; }
            set { CurrentColumn.SqlType = value; }
        }

        [DisplayText("PropIsPrimaryKey", "PropIsPrimaryKeyDesc", "DatabaseCategory")]
        public bool IsPrimaryKey
        {
            get { return CurrentColumn.IsPrimaryKey; }
            set { CurrentColumn.IsPrimaryKey = value; }
        }

        [DisplayText("PropIsForeignKey", "PropIsForeignKeyDesc", "DatabaseCategory")]
        public bool IsForeignKey
        {
            get { return CurrentColumn.IsForeignKey; }
        }

        [DisplayText("PropIsIdentity", "PropIsIdentityDesc", "DatabaseCategory")]
        public bool IsIdentity
        {
            get { return CurrentColumn.IsIdentity; }
            set { CurrentColumn.IsIdentity = value; }
        }

        [DisplayText("PropIsRequired", "PropIsRequiredDesc", "DatabaseCategory")]
        public bool IsRequired
        {
            get { return CurrentColumn.IsRequired; }
            set { CurrentColumn.IsRequired = value; }
        }

        [DisplayText("PropNetDataType", "PropNetDataTypeDesc", "DatabaseCategory")]
        public string NetDataType
        {
            get { return CurrentColumn.NetDataType; }
            set { CurrentColumn.NetDataType = value; }
        }

        [DisplayText("PropLength", "PropLengthDesc", "DatabaseCategory")]
        public int Length
        {
            get { return CurrentColumn.Length; }
            set { CurrentColumn.Length = value; }
        }

        [DisplayText("PropPrecision", "PropPrecisionDesc", "DatabaseCategory")]
        public int Precision
        {
            get { return CurrentColumn.Precision; }
            set { CurrentColumn.Precision = value; }
        }

        [DisplayText("PropScale", "PropScaleDesc", "DatabaseCategory")]
        public int Scale
        {
            get { return CurrentColumn.Scale; }
            set { CurrentColumn.Scale = value; }
        }

        [DisplayText("DefaultValueSchema", "PropDefaultValueDesc", "DatabaseCategory")]
        public String DefaultValue
        {
            get { return CurrentColumn.DefaultValue; }
            set { CurrentColumn.DefaultValue = value; }
        }

        [DisplayText("PropColumnHistory", "PropColumnHistoryDesc", "DatabaseCategory")]
        public SmartCode.Model.ColumnSchema.ColumnHistoryType ColumnHistory
        {
            get { return CurrentColumn.ColumnHistory; }
            set { CurrentColumn.ColumnHistory = value; }
        }

        [DisplayText("PropOriginalSQLType", "PropOriginalSQLTypeDesc", "DatabaseCategory")]
        public string OriginalSQLType
        {
            get { return CurrentColumn.OriginalSQLType; }
            set { CurrentColumn.OriginalSQLType = value; }
        }

        [DisplayText("PropProfile", "PropProfileDesc", "ClientProfileCategory")]
        [Editor(typeof(ClientProfilerEditor), typeof(UITypeEditor))]
        public string Profile
        {
            get
            {
               // ClientProfileBase 
                return CurrentColumn.Control.Name;
            }
            set
            {
                if (! string.IsNullOrEmpty(value))
                {
                    CurrentColumn.Control = this.CurrentColumn.Table.Domain.Controls[value];
                }
            }
        }

        [DisplayText("PropLOV", "PropLOVDesc", "JoinCategory")]
        [Editor(typeof(LOVEditor), typeof(UITypeEditor))]
        public string LOV
        {
            get
            {
                string results = "";
                foreach (ColumnSchema column in CurrentColumn.GetLOV())
                {
                    results += column.Name + ",";
                }
                return string.IsNullOrEmpty(results) ? "" : results.Substring(0, results.Length - 1);
            }
            set
            {
                foreach (ReferenceSchema reference in CurrentColumn.Table.InReferences)
                {
                    foreach (ReferenceJoin join in reference.Joins)
                    {
                        if (join.ChildColumn == CurrentColumn)
                        {
                            join.LOV.Clear();
                            if (!string.IsNullOrEmpty(value))
                            {
                                TableSchema parentTable = join.ParentReference.ParentTable;
                                string[] columns = value.Split(',');
                                foreach (string colName in columns)
                                {
                                    ColumnSchema column = parentTable.FindColumn(colName);
                                    if (column != null && join.LOV.IndexOf(column) == -1)
                                    {
                                        join.LOV.Add(column);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal IDictionary<String, ControlBase> GetAllControls()
        {
            return this.CurrentColumn.Table.Domain.Controls; 
        }


        [DisplayText("PropDeleteBy", "PropDeleteByDesc", "CustomPropertiesCategory")]
        public bool DeleteBy
        {
            get { return CurrentColumn.CustomProperties.DeleteBy; }
            set { CurrentColumn.CustomProperties.DeleteBy = value; }
        }

        [DisplayText("PropSearchBy", "PropSearchByDesc", "CustomPropertiesCategory")]
        public bool SearchBy
        {
            get { return CurrentColumn.CustomProperties.SearchBy; }
            set { CurrentColumn.CustomProperties.SearchBy = value; }
        }

    }
}
