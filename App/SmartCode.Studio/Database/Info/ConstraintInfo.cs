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

namespace SmartCode.Studio.Database.Info
{
    /// <summary>
    /// Describes a single foreign key of a table.
    /// </summary>
    public struct ConstraintInfo
    {
        /// <summary>
        /// Name of constraint.
        /// </summary>
        public string Name;
        /// <summary>
        /// Name of table containing coressponding primary key. 
        /// </summary>
        public string PrimaryKeyTable;
        /// <summary>
        /// Column names in the primary key table.
        /// </summary>
        public string[] PrimaryKeyTableColumns;
        /// <summary>
        /// Foreign key column names in the table having foreign keys.
        /// </summary>
        public string[] Columns;
        /// <summary>
        /// if foreign key is cascade on delete;
        /// </summary>
        public bool OnDeleteCascade;
        /// <summary>
        /// if foreign key is cascade on update;
        /// </summary>
        public bool OnUpdateCascade;
    }
}
