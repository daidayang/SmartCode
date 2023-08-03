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

namespace SmartCode.Model.Mapping.CS
{
    /// <summary>
    /// Map Object Model to SchemaObjectBase
    /// </summary>
    public class TableSchema : TabularObjectBase
    {
        string m_fullName;
        IList<ColumnSchema> m_Columns;
        string m_Owner;
        bool m_HasPrimaryKey;
        PrimaryKeySchema m_PrimaryKey;

        IList<ColumnSchema> m_ForeignKeyColumns;
        IList<ColumnSchema> m_NonForeignKeyColumns;
        IList<ColumnSchema> m_NonKeyColumns;
        IList<ColumnSchema> m_NonPrimaryKeyColumns;

        IList<TableKeySchema> m_ForeignKeys;
        IList<TableKeySchema> m_Keys;
        IList<TableKeySchema> m_PrimaryKeys;

        IList<IndexSchema> m_Indexes;

        public TableSchema()
        {
            m_Columns = new List<ColumnSchema>();
            m_ForeignKeyColumns = new List<ColumnSchema>();
            m_NonForeignKeyColumns = new List<ColumnSchema>();
            m_NonKeyColumns = new List<ColumnSchema>();
            m_NonPrimaryKeyColumns = new List<ColumnSchema>();
            m_ForeignKeys = new List<TableKeySchema>();
            m_Keys = new List<TableKeySchema>();
            m_PrimaryKeys = new List<TableKeySchema>();
            m_Indexes = new List<IndexSchema>();

        }

        public IList<ColumnSchema> Columns
        {
            get { return m_Columns; }
            set { m_Columns = value; }
        }


        public string Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        public IList<ColumnSchema> ForeignKeyColumns
        {
            get { return m_ForeignKeyColumns; }
            set { m_ForeignKeyColumns = value; }
        }

        public IList<ColumnSchema> NonForeignKeyColumns
        {
            get { return m_NonForeignKeyColumns; }
            set { m_NonForeignKeyColumns = value; }
        }

        public IList<ColumnSchema> NonKeyColumns
        {
            get { return m_NonKeyColumns; }
            set { m_NonKeyColumns = value; }
        }

        public IList<ColumnSchema> NonPrimaryKeyColumns
        {
            get { return m_NonPrimaryKeyColumns; }
            set { m_NonPrimaryKeyColumns = value; }
        }

        public IList<TableKeySchema> ForeignKeys
        {
            get { return m_ForeignKeys; }
            set { m_ForeignKeys = value; }
        }

        public IList<TableKeySchema> Keys
        {
            get { return m_Keys; }
            set { m_Keys = value; }
        }

        public IList<TableKeySchema> PrimaryKeys
        {
            get { return m_PrimaryKeys; }
            set { m_PrimaryKeys = value; }
        }

        public IList<IndexSchema> Indexes
        {
            get { return m_Indexes; }
            set { m_Indexes = value; }
        }

        public bool HasPrimaryKey
        {
            get { return m_HasPrimaryKey; }
            set { m_HasPrimaryKey = value; }
        }


        public PrimaryKeySchema PrimaryKey
        {
            get { return m_PrimaryKey; }
            set { m_PrimaryKey = value; }
        }

        public override string FullName
        {
            get { return m_fullName; }
            set { m_fullName = value; }
        }

        public DateTime DateCreated
        {
            get
            {
                return DateTime.Now ;
            }
        }
 

         
    }
}
