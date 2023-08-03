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
	/// Productss Business Class  
	/// </summary>
	public class ProductsBiz {

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsBiz"/> 
        /// class with the specified <see cref="Biz"/>.
        /// </summary>
        public ProductsBiz()
        {
        }
        #endregion

        #region Persist Methods

        /// <summary>
		/// A method for inserting and updating Products data.
		/// Multiple rows may be inserted or updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a ProductsDataTable with data to insert or update.</param>
		public ProductsDS Persist(ProductsDS updates) {
			ProductsDal dao = null;
			try {
				dao = new ProductsDal ();
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
		/// A method for inserting and updating Products data.
		/// Multiple rows may be inserted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a ProductsDataTable with data to insert or update.</param>
		public ProductsDS InsertProducts(ProductsDS ds) {
			ProductsDal dao = null;
			try {
				dao = new ProductsDal ();
				return dao.InsertProducts(ds);
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
		/// A method for updating Products data.
		/// Multiple rows may be  updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a ProductsDataTable with data to insert or update.</param>
		public ProductsDS UpdateProducts(ProductsDS ds) {
			ProductsDal dao = null;
			try {
				dao = new ProductsDal ();
				return dao.UpdateProducts(ds);
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
		/// A method for delete Products data.
		/// Multiple rows may be deleted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a ProductsDataTable with data to insert or update.</param>
		public void DeleteProducts(ProductsDS ds) {
			ProductsDal dao = null;
			try {
				dao = new ProductsDal ();
				dao.DeleteProducts(ds);
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
		/// Fills a DataSet with a Products by its primary-key attributes:System.Int32 productid
		/// </summary> 
		/// <param name="productid">The ProductID</param>
		/// <returns>A ProductsDS</returns>
		public ProductsDS Populate(System.Int32 productid) {
			ProductsDal dao = null;
			try {
				dao = new ProductsDal ();
				return dao.Populate(productid);
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
		/// Fills a DataSet with all Productss based on a condition.
		/// </summary>
		/// <param name="whereSql">A string with an SQL condition for the data to look up.</param>
		/// <returns>A ProductsDS</returns>
		public ProductsDS PopulateList(string whereSql) {
			ProductsDal dao = null;
			try {
				dao = new ProductsDal ();
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
