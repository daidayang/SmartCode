using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

using StoreProcedures.Utils;

namespace SmartCode.Database
{
    public class SQLStoredProc : TemplateBase
    {
        // Constructor.
        public SQLStoredProc()
        {
            this.CreateOutputFile = true;
            this.Description = "Generates base SELECT, INSERT, DELETE, UPDATE sp for a database table";
            this.Name = "SQL base SP";
            this.OutputFolder = @"SQL";
        }

        public override string OutputFileName()
        {
            return Table.Name + "_sp.sql";
        }

        public override void ProduceCode()
        {
            bool HasActive = false;
            bool HasHotelID = false;
            bool HasSortRank = false;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.Name.ToLower() == "hotelid" && (column.Comment == null || column.Comment.ToLower() != "noscript"))
                    HasHotelID = true;

                if (column.Name.ToLower() == "active")
                    HasActive = true;

                if (column.Name.ToLower() == "sortrank")
                    HasSortRank = true;
            }

            int columnCnt = Table.ColumnCount;
            string spName;
            string spPurpose;
            string inputParameters;
            StringBuilder selectStm;
            string alignment;
            int i;
            string keyCondition;

            //WriteLine("	USE [{0}]", Code);
            //WriteLine();
            //WriteLine("	GO ");

            #region SELECT by Hotel

            if (HasHotelID)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                spName = Common.SP_NAME_PREFIX + Table.Code + "_SelByHotel";
                spPurpose = "Get an existing row in table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                WriteLine(" CREATE PROCEDURE " + spName);

                inputParameters = String.Empty;
                if ( HasActive )
                    inputParameters = string.Format( "    @HotelID int,{0}   @ActiveOnly bit", Environment.NewLine );
                else
                    inputParameters = "    @HotelID int";


                WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                WriteLine(" AS");

                selectStm = new StringBuilder("SELECT " + Environment.NewLine);
                foreach (ColumnSchema columm in Table.Columns())
                {
                    selectStm.AppendFormat("    [{0}].[{1}] as {2},{3}", Table.Name, columm.Name, columm.Code, Environment.NewLine);
                }

                i = 0;

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
                alignment = "";

                foreach (ReferenceSchema reference in Table.InReferences)
                {
                    if (reference.Alignment == ReferenceSchema.AlignmentType.Inner) alignment = " INNER JOIN ";
                    else if (reference.Alignment == ReferenceSchema.AlignmentType.Left) alignment = " LEFT OUTER JOIN ";
                    else if (reference.Alignment == ReferenceSchema.AlignmentType.Right) alignment = " RIGHT OUTER JOIN ";

                    selectStm.AppendFormat("    {0} {1} AS T{2} ", alignment + Environment.NewLine, reference.ParentTable.Name, i);

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

                keyCondition = String.Empty;
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    keyCondition += String.Format(" [{0}] = @{1} AND {2}", column.Name, column.Code, Environment.NewLine);
                }
                keyCondition = Common.Substring(keyCondition, (Environment.NewLine.Length + 5));

                WriteLine("	WHERE {0}({0} [{1}].[HotelID]=@HotelID ", Environment.NewLine, Table.Name);
                if ( HasActive )
                    WriteLine("{0}	AND ( [{1}].[Active]=1 OR @ActiveOnly=0 ){0}", Environment.NewLine, Table.Name);
                WriteLine(")");
                if ( HasSortRank )
                    WriteLine(" {0}ORDER BY [{1}].[SortRank]", Environment.NewLine, Table.Name);

                WriteLine();
                WriteLine("	GO ");
                WriteLine();
                WriteLine("-- End Procedure");
            }

            #endregion

            #region DELETE by Hotel

            if (HasHotelID)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                spName = Common.SP_NAME_PREFIX + Table.Code + "_DelByHotel";
                spPurpose = "Delete an existing row in table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                //WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                //WriteLine(" CREATE PROCEDURE " + spName);

                //inputParameters = String.Empty;
                //inputParameters = "    @HotelID int";

                //WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                //WriteLine(" AS");

