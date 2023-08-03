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
using System.Runtime.Serialization;
using SmartCode.Model.Profile;
using System.Collections;
using System.Data;

namespace SmartCode.Model
{
    [Serializable()]
    public class ColumnSchema : NamedObject, ICloneable 
    {
        public enum ColumnHistoryType
        {
            None,
            CreatedOn,
            ModifiedOn,
            CreatedBy,
            ModifiedBy,
        }

        #region Fields
        private TableSchema table;
 
        private bool isPrimaryKey;
        private bool isForeignKey;
        private bool isIdentity;
        private bool isRequired;
        private string netDataType;
        private SqlType sqlType;
        private int length;
        private int precision;
        private int scale;
        private String defaultValue;

        private String originalSQLType;

        private ColumnHistoryType columnHistory;

        private ControlBase control;

        private CustomColumnsProperties customColumnsProperties;


        #endregion
        
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        internal ColumnSchema()
            : base("")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table"></param>
        public ColumnSchema(string name, TableSchema table)
            : base(name)
        {
            if (table == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "table"));
            }
            this.table = table;
            this.columnHistory = ColumnHistoryType.None;
            this.customColumnsProperties = new CustomColumnsProperties();
            table.Domain.Controls.TryGetValue("TextBox", out this.control);
        }

        public ColumnSchema(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.table = (TableSchema)Info.GetValue("table", typeof(TableSchema));
            this.isPrimaryKey = (bool)Info.GetValue("isPrimaryKey", typeof(bool));
            this.isForeignKey = (bool)Info.GetValue("isForeignKey", typeof(bool));
            this.isIdentity = (bool)Info.GetValue("isIdentity", typeof(bool));
            this.isRequired = (bool)Info.GetValue("isRequired", typeof(bool));
            this.netDataType = (string)Info.GetValue("netDataType", typeof(string));
            this.sqlType = (SqlType)Info.GetValue("sqlType", typeof(SqlType));
            this.length = (int)Info.GetValue("length", typeof(int));
            this.precision = (int)Info.GetValue("precision", typeof(int));
            this.scale = (int)Info.GetValue("scale", typeof(int));
            this.defaultValue = (string)Info.GetValue("defaultValue", typeof(string));
            this.originalSQLType = (string)Info.GetValue("originalSQLType", typeof(string));
            this.columnHistory = (ColumnHistoryType)Info.GetValue("columnHistory", typeof(ColumnHistoryType));
            this.control = (ControlBase)Info.GetValue("control", typeof(ControlBase));
            this.customColumnsProperties = (CustomColumnsProperties)Info.GetValue("customColumnsProperties", typeof(CustomColumnsProperties));
        }
        #endregion

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("table", this.table);
            Info.AddValue("isPrimaryKey", this.isPrimaryKey);
            Info.AddValue("isForeignKey", this.isForeignKey);
            Info.AddValue("isIdentity", this.isIdentity);
            Info.AddValue("isRequired", this.isRequired);
            Info.AddValue("netDataType", this.netDataType);
            Info.AddValue("sqlType", this.sqlType);
            Info.AddValue("length", this.length);
            Info.AddValue("precision", this.precision);
            Info.AddValue("scale", this.scale);
            Info.AddValue("defaultValue", this.defaultValue);
            Info.AddValue("originalSQLType", this.originalSQLType);
            Info.AddValue("columnHistory", this.columnHistory);
            Info.AddValue("control", this.control);
            Info.AddValue("customColumnsProperties", this.customColumnsProperties);
        }

        #endregion

        #region Properties
        public TableSchema Table
        {
            get { return table; }
            set { table = value; }
        }


        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        public bool IsForeignKey
        {
            get { return isForeignKey; }
        }

        public bool IsIdentity
        {
            get { return isIdentity; }
            set { isIdentity = value; }
        }

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }

        public string NetDataType
        {
            get { return netDataType; }
            set { netDataType = value; }
        }

        public SqlType SqlType
        {
            get { return sqlType; }
            set { sqlType = value; }
        }


        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public int Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        public int Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public String DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        public ColumnHistoryType ColumnHistory
        {
            get { return columnHistory; }
            set { columnHistory = value; }
        }


        public ControlBase Control
        {
            get { return control; }
            set { control = value; }
        }

        public String OriginalSQLType
        {
            get { return originalSQLType; }
            set { originalSQLType = value; }
        }


        public SmartCode.Model.Profile.CustomColumnsProperties CustomProperties
        {
            get { return customColumnsProperties; }
            set { customColumnsProperties = value; }
        }

        public IList<ColumnSchema> GetLOV()
        {
            IList<ColumnSchema> lovColumns = new List<ColumnSchema>();
            foreach (ReferenceSchema reference in Table.InReferences)
            {
                foreach (ReferenceJoin join in reference.Joins)
                {
                    if (join.ChildColumn == this && join.ChildColumn.Name == this.Name)
                    {
                        foreach (ColumnSchema lovColumn in join.LOV)
                        {
                            lovColumns.Add(lovColumn);
                        }
                    }
                }
            }
            return lovColumns;
        }

  
        #endregion

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Column [name=");
            result.Append(Name);
            result.Append("; type=");
            result.Append(this.SqlType.ToString());
            result.Append("]");

            return result.ToString();
        }

        #region ICloneable Members

        public object Clone()
        {
            ColumnSchema column = new ColumnSchema(Name, this.table);
            column.Code = Code;
            column.Caption = base.Caption;
            column.IsPrimaryKey = this.IsPrimaryKey;
            column.IsIdentity = this.isIdentity;
            column.IsRequired = this.isRequired;
            column.NetDataType = this.netDataType;
            column.SqlType = this.sqlType;
            column.Length = this.length;
            column.Precision = this.precision;
            column.Scale = this.scale;
            column.DefaultValue = this.defaultValue;
            column.columnHistory = this.columnHistory;
            column.customColumnsProperties = this.customColumnsProperties;
            column.OriginalSQLType = this.originalSQLType;
            return column;
        }

        #endregion

    }
}
