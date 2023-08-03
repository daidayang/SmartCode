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
    /// Map Object Model to PrimaryKeySchema
    /// </summary>
    public class PrimaryKeySchema : SchemaObjectBase
    {
        IList<ColumnSchema> m_memberColumns;
        TableSchema m_table;

        public PrimaryKeySchema()
        {
            m_memberColumns = new List<ColumnSchema>();
        }

        public IList<ColumnSchema> MemberColumns
        {
            get { return m_memberColumns; }
            set { m_memberColumns = value; }
        }

        public TableSchema Table
        {
            get { return m_table; }
            set { m_table = value; }
        }
    }
}
