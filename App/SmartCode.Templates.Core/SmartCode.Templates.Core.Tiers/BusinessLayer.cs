/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;
using SmartCode.Templates.Core.SQLServer.Utils;

namespace SmartCode.Templates.Core.Tiers
{
    public class BusinessLayer : TemplateBase
    {
        public BusinessLayer()
        {
            CreateOutputFile = true;
            Description = "The Business layer";
            Name = "Business Layer";
            OutputFolder = @"Biz";
            IsProjectTemplate = true;
        }

        public override string OutputFileName()
        {
            return base.Entity.Code + "Biz.cs";
        }

        public override void ProduceCode()
        {
            if (! Table.IsTable)
            {
                throw new Exception ("' The used for this class is only available for tables, not for views.");
            }

            WriteLine(@"namespace Businesslayer{");
            WriteLine(@"    using System;");
            WriteLine(@"    using System.Data;");
            WriteLine(@"    using System.Data.SqlClient;");
            WriteLine(@"    using System.Collections;");
            WriteLine(@"    using DataAccess;");
            WriteLine(@"    using Common;");
            WriteLine();

            WriteLine(@"	/// <summary>");
            WriteLine(@"	/// " + Table.Code + @"s Business Class  ");
            WriteLine(@"	/// </summary>");

            WriteLine(@"	public class " + Table.Code + @"Biz {");
            WriteLine();
            WriteLine(@"        #region Constructor");
            WriteLine(@"        /// <summary>");
            WriteLine(@"        /// Initializes a new instance of the <see cref=""" + Table.Code + @"Biz""/> ");
            WriteLine(@"        /// class with the specified <see cref=""Biz""/>.");
            WriteLine(@"        /// </summary>");
            WriteLine(@"        public " + Table.Code + @"Biz()");
            WriteLine(@"        {");
            WriteLine(@"        }");

            WriteLine(@"        #endregion");
            WriteLine();

            WriteLine(@"        #region Persist Methods");
            WriteLine();

            WriteLine(@"        /// <summary>");
            WriteLine(@"		/// A method for inserting and updating " + Table.Code + @" data.");
            WriteLine(@"		/// Multiple rows may be inserted or updated simultaneously.");
            WriteLine(@"		/// </summary>");
            WriteLine(@"		/// <param name=""updates"">A DataSet containing a " + Table.Code + @"DataTable with data to insert or update.</param>");
            WriteLine(@"		public " + Table.Code + @"DS Persist(" + Table.Code + @"DS updates) {");
            WriteLine(@"			" + Table.Code + @"Dal dao = null;");
            WriteLine(@"			try {");
            WriteLine(@"				dao = new " + Table.Code + @"Dal ();");
            WriteLine(@"				return dao.Persist(updates);");
            WriteLine(@"			}");
            WriteLine(@"			catch (Exception ex) ");
            WriteLine(@"			{");
            WriteLine(@"				throw new Exception (ex.Message ,ex);");
            WriteLine(@"			}");
            WriteLine(@"			finally ");
            WriteLine(@"			{");
            WriteLine(@"				if (dao != null) dao.Dispose();");
            WriteLine(@"			}");

            WriteLine(@"        }");
            WriteLine();

            WriteLine(@"		/// <summary>");
            WriteLine(@"		/// A method for inserting and updating " + Table.Code + @" data.");
            WriteLine(@"		/// Multiple rows may be inserted  simultaneously.");
            WriteLine(@"		/// </summary>");
            WriteLine(@"		/// <param name=""updates"">A DataSet containing a " + Table.Code + @"DataTable with data to insert or update.</param>");
            WriteLine(@"		public " + Table.Code + @"DS Insert" + Table.Code + @"(" + Table.Code + @"DS ds) {");
            WriteLine(@"			" + Table.Code + @"Dal dao = null;");
            WriteLine(@"			try {");
            WriteLine(@"				dao = new " + Table.Code + @"Dal ();");
            WriteLine(@"				return dao.Insert" + Table.Code + @"(ds);");
            WriteLine(@"			}");
            WriteLine(@"			catch (Exception ex) ");
            WriteLine(@"			{");
            WriteLine(@"				throw new Exception (ex.Message ,ex);");
            WriteLine(@"			}");
            WriteLine(@"			finally ");
            WriteLine(@"			{");
            WriteLine(@"				if (dao != null) dao.Dispose();");
            WriteLine(@"			}");

            WriteLine(@"        }");
            WriteLine();

            WriteLine(@"		/// <summary>");
            WriteLine(@"		/// A method for updating " + Table.Code + @" data.");
            WriteLine(@"		/// Multiple rows may be  updated simultaneously.");
            WriteLine(@"		/// </summary>");
            WriteLine(@"		/// <param name=""updates"">A DataSet containing a " + Table.Code + @"DataTable with data to insert or update.</param>");
            WriteLine(@"		public " + Table.Code + @"DS Update" + Table.Code + @"(" + Table.Code + @"DS ds) {");
            WriteLine(@"			" + Table.Code + @"Dal dao = null;");
            WriteLine(@"			try {");
            WriteLine(@"				dao = new " + Table.Code + @"Dal ();");
            WriteLine(@"				return dao.Update" + Table.Code + @"(ds);");
            WriteLine(@"			}");
            WriteLine(@"			catch (Exception ex) ");
            WriteLine(@"			{");
            WriteLine(@"				throw new Exception (ex.Message ,ex);");
            WriteLine(@"			}");
            WriteLine(@"			finally ");
            WriteLine(@"			{");
            WriteLine(@"				if (dao != null) dao.Dispose();");
            WriteLine(@"			}");

            WriteLine(@"        }");
            WriteLine();

            WriteLine(@"		/// <summary>");
            WriteLine(@"		/// A method for delete " + Table.Code + @" data.");
            WriteLine(@"		/// Multiple rows may be deleted  simultaneously.");
            WriteLine(@"		/// </summary>");
            WriteLine(@"		/// <param name=""updates"">A DataSet containing a " + Table.Code + @"DataTable with data to insert or update.</param>");
            WriteLine(@"		public void Delete" + Table.Code + @"(" + Table.Code + @"DS ds) {");
            WriteLine(@"			" + Table.Code + @"Dal dao = null;");
            WriteLine(@"			try {");
            WriteLine(@"				dao = new " + Table.Code + @"Dal ();");
            WriteLine(@"				dao.Delete" + Table.Code + @"(ds);");
            WriteLine(@"			}");
            WriteLine(@"			catch (Exception ex) ");
            WriteLine(@"			{");
            WriteLine(@"				throw new Exception (ex.Message ,ex);");
            WriteLine(@"			}");
            WriteLine(@"			finally ");
            WriteLine(@"			{");
            WriteLine(@"				if (dao != null) dao.Dispose();");
            WriteLine(@"			}");

            WriteLine(@"        }");
            WriteLine(@"        #endregion");
            WriteLine();
            WriteLine(@"        #region Get Methods");
            WriteLine();
            string pkArguments = String.Empty;
            string pkArgumentsWOTypes = String.Empty;
            bool isFirst = true;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsPrimaryKey)
                {
                    if (!isFirst)
                    {
                        pkArguments += ", ";
                        pkArgumentsWOTypes += ", ";
                    }
                    pkArguments += column.NetDataType + " " + column.Code.ToLower() ;
                    pkArgumentsWOTypes += column.Code.ToLower();
                    isFirst = false;

                }
            }

