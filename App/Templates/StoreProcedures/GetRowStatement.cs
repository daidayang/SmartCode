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
    public class GetRowStatement : TemplateBase
    {
        // Constructor.
        public GetRowStatement()
        {
            CreateOutputFile = true;
            Description = "Generates a stored procedure to retrieve a row by its primary key";
            Name = "Retrieve Row by Primary Key";
            OutputFolder = @"Stored Procedures\GetRowByPrimaryKey";
        }

        public override string OutputFileName()
        {
            return Table.Name + "_GetRowByPrimaryKey.sql";
        }

        public override void ProduceCode()
        {
            if (Table.PrimaryKeyColumns().Count > 0)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                string spName = Common.SP_NAME_PREFIX + Table.Code + "_GetRowByPrimaryKey";
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

                StringBuilder selectStm = new StringBuilder( "SELECT " + Environment.NewLine);
                foreach (ColumnSchema columm in Table.Columns())
                {
                    selectStm.AppendFormat("    [{0}].[{1}] as {2},{3}", Table.Name, columm.Name, columm.Code, Environment.NewLine);
                }

                int i = 0;

                foreach (ReferenceSchema reference in Table.InReferences)
                {
                    foreach (ReferenceJoin join in reference.Joins)
                    {
                        //selectStm.AppendFormat("    [T{0}].[{1}] as {2}_{3},{4}", i, join.ParentColumn.Name, join.ChildColumn.Code, join.ParentColumn.Code, Environment.NewLine);
                        //Look for LOV
                        foreach (ColumnSchema lovColumn in join.LOV)
                        {
                            if (lovColumn.Name != join.ParentColumn.Name)
                            {
                                selectStm.AppendFormat("    [T{0}].[{1}] as {2}_{3},{4}", i, lovColumn.Name, join.ChildColumn.Code, lovColumn.Code, Environment.NewLine);
                            }
                        }
                    }
                    i += 1;
                }

                //Remove the '\n,'
                selectStm = new StringBuilder(Common.Substring(selectStm.ToString(), Environment.NewLine.Length + 1));
                selectStm.AppendFormat("  FROM [{0}] ", Table.Name);

                i = 0;
                string alignment = "";

                foreach (ReferenceSchema reference in Table.InReferences)
                {
                    if (reference.Alignment == ReferenceSchema.AlignmentType.Inner) alignment = " INNER JOIN ";
                    else if (reference.Alignment == ReferenceSchema.AlignmentType.Left) alignment = " LEFT OUTER JOIN ";
                    else if (reference.Alignment == ReferenceSchema.AlignmentType.Right) alignment = " RIGHT OUTER JOIN ";

                    selectStm.AppendFormat("    {0} {1} AS T{2} ",alignment + Environment.NewLine , reference.ParentTable.Name, i);  

                    string onJoin = "";
                    foreach (ReferenceJoin join in reference.Joins)
                    {
                        onJoin += String.Format(" ON  T{0}.[{1}] = [{2}].[{3}] AND", i, join.ParentColumn.Name, Table.Name, join.ChildColumn.Name);
                        onJoin += Environment.NewLine;
                    }
                    onJoin = Common.Substring(onJoin, Environment.NewLine.Length + 3);
                    selectStm.Append(onJoin);

                    i += 1;
                }

                WriteLine(selectStm.ToString());
                
                string keyCondition = String.Empty;
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    keyCondition += String.Format(" [{0}] = @{1} AND {2}", column.Name, column.Code, Environment.NewLine);
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
