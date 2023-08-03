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
using StoreProcedures.Utils;
using SmartCode.Model;

namespace StoreProcedures
{
    public class InsertStatement : TemplateBase
    {
        public InsertStatement()
        {
            CreateOutputFile = true;
            Description = "Generates a stored procedure to insert one row in a table";
            Name = "Insert One Row";
            OutputFolder = @"Stored Procedures\InsertCode";
        }

        public override string OutputFileName()
        {
            return base.Entity.Name + "_InsertRow.sql";
        }

        public override void ProduceCode()
        {
            WriteLine("SET QUOTED_IDENTIFIER ON ");
            WriteLine("GO");
            WriteLine("SET ANSI_NULLS ON ");
            WriteLine("GO");

            string spName = Common.SP_NAME_PREFIX + Table.Code + "_InsertRow";
            string spPurpose = "Insert one row in table " + Table.Name;

            WriteLine("-- Stored Procedure " + spName);
            WriteLine("-- Purpose: " + spPurpose);
            WriteLine("-- Parameters:");

            // First, delete the stored procedure, if it exists
            WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

            WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
            WriteLine(" CREATE PROCEDURE " + spName);

            string inputParams = String.Empty;

            foreach (KeyValuePair<String, ColumnSchema> keyPair in Table.ColumnSchemaCollection)
            {
                if (keyPair.Value.IsIdentity)
                    continue;
                inputParams += "    " + Common.GetSqlParameterLine(keyPair.Value);
            }

            inputParams = Common.Substring(inputParams, Environment.NewLine.Length + 1);
            WriteLine(inputParams);

            WriteLine("     AS ");
            WriteLine("         INSERT INTO [" + Table.Name + "]");

            string keyConditions = String.Empty;
           
            foreach (ColumnSchema column in Table.Columns())
            {
                if (!column.IsIdentity)
                {
                    keyConditions += "           [" + column.Name + "] ," + " \n";
                }
            }
            keyConditions = Common.Substring(keyConditions, ((" ,").Length + ("\n").Length));

            WriteLine("     ({1}{0}{1}	)", keyConditions, Environment.NewLine);
            WriteLine("     VALUES ");

            inputParams = "";
            foreach (ColumnSchema column in Table.Columns())
            {
                if (!column.IsIdentity )
                {
                    inputParams += "         @" + column.Code + " ,\n";
                }
            }
            inputParams = Common.Substring(inputParams, ((" ,").Length + ("\n").Length));

            WriteLine("     ({1}{0}{1}	)", inputParams, Environment.NewLine);
            WriteLine();
            WriteLine("     GO ");
            WriteLine();
            WriteLine("-- End Procedure"); 
        }
    }
}