            WriteLine(@"        /// <summary>");
            WriteLine(@"		/// Fills a DataSet with a " + Table.Code + @" by its primary-key attributes:" + pkArguments);
            WriteLine(@"		/// </summary> ");
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsPrimaryKey)
                {
                    WriteLine(@"		/// <param name=""" + column.Code.ToLower() + @""">The " + column.Code + @"</param>");
                }
            }
            WriteLine(@"		/// <returns>A " + Table.Code + @"DS</returns>");
            WriteLine(@"		public " + Table.Code + @"DS Populate(" + pkArguments + ") {");
            WriteLine(@"			" + Table.Code + @"Dal dao = null;");
            WriteLine(@"			try {");
            WriteLine(@"				dao = new " + Table.Code + @"Dal ();");
            WriteLine(@"				return dao.Populate(" + pkArgumentsWOTypes + ");");
            WriteLine(@"			}");
            WriteLine(@"			catch (Exception ex) ");
            WriteLine(@"			{");
            WriteLine(@"				throw new Exception (ex.Message ,ex);");
            WriteLine(@"			}");
            WriteLine(@"			finally ");
            WriteLine(@"			{");
            WriteLine(@"				if (dao != null) dao.Dispose();");
            WriteLine(@"			}");
            WriteLine(@"		}");
            WriteLine(@"");
            WriteLine(@"		/// <summary>");
            WriteLine(@"		/// Fills a DataSet with all " + Table.Code + @"s based on a condition.");
            WriteLine(@"		/// </summary>");
            WriteLine(@"		/// <param name=""whereSql"">A string with an SQL condition for the data to look up.</param>");
            WriteLine(@"		/// <returns>A " + Table.Code + @"DS</returns>");
            WriteLine(@"		public " + Table.Code + @"DS PopulateList(string whereSql) {");
            WriteLine(@"			" + Table.Code + @"Dal dao = null;");
            WriteLine(@"			try {");
            WriteLine(@"				dao = new " + Table.Code + @"Dal ();");
            WriteLine(@"				return dao.PopulateList(whereSql);");
            WriteLine(@"			}");
            WriteLine(@"			catch (Exception ex) ");
            WriteLine(@"			{");
            WriteLine(@"				throw new Exception (ex.Message ,ex);");
            WriteLine(@"			}");
            WriteLine(@"			finally ");
            WriteLine(@"			{");
            WriteLine(@"				if (dao != null) dao.Dispose();");
            WriteLine(@"			}");
            WriteLine(@"		}");
            WriteLine();
            WriteLine(@"        #endregion");
            WriteLine();

            WriteLine(@"        #region Get Methods for each child");
            foreach (ColumnSchema column in Table.Columns())
            {
                if (! column.CustomProperties.SearchBy )
                    continue;
                WriteLine(@"		/// <summary>");
                WriteLine(@"		/// Fills a DataSet based on the value of " + column.Code);
                WriteLine(@"		/// </summary>");
                WriteLine(@"		/// <returns>A " + Table.Code + @"DS</returns>");
                WriteLine(@"		public " + Table.Code + @"DS PopulateBy" + column.Code + @"(" + column.NetDataType + " " + column.Code + ") {");
                WriteLine(@"            " + Table.Code + @"Dal dao = null;");
                WriteLine(@"			try {");
                WriteLine(@"				dao = new " + Table.Code + @"Dal ();");
                WriteLine(@"				return dao.PopulateBy" + column.Code + @"( " + column.Code + ");");
                WriteLine(@"			}");
                WriteLine(@"			catch (Exception ex) ");
                WriteLine(@"			{");
                WriteLine(@"				throw new Exception (ex.Message ,ex);");
                WriteLine(@"			}");
                WriteLine(@"			finally ");
                WriteLine(@"			{");
                WriteLine(@"				if (dao != null) dao.Dispose();");
                WriteLine(@"			}");
                WriteLine(@"		}");
                WriteLine();

            }
            WriteLine(@"        #endregion");


            WriteLine(@"    }");

            WriteLine(@"}");
           
        }


    }
}
