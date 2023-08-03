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
using SmartCode.Model;

namespace SmartCode.Studio.Database.Info
{
    /// <summary>
    /// Describes a single column of a table.
    /// </summary>
    public struct ColumnInfo
    {
        /// <summary>
        /// Data type of the column.
        /// </summary>
        public SqlType SqlType;

        /// <summary>
        /// Data size or leght of the column.
        /// </summary>
        public int Size;
        
        /// <summary>
        /// The precision of the numeric data in this column.
        /// </summary>
        public int Precision;   //[Added by Fredy Muñoz] This field was absent.
        
        /// <summary>
        /// The scale of the numeric data in this column.
        /// </summary>
        public int Scale;
        
        /// <summary>
        /// if this column is nullable.
        /// </summary>
        public bool AllowNull;
        
        /// <summary>
        /// The name of the column.
        /// </summary>
        public string Name;
        
        /// <summary>
        /// if autoincrement column.
        /// </summary>
        public bool AutoIncrement;
        
        /// <summary>
        /// The .NET column Data type.
        /// </summary>
        public string NetDataType;
        
        /// <summary>
        /// The Original SQL type.
        /// </summary>
        public string OriginalSQLType;

        /// <summary>
        /// Default value of the column
        /// </summary>
        public string DefaultValue;
    }
}
