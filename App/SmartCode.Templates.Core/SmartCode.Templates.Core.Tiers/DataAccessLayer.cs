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
using SmartCode.Model;
using SmartCode.Templates.Core.SQLServer.Utils;

namespace SmartCode.Templates.Core.Tiers
{
    public class DataAccess : TemplateBase
    {
        public DataAccess()
        {
            CreateOutputFile = true;
            Description = "The data access layer";
            Name = "Data Access Layer";
            OutputFolder = @"DAL";
        }

        public override string OutputFileName()
        {
            return base.Entity.Name + "Dal.cs";
        }

        public override void ProduceCode()
        {
            if (! Table.IsTable)
            {
                throw new Exception ("' The used for this class is only available for tables, not for views.");
            }

            WriteLine(@"namespace DataAccess{");
            WriteLine(@"    using System;");
            WriteLine(@"    using System.Data;");
            WriteLine(@"    using System.Data.SqlClient;");
            WriteLine(@"    using System.Collections;");
            WriteLine(@"    using Common;");
            WriteLine();

            WriteLine(@"    ///<summary>");
            WriteLine(@"    ///The Class Provides methods for common database table operations. ");
            WriteLine(@"    ///</summary>");
            WriteLine(@"    ///<remarks>");
            WriteLine(@"    ///Changes to this file may cause incorrect behavior and will be lost if  the code is regenerated.");
            WriteLine(@"    ///Update the <see cref=""" + Table.Code + @"Dal""/> class if you need to add or change some functionality");
            WriteLine(@"    ///</remarks>");
            WriteLine(@"    public class " + Table.Code + @"Dal:BaseDAL  {");
            W();
            WriteLine(@"        private const string SP_SELECT_BY_PRIMARY_KEY=""" + Common.SP_NAME_PREFIX + Table.Code + @"_SelectByPrimaryKey"";");
            WriteLine(@"        private const string SP_SELECT_ROWS_BY_WHERE=""" + Common.SP_NAME_PREFIX + Table.Code + @"_SelectRowsByWhere"";");
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.CustomProperties.SearchBy)
                {
                    WriteLine(@"        private const string SP_SELECT_ROWS_BY_" + column.Code.ToUpper() + @"=""" + Common.SP_NAME_PREFIX + Table.Code + "_" + column.Code + @"_SelectRowsByWhere"";");
                }
            }
            WriteLine(@"        private const string SP_INSERT=""" + Common.SP_NAME_PREFIX + Table.Code + @"_Insert"";");
            WriteLine(@"        private const string SP_UPDATE=""" + Common.SP_NAME_PREFIX + Table.Code + @"_Update"";");
            WriteLine(@"        private const string SP_DELETE=""" + Common.SP_NAME_PREFIX + Table.Code + @"_DeleteByPrimaryKey"";");
            W();
            WriteLine(@"        #region Constructor");
            W();
            WriteLine(@"        ///<summary>");
            WriteLine(@"        ///Initializes a new instance of the <see cref=""" + Table.Code + @"Dal""/> ");
            WriteLine(@"        ///class with the specified <see cref=""BaseDAL""/>.");
            WriteLine(@"        ///</summary>");
            WriteLine(@"        public " + Table.Code + @"Dal(): base()");
            WriteLine(@"        {");
            W();
            WriteLine(@"        }");
            W();
            WriteLine(@"        #endregion");
            W();
            WriteLine(@"        #region Persist Methods");
            WriteLine(@"        ///<summary>");
            WriteLine(@"        ///Inserts, updates or deletes rows in a DataSet.");
            WriteLine(@"        ///</summary>");

            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsIdentity)
                {
                    WriteLine(@"        private static SqlCommand cmdGetIdentity;");
                }
            }

            WriteLine(@"		public " + Table.Code + @"DS Persist(" + Table.Code + @"DS updates) {");

            WriteLine(@"		    SqlConnection conn = new SqlConnection(ConnectionString);");
            WriteLine(@"		    conn.Open ();");
            WriteLine(@"		    SqlTransaction tx = conn.BeginTransaction ();");
            WriteLine(@"		    try");
            WriteLine(@"		    {");
            WriteLine(@"		        " + Table.Code + @"DS." + Table.Code + @"DataTable tbl = updates." + Table.Code + @";");
            WriteLine(@"		        //Create the adapter initial");
            WriteLine(@"		        SqlDataAdapter dataAdapter = Create" + Table.Code + @"DataAdapter(conn,tx);");
            WriteLine();

            //Its unic column Identity
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsIdentity)
                {
                    WriteLine(@"		        cmdGetIdentity = new SqlCommand(""SELECT @@IDENTITY"",conn, tx);");
                    WriteLine(@"		        dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);");
                }
            }


            W();
            WriteLine(@"		        //First Add " + Table.Code + @"");
            WriteLine(@"		        dataAdapter.Update(tbl.Select("""", """", DataViewRowState.Added));");
            WriteLine(@"		        //Next Modified");
            WriteLine(@"		        dataAdapter.Update(tbl.Select("""", """", DataViewRowState.ModifiedCurrent));");
            WriteLine(@"		        //Next Deleted");
            WriteLine(@"		        dataAdapter.Update(tbl.Select("""", """", DataViewRowState.Deleted));");
            WriteLine(@"		        tx.Commit ();");
            WriteLine(@"		        return updates;");

            WriteLine(@"		    }");
            WriteLine(@"		    catch (Exception ex)");
            WriteLine(@"		    {");
            WriteLine(@"		        tx.Rollback ();");
            WriteLine(@"		        throw new Exception( ex.Message);");
            WriteLine(@"		    }");
            WriteLine(@"		    finally");
            WriteLine(@"		    {");
            WriteLine(@"		        if (conn != null) ");
            WriteLine(@"		            conn.Dispose();");
            WriteLine(@"		    }");
            WriteLine(@"		}");

            W();

            WriteLine(@"		public " + Table.Code + @"DS Insert" + Table.Code + @"(" + Table.Code + @"DS updates) {");

            WriteLine(@"		    SqlConnection conn = new SqlConnection(ConnectionString);");
            WriteLine(@"		    try");
            WriteLine(@"		    {");
            WriteLine(@"		        " + Table.Code + @"DS." + Table.Code + @"DataTable tbl = updates." + Table.Code + @";");
            WriteLine(@"		        //Create the adapter initial");
            WriteLine(@"		        SqlDataAdapter dataAdapter = new SqlDataAdapter();");
            WriteLine(@"		        dataAdapter.InsertCommand = CreateInsert" + Table.Code + @"ViaSPCommand(conn);");
            W();
            WriteLine(@"		        //Roll Back the changes if some one error have");
            WriteLine(@"		        dataAdapter.ContinueUpdateOnError = false;");
            W();
            //Its unic column Identity
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsIdentity)
                {
                    WriteLine(@"		        cmdGetIdentity = new SqlCommand(""SELECT @@IDENTITY"",conn);");
                    WriteLine(@"		        dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);");
                }
            }
            W();
            WriteLine(@"		        dataAdapter.Update(tbl.Select("""", """", DataViewRowState.Added));");
            WriteLine(@"		        return updates;");
            WriteLine(@"		    }");
            WriteLine(@"		    catch (Exception ex)");
            WriteLine(@"		    {");
            WriteLine(@"		        throw new Exception( ex.Message , ex );");
            WriteLine(@"		    }");
            WriteLine(@"		    finally");
            WriteLine(@"		    {");
            WriteLine(@"		        if (conn != null)");
            WriteLine(@"		            conn.Dispose();");
            WriteLine(@"		    }");
            WriteLine(@"		}");

            W();

            WriteLine(@"		public " + Table.Code + @"DS Update" + Table.Code + @"(" + Table.Code + @"DS updates) {");

            WriteLine(@"		    SqlConnection conn = new SqlConnection(ConnectionString);");
            WriteLine(@"		    try");
            WriteLine(@"		    {");
            WriteLine(@"		        " + Table.Code + @"DS." + Table.Code + @"DataTable tbl = updates." + Table.Code + @";");
            WriteLine(@"		        //Create the adapter initial");
            WriteLine(@"		        SqlDataAdapter dataAdapter = new SqlDataAdapter();");
            WriteLine(@"		        dataAdapter.UpdateCommand = CreateUpdate" + Table.Code + @"ViaSPCommand(conn);");
            W();
            WriteLine(@"		        //Roll Back the changes if some one error have");
            WriteLine(@"		        dataAdapter.ContinueUpdateOnError = false;");
            W();
            WriteLine(@"		        dataAdapter.Update(tbl.Select("""", """", DataViewRowState.ModifiedCurrent));");
            WriteLine(@"		        return updates;");

            WriteLine(@"		    }");
            WriteLine(@"		    catch (Exception ex)");
            WriteLine(@"		    {");
            WriteLine(@"		        throw new Exception(ex.Message );");
            WriteLine(@"		    }");
            WriteLine(@"		    finally");
            WriteLine(@"		    {");
            WriteLine(@"		        if (conn != null) ");
            WriteLine(@"		            conn.Dispose();");
            WriteLine(@"		    }");
            WriteLine(@"		}");

            W();
            WriteLine(@"		public void Delete" + Table.Code + @"(" + Table.Code + @"DS updates) {");

            WriteLine(@"		    SqlConnection conn = new SqlConnection(ConnectionString);");
            WriteLine(@"		    try");
            WriteLine(@"		    {");
            WriteLine(@"		        " + Table.Code + @"DS." + Table.Code + @"DataTable tbl = updates." + Table.Code + @";");
            WriteLine(@"		        //Create the adapter initial");
            WriteLine(@"		        SqlDataAdapter dataAdapter = new SqlDataAdapter();");
            WriteLine(@"		        dataAdapter.DeleteCommand = CreateDelete" + Table.Code + @"ViaSPCommand(conn);");
            W();
            WriteLine(@"		        //Roll Back the changes if some one error have");
            WriteLine(@"		        dataAdapter.ContinueUpdateOnError = false;");
            W();
            WriteLine(@"		        dataAdapter.Update(tbl.Select("""", """", DataViewRowState.Deleted));");
            WriteLine(@"		    }");
            WriteLine(@"		    catch (Exception ex)");
            WriteLine(@"		    {");
            WriteLine(@"		        throw new Exception(ex.Message );");
            WriteLine(@"		    }");
            WriteLine(@"		    finally");
            WriteLine(@"		    {");
            WriteLine(@"		        if (conn != null) ");
            WriteLine(@"		            conn.Dispose();");
            WriteLine(@"		    }");
            WriteLine(@"		}");

            W();

            //Its unic column Identity
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsIdentity)
                {
                    WriteLine(@"		///<summary>");
                    WriteLine(@"		///");
                    WriteLine(@"		///</summary>");
                    WriteLine(@"		///<param name=""sender""></param>");
                    WriteLine(@"		///<param name=""e""></param>");
                    WriteLine(@"		private static void HandleRowUpdated(object sender, SqlRowUpdatedEventArgs e)");
                    WriteLine(@"		{");
                    WriteLine(@"		    if ((e.Status == UpdateStatus.Continue) && (e.StatementType == StatementType.Insert))");
                    WriteLine(@"				{");
                    WriteLine(@"					e.Row[""" + column.Name + @"""] = Convert.ToInt32 (cmdGetIdentity.ExecuteScalar());");
                    WriteLine(@"					e.Row.AcceptChanges();");
                    WriteLine(@"				}");
                    WriteLine(@"			}");
                }
            }

            WriteLine(@"        #endregion");
            WriteLine();
            WriteLine(@"        #region Get Methods");
            WriteLine();

            string primaryKeyArguments = String.Empty;
            string primaryKeyArgumentsWOTypes = String.Empty;
            bool isFirst = true;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsPrimaryKey)
                {
                    if (! isFirst)
                    {
                        primaryKeyArguments += ", ";
                        primaryKeyArgumentsWOTypes += ", ";
                    }
                    primaryKeyArguments += column.NetDataType + " " + column.Code.ToLower() ;
                    primaryKeyArgumentsWOTypes += column.Code.ToLower() ;
                    isFirst = false;
                }
            }

            WriteLine(@"		///<summary>");
            WriteLine(@"		///Looks up a " + Table.Code + @" based on its primary-key:" + primaryKeyArguments);
            WriteLine(@"		///</summary>");
            WriteLine(@"		public " + Table.Code + @"DS Populate(" + primaryKeyArguments + ") {");
            WriteLine(@"		    SqlConnection conn = new SqlConnection(ConnectionString);");
            WriteLine(@"		    try");
            WriteLine(@"		    {");
            WriteLine(@"		        conn.Open();");
            WriteLine(@"		        SqlCommand cmd = new SqlCommand(SP_SELECT_BY_PRIMARY_KEY, conn);");
            WriteLine(@"		        " + Table.Code + @"DS dataSet = new " + Table.Code + @"DS();");
            WriteLine(@"		        cmd.CommandType = CommandType.StoredProcedure;");

            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsPrimaryKey)
                {
                    WriteLine(@"		        AddParameter(cmd,  ""@" + column.Code + @"""," + GetSqlDbType(column) + "," + column.Code.ToLower() + @");");
                }
            }

            WriteLine(@"		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );");
            WriteLine(@"		        adapter.Fill(dataSet,dataSet." + Table.Code + @".TableName );");
            WriteLine(@"		        dataSet.AcceptChanges();");
            WriteLine(@"		        return dataSet;");
            WriteLine(@"		    }");
            WriteLine(@"		    catch (Exception ex)");
            WriteLine(@"		    {");
            WriteLine(@"		        throw new Exception(ex.Message );");
            WriteLine(@"		    }");
            WriteLine(@"		    finally");
            WriteLine(@"		    {");
            WriteLine(@"		        if (conn != null) ");
            WriteLine(@"		            conn.Dispose();");
            WriteLine(@"		    }");
            WriteLine(@"		}");
            WriteLine();


            WriteLine(@"		///<summary>");
            WriteLine(@"		///Gets an Strongly  Typed <see cref=""" + Table.Code + @"DS""/> DataSet  objects that  match the search condition.");
            WriteLine(@"		///</summary>");
            WriteLine(@"		///<param name=""sqlWhere"">The SQL search condition. </param>");
            WriteLine(@"		///<returns>An Strongly  Typed <see cref=""" + Table.Code + @"DS""/> DataSet.</returns>");
            WriteLine(@"		public " + Table.Code + @"DS PopulateList(string sqlWhere) {");
            WriteLine(@"		    SqlConnection conn = new SqlConnection(ConnectionString);");
            WriteLine(@"		    try");
            WriteLine(@"		    {");
            WriteLine(@"		        conn.Open();");
            WriteLine(@"		        SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_WHERE, conn);");
            WriteLine(@"		        " + Table.Code + @"DS dataSet = new " + Table.Code + @"DS();");
            WriteLine(@"		        cmd.CommandType = CommandType.StoredProcedure;");
            WriteLine(@"		        AddParameter(cmd,  ""@SqlWhere"",System.Data.SqlDbType.VarChar,sqlWhere);");
            WriteLine(@"		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );");
            WriteLine(@"		        adapter.Fill(dataSet,dataSet." + Table.Code + @".TableName );");
            WriteLine(@"		        dataSet.AcceptChanges();");
            WriteLine(@"		        return dataSet;");
            WriteLine(@"		    }");
            WriteLine(@"		    catch (Exception ex)");
            WriteLine(@"		    {");
            WriteLine(@"		        throw new Exception(ex.Message );");
            WriteLine(@"		    }");
            WriteLine(@"		    finally");
            WriteLine(@"		    {");
            WriteLine(@"		        if (conn != null) ");
            WriteLine(@"		            conn.Dispose();");
            WriteLine(@"		    }");
            WriteLine(@"		}");
            WriteLine();

            foreach (ColumnSchema column in Table.Columns())
            {
                if (! column.CustomProperties.SearchBy )
                    continue;

                WriteLine();

                WriteLine(@"		///<summary>");
                WriteLine(@"		///Gets an Strongly  Typed <see cref=""" + Table.Code + @"DS""/> DataSet  objects that  match the search condition.");
                WriteLine(@"		///</summary>");
                WriteLine(@"		///<param name=""" + column.Code + @""">The value for " + column.Code + @" Field. </param>");
                WriteLine(@"		///<returns>An Strongly  Typed <see cref=""" + Table.Code + @"DS""/> DataSet.</returns>");

                WriteLine(@"		public " + Table.Code + @"DS PopulateBy" + column.Code + @"(" + column.NetDataType + " " + column.Code + ") {");
                WriteLine(@"		    SqlConnection conn = new SqlConnection(ConnectionString);");
                WriteLine(@"		    try");
                WriteLine(@"		    {");
                WriteLine(@"		        conn.Open();");
                WriteLine(@"		        SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_" + column.Code.ToUpper() + @", conn);");
                WriteLine(@"		        " + Table.Code + @"DS dataSet = new " + Table.Code + @"DS();");
                WriteLine(@"		        cmd.CommandType = CommandType.StoredProcedure;");
                WriteLine(@"		        AddParameter(cmd,  ""@" + column.Code + @"""," + GetSqlDbType(column) + "," + column.Code + @");");
                WriteLine(@"		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );");
                WriteLine(@"		        adapter.Fill(dataSet,dataSet." + Table.Code + @".TableName );");
                WriteLine(@"		        dataSet.AcceptChanges();");
                WriteLine(@"		        return dataSet;");
                WriteLine(@"		    }");
                WriteLine(@"		    catch (Exception ex)");
                WriteLine(@"		    {");
                WriteLine(@"		        throw new Exception(ex.Message );");
                WriteLine(@"		    }");
                WriteLine(@"		    finally");
                WriteLine(@"		    {");
                WriteLine(@"		        if (conn != null) ");
                WriteLine(@"		            conn.Dispose();");
                WriteLine(@"		    }");
                WriteLine(@"		}");
                WriteLine();
            }

            WriteLine(@"		#endregion");

            WriteLine();


            WriteLine(@"		#region SqlDataAdapter and SqlCommand");
            W();
            WriteLine(@"		///<summary>");
            WriteLine(@"		///");
            WriteLine(@"		///</summary>");
            WriteLine(@"		///<param name=""conn""></param>");
            WriteLine(@"		///<returns>SqlDataAdapter</returns>");
            WriteLine(@"		private SqlDataAdapter Create" + Table.Code + @"DataAdapter(SqlConnection conn)");
            WriteLine(@"		{");
            WriteLine(@"		    SqlDataAdapter dataAdapter = new SqlDataAdapter();");
            WriteLine(@"		    dataAdapter.InsertCommand = CreateInsert" + Table.Code + @"ViaSPCommand(conn);");
            WriteLine(@"		    dataAdapter.UpdateCommand = CreateUpdate" + Table.Code + @"ViaSPCommand(conn);");
            WriteLine(@"		    dataAdapter.DeleteCommand = CreateDelete" + Table.Code + @"ViaSPCommand(conn);");
            WriteLine(@"		    return dataAdapter;");
            WriteLine(@"		}");
            W();
            WriteLine(@"		///<summary>");
            WriteLine(@"		///</summary>");
            WriteLine(@"		///<param name=""conn""></param>");
            WriteLine(@"		///<param name=""tx""></param>");
            WriteLine(@"		///<returns>SqlDataAdapter</returns>");
            WriteLine(@"		private SqlDataAdapter Create" + Table.Code + @"DataAdapter(SqlConnection conn,SqlTransaction tx)");
            WriteLine(@"		{");
            WriteLine(@"		    SqlDataAdapter dataAdapter = new SqlDataAdapter();");
            WriteLine(@"		    dataAdapter.InsertCommand = CreateInsert" + Table.Code + @"ViaSPCommand(conn);");
            WriteLine(@"		    dataAdapter.UpdateCommand = CreateUpdate" + Table.Code + @"ViaSPCommand(conn);");
            WriteLine(@"		    dataAdapter.DeleteCommand = CreateDelete" + Table.Code + @"ViaSPCommand(conn);");

            WriteLine(@"		    dataAdapter.InsertCommand.Transaction = tx;");
            WriteLine(@"		    dataAdapter.UpdateCommand.Transaction = tx;");
            WriteLine(@"		    dataAdapter.DeleteCommand.Transaction = tx;");

            WriteLine(@"		    return dataAdapter;");
            WriteLine(@"		}");
            W();


            WriteLine(@"		private static SqlCommand CreateInsert" + Table.Code + @"ViaSPCommand(SqlConnection conn)");
            WriteLine(@"		{");
            WriteLine(@"		    SqlCommand cmd = new SqlCommand(SP_INSERT, conn);");
            WriteLine(@"		    cmd.CommandType = CommandType.StoredProcedure;");
            W();
            WriteLine(@"		    SqlParameterCollection pc = cmd.Parameters;");

            foreach (ColumnSchema column in Table.Columns())
            {
                if (!column.IsIdentity)
                {
                    WriteLine(@"		    pc.Add(CreateParameter(""" + column.Code + @"""," + GetSqlDbType(column) + "));");
                }
            }
            WriteLine(@"		    return cmd;");
            WriteLine(@"		} ");

            W();

            WriteLine(@"		private static SqlCommand CreateUpdate" + Table.Code + @"ViaSPCommand(SqlConnection conn)");
            WriteLine(@"		{");
            WriteLine(@"		    SqlCommand cmd = new SqlCommand(SP_UPDATE, conn);");
            WriteLine(@"		    cmd.CommandType = CommandType.StoredProcedure;");
            W();
            WriteLine(@"		    SqlParameterCollection pc = cmd.Parameters;");
            foreach (ColumnSchema column in Table.Columns())
            {
                WriteLine(@"		    pc.Add(CreateParameter(""" + column.Code + @"""," + GetSqlDbType(column) + "));");
            }
            WriteLine(@"		    return cmd;");
            WriteLine(@"		} ");

            WriteLine(@"		private static SqlCommand CreateDelete" + Table.Code + @"ViaSPCommand(SqlConnection conn)");
            WriteLine(@"			{");
            WriteLine(@"		    SqlCommand cmd = new SqlCommand(SP_DELETE,conn);");
            WriteLine(@"				cmd.CommandType = CommandType.StoredProcedure;");
            W();
            WriteLine(@"				SqlParameterCollection pc = cmd.Parameters;");

            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsPrimaryKey)
                {
                    WriteLine(@"				pc.Add(CreateParameter(""" + column.Code + @"""," + GetSqlDbType(column) + "));");
                }
            }
            W();
            WriteLine(@"				return cmd;");
            WriteLine(@"		} ");


            WriteLine(@"		#endregion");

            WriteLine(@"        }");
            WriteLine(@"    }"); 
           
        }

        private string GetSqlDbType(ColumnSchema column)
        {
            switch (column.OriginalSQLType)
            {
                case "bigint":
                    return "System.Data.SqlDbType.BigInt";
                    
                case "binary":
                    return "System.Data.SqlDbType.Binary";
                    
                case "bit":
                    return "System.Data.SqlDbType.Bit";
                    
                case "char":
                    return "System.Data.SqlDbType.Char";
                    
                case "datetime":
                    return "System.Data.SqlDbType.DateTime";
                    
                case "decimal":
                    return "System.Data.SqlDbType.Decimal";
                    
                case "float":
                    return "System.Data.SqlDbType.Float";
                    
                case "image":
                    return "System.Data.SqlDbType.Image";
                    
                case "int":
                    return "System.Data.SqlDbType.Int";
                    
                case "int identity":
                    return "System.Data.SqlDbType.Int";
                    
                case "money":
                    return "System.Data.SqlDbType.Money";
                    
                case "nchar":
                    return "System.Data.SqlDbType.NChar";
                    
                case "ntext":
                    return "System.Data.SqlDbType.NText";
                    
                case "numeric":
                    return "System.Data.SqlDbType.Decimal";
                    
                case "nvarchar":
                    return "System.Data.SqlDbType.NVarChar";
                    
                case "nvarwchar":
                    return "System.Data.SqlDbType.NVarChar";
                    
                case "real":
                    return "System.Data.SqlDbType.Real";
                    
                case "smalldatetime":
                    return "System.Data.SqlDbType.SmallDateTime";
                    
                case "smallint":
                    return "System.Data.SqlDbType.SmallInt";
                    
                case "smallmoney":
                    return "System.Data.SqlDbType.SmallMoney";
                    
                case "text":
                    return "System.Data.SqlDbType.Text";
                    
                case "timestamp":
                    return "System.Data.SqlDbType.Timestamp";
                    
                case "tinyint":
                    return "System.Data.SqlDbType.TinyInt";
                    
                case "uniqueidentifier":
                    return "System.Data.SqlDbType.UniqueIdentifier";
                    
                case "varbinary":
                    return "System.Data.SqlDbType.VarBinary";
                    
                case "varchar":
                    return "System.Data.SqlDbType.VarChar";

                default :
                    return "System.Data.SqlDbType.VarChar";
                    
            }
        }


    }
}
