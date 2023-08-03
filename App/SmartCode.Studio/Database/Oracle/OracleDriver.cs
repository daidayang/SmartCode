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
using System.Data.OracleClient;

namespace SmartCode.Studio.Database.Oracle
{
    public class OracleDriver : Driver
    {
        public OracleDriver(DatabaseSchema databaseSchema)
            : base(databaseSchema)
        {

        }

        public override System.Data.IDbConnection CreateConnection()
        {
            StringBuilder connString = new StringBuilder();
            connString.AppendFormat("Data Source={0};", DatabaseSchema.ConnectionInfo.Database);

            if (base.DatabaseSchema.ConnectionInfo.User != "")
            {
                connString.AppendFormat("User ID={0}; Password={1};", DatabaseSchema.ConnectionInfo.User, DatabaseSchema.ConnectionInfo.Password);
            }
            else
            {
                connString.Append("User ID=/;Integrated Security=True;Persist Security Info=False;");
            }
            connString.Append("Unicode=True;Enlist=False;");

            return new OracleConnection(connString.ToString());
        }

        public override void ConfigureConnection(System.Data.IDbConnection connection)
        {
            throw new Exception("The method or operation is not implemented.");
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
            return new OracleSchemaExtractor(this);
        }
    }
}