//                WriteLine(" SET NOCOUNT ON");
                //WriteLine(" DELETE FROM [{0}]", Table.Name);

                //keyCondition = String.Empty;

                //foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                //{
                //    keyCondition += String.Format(" [{0}] = @{1} AND {2}", column.Name, column.Code, Environment.NewLine);
                //}
                //keyCondition = Common.Substring(keyCondition, (Environment.NewLine.Length + 5));

                //WriteLine("	WHERE {0}({0} [{1}].[HotelID]=@HotelID {0})", Environment.NewLine, Table.Name);
                //WriteLine();
                //WriteLine("	GO ");
                //WriteLine();
                //WriteLine("-- End Procedure");
            }

            #endregion

            #region Deactivate by Hotel

            if (HasHotelID && HasActive)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                spName = Common.SP_NAME_PREFIX + Table.Code + "_DeActivateByHotel";
                spPurpose = "Delete an existing row in table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                //WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                //WriteLine(" CREATE PROCEDURE " + spName);

                //inputParameters = String.Empty;
                //inputParameters = "    @HotelID int";

                //WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                //WriteLine(" AS");

//                WriteLine(" SET NOCOUNT ON");
                //WriteLine(" UPDATE [{1}] {0} SET [{1}].[Active]=0", Environment.NewLine, Table.Name);

                //keyCondition = String.Empty;

                //foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                //{
                //    keyCondition += String.Format(" [{0}] = @{1} AND {2}", column.Name, column.Code, Environment.NewLine);
                //}
                //keyCondition = Common.Substring(keyCondition, (Environment.NewLine.Length + 5));

                //WriteLine("	WHERE {0}({0} [{1}].[HotelID]=@HotelID AND [{1}].[Active]=1 {0})", Environment.NewLine, Table.Name);
                //WriteLine();
                //WriteLine("	GO ");
                //WriteLine();
                //WriteLine("-- End Procedure");
            }

            #endregion

            #region SELECT

            if (Table.PrimaryKeyColumns().Count > 0)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                spName = Common.SP_NAME_PREFIX + Table.Code + "_Sel";
                spPurpose = "Get an existing row in table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                WriteLine(" CREATE PROCEDURE " + spName);

                inputParameters = String.Empty;
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    inputParameters += "    " + Common.GetSqlParameterLine(column);
                }

                inputParameters = Common.Substring(inputParameters, Environment.NewLine.Length + 1);

                WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                WriteLine(" AS");

                selectStm = new StringBuilder("SELECT " + Environment.NewLine);
                foreach (ColumnSchema columm in Table.Columns())
                {
                    selectStm.AppendFormat("    [{0}].[{1}] as {2},{3}", Table.Name, columm.Name, columm.Code, Environment.NewLine);
                }

                i = 0;

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
                alignment = "";

                foreach (ReferenceSchema reference in Table.InReferences)
                {
                    if (reference.Alignment == ReferenceSchema.AlignmentType.Inner) alignment = " INNER JOIN ";
                    else if (reference.Alignment == ReferenceSchema.AlignmentType.Left) alignment = " LEFT OUTER JOIN ";
                    else if (reference.Alignment == ReferenceSchema.AlignmentType.Right) alignment = " RIGHT OUTER JOIN ";

                    selectStm.AppendFormat("    {0} {1} AS T{2} ", alignment + Environment.NewLine, reference.ParentTable.Name, i);

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

                keyCondition = String.Empty;
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

            #endregion

            #region DELETE

            if (Table.PrimaryKeyColumns().Count > 0)
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                spName = Common.SP_NAME_PREFIX + Table.Code + "_del";
                spPurpose = "Delete an existing row in table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                //WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                //WriteLine(" CREATE PROCEDURE " + spName);

                //inputParameters = String.Empty;
                //foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                //{
                //    inputParameters += "    " + Common.GetSqlParameterLine(column);
                //}

                //inputParameters = Common.Substring(inputParameters, Environment.NewLine.Length + 1);

                //WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                //WriteLine(" AS");

//                WriteLine(" SET NOCOUNT ON");
                //WriteLine(" DELETE FROM [{0}]", Table.Name);

                //keyCondition = String.Empty;

                //foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                //{
                //    keyCondition += String.Format(" [{0}] = @{1} AND {2}", column.Name, column.Code, Environment.NewLine);
                //}
                //keyCondition = Common.Substring(keyCondition, (Environment.NewLine.Length + 5));

                //WriteLine("	WHERE {0}({0} {1} {0})", Environment.NewLine, keyCondition);
                //WriteLine();
                //WriteLine("	GO ");
                //WriteLine();
                //WriteLine("-- End Procedure");
            }

            #endregion

            #region Deactivate

            if (Table.PrimaryKeyColumns().Count > 0 && HasActive) 
            {
                WriteLine("SET QUOTED_IDENTIFIER ON ");
                WriteLine("GO");
                WriteLine("SET ANSI_NULLS ON ");
                WriteLine("GO");

                spName = Common.SP_NAME_PREFIX + Table.Code + "_DeActivate";
                spPurpose = "Delete an existing row in table " + Table.Name;

                WriteLine("-- Stored Procedure " + spName);
                WriteLine("-- Purpose: " + spPurpose);
                WriteLine("-- Parameters:");

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                //WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                //WriteLine(" CREATE PROCEDURE " + spName);

                //inputParameters = String.Empty;
                //foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                //{
                //    inputParameters += "    " + Common.GetSqlParameterLine(column);
                //}

                //inputParameters = Common.Substring(inputParameters, Environment.NewLine.Length + 1);

                //WriteLine(" ({0} {1} {0})", Environment.NewLine, inputParameters);
                //WriteLine(" AS");

//                WriteLine(" SET NOCOUNT ON");
                //WriteLine(" UPDATE [{1}] {0} SET Active=0", Environment.NewLine, Table.Name);

                //keyCondition = String.Empty;

                //foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                //{
                //    keyCondition += String.Format(" [{0}] = @{1} AND {2}", column.Name, column.Code, Environment.NewLine);
                //}
                //keyCondition = Common.Substring(keyCondition, (Environment.NewLine.Length + 5));

                //WriteLine("	WHERE {0}({0} {1} {0} AND Active=1)", Environment.NewLine, keyCondition);
                //WriteLine();
                //WriteLine("	GO ");
                //WriteLine();
                //WriteLine("-- End Procedure");
            }

            #endregion

            #region INSERT

            WriteLine("SET QUOTED_IDENTIFIER ON ");
            WriteLine("GO");
            WriteLine("SET ANSI_NULLS ON ");
            WriteLine("GO");

            spName = Common.SP_NAME_PREFIX + Table.Code + "_add";
            spPurpose = "Insert one row in table " + Table.Name;

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
                if (!column.IsIdentity)
                {
                    inputParams += "         @" + column.Code + " ,\n";
                }
            }
            inputParams = Common.Substring(inputParams, ((" ,").Length + ("\n").Length));

            WriteLine("     ({1}{0}{1}	)", inputParams, Environment.NewLine);
            WriteLine();

            if (Table.PrimaryKeyColumns().Count == 1)
            {
                if (Table.PrimaryKeyColumns()[0].IsIdentity)
                {
                    WriteLine("RETURN SCOPE_IDENTITY()");
                }
            }

            WriteLine("     GO ");
            WriteLine();
            WriteLine("-- End Procedure");

            #endregion

            #region UPDATE

