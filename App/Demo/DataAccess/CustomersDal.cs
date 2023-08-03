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
    ///Update the <see cref="CustomersDal"/> class if you need to add or change some functionality
    ///</remarks>
    public class CustomersDal:BaseDAL  {

        private const string SP_SELECT_BY_PRIMARY_KEY="usp_Customers_SelectByPrimaryKey";
        private const string SP_SELECT_ROWS_BY_WHERE="usp_Customers_SelectRowsByWhere";
        private const string SP_INSERT="usp_Customers_Insert";
        private const string SP_UPDATE="usp_Customers_Update";
        private const string SP_DELETE="usp_Customers_DeleteByPrimaryKey";

        #region Constructor

        ///<summary>
        ///Initializes a new instance of the <see cref="CustomersDal"/> 
        ///class with the specified <see cref="BaseDAL"/>.
        ///</summary>
        public CustomersDal(): base()
        {

        }

        #endregion

        #region Persist Methods
        ///<summary>
        ///Inserts, updates or deletes rows in a DataSet.
        ///</summary>
		public CustomersDS Persist(CustomersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    conn.Open ();
		    SqlTransaction tx = conn.BeginTransaction ();
		    try
		    {
		        CustomersDS.CustomersDataTable tbl = updates.Customers;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = CreateCustomersDataAdapter(conn,tx);


		        //First Add Customers
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

		public CustomersDS InsertCustomers(CustomersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        CustomersDS.CustomersDataTable tbl = updates.Customers;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.InsertCommand = CreateInsertCustomersViaSPCommand(conn);

		        //Roll Back the changes if some one error have
		        dataAdapter.ContinueUpdateOnError = false;


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

		public CustomersDS UpdateCustomers(CustomersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        CustomersDS.CustomersDataTable tbl = updates.Customers;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.UpdateCommand = CreateUpdateCustomersViaSPCommand(conn);

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

		public void DeleteCustomers(CustomersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        CustomersDS.CustomersDataTable tbl = updates.Customers;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.DeleteCommand = CreateDeleteCustomersViaSPCommand(conn);

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

#endregion

#region Get Methods

		///<summary>
		///Looks up a Customers based on its primary-key:System.String customerid
		///</summary>
		public CustomersDS Populate(System.String customerid) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_BY_PRIMARY_KEY, conn);
		        CustomersDS dataSet = new CustomersDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@CustomerID",System.Data.SqlDbType.NChar,customerid);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Customers.TableName );
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
		///Gets an Strongly  Typed <see cref="CustomersDS"/> DataSet  objects that  match the search condition.
		///</summary>
		///<param name="sqlWhere">The SQL search condition. </param>
		///<returns>An Strongly  Typed <see cref="CustomersDS"/> DataSet.</returns>
		public CustomersDS PopulateList(string sqlWhere) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_WHERE, conn);
		        CustomersDS dataSet = new CustomersDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@SqlWhere",System.Data.SqlDbType.VarChar,sqlWhere);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Customers.TableName );
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
		private SqlDataAdapter CreateCustomersDataAdapter(SqlConnection conn)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertCustomersViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateCustomersViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteCustomersViaSPCommand(conn);
		    return dataAdapter;
		}

		///<summary>
		///</summary>
		///<param name="conn"></param>
		///<param name="tx"></param>
		///<returns>SqlDataAdapter</returns>
		private SqlDataAdapter CreateCustomersDataAdapter(SqlConnection conn,SqlTransaction tx)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertCustomersViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateCustomersViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteCustomersViaSPCommand(conn);
		    dataAdapter.InsertCommand.Transaction = tx;
		    dataAdapter.UpdateCommand.Transaction = tx;
		    dataAdapter.DeleteCommand.Transaction = tx;
		    return dataAdapter;
		}

		private static SqlCommand CreateInsertCustomersViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_INSERT, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("CustomerID",System.Data.SqlDbType.NChar));
		    pc.Add(CreateParameter("CompanyName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ContactName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ContactTitle",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Address",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("City",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Region",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("PostalCode",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Country",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Phone",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Fax",System.Data.SqlDbType.NVarChar));
		    return cmd;
		} 

		private static SqlCommand CreateUpdateCustomersViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_UPDATE, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("CustomerID",System.Data.SqlDbType.NChar));
		    pc.Add(CreateParameter("CompanyName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ContactName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ContactTitle",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Address",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("City",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Region",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("PostalCode",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Country",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Phone",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("Fax",System.Data.SqlDbType.NVarChar));
		    return cmd;
		} 
		private static SqlCommand CreateDeleteCustomersViaSPCommand(SqlConnection conn)
			{
		    SqlCommand cmd = new SqlCommand(SP_DELETE,conn);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection pc = cmd.Parameters;
				pc.Add(CreateParameter("CustomerID",System.Data.SqlDbType.NChar));

				return cmd;
		} 
		#endregion
        }
    }
