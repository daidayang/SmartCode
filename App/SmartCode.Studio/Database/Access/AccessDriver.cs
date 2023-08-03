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
using System.Data.OleDb;

namespace SmartCode.Studio.Database.Access
{
    internal class AccessDriver : Driver
    {
        /// <summary>
        /// msaccess://localhost/C:\\SampleDB.mdb
        /// Microsoft.Jet.OLEDB.4.0://localhost/C:\\SampleDB.mdb
        /// </summary>
        /// <param name="databaseSchema"></param>
        public AccessDriver(DatabaseSchema databaseSchema)
            : base(databaseSchema)
        {

        }

        public override System.Data.IDbConnection CreateConnection()
        {
            StringBuilder sb = new StringBuilder();

            //Open connection to Access database:
            //"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\App1\Your_Database_Name.mdb; User Id=admin; Password=" 

            //Open connection to password protected Access database:
            //"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\App1\Your_Database_Name.mdb; Jet OLEDB:Database Password=Your_Password" 

            sb.AppendFormat("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", DatabaseSchema.ConnectionInfo.Database);

            if (!String.IsNullOrEmpty(DatabaseSchema.ConnectionInfo.Password))
            {
                sb.AppendFormat("Jet OLEDB:Database Password={0}", DatabaseSchema.ConnectionInfo.Password);
            }
            else
            {
                sb.AppendFormat("User Id={0}; Password=", DatabaseSchema.ConnectionInfo.User);
            }

            return new OleDbConnection(sb.ToString());
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
            return new AccessSchemaExtractor(this);
        }
    }
}
