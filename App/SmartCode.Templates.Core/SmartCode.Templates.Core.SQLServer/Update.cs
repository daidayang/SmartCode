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
using System.Collections;
using SmartCode.Model;
using SmartCode.Templates.Core.SQLServer.Utils;

namespace SmartCode.Templates.Core.SQLServer
{
    public class Update : TemplateBase
    {
        public Update()
        {
            CreateOutputFile = true;
            Description = "Generates a stored procedure to update a row by its primary key";
            Name = "Update Row By Primary Key";
            OutputFolder = @"Stored Procedures\Update";
        }

        public override string OutputFileName()
        {
            return base.Entity.Name + "_Update.sql";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> keyColumns = Table.PrimaryKeyColumns();

            WriteLine("SET QUOTED_IDENTIFIER ON ");
            WriteLine("GO");
            WriteLine("SET ANSI_NULLS ON ");
            WriteLine("GO");
            // Generate code only if the entity has a primary key an other no PK column 

            if (keyColumns.Count > 0)
            {
                string spName = Common.SP_NAME_PREFIX + Table.Code + "_Update";
                string spPurpose = "Update an existing row in table " + Table.Name + " by its primary key.";

                WriteLine("-- Stored Procedure " + spName);
                WriteLine(@"-- Purpose: " + spPurpose);

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                WriteLine(" CREATE PROCEDURE " + spName);

                string inputParams = "";
                bool isFirst = true;
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (!isFirst)
                    {
                        inputParams += "    ,";
                    }
                    isFirst = false;
                    inputParams += "@" + column.Code + " " + Common.GetFieldTypeAsTSQLType(column.OriginalSQLType, column.Length, column.Precision, column.Scale.ToString()) + NewLine;
                }

                WriteLine("	({0}{1}{0})", NewLine, inputParams);
                WriteLine("	AS");
                WriteLine("	SET NOCOUNT ON");
                WriteLine("	UPDATE [" + Table.Name + "]");

                string setSection = "	SET" + NewLine;

                isFirst = true;
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (!column.IsPrimaryKey)
                    {
                        if (!column.IsIdentity)
                        {
                            if (!isFirst)
                            {
                                setSection += "    ,";
                            }
                            setSection += "[" + column.Name + "] = @" + column.Code + NewLine;
                            isFirst = false;
                        }
                    }
                }


                WriteLine(setSection);

                WriteLine("	WHERE(");
                string whereSection = "";

                isFirst = true;
                foreach (ColumnSchema column in keyColumns)
                {
                    if (!isFirst)
                    {
                        setSection += "    AND ";
                    }
                    whereSection += "[" + column.Name + "] =  @" + column.Code + NewLine;
                    isFirst = false;
                }

                WriteLine(whereSection + NewLine + ")");

                WriteLine();
                WriteLine("	GO ");
                WriteLine("-- End Procedure");
            }
            else
            {
                WriteLine("-- Entity " + Table.Name + " does not have a primary key.");

            }
        }
    }
}
