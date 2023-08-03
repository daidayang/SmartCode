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
    public class SelectByPrimaryKey : TemplateBase
    {
        // Constructor.
        public SelectByPrimaryKey()
        {
            CreateOutputFile = true;
            Description = "Generates a stored procedure to retrieve a row by its primary key";
            Name = "Retrieve Row by Primary Key";
            OutputFolder = @"Stored Procedures\SelectByPrimaryKey";
        }

        public override string OutputFileName()
        {
            return Table.Name + "_SelectByPrimaryKey.sql";
        }

        public override void ProduceCode()
        {
            if (Table.PrimaryKeyColumns().Count > 0)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                string spName = Common.SP_NAME_PREFIX + Table.Code + "_SelectByPrimaryKey";
                string spPurpose = "Get an existing row in table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                WriteLine(" CREATE PROCEDURE " + spName);

                string inputParameters = String.Empty;
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    inputParameters += "    " + Common.GetSqlParameterLine(column);
                }

                inputParameters = Common.Substring(inputParameters, Environment.NewLine.Length + 1);

                WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                WriteLine(" AS");


                WriteLine(Common.GetComplexSelect(this.Table ));
                
                string keyCondition = String.Empty;
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    keyCondition += String.Format(" [{2}].[{0}] = @{1} AND {3}", column.Name, column.Code, Table.Name, Environment.NewLine);
                }
                keyCondition = Common.Substring(keyCondition, (Environment.NewLine.Length + 5));

                WriteLine("	WHERE {0}({0} {1} {0})", Environment.NewLine, keyCondition);
                WriteLine();
                WriteLine("	GO ");
                WriteLine();
                WriteLine("-- End Procedure");
            }
            else
            {
                WriteLine("-- Entity " + Entity.Name + " does not have a primary key.");
            }   
        }
    }
}
