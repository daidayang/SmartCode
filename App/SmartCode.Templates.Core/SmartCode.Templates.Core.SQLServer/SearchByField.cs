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
using SmartCode.Template;
using SmartCode.Templates.Core.SQLServer.Utils;
using SmartCode.Model;

namespace SmartCode.Templates.Core.SQLServer
{
    public class SearchByField : TemplateBase
    {
        // Constructor.
        public SearchByField()
        {
            this.CreateOutputFile = true;
            Description = "Generates a stored procedure to retrieve Rows by a field";
            Name = "Retrieve Rows by a Field";
            OutputFolder = @"Stored Procedures\SearchByField";
        }

        public override string OutputFileName()
        {
            return Table.Name + "_SearchByField.sql";
        }

        public override void ProduceCode()
        {

            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.CustomProperties.SearchBy)
                {
                    string spName = Common.SP_NAME_PREFIX + Table.Code + "_" + column.Code + "_SearchByField";
                    string spPurpose = "Get Rows from the table " + Table.Name + " by Field " + column.Code;

                    WriteLine("--Begin " + spName);
                    W();
                    WriteLine("SET QUOTED_IDENTIFIER ON ");
                    WriteLine("GO");
                    WriteLine("SET ANSI_NULLS ON ");
                    WriteLine("GO");


                    WriteLine("-- Stored Procedure " + spName);
                    WriteLine("-- Purpose: " + spPurpose);
                    WriteLine("-- Parameters:");

                    // First, delete the stored procedure, if it exists
                    WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                    WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                    WriteLine1("CREATE PROCEDURE " + spName);

                    WriteLine1("(@{0} {1} ,{2})", column.Code, Common.GetFieldTypeAsTSQLType(column), Environment.NewLine);

                    WriteLine1("AS");


                    WriteLine2(Common.GetComplexSelect(this.Table));

                    WriteLine1("WHERE ");

                    WriteLine1("[{1}].[{2}] LIKE @{0} ", column.Code, Table.Name, column.Name);

                    WriteLine();
                    WriteLine1("GO ");
                    WriteLine();
                    WriteLine1("-- End Procedure");
                }
            }

        }
    }
}
