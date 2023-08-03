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
	/// Orderss Business Class  
	/// </summary>
	public class OrdersBiz {

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersBiz"/> 
        /// class with the specified <see cref="Biz"/>.
        /// </summary>
        public OrdersBiz()
        {
        }
        #endregion

        #region Persist Methods

        /// <summary>
		/// A method for inserting and updating Orders data.
		/// Multiple rows may be inserted or updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a OrdersDataTable with data to insert or update.</param>
		public OrdersDS Persist(OrdersDS updates) {
			OrdersDal dao = null;
			try {
				dao = new OrdersDal ();
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
		/// A method for inserting and updating Orders data.
		/// Multiple rows may be inserted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a OrdersDataTable with data to insert or update.</param>
		public OrdersDS InsertOrders(OrdersDS ds) {
			OrdersDal dao = null;
			try {
				dao = new OrdersDal ();
				return dao.InsertOrders(ds);
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
		/// A method for updating Orders data.
		/// Multiple rows may be  updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a OrdersDataTable with data to insert or update.</param>
		public OrdersDS UpdateOrders(OrdersDS ds) {
			OrdersDal dao = null;
			try {
				dao = new OrdersDal ();
				return dao.UpdateOrders(ds);
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
		/// A method for delete Orders data.
		/// Multiple rows may be deleted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a OrdersDataTable with data to insert or update.</param>
		public void DeleteOrders(OrdersDS ds) {
			OrdersDal dao = null;
			try {
				dao = new OrdersDal ();
				dao.DeleteOrders(ds);
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
		/// Fills a DataSet with a Orders by its primary-key attributes:System.Int32 orderid
		/// </summary> 
		/// <param name="orderid">The OrderID</param>
		/// <returns>A OrdersDS</returns>
		public OrdersDS Populate(System.Int32 orderid) {
			OrdersDal dao = null;
			try {
				dao = new OrdersDal ();
				return dao.Populate(orderid);
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
		/// Fills a DataSet with all Orderss based on a condition.
		/// </summary>
		/// <param name="whereSql">A string with an SQL condition for the data to look up.</param>
		/// <returns>A OrdersDS</returns>
		public OrdersDS PopulateList(string whereSql) {
			OrdersDal dao = null;
			try {
				dao = new OrdersDal ();
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
