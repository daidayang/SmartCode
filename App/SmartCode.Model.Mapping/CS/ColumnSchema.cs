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
    /// Map Object Model to ColumnSchema
    /// </summary>
    public class ColumnSchema : DataObjectBase
    {
        TableSchema m_table;
        bool m_isForeignKeyMember;
        bool m_isPrimaryKeyMember;
        bool m_isUnique;

        public ColumnSchema()
            : base()
        {

        }

        public bool IsUnique
        {
            get { return m_isUnique; }
            set { m_isUnique = value; }
        }

        public TableSchema Table
        {
            get { return m_table; }
            set { m_table = value; }
        }

        public bool IsForeignKeyMember
        {
            get { return m_isForeignKeyMember; }
            set { m_isForeignKeyMember = value; }
        }

        public bool IsPrimaryKeyMember
        {
            get { return m_isPrimaryKeyMember; }
            set { m_isPrimaryKeyMember = value; }
        }


    }
}
