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
	/// Categoriess Business Class  
	/// </summary>
	public class CategoriesBiz {

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesBiz"/> 
        /// class with the specified <see cref="Biz"/>.
        /// </summary>
        public CategoriesBiz()
        {
        }
        #endregion

        #region Persist Methods

        /// <summary>
		/// A method for inserting and updating Categories data.
		/// Multiple rows may be inserted or updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CategoriesDataTable with data to insert or update.</param>
		public CategoriesDS Persist(CategoriesDS updates) {
			CategoriesDal dao = null;
			try {
				dao = new CategoriesDal ();
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
		/// A method for inserting and updating Categories data.
		/// Multiple rows may be inserted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CategoriesDataTable with data to insert or update.</param>
		public CategoriesDS InsertCategories(CategoriesDS ds) {
			CategoriesDal dao = null;
			try {
				dao = new CategoriesDal ();
				return dao.InsertCategories(ds);
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
		/// A method for updating Categories data.
		/// Multiple rows may be  updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CategoriesDataTable with data to insert or update.</param>
		public CategoriesDS UpdateCategories(CategoriesDS ds) {
			CategoriesDal dao = null;
			try {
				dao = new CategoriesDal ();
				return dao.UpdateCategories(ds);
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
		/// A method for delete Categories data.
		/// Multiple rows may be deleted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a CategoriesDataTable with data to insert or update.</param>
		public void DeleteCategories(CategoriesDS ds) {
			CategoriesDal dao = null;
			try {
				dao = new CategoriesDal ();
				dao.DeleteCategories(ds);
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
		/// Fills a DataSet with a Categories by its primary-key attributes:System.Int32 categoryid
		/// </summary> 
		/// <param name="categoryid">The CategoryID</param>
		/// <returns>A CategoriesDS</returns>
		public CategoriesDS Populate(System.Int32 categoryid) {
			CategoriesDal dao = null;
			try {
				dao = new CategoriesDal ();
				return dao.Populate(categoryid);
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
		/// Fills a DataSet with all Categoriess based on a condition.
		/// </summary>
		/// <param name="whereSql">A string with an SQL condition for the data to look up.</param>
		/// <returns>A CategoriesDS</returns>
		public CategoriesDS PopulateList(string whereSql) {
			CategoriesDal dao = null;
			try {
				dao = new CategoriesDal ();
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
