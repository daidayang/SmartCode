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
    ///Update the <see cref="OrdersDal"/> class if you need to add or change some functionality
    ///</remarks>
    public class OrdersDal:BaseDAL  {

        private const string SP_SELECT_BY_PRIMARY_KEY="usp_Orders_SelectByPrimaryKey";
        private const string SP_SELECT_ROWS_BY_WHERE="usp_Orders_SelectRowsByWhere";
        private const string SP_INSERT="usp_Orders_Insert";
        private const string SP_UPDATE="usp_Orders_Update";
        private const string SP_DELETE="usp_Orders_DeleteByPrimaryKey";

        #region Constructor

        ///<summary>
        ///Initializes a new instance of the <see cref="OrdersDal"/> 
        ///class with the specified <see cref="BaseDAL"/>.
        ///</summary>
        public OrdersDal(): base()
        {

        }

        #endregion

        #region Persist Methods
        ///<summary>
        ///Inserts, updates or deletes rows in a DataSet.
        ///</summary>
        private static SqlCommand cmdGetIdentity;
		public OrdersDS Persist(OrdersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    conn.Open ();
		    SqlTransaction tx = conn.BeginTransaction ();
		    try
		    {
		        OrdersDS.OrdersDataTable tbl = updates.Orders;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = CreateOrdersDataAdapter(conn,tx);

                cmdGetIdentity = new SqlCommand("SELECT @@IDENTITY", conn, tx);
		        dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);

		        //First Add Orders
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

		public OrdersDS InsertOrders(OrdersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        OrdersDS.OrdersDataTable tbl = updates.Orders;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.InsertCommand = CreateInsertOrdersViaSPCommand(conn);

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

		public OrdersDS UpdateOrders(OrdersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        OrdersDS.OrdersDataTable tbl = updates.Orders;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.UpdateCommand = CreateUpdateOrdersViaSPCommand(conn);

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

		public void DeleteOrders(OrdersDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        OrdersDS.OrdersDataTable tbl = updates.Orders;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.DeleteCommand = CreateDeleteOrdersViaSPCommand(conn);

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
					e.Row["OrderID"] = Convert.ToInt32 (cmdGetIdentity.ExecuteScalar());
					e.Row.AcceptChanges();
				}
			}
#endregion

#region Get Methods

		///<summary>
		///Looks up a Orders based on its primary-key:System.Int32 orderid
		///</summary>
		public OrdersDS Populate(System.Int32 orderid) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_BY_PRIMARY_KEY, conn);
		        OrdersDS dataSet = new OrdersDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@OrderID",System.Data.SqlDbType.Int,orderid);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Orders.TableName );
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
		///Gets an Strongly  Typed <see cref="OrdersDS"/> DataSet  objects that  match the search condition.
		///</summary>
		///<param name="sqlWhere">The SQL search condition. </param>
		///<returns>An Strongly  Typed <see cref="OrdersDS"/> DataSet.</returns>
		public OrdersDS PopulateList(string sqlWhere) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_WHERE, conn);
		        OrdersDS dataSet = new OrdersDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@SqlWhere",System.Data.SqlDbType.VarChar,sqlWhere);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Orders.TableName );
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
		private SqlDataAdapter CreateOrdersDataAdapter(SqlConnection conn)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertOrdersViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateOrdersViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteOrdersViaSPCommand(conn);
		    return dataAdapter;
		}

		///<summary>
		///</summary>
		///<param name="conn"></param>
		///<param name="tx"></param>
		///<returns>SqlDataAdapter</returns>
		private SqlDataAdapter CreateOrdersDataAdapter(SqlConnection conn,SqlTransaction tx)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertOrdersViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateOrdersViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteOrdersViaSPCommand(conn);
		    dataAdapter.InsertCommand.Transaction = tx;
		    dataAdapter.UpdateCommand.Transaction = tx;
		    dataAdapter.DeleteCommand.Transaction = tx;
		    return dataAdapter;
		}

		private static SqlCommand CreateInsertOrdersViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_INSERT, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("CustomerID",System.Data.SqlDbType.NChar));
		    pc.Add(CreateParameter("EmployeeID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("OrderDate",System.Data.SqlDbType.DateTime));
		    pc.Add(CreateParameter("RequiredDate",System.Data.SqlDbType.DateTime));
		    pc.Add(CreateParameter("ShippedDate",System.Data.SqlDbType.DateTime));
		    pc.Add(CreateParameter("ShipVia",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("Freight",System.Data.SqlDbType.Money));
		    pc.Add(CreateParameter("ShipName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipAddress",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipCity",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipRegion",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipPostalCode",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipCountry",System.Data.SqlDbType.NVarChar));
		    return cmd;
		} 

		private static SqlCommand CreateUpdateOrdersViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_UPDATE, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("OrderID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("CustomerID",System.Data.SqlDbType.NChar));
		    pc.Add(CreateParameter("EmployeeID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("OrderDate",System.Data.SqlDbType.DateTime));
		    pc.Add(CreateParameter("RequiredDate",System.Data.SqlDbType.DateTime));
		    pc.Add(CreateParameter("ShippedDate",System.Data.SqlDbType.DateTime));
		    pc.Add(CreateParameter("ShipVia",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("Freight",System.Data.SqlDbType.Money));
		    pc.Add(CreateParameter("ShipName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipAddress",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipCity",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipRegion",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipPostalCode",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("ShipCountry",System.Data.SqlDbType.NVarChar));
		    return cmd;
		} 
		private static SqlCommand CreateDeleteOrdersViaSPCommand(SqlConnection conn)
			{
		    SqlCommand cmd = new SqlCommand(SP_DELETE,conn);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection pc = cmd.Parameters;
				pc.Add(CreateParameter("OrderID",System.Data.SqlDbType.Int));

				return cmd;
		} 
		#endregion
        }
    }
