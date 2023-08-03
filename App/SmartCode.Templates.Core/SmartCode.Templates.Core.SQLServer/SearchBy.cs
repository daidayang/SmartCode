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
    public class SearchBy : TemplateBase
    {
        // Constructor.
        public SearchBy()
        {
            this.CreateOutputFile = true;
            Description = "Generates a stored procedure to retrieve Rows by fields";
            Name = "Retrieve Rows by Fields";
            OutputFolder = @"Stored Procedures\SearchBy";
        }

        public override string OutputFileName()
        {
            return Table.Name + "_SearchBy.sql";
        }

        public override void ProduceCode()
        {
            bool runTemplate = false;

            foreach (ColumnSchema column in Table.Columns())
            {
                runTemplate |= column.CustomProperties.SearchBy;
            }

            if (runTemplate)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                string spName = Common.SP_NAME_PREFIX + Table.Code + "_SearchBy";
                string spPurpose = "Get Rows from the table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                WriteLine(" CREATE PROCEDURE " + spName);

                string inputParameters = String.Empty;
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (column.CustomProperties.SearchBy)
                    {
                        inputParameters += "    " + Common.GetSqlParameterLine(column);
                    }
                }

                inputParameters = Common.Substring(inputParameters, Environment.NewLine.Length + 1);

                WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                WriteLine(" AS");


                WriteLine(Common.GetComplexSelect(this.Table));

                WriteLine(" WHERE ");
                
                bool isFirst = true;
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (column.CustomProperties.SearchBy)
                    {
                        if (! isFirst)
                        {
                            WriteLine(" AND ");
                        }
                        WriteLine(" (@{0} = '%' OR @{0} IS NULL OR [{1}].[{2}] LIKE @{0}) ", column.Code, Table.Name, column.Name );
                        isFirst = false;
                    }
                }

                WriteLine();
                WriteLine("	GO ");
                WriteLine();
                WriteLine("-- End Procedure");
            }
            else
            {
                WriteLine("-- Entity " + Entity.Name + " does not have a search by assigned property.");
            }   
        }
    }
}
