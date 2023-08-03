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
    /// Map Object Model to IndexSchema
    /// </summary>
    public class TableKeySchema : SchemaObjectBase
    {
        IList<ColumnSchema> m_ForeignKeyMemberColumns;
        IList<ColumnSchema> m_PrimaryKeyMemberColumns;
        TableSchema m_ForeignKeyTable;
        PrimaryKeySchema m_PrimaryKey;
        TableSchema m_PrimaryKeyTable;

        public TableKeySchema()
        {
            m_ForeignKeyMemberColumns = new List<ColumnSchema>();
            m_PrimaryKeyMemberColumns = new List<ColumnSchema>();
        }

        public IList<ColumnSchema> ForeignKeyMemberColumns
        {
            get { return m_ForeignKeyMemberColumns; }
            set { m_ForeignKeyMemberColumns = value; }
        }

        public IList<ColumnSchema> PrimaryKeyMemberColumns
        {
            get { return m_PrimaryKeyMemberColumns; }
            set { m_PrimaryKeyMemberColumns = value; }
        }

        public TableSchema ForeignKeyTable
        {
            get { return m_ForeignKeyTable; }
            set { m_ForeignKeyTable = value; }
        }

        public PrimaryKeySchema PrimaryKey
        {
            get { return m_PrimaryKey; }
            set { m_PrimaryKey = value; }
        }

        public TableSchema PrimaryKeyTable
        {
            get { return m_PrimaryKeyTable; }
            set { m_PrimaryKeyTable = value; }
        }
    }
}
