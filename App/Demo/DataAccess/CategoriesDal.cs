/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace DataAccess{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections;
    using Common;

    ///<summary>
    ///The Class Provides methods for common database table operations. 
    ///</summary>
    ///<remarks>
    ///Changes to this file may cause incorrect behavior and will be lost if  the code is regenerated.
    ///Update the <see cref="CategoriesDal"/> class if you need to add or change some functionality
    ///</remarks>
    public class CategoriesDal:BaseDAL  {

        private const string SP_SELECT_BY_PRIMARY_KEY="usp_Categories_SelectByPrimaryKey";
        private const string SP_SELECT_ROWS_BY_WHERE="usp_Categories_SelectRowsByWhere";
        private const string SP_INSERT="usp_Categories_Insert";
        private const string SP_UPDATE="usp_Categories_Update";
        private const string SP_DELETE="usp_Categories_DeleteByPrimaryKey";

        #region Constructor

        ///<summary>
        ///Initializes a new instance of the <see cref="CategoriesDal"/> 
        ///class with the specified <see cref="BaseDAL"/>.
        ///</summary>
        public CategoriesDal(): base()
        {

        }

        #endregion

        #region Persist Methods
        ///<summary>
        ///Inserts, updates or deletes rows in a DataSet.
        ///</summary>
        private static SqlCommand cmdGetIdentity;
		public CategoriesDS Persist(CategoriesDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    conn.Open ();
		    SqlTransaction tx = conn.BeginTransaction ();
		    try
		    {
		        CategoriesDS.CategoriesDataTable tbl = updates.Categories;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = CreateCategoriesDataAdapter(conn,tx);

                cmdGetIdentity = new SqlCommand("SELECT @@IDENTITY", conn, tx);
		        dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);

		        //First Add Categories
		        dataAdapter.Update(tbl.Select("", "", DataViewRowState.Added));
		        //Next Modified
		        dataAdapter.Update(tbl.Select("", "", DataViewRowState.ModifiedCurrent));
		        //Next Deleted
		        dataAdapter.Update(tbl.Select("", "", DataViewRowState.Deleted));
		        tx.Commit ();
		        return updates;
		    }
		    catch (Exception ex)
		    {
		        tx.Rollback ();
		        throw new Exception( ex.Message);
		    }
		    finally
		    {
		        if (conn != null) 
		            conn.Dispose();
		    }
		}

		public CategoriesDS InsertCategories(CategoriesDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        CategoriesDS.CategoriesDataTable tbl = updates.Categories;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.InsertCommand = CreateInsertCategoriesViaSPCommand(conn);

		        //Roll Back the changes if some one error have
		        dataAdapter.ContinueUpdateOnError = false;

		        cmdGetIdentity = new SqlCommand("SELECT @@IDENTITY",conn);
		        dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);

		        dataAdapter.Update(tbl.Select("", "", DataViewRowState.Added));
		        return updates;
		    }
		    catch (Exception ex)
		    {
		        throw new Exception( ex.Message , ex );
		    }
		    finally
		    {
		        if (conn != null)
		            conn.Dispose();
		    }
		}

		public CategoriesDS UpdateCategories(CategoriesDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        CategoriesDS.CategoriesDataTable tbl = updates.Categories;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.UpdateCommand = CreateUpdateCategoriesViaSPCommand(conn);

		        //Roll Back the changes if some one error have
		        dataAdapter.ContinueUpdateOnError = false;

		        dataAdapter.Update(tbl.Select("", "", DataViewRowState.ModifiedCurrent));
		        return updates;
		    }
		    catch (Exception ex)
		    {
		        throw new Exception(ex.Message );
		    }
		    finally
		    {
		        if (conn != null) 
		            conn.Dispose();
		    }
		}

		public void DeleteCategories(CategoriesDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        CategoriesDS.CategoriesDataTable tbl = updates.Categories;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.DeleteCommand = CreateDeleteCategoriesViaSPCommand(conn);

		        //Roll Back the changes if some one error have
		        dataAdapter.ContinueUpdateOnError = false;

		        dataAdapter.Update(tbl.Select("", "", DataViewRowState.Deleted));
		    }
		    catch (Exception ex)
		    {
		        throw new Exception(ex.Message );
		    }
		    finally
		    {
		        if (conn != null) 
		            conn.Dispose();
		    }
		}

		///<summary>
		///
		///</summary>
		///<param name="sender"></param>
		///<param name="e"></param>
		private static void HandleRowUpdated(object sender, SqlRowUpdatedEventArgs e)
		{
		    if ((e.Status == UpdateStatus.Continue) && (e.StatementType == StatementType.Insert))
				{
					e.Row["CategoryID"] = Convert.ToInt32 (cmdGetIdentity.ExecuteScalar());
					e.Row.AcceptChanges();
				}
			}