//            IList<ColumnSchema> keyColumns = Table.PrimaryKeyColumns(); 
            if (Table.PrimaryKeyColumns().Count > 0)
            {
                spName = Common.SP_NAME_PREFIX + Table.Code + "_edt";
                spPurpose = "Update an existing row in table " + Table.Name + " by its primary key.";

                WriteLine("-- Stored Procedure " + spName);
                WriteLine(@"-- Purpose: " + spPurpose);

                // First, delete the stored procedure, if it exists
                WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

                WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
                WriteLine("	CREATE PROCEDURE " + spName);

                inputParams = "";

                foreach (ColumnSchema column in Table.Columns())
                {
                    inputParams += "		@" + column.Code + " " + Common.GetFieldTypeAsTSQLType(column.OriginalSQLType, column.Length, column.Precision, column.Scale.ToString()) + ",\n";
                }

                inputParams = Common.Substring(inputParams, (",\n").Length);
                WriteLine("	(\n" + inputParams + "\n	)");
                WriteLine("	AS");
//                WriteLine("	SET NOCOUNT ON");
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

                setSection = Common.Substring(setSection, (",\n").Length);

                WriteLine(setSection);

                WriteLine("	WHERE(");
                string whereSection = "";
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    whereSection += "		[" + column.Name + "] =  @" + column.Code + " AND \n";
                }
                whereSection = Common.Substring(whereSection, " AND \n".Length);

                WriteLine(whereSection + "\n	)");

                WriteLine();
                WriteLine("	GO ");
                WriteLine("-- End Procedure");
            }
            #endregion

        }
    }
}
