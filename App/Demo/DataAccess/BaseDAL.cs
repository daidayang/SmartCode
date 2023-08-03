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
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class BaseDAL: IDisposable 
    {
        protected static string ConnectionString
        {
            get
            {
                return "server=(local);User ID=sa;Password=;database=Northwind;Connection Reset=FALSE";
            }
        }

        /// <summary>
        /// Adds a new parameter to the specified command. It is not recommended that 
        /// you use this method directly from your custom code.
        /// <param name="cmd">The <see cref="System.Data.IDbCommand"/> object to add the parameter to.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="dbType">One of the <see cref="System.Data.DbType"/> values. </param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>A reference to the added parameter.</returns>
        internal IDbDataParameter AddParameter(IDbCommand cmd, string paramName, SqlDbType dbType, object value)
        {
            SqlParameter parameter = new SqlParameter(paramName, dbType);
            parameter.Value = (null == value) ? DBNull.Value : value;
            cmd.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Adds a new parameter to the specified command. It is not recommended that 
        /// you use this method directly from your custom code.
        /// </summary>
        /// <param name="cmd">The <see cref="System.Data.IDbCommand"/> object to add the parameter to.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="dbType">One of the <see cref="System.Data.DbType"/> values. </param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>A reference to the added parameter.</returns>
        internal IDbDataParameter AddParameter(IDbCommand cmd, string paramName, SqlDbType dbType, object value, System.Data.ParameterDirection paramDirection)
        {
            IDbDataParameter parameter = AddParameter(cmd, paramName, dbType, value);
            parameter.Direction = paramDirection;
            return parameter;
        }


        /// <summary>
        /// Creta Parameter to use its with Data Adapter and strong type dataset
        /// </summary>
        /// <param name="cmd">The <see cref="System.Data.IDbCommand"/> object to add the parameter to.</param>
        /// <param name="fieldName">el name of column field for parameter</param>
        /// <param name="dbType">One of the <see cref="System.Data.DbType"/> values. </param>
        /// <returns>A reference to the added parameter.</returns>
        protected static IDbDataParameter CreateParameter(string fieldName, SqlDbType dbType)
        {
            SqlParameter parameter = new SqlParameter("@" + fieldName, dbType);
            parameter.SourceColumn = fieldName;
            return parameter;
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
