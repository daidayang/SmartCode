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
using SmartCode.Studio.Database.Info;
using System.Data;
using System.Collections;
using SmartCode.Model;

namespace SmartCode.Studio.Database
{
    /// <summary>
    /// An abstract class that declares a set of methods for database schema extraction.
    /// </summary>
    public abstract class SchemaExtractor
    {
        private Driver driver;
        protected IDictionary<String, SqlType> sqlTypes;
        private IDictionary<SqlType, String> netDataTypes;


        public SchemaExtractor(Driver currentDriver)
        {
            Driver = currentDriver;

            sqlTypes = TypesFactory.GetSQLTypes(driver.DatabaseSchema.ConnectionInfo.Provider);
            netDataTypes = TypesFactory.GetNetDataTypes();

        }

        /// <summary>
        /// Gets a list of all essential tables from the database.
        /// </summary>
        /// <returns>An array of strings containing view names.</returns>
        public abstract string[] GetAllTables();

        /// <summary>
        /// Gets a list of all essential views from the database.
        /// </summary>
        /// <returns>An array of strings containing table names.</returns>
        public abstract string[] GetAllViews();

        /// <summary>
        /// Gets a list of all columns from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to extract columns information from.</param>
        /// <returns>An array of <see cref="ColumnProperties"/> objects describing all columns.</returns>
        public abstract ColumnInfo[] GetColumns(string tableName);

        /// <summary>
        /// Get all constraint properties from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to extract constraint from.</param>
        /// <returns>An array of ConstraintInfo objects describing all the constraints.</returns>
        public abstract ConstraintInfo[] GetConstraints(string tableName);

        /// <summary>
        /// Get all keys from the specified table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract KeyInfo[] GetKeys(string tableName);

        protected virtual IDbCommand CreateCommand()
        {
            IDbConnection connection = this.driver.CreateConnection();
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandTimeout = 0;
            return cmd;
        }

        protected Driver Driver
        {
            get { return driver; }
            set { driver = value; }
        }

        protected IDictionary<SqlType, String> NetDataTypes
        {
            get { return netDataTypes; }
            set { netDataTypes = value; }
        }

    }
}
