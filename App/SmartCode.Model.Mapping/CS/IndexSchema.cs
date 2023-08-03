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
    public class IndexSchema : SchemaObjectBase
    {
        bool m_IsClustered;
        bool m_IsPrimaryKey;
        bool m_IsUnique;
        IList<ColumnSchema> m_MemberColumns;
        TableSchema m_Table;

        public IndexSchema()
        {
            m_MemberColumns = new List<ColumnSchema>();
        }

        public bool IsPrimaryKey
        {
            get { return m_IsPrimaryKey; }
            set { m_IsPrimaryKey = value; }
        }

        public bool IsUnique
        {
            get { return m_IsUnique; }
            set { m_IsUnique = value; }
        }

        public IList<ColumnSchema> MemberColumns
        {
            get { return m_MemberColumns; }
            set { m_MemberColumns = value; }
        }

        public TableSchema Table
        {
            get { return m_Table; }
            set { m_Table = value; }
        }

        public bool IsClustered
        {
            get { return m_IsClustered; }
            set { m_IsClustered = value; }
        }
    }
}
