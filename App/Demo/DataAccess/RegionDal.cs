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
    ///Update the <see cref="RegionDal"/> class if you need to add or change some functionality
    ///</remarks>
    public class RegionDal:BaseDAL  {

        private const string SP_SELECT_BY_PRIMARY_KEY="usp_Region_SelectByPrimaryKey";
        private const string SP_SELECT_ROWS_BY_WHERE="usp_Region_SelectRowsByWhere";
        private const string SP_INSERT="usp_Region_Insert";
        private const string SP_UPDATE="usp_Region_Update";
        private const string SP_DELETE="usp_Region_DeleteByPrimaryKey";

        #region Constructor

        ///<summary>
        ///Initializes a new instance of the <see cref="RegionDal"/> 
        ///class with the specified <see cref="BaseDAL"/>.
        ///</summary>
        public RegionDal(): base()
        {

        }

        #endregion

        #region Persist Methods
        ///<summary>
        ///Inserts, updates or deletes rows in a DataSet.
        ///</summary>
		public RegionDS Persist(RegionDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    conn.Open ();
		    SqlTransaction tx = conn.BeginTransaction ();
		    try
		    {
		        RegionDS.RegionDataTable tbl = updates.Region;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = CreateRegionDataAdapter(conn,tx);


		        //First Add Region
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

		public RegionDS InsertRegion(RegionDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        RegionDS.RegionDataTable tbl = updates.Region;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.InsertCommand = CreateInsertRegionViaSPCommand(conn);

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

		public RegionDS UpdateRegion(RegionDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        RegionDS.RegionDataTable tbl = updates.Region;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.UpdateCommand = CreateUpdateRegionViaSPCommand(conn);

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

		public void DeleteRegion(RegionDS updates) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        RegionDS.RegionDataTable tbl = updates.Region;
		        //Create the adapter initial
		        SqlDataAdapter dataAdapter = new SqlDataAdapter();
		        dataAdapter.DeleteCommand = CreateDeleteRegionViaSPCommand(conn);

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
		///Looks up a Region based on its primary-key:System.Int32 regionid
		///</summary>
		public RegionDS Populate(System.Int32 regionid) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_BY_PRIMARY_KEY, conn);
		        RegionDS dataSet = new RegionDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@RegionID",System.Data.SqlDbType.Int,regionid);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Region.TableName );
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
		///Gets an Strongly  Typed <see cref="RegionDS"/> DataSet  objects that  match the search condition.
		///</summary>
		///<param name="sqlWhere">The SQL search condition. </param>
		///<returns>An Strongly  Typed <see cref="RegionDS"/> DataSet.</returns>
		public RegionDS PopulateList(string sqlWhere) {
		    SqlConnection conn = new SqlConnection(ConnectionString);
		    try
		    {
		        conn.Open();
		        SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_WHERE, conn);
		        RegionDS dataSet = new RegionDS();
		        cmd.CommandType = CommandType.StoredProcedure;
		        AddParameter(cmd,  "@SqlWhere",System.Data.SqlDbType.VarChar,sqlWhere);
		        SqlDataAdapter adapter = new SqlDataAdapter( cmd );
		        adapter.Fill(dataSet,dataSet.Region.TableName );
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
		private SqlDataAdapter CreateRegionDataAdapter(SqlConnection conn)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertRegionViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateRegionViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteRegionViaSPCommand(conn);
		    return dataAdapter;
		}

		///<summary>
		///</summary>
		///<param name="conn"></param>
		///<param name="tx"></param>
		///<returns>SqlDataAdapter</returns>
		private SqlDataAdapter CreateRegionDataAdapter(SqlConnection conn,SqlTransaction tx)
		{
		    SqlDataAdapter dataAdapter = new SqlDataAdapter();
		    dataAdapter.InsertCommand = CreateInsertRegionViaSPCommand(conn);
		    dataAdapter.UpdateCommand = CreateUpdateRegionViaSPCommand(conn);
		    dataAdapter.DeleteCommand = CreateDeleteRegionViaSPCommand(conn);
		    dataAdapter.InsertCommand.Transaction = tx;
		    dataAdapter.UpdateCommand.Transaction = tx;
		    dataAdapter.DeleteCommand.Transaction = tx;
		    return dataAdapter;
		}

		private static SqlCommand CreateInsertRegionViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_INSERT, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("RegionID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("RegionDescription",System.Data.SqlDbType.NChar));
		    return cmd;
		} 

		private static SqlCommand CreateUpdateRegionViaSPCommand(SqlConnection conn)
		{
		    SqlCommand cmd = new SqlCommand(SP_UPDATE, conn);
		    cmd.CommandType = CommandType.StoredProcedure;

		    SqlParameterCollection pc = cmd.Parameters;
		    pc.Add(CreateParameter("RegionID",System.Data.SqlDbType.Int));
		    pc.Add(CreateParameter("RegionDescription",System.Data.SqlDbType.NChar));
		    return cmd;
		} 
		private static SqlCommand CreateDeleteRegionViaSPCommand(SqlConnection conn)
			{
		    SqlCommand cmd = new SqlCommand(SP_DELETE,conn);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection pc = cmd.Parameters;
				pc.Add(CreateParameter("RegionID",System.Data.SqlDbType.Int));

				return cmd;
		} 
		#endregion
        }
    }
