
/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * Julio Cesar Aragón <julio.aragonj@kontac.net>
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
using MySql.Data.MySqlClient;

namespace SmartCode.Studio.Database.MySQL
{
    public class MySQLDriver : Driver
    {
        public MySQLDriver(DatabaseSchema databaseSchema)
            : base(databaseSchema)
        {

        }

        public override System.Data.IDbConnection CreateConnection()
        {
            StringBuilder connString = new StringBuilder();
            connString.AppendFormat("server={0};user id={1};database={2};password={3}", DatabaseSchema.ConnectionInfo.Host, DatabaseSchema.ConnectionInfo.User, DatabaseSchema.ConnectionInfo.Database, DatabaseSchema.ConnectionInfo.Password);

            /*  if (base.DatabaseSchema.ConnectionInfo.User != "")
              {
                  connString.AppendFormat("User ID={0}; Password={1};", DatabaseSchema.ConnectionInfo.User, DatabaseSchema.ConnectionInfo.Password);
              }
              else
              {
                  connString.Append("User ID=/;Integrated Security=True;Persist Security Info=False;");
              }
              connString.Append("Unicode=True;Enlist=False;"); */

            return new MySqlConnection(connString.ToString());
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
            return new MySQLSchemaExtractor(this);
        }
    }
}
