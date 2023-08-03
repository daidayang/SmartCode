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
using SmartCode.Templates.Core.SQLServer.Utils;
using SmartCode.Model;

namespace SmartCode.Templates.Core.SQLServer
{
    public class SelectRowsByWhere : TemplateBase
    {
        // Constructor.
        public SelectRowsByWhere()
        {
            CreateOutputFile = true;
            Description = "Generates a stored procedure to retrieve All Rows";
            Name = "Retrieve All Rows";
            OutputFolder = @"Stored Procedures\SelectRowsByWhere";
        }

        public override string OutputFileName()
        {
            return Table.Name + "_SelectRowsByWhere.sql";
        }

        public override void ProduceCode()
        {
            WriteLine("SET QUOTED_IDENTIFIER ON ");
            WriteLine("GO");
            WriteLine("SET ANSI_NULLS ON ");
            WriteLine("GO");

            string spName = Common.SP_NAME_PREFIX + Table.Code + "_SelectRowsByWhere";
            string spPurpose = "Get All Rows from the table " + Table.Name;

            WriteLine("-- Stored Procedure " + spName);
            WriteLine("-- Purpose: " + spPurpose);
            WriteLine("-- Parameters:");

            // First, delete the stored procedure, if it exists
            WriteLine(Common.GetStoredProcedureDelete(Domain.DatabaseSchema.ConnectionInfo.Database, spName));

            WriteLine(Common.GetSimpleStoredProcedureHeader("--", this, spPurpose));
            WriteLine(" CREATE PROCEDURE " + spName);
            WriteLine(" @SqlWhere varchar(250)");
            WriteLine(" AS");
            WriteLine("	IF NOT @SqlWhere = ''"); 
            WriteLine("	    exec('" + Common.GetComplexSelect(this.Table) + " WHERE ' + @SqlWhere)");
            WriteLine("	ELSE ");
            WriteLine("	    exec('" + Common.GetComplexSelect(this.Table) + "')");
            WriteLine();
            WriteLine("	GO ");
            WriteLine();
            WriteLine("-- End Procedure");
        }
    }
}
