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
using System.Text.RegularExpressions;

namespace SmartCode.Model
{
    [Serializable()]
    public class DatabaseSchema : IdentifiedObject
    {
        private IList<TableSchema> tables;
        private ConnectionInfo connectionInfo;

        internal DatabaseSchema()
        {
            this.tables = new List<TableSchema>();
        }

        public DatabaseSchema(ConnectionInfo connectionInfo)
            : this()
        {
            this.connectionInfo = connectionInfo;
        }

        public DatabaseSchema(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.tables = (IList<TableSchema>)Info.GetValue("tables", typeof(IList<TableSchema>));
            this.connectionInfo = (ConnectionInfo)Info.GetValue("connectionInfo", typeof(ConnectionInfo));
        }

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("tables", this.tables);
            Info.AddValue("connectionInfo", this.connectionInfo);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IList<TableSchema> Tables
        {
            get { return tables; }
        }


        public ConnectionInfo ConnectionInfo
        {
            get { return connectionInfo; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the given tables.
        /// </summary>
        /// <param name="table">The table to add</param>
        public void AddTable(TableSchema table)
        {
            if (this.tables != null)
            {
                this.tables.Add(table);
            }
        }

        /// <summary>
        /// Removes the given table.
        /// </summary>
        /// <param name="table">The table to remove</param>
        public bool RemoveTable(TableSchema table)
        {
            if (this.tables != null)
            {
               return  this.tables.Remove(table);
            }
            return false;
        }


        /// <summary>
        /// Finds the table with the specified name, using case insensitive matching.
        /// </summary>
        /// <param name="name">The name of the table to find</param>
        /// <returns>The table or <code>null</code> if there is no such table</returns>
        public TableSchema FindTable(String name)
        {
            return FindTable(name, false);
        }

        /// <summary>
        /// Finds the table with the specified name, using case insensitive matching.
        /// </summary>
        /// <param name="name">The name of the table to find</param>
        /// <param name="caseSensitive">Whether case matters for the names</param>
        /// <returns>The table or <code>null</code> if there is no such table</returns>
        public TableSchema FindTable(String name, bool caseSensitive)
        {
            foreach (TableSchema table in this.tables)
            {
                if (caseSensitive)
                {
                    if (table.Name.Equals(name))
                    {
                        return table;
                    }
                }
                else
                {
                    if (table.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return table;
                    }
                }
            }
            return null;
        }

        public System.Collections.ArrayList GetAllTables()
        {
            System.Collections.ArrayList results = new System.Collections.ArrayList();
            foreach (TableSchema table in this.tables)
            {
                if (table.IsTable)
                {
                    results.Add(table);
                }
            }
            return results;
        }


        public System.Collections.ArrayList GetAllViews()
        {
            System.Collections.ArrayList results = new System.Collections.ArrayList();
            foreach (TableSchema table in this.tables)
            {
                if (! table.IsTable)
                {
                    results.Add(table);
                }
            }
            return results;
        }

        #endregion


    }
}
