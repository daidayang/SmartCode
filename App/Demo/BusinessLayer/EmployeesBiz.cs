/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace Businesslayer{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections;
    using DataAccess;
    using Common;

	/// <summary>
	/// Employeess Business Class  
	/// </summary>
	public class EmployeesBiz {

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesBiz"/> 
        /// class with the specified <see cref="Biz"/>.
        /// </summary>
        public EmployeesBiz()
        {
        }
        #endregion

        #region Persist Methods

        /// <summary>
		/// A method for inserting and updating Employees data.
		/// Multiple rows may be inserted or updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a EmployeesDataTable with data to insert or update.</param>
		public EmployeesDS Persist(EmployeesDS updates) {
			EmployeesDal dao = null;
			try {
				dao = new EmployeesDal ();
				return dao.Persist(updates);
			}
			catch (Exception ex) 
			{
				throw new Exception (ex.Message ,ex);
			}
			finally 
			{
				if (dao != null) dao.Dispose();
			}
        }

		/// <summary>
		/// A method for inserting and updating Employees data.
		/// Multiple rows may be inserted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a EmployeesDataTable with data to insert or update.</param>
		public EmployeesDS InsertEmployees(EmployeesDS ds) {
			EmployeesDal dao = null;
			try {
				dao = new EmployeesDal ();
				return dao.InsertEmployees(ds);
			}
			catch (Exception ex) 
			{
				throw new Exception (ex.Message ,ex);
			}
			finally 
			{
				if (dao != null) dao.Dispose();
			}
        }

		/// <summary>
		/// A method for updating Employees data.
		/// Multiple rows may be  updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a EmployeesDataTable with data to insert or update.</param>
		public EmployeesDS UpdateEmployees(EmployeesDS ds) {
			EmployeesDal dao = null;
			try {
				dao = new EmployeesDal ();
				return dao.UpdateEmployees(ds);
			}
			catch (Exception ex) 
			{
				throw new Exception (ex.Message ,ex);
			}
			finally 
			{
				if (dao != null) dao.Dispose();
			}
        }

		/// <summary>
		/// A method for delete Employees data.
		/// Multiple rows may be deleted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a EmployeesDataTable with data to insert or update.</param>
		public void DeleteEmployees(EmployeesDS ds) {
			EmployeesDal dao = null;
			try {
				dao = new EmployeesDal ();
				dao.DeleteEmployees(ds);
			}
			catch (Exception ex) 
			{
				throw new Exception (ex.Message ,ex);
			}
			finally 
			{
				if (dao != null) dao.Dispose();
			}
        }
        #endregion

        #region Get Methods

        /// <summary>
		/// Fills a DataSet with a Employees by its primary-key attributes:System.Int32 employeeid
		/// </summary> 
		/// <param name="employeeid">The EmployeeID</param>
		/// <returns>A EmployeesDS</returns>
		public EmployeesDS Populate(System.Int32 employeeid) {
			EmployeesDal dao = null;
			try {
				dao = new EmployeesDal ();
				return dao.Populate(employeeid);
			}
			catch (Exception ex) 
			{
				throw new Exception (ex.Message ,ex);
			}
			finally 
			{
				if (dao != null) dao.Dispose();
			}
		}

		/// <summary>
		/// Fills a DataSet with all Employeess based on a condition.
		/// </summary>
		/// <param name="whereSql">A string with an SQL condition for the data to look up.</param>
		/// <returns>A EmployeesDS</returns>
		public EmployeesDS PopulateList(string whereSql) {
			EmployeesDal dao = null;
			try {
				dao = new EmployeesDal ();
				return dao.PopulateList(whereSql);
			}
			catch (Exception ex) 
			{
				throw new Exception (ex.Message ,ex);
			}
			finally 
			{
				if (dao != null) dao.Dispose();
			}
		}

        #endregion

        #region Get Methods for each child
        #endregion
    }
}
