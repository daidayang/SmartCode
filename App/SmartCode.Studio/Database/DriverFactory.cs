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
using SmartCode.Studio.Database.Access;
using SmartCode.Studio.Database.MSSQL;
using SmartCode.Studio.Database.Oracle;
using SmartCode.Studio.Database.MySQL;

namespace SmartCode.Studio.Database
{
    internal sealed class DriverFactory
    {
        internal static Driver GetDriver(string location)
        {
            try
            {
                ConnectionInfo connInfo = new ConnectionInfo(location);
                DatabaseSchema dbSchema = new DatabaseSchema(connInfo);
                return GetDriver(dbSchema);
            }
            catch 
            {
                throw;
            }
        }

        internal static Driver GetDriver(DatabaseSchema databaseSchema)
        {
            switch (databaseSchema.ConnectionInfo.Provider)
            {
                case "mssql":
                case "mssql2005":
                    return new MSSQLDriver(databaseSchema);
                case "msaccess":
                    return new AccessDriver(databaseSchema);
                case "oracle":
                    return new OracleDriver(databaseSchema);
                case "mysql":
                    return new MySQLDriver(databaseSchema);
                default:
                    throw new Exception("Invalid Provider Type");
            }
        }

    }
}
