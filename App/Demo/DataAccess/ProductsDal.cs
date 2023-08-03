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
    ///Update the <see cref="ProductsDal"/> class if you need to add or change some functionality
    ///</remarks>
    public class ProductsDal:BaseDAL  {

        private const string SP_SELECT_BY_PRIMARY_KEY="usp_Products_SelectByPrimaryKey";
        private const string SP_SELECT_ROWS_BY_WHERE="usp_Products_SelectRowsByWhere";
        private const string SP_INSERT="usp_Products_Insert";
        private const string SP_UPDATE="usp_Products_Update";
        private const string SP_DELETE="usp_Products_DeleteByPrimaryKey";

        #region Constructor

        ///<summary>
        ///Initializes a new instance of the <see cref="ProductsDal"/> 
        ///class with the specified <see cref="BaseDAL"/>.
        ///</summary>
        public ProductsDal(): base()
        {

        }

        #endregion

        #region Persist Methods
        ///<summary>
        ///Inserts, updates or deletes rows in a DataSet.
        ///</summary>
        private static SqlCommand cmdGetIdentity;
		public ProductsDS Persist(ProductsDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    conn.Open ();
		    SqlTransaction tx = conn.BeginTransaction ();
		    try
		    {
		        ProductsDS.ProductsDataTable tbl = updates.Products;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = CreateProductsDataAdapter(conn,tx);

		        cmdGetIdentity = new SqlCommand("SELECT @@IDENTITY",conn, tx);
		        dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);

		        //First Add Products
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

		public ProductsDS InsertProducts(ProductsDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        ProductsDS.ProductsDataTable tbl = updates.Products;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.InsertCommand = CreateInsertProductsViaSPCommand(conn);

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

		public ProductsDS UpdateProducts(ProductsDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        ProductsDS.ProductsDataTable tbl = updates.Products;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.UpdateCommand = CreateUpdateProductsViaSPCommand(conn);

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

		public void DeleteProducts(ProductsDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        ProductsDS.ProductsDataTable tbl = updates.Products;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.DeleteCommand = CreateDeleteProductsViaSPCommand(conn);

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
					e.Row["ProductID"] = Convert.ToInt32 (cmdGetIdentity.ExecuteScalar());
					e.Row.AcceptChanges();
				}
			}
#endregion

#region Get Methods

		///<summary>
		///Looks up a Products based on its primary-key:System.Int32 productid
		///</summary>
		public ProductsDS Populate(System.Int32 productid) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_BY_PRIMARY_KEY, conn);
		        ProductsDS dataSet = new ProductsDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@ProductID",System.Data.SqlDbType.Int,productid);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Products.TableName );
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
		///Gets an Strongly  Typed <see cref="ProductsDS"/> DataSet  objects that  match the search condition.
		///</summary>
		///<param name="sqlWhere">The SQL search condition. </param>
		///<returns>An Strongly  Typed <see cref="ProductsDS"/> DataSet.</returns>
		public ProductsDS PopulateList(string sqlWhere) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_WHERE, conn);
		        ProductsDS dataSet = new ProductsDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@SqlWhere",System.Data.SqlDbType.VarChar,sqlWhere);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Products.TableName );
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
		private SqlDataAdapter CreateProductsDataAdapter(SqlConnection conn)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertProductsViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateProductsViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteProductsViaSPCommand(conn);
		    return dataAdapter;
		}

		///<summary>
		///</summary>
		///<param name="conn"></param>
		///<param name="tx"></param>
		///<returns>SqlDataAdapter</returns>
		private SqlDataAdapter CreateProductsDataAdapter(SqlConnection conn,SqlTransaction tx)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertProductsViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateProductsViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteProductsViaSPCommand(conn);
		    dataAdapter.InsertCommand.Transaction = tx;
		    dataAdapter.UpdateCommand.Transaction = tx;
		    dataAdapter.DeleteCommand.Transaction = tx;
		    return dataAdapter;
		}

		private static SqlCommand CreateInsertProductsViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_INSERT, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("ProductName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("SupplierID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("CategoryID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("QuantityPerUnit",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("UnitPrice",System.Data.SqlDbType.Money));
		    pc.Add(CreateParameter("UnitsInStock",System.Data.SqlDbType.SmallInt));
		    pc.Add(CreateParameter("UnitsOnOrder",System.Data.SqlDbType.SmallInt));
		    pc.Add(CreateParameter("ReorderLevel",System.Data.SqlDbType.SmallInt));
		    pc.Add(CreateParameter("Discontinued",System.Data.SqlDbType.Bit));
		    return cmd;
		} 

		private static SqlCommand CreateUpdateProductsViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_UPDATE, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("ProductID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("ProductName",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("SupplierID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("CategoryID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("QuantityPerUnit",System.Data.SqlDbType.NVarChar));
		    pc.Add(CreateParameter("UnitPrice",System.Data.SqlDbType.Money));
		    pc.Add(CreateParameter("UnitsInStock",System.Data.SqlDbType.SmallInt));
		    pc.Add(CreateParameter("UnitsOnOrder",System.Data.SqlDbType.SmallInt));
		    pc.Add(CreateParameter("ReorderLevel",System.Data.SqlDbType.SmallInt));
		    pc.Add(CreateParameter("Discontinued",System.Data.SqlDbType.Bit));
		    return cmd;
		} 
		private static SqlCommand CreateDeleteProductsViaSPCommand(SqlConnection conn)
			{
		    SqlCommand cmd = new SqlCommand(SP_DELETE,conn);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection pc = cmd.Parameters;
				pc.Add(CreateParameter("ProductID",System.Data.SqlDbType.Int));

				return cmd;
		} 
		#endregion
        }
    }
