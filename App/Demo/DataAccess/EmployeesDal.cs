/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace DataAccess
{
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
    ///Update the <see cref="Employees_Dal"/> class if you need to add or change some functionality
    ///</remarks>
    public class EmployeesDal : BaseDAL
    {

        private const string SP_SELECT_BY_PRIMARY_KEY = "usp_Employees_SelectByPrimaryKey";
        private const string SP_SELECT_ROWS_BY_WHERE = "usp_Employees_SelectRowsByWhere";
        private const string SP_INSERT = "usp_Employees_Insert";
        private const string SP_UPDATE = "usp_Employees_Update";
        private const string SP_DELETE = "usp_Employees_DeleteByPrimaryKey";

        #region Constructor

        ///<summary>
        ///Initializes a new instance of the <see cref="Employees_Dal"/> 
        ///class with the specified <see cref="BaseDAL"/>.
        ///</summary>
        public EmployeesDal()
            : base()
        {

        }

        #endregion

        #region Persist Methods
        ///<summary>
        ///Inserts, updates or deletes rows in a DataSet.
        ///</summary>
        private static SqlCommand cmdGetIdentity;
        public EmployeesDS Persist(EmployeesDS updates)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlTransaction tx = conn.BeginTransaction();
            try
            {
                EmployeesDS.EmployeesDataTable tbl = updates.Employees;
                //Create the adapter initial
                SqlDataAdapter dataAdapter = CreateEmployeesDataAdapter(conn, tx);

                cmdGetIdentity = new SqlCommand("SELECT @@IDENTITY", conn, tx);
                dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);

                //First Add Employees
                dataAdapter.Update(tbl.Select("", "", DataViewRowState.Added));
                //Next Modified
                dataAdapter.Update(tbl.Select("", "", DataViewRowState.ModifiedCurrent));
                //Next Deleted
                dataAdapter.Update(tbl.Select("", "", DataViewRowState.Deleted));
                tx.Commit();
                return updates;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                    conn.Dispose();
            }
        }

        public EmployeesDS InsertEmployees(EmployeesDS updates)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                EmployeesDS.EmployeesDataTable tbl = updates.Employees;
                //Create the adapter initial
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.InsertCommand = CreateInsertEmployeesViaSPCommand(conn);

                //Roll Back the changes if some one error have
                dataAdapter.ContinueUpdateOnError = false;

                cmdGetIdentity = new SqlCommand("SELECT @@IDENTITY", conn);
                dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(HandleRowUpdated);

                dataAdapter.Update(tbl.Select("", "", DataViewRowState.Added));
                return updates;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (conn != null)
                    conn.Dispose();
            }
        }

        public EmployeesDS UpdateEmployees(EmployeesDS updates)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                EmployeesDS.EmployeesDataTable tbl = updates.Employees;
                //Create the adapter initial
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.UpdateCommand = CreateUpdateEmployeesViaSPCommand(conn);

                //Roll Back the changes if some one error have
                dataAdapter.ContinueUpdateOnError = false;

                dataAdapter.Update(tbl.Select("", "", DataViewRowState.ModifiedCurrent));
                return updates;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                    conn.Dispose();
            }
        }

        public void DeleteEmployees(EmployeesDS updates)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                EmployeesDS.EmployeesDataTable tbl = updates.Employees;
                //Create the adapter initial
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.DeleteCommand = CreateDeleteEmployeesViaSPCommand(conn);

                //Roll Back the changes if some one error have
                dataAdapter.ContinueUpdateOnError = false;

                dataAdapter.Update(tbl.Select("", "", DataViewRowState.Deleted));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                e.Row["EmployeeID"] = Convert.ToInt32(cmdGetIdentity.ExecuteScalar());
                e.Row.AcceptChanges();
            }
        }
        #endregion

        #region Get Methods

        ///<summary>
        ///Looks up a Employees based on its primary-key:System.Int32 employeeid
        ///</summary>
        public EmployeesDS Populate(System.Int32 employeeid)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SELECT_BY_PRIMARY_KEY, conn);
                EmployeesDS dataSet = new EmployeesDS();
                cmd.CommandType = CommandType.StoredProcedure;
                AddParameter(cmd, "@EmployeeID", System.Data.SqlDbType.Int, employeeid);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet, dataSet.Employees.TableName);
                dataSet.AcceptChanges();
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                    conn.Dispose();
            }
        }

        ///<summary>
        ///Gets an Strongly  Typed <see cref="EmployeesDS"/> DataSet  objects that  match the search condition.
        ///</summary>
        ///<param name="sqlWhere">The SQL search condition. </param>
        ///<returns>An Strongly  Typed <see cref="EmployeesDS"/> DataSet.</returns>
        public EmployeesDS PopulateList(string sqlWhere)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SELECT_ROWS_BY_WHERE, conn);
                EmployeesDS dataSet = new EmployeesDS();
                cmd.CommandType = CommandType.StoredProcedure;
                AddParameter(cmd, "@SqlWhere", System.Data.SqlDbType.VarChar, sqlWhere);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet, dataSet.Employees.TableName);
                dataSet.AcceptChanges();
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
        private SqlDataAdapter CreateEmployeesDataAdapter(SqlConnection conn)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = CreateInsertEmployeesViaSPCommand(conn);
            dataAdapter.UpdateCommand = CreateUpdateEmployeesViaSPCommand(conn);
            dataAdapter.DeleteCommand = CreateDeleteEmployeesViaSPCommand(conn);
            return dataAdapter;
        }

        ///<summary>
        ///</summary>
        ///<param name="conn"></param>
        ///<param name="tx"></param>
        ///<returns>SqlDataAdapter</returns>
        private SqlDataAdapter CreateEmployeesDataAdapter(SqlConnection conn, SqlTransaction tx)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = CreateInsertEmployeesViaSPCommand(conn);
            dataAdapter.UpdateCommand = CreateUpdateEmployeesViaSPCommand(conn);
            dataAdapter.DeleteCommand = CreateDeleteEmployeesViaSPCommand(conn);
            dataAdapter.InsertCommand.Transaction = tx;
            dataAdapter.UpdateCommand.Transaction = tx;
            dataAdapter.DeleteCommand.Transaction = tx;
            return dataAdapter;
        }

        private static SqlCommand CreateInsertEmployeesViaSPCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(SP_INSERT, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameterCollection pc = cmd.Parameters;
            pc.Add(CreateParameter("LastName", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("FirstName", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Title", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("TitleOfCourtesy", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("BirthDate", System.Data.SqlDbType.DateTime));
            pc.Add(CreateParameter("HireDate", System.Data.SqlDbType.DateTime));
            pc.Add(CreateParameter("Address", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("City", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Region", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("PostalCode", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Country", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("HomePhone", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Extension", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Photo", System.Data.SqlDbType.Image));
            pc.Add(CreateParameter("Notes", System.Data.SqlDbType.NText));
            pc.Add(CreateParameter("ReportsTo", System.Data.SqlDbType.Int));
            pc.Add(CreateParameter("PhotoPath", System.Data.SqlDbType.NVarChar));
            return cmd;
        }

        private static SqlCommand CreateUpdateEmployeesViaSPCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(SP_UPDATE, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameterCollection pc = cmd.Parameters;
            pc.Add(CreateParameter("EmployeeID", System.Data.SqlDbType.Int));
            pc.Add(CreateParameter("LastName", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("FirstName", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Title", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("TitleOfCourtesy", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("BirthDate", System.Data.SqlDbType.DateTime));
            pc.Add(CreateParameter("HireDate", System.Data.SqlDbType.DateTime));
            pc.Add(CreateParameter("Address", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("City", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Region", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("PostalCode", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Country", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("HomePhone", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Extension", System.Data.SqlDbType.NVarChar));
            pc.Add(CreateParameter("Photo", System.Data.SqlDbType.Image));
            pc.Add(CreateParameter("Notes", System.Data.SqlDbType.NText));
            pc.Add(CreateParameter("ReportsTo", System.Data.SqlDbType.Int));
            pc.Add(CreateParameter("PhotoPath", System.Data.SqlDbType.NVarChar));
            return cmd;
        }
        private static SqlCommand CreateDeleteEmployeesViaSPCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(SP_DELETE, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameterCollection pc = cmd.Parameters;
            pc.Add(CreateParameter("EmployeeID", System.Data.SqlDbType.Int));

            return cmd;
        }
        #endregion
    }
}
