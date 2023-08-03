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
using StoreProcedures.Utils;

namespace StoreProcedures
{
    public class UpdateStatement : TemplateBase
    {
        public UpdateStatement()
        {
            CreateOutputFile = true;
            Description = "Generates a stored procedure to update a row by its primary key";
            Name = "Update Row By Primary Key";
            OutputFolder = @"Stored Procedures\UpdateCode";
        }

        public override string OutputFileName()
        {
            return base.Entity.Name + "_UpdateRow.sql";
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
                string spName = Common.SP_NAME_PREFIX + Table.Code + "_UpdateRow";
                string spPurpose = "Update an existing row in table " + Table.Name + " by its primary key.";

                WriteLine("-- Stored Procedure " + spName);
                WriteLine(@"-- Purpose: " + spPurpose);

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                WriteLine("	CREATE PROCEDURE " + spName);

                string inputParams = "";

                foreach (ColumnSchema column in Table.Columns())
                {
                    inputParams += "		@" + column.Code + " " + Common.GetFieldTypeAsTSQLType(column.OriginalSQLType, column.Length, column.Precision, column.Scale.ToString()) + ",\n";
                }

                inputParams = Common.Substring(inputParams, (",\n").Length);
                WriteLine("	(\n" + inputParams + "\n	)");
                WriteLine("	AS");
                WriteLine("	SET NOCOUNT ON");
                WriteLine("	UPDATE [" + Table.Name + "]");
                string setSection = "	SET\n";
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (!column.IsPrimaryKey)
                    {
                        if (!column.IsIdentity)
                        {
                            setSection += "		[" + column.Name + "] = @" + column.Code + ",\n";
                        }
                    }
                }

                setSection = Common.Substring(setSection, "\n");

                WriteLine(setSection);

                WriteLine("	WHERE(");
                string whereSection = "";
                foreach (ColumnSchema column in keyColumns)
                {
                    whereSection += "		[" + column.Name + "] =  @" + column.Code + " AND \n";
                }
                whereSection = Common.Substring(whereSection, "\n");

                WriteLine(whereSection + "\n	)");

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
