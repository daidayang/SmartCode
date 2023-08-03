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
	/// Customerss Business Class  
	/// </summary>
	public class CustomersBiz {

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersBiz"/> 
        /// class with the specified <see cref="Biz"/>.
        /// </summary>
        public CustomersBiz()
        {
        }
        #endregion

        #region Persist Methods

        /// <summary>
		/// A method for inserting and updating Customers data.
		/// Multiple rows may be inserted or updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CustomersDataTable with data to insert or update.</param>
		public CustomersDS Persist(CustomersDS updates) {
			CustomersDal dao = null;
			try {
				dao = new CustomersDal ();
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
		/// A method for inserting and updating Customers data.
		/// Multiple rows may be inserted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CustomersDataTable with data to insert or update.</param>
		public CustomersDS InsertCustomers(CustomersDS ds) {
			CustomersDal dao = null;
			try {
				dao = new CustomersDal ();
				return dao.InsertCustomers(ds);
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
		/// A method for updating Customers data.
		/// Multiple rows may be  updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CustomersDataTable with data to insert or update.</param>
		public CustomersDS UpdateCustomers(CustomersDS ds) {
			CustomersDal dao = null;
			try {
				dao = new CustomersDal ();
				return dao.UpdateCustomers(ds);
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
		/// A method for delete Customers data.
		/// Multiple rows may be deleted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CustomersDataTable with data to insert or update.</param>
		public void DeleteCustomers(CustomersDS ds) {
			CustomersDal dao = null;
			try {
				dao = new CustomersDal ();
				dao.DeleteCustomers(ds);
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
		/// Fills a DataSet with a Customers by its primary-key attributes:System.String customerid
		/// </summary> 
		/// <param name="customerid">The CustomerID</param>
		/// <returns>A CustomersDS</returns>
		public CustomersDS Populate(System.String customerid) {
			CustomersDal dao = null;
			try {
				dao = new CustomersDal ();
				return dao.Populate(customerid);
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
		/// Fills a DataSet with all Customerss based on a condition.
		/// </summary>
		/// <param name="whereSql">A string with an SQL condition for the data to look up.</param>
		/// <returns>A CustomersDS</returns>
		public CustomersDS PopulateList(string whereSql) {
			CustomersDal dao = null;
			try {
				dao = new CustomersDal ();
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
