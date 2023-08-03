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

namespace StoreProcedures.Utils
{
    internal class Common
    {
        public const string SP_NAME_PREFIX = "usp_";

        /// <summary>
        /// Generates the T-SQL code to delete a stored procedure.
        /// </summary>
        /// <param name="strDataSource">The name of the database.</param>
        /// <param name="strStoredProcName">The simple string name of the stored procedure.</param>
        /// <param name="strStoredProcNameSimple">The fully qualified name of the stored procedure.</param>
        public static string GetStoredProcedureDelete(string dataSource, string spName)
        {
            string strResult = String.Empty;

            strResult += @"" + "\n";
            strResult += @"	USE " + dataSource + "\n";
            strResult += @"" + "\n";
            strResult += @"	-- First delete the stored procedure if it already exists." + "\n";
            strResult += @"	If (EXISTS (SELECT * FROM dbo.sysobjects" + "\n";
            strResult += @"			WHERE (Name = '" + spName + "') AND (Type = 'P')))" + "\n";
            strResult += @"		DROP PROCEDURE " + spName + "\n";
            strResult += @"	GO" + "\n";

            return strResult;
        }

        /// <summary>
        /// Generates a short header for documenting a stored procedure.
        /// </summary>
        /// <param name="strCommentPrefix">The string prefix for each line of the header.</param>
        /// <param name="oLibrary">The library of the template.</param>
        /// <param name="oTemplate">The template.</param>
        /// <param name="strStoredProcPurpose">A short comment on the purpose of the stored procedure.</param>
        /// <returns></returns>
        public static string GetSimpleStoredProcedureHeader(string strCommentPrefix, TemplateBase oTemplate, string strStoredProcPurpose)
        {
            string strHeader = String.Empty;

            strHeader += strCommentPrefix + @" This stored procedure was created " + "\n";
            strHeader += strCommentPrefix + @" on " + System.DateTime.Now.ToLongDateString() + " at " + System.DateTime.Now.ToLongTimeString() + ".\n";
            strHeader += strCommentPrefix + @" Template: " + oTemplate.Name + "\n";
            strHeader += strCommentPrefix + @" Purpose: " + strStoredProcPurpose + "\n";

            return strHeader;
        }

        public static string GetSqlParameterLine(ColumnSchema column)
        {
            return String.Format ("@{0} {1} ,{2}",column.Code ,Common.GetFieldTypeAsTSQLType(column), Environment.NewLine );
        }

        /// <summary>
        /// Returns the type of the field as a T-SQL type, like varchar(length-of-field).
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetFieldTypeAsTSQLType(ColumnSchema column)
        {
            return GetFieldTypeAsTSQLType(column.OriginalSQLType, column.Length, column.Precision, column.Scale.ToString());
        }
        /// <summary>
        /// Returns the type of the field as a T-SQL type, like varchar(length-of-field).
        /// </summary>
        /// <returns>T-SQL version of the field's type</returns>
        public static string GetFieldTypeAsTSQLType(string dataType, int length, int precision, string scale)
        {
            string strReturn;

            string stypeNotIdentity = dataType.Replace("identity", "").Trim();
            switch (stypeNotIdentity)
            {
                case "bigint":
                case "bit":
                case "datetime":
                case "image":
                case "int":
                case "money":
                case "ntext":
                case "smalldatetime":
                case "smallint":
                case "smallmoney":
                case "sql_variant":
                case "sysname":
                case "text":
                case "timestamp":
                case "tinyint":
                case "float":
                case "real":
                    strReturn = stypeNotIdentity;
                    break;
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                    strReturn = dataType + "(" + length.ToString() + ")";
                    break;
                case "decimal":
                case "numeric":
                    strReturn = dataType + "(" + precision.ToString() + ", " + scale.ToString() + ")";
                    break;
                case "int identity":
                    strReturn = "int";
                    break;

                case "uniqueidentifier":
                    strReturn = "varchar(36)";
                    break;
                default:
                    strReturn = "userdefined_type";
                    break;
            }
            return strReturn;
        }

        /// <summary>
        /// Removes a 'StringToRemove' from the end of a 'StringForRemove'.
        /// </summary>
        /// <param name="StringForRemove"></param>
        /// <param name="StringToRemove"></param>
        /// <returns></returns>
        public static string Substring(string StringForRemove, string StringToRemove)
        {
            return Substring(StringForRemove, StringToRemove.Length);
        }
        /// <summary>
        /// Removes a certain number of characters from the end of a string.
        /// </summary>
        /// <param name="str">The string to operate on.</param>
        /// <param name="intCharactersToTrim">The number of characters to remove.</param>
        /// <returns></returns>
        public static string Substring(string StringForRemove, int intCharactersToRemove)
        {
            string strReturn = StringForRemove;
            if (StringForRemove.Trim().Length != 0)
            {
                strReturn = StringForRemove.Substring(0, StringForRemove.Length - intCharactersToRemove);
            }
            return strReturn;
        }
    }
}
