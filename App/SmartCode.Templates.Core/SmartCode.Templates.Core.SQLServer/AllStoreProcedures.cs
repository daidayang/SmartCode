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
    public class AllStoreProcedures : TemplateBase
    {
        // Constructor.
        public AllStoreProcedures()
        {
            this.IsProjectTemplate = true;
            CreateOutputFile = true;
            Description = "Generates all stored procedure for all entities in the domain";
            Name = "AllStoreProcedures";
            OutputFolder = @"Stored Procedures\AllStoreProcedures";
        }

        public override string OutputFileName()
        {
            return Domain.Name + "_AllSps.sql";
        }

        public override void ProduceCode()
        {

            foreach (TableSchema table in Domain.DatabaseSchema.Tables)
            {
                if (table.IsTable && table.PrimaryKeyColumns().Count > 0)
                {
                    RunTemplate(new DeleteRowByPrimaryKey(), table);
                    RunTemplate(new Insert(), table);
                    RunTemplate(new Update(), table);
                }
            }
        }

        private void RunTemplate(TemplateBase template, TableSchema table)
        {
            System.Collections.ArrayList results = template.Run(Domain, table);

            //The code prop, containts the output string. append the results, look for Run 
            Write(results[0].ToString());
        }
    }
}
