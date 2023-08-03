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
using SmartCode.Model;
using System.Data.SqlClient;

namespace SmartCode.Studio.Database.MSSQL
{
    public class MSSQLDriver: Driver
    {
        public MSSQLDriver(DatabaseSchema databaseSchema)
            : base(databaseSchema)
        {

        }

        public override System.Data.IDbConnection CreateConnection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Data Source={0};Initial Catalog={1};", DatabaseSchema.ConnectionInfo.Host, DatabaseSchema.ConnectionInfo.Database);
            if (!String.IsNullOrEmpty(DatabaseSchema.ConnectionInfo.User))
            {
                sb.AppendFormat("User ID={0};Password={1};", DatabaseSchema.ConnectionInfo.User, DatabaseSchema.ConnectionInfo.Password);
            }
            else
            {
                sb.Append("Integrated Security=SSPI;Persist Security Info=False;");
            }
            return new SqlConnection(sb.ToString());
        }

        public override void ConfigureConnection(System.Data.IDbConnection connection)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void TestConnection()
        {
            System.Data.IDbConnection connection = CreateConnection();
            connection.Open();
            connection.Close();
            connection.Dispose();
        }

        protected override SchemaExtractor CreateExtractor()
        {
            return new MSSQLSchemaExtractor(this);
        }
    }
}
