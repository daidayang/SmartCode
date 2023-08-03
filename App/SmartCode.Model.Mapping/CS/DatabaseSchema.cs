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
using System.Collections.Specialized;

namespace SmartCode.Model.Mapping.CS
{
    /// <summary>
    /// Map Object Model to DatabaseSchema
    /// </summary>
    public class DatabaseSchema : SchemaObjectBase
    {
        private IList<TableSchema> m_tables;
        private IList<ViewSchema> m_views;
        private string m_connectionString;

       
        public DatabaseSchema()
        {
            m_tables = new List<TableSchema>();
            m_views = new List<ViewSchema>();
        }

        internal IList<TableSchema> Tables
        {
            get { return m_tables; }
        }

        internal IList<ViewSchema> Views
        {
            get { return m_views; }
        }

        public string ConnectionString
        {
            get { return m_connectionString; }
            set { m_connectionString = value; }
        }

    }
}
