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
	/// Regions Business Class  
	/// </summary>
	public class RegionBiz {

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionBiz"/> 
        /// class with the specified <see cref="Biz"/>.
        /// </summary>
        public RegionBiz()
        {
        }
        #endregion

        #region Persist Methods

        /// <summary>
		/// A method for inserting and updating Region data.
		/// Multiple rows may be inserted or updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a RegionDataTable with data to insert or update.</param>
		public RegionDS Persist(RegionDS updates) {
			RegionDal dao = null;
			try {
				dao = new RegionDal ();
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
		/// A method for inserting and updating Region data.
		/// Multiple rows may be inserted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a RegionDataTable with data to insert or update.</param>
		public RegionDS InsertRegion(RegionDS ds) {
			RegionDal dao = null;
			try {
				dao = new RegionDal ();
				return dao.InsertRegion(ds);
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
		/// A method for updating Region data.
		/// Multiple rows may be  updated simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a RegionDataTable with data to insert or update.</param>
		public RegionDS UpdateRegion(RegionDS ds) {
			RegionDal dao = null;
			try {
				dao = new RegionDal ();
				return dao.UpdateRegion(ds);
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
		/// A method for delete Region data.
		/// Multiple rows may be deleted  simultaneously.
		/// </summary>
		/// <param name="updates">A DataSet containing a RegionDataTable with data to insert or update.</param>
		public void DeleteRegion(RegionDS ds) {
			RegionDal dao = null;
			try {
				dao = new RegionDal ();
				dao.DeleteRegion(ds);
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
		/// Fills a DataSet with a Region by its primary-key attributes:System.Int32 regionid
		/// </summary> 
		/// <param name="regionid">The RegionID</param>
		/// <returns>A RegionDS</returns>
		public RegionDS Populate(System.Int32 regionid) {
			RegionDal dao = null;
			try {
				dao = new RegionDal ();
				return dao.Populate(regionid);
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
		/// Fills a DataSet with all Regions based on a condition.
		/// </summary>
		/// <param name="whereSql">A string with an SQL condition for the data to look up.</param>
		/// <returns>A RegionDS</returns>
		public RegionDS PopulateList(string whereSql) {
			RegionDal dao = null;
			try {
				dao = new RegionDal ();
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