#endregion

#region Get Methods

		///<summary>
		///Looks up a Categories based on its primary-key:System.Int32 categoryid
		///</summary>
		public CategoriesDS Populate(System.Int32 categoryid) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_BY_PRIMARY_KEY, conn);
		        CategoriesDS dataSet = new CategoriesDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@CategoryID",System.Data.SqlDbType.Int,categoryid);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Categories.TableName );
		        dataSet.AcceptChanges();
		        return dataSet;
		    }
		    catch (Exception ex)
		    {
		        throw new Exception(ex.Message );
		    }
		    finally
		    {
		        if (conn != null) 
		            conn.Dispose();
		    }
		}

		///<summary>
		///Gets an Strongly  Typed <see cref="CategoriesDS"/> DataSet  objects that  match the search condition.
		///</summary>
		///<param name="sqlWhere">The SQL search condition. </param>
		///<returns>An Strongly  Typed <see cref="CategoriesDS"/> DataSet.</returns>
		public CategoriesDS PopulateList(string sqlWhere) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_WHERE, conn);
		        CategoriesDS dataSet = new CategoriesDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@SqlWhere",System.Data.SqlDbType.VarChar,sqlWhere);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Categories.TableName );
		        dataSet.AcceptChanges();
		        return dataSet;
		    }
		    catch (Exception ex)
		    {
		        throw new Exception(ex.Message );
		    }
		    finally
		    {
		        if (conn != null) 
		            conn.Dispose();
		    }
		}

		#endregion

		#region SqlDataAdapter and SqlCommand

		///<summary>
		///
		///</summary>
		///<param name="conn"></param>
		///<returns>SqlDataAdapter</returns>
		private SqlDataAdapter CreateCategoriesDataAdapter(SqlConnection conn)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertCategoriesViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateCategoriesViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteCategoriesViaSPCommand(conn);
		    return dataAdapter;
		}

		///<summary>
		///</summary>
		///<param name="conn"></param>
		///<param name="tx"></param>
		///<returns>SqlDataAdapter</returns>
		private SqlDataAdapter CreateCategoriesDataAdapter(SqlConnection conn,SqlTransaction tx)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertCategoriesViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateCategoriesViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteCategoriesViaSPCommand(conn);
		    dataAdapter.InsertCommand.Transaction = tx;
		    dataAdapter.UpdateCommand.Transaction = tx;
		    dataAdapter.DeleteCommand.Transaction = tx;
		    return dataAdapter;
		}

		private static SqlCommand CreateInsertCategoriesViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_INSERT, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("CategoryName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Description",System.Data.SqlDbType.NText));
		    pc.Add(CreateParameter("Picture",System.Data.SqlDbType.Image));
		    return cmd;
		} 

		private static SqlCommand CreateUpdateCategoriesViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_UPDATE, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("CategoryID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("CategoryName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Description",System.Data.SqlDbType.NText));
		    pc.Add(CreateParameter("Picture",System.Data.SqlDbType.Image));
		    return cmd;
		} 
		private static SqlCommand CreateDeleteCategoriesViaSPCommand(SqlConnection conn)
			{
		    SqlCommand cmd = new SqlCommand(SP_DELETE,conn);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection pc = cmd.Parameters;
				pc.Add(CreateParameter("CategoryID",System.Data.SqlDbType.Int));

				return cmd;
		} 
		#endregion
        }
    }
