using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using System.IO;
using TaHoGen;
using System.Diagnostics;
using TaHoGen.Targets;
using System.Reflection;

namespace NHibernate
{
    public class NHibernatHbm : ASPStyleTemplates
    {
        public NHibernatHbm()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "Template to generate the class to db mapping file for use with NHibernate.";
            base.Name = "NHibernatHbm";
            base.OutputFolder = @"NHibernate\Hbm";
        }

        public override string OutputFileName()
        {
            return Table.Code + ".xml";
        }

        public override string SCTFileName
        {
            get { return "/NHibernate/NHibernate.hbm.sct"; }
        }

        public override PropertyTable GetCustomPropertyTable()
        {
            PropertyTable properties = new PropertyTable();
            SmartCode.Model.Mapping.CS.CSMap csMap =
                new SmartCode.Model.Mapping.CS.CSMap(Domain);
            csMap.RunMapToCS();

            SmartCode.Model.Mapping.CS.DatabaseSchema databaseSchema = csMap.DatabaseSchema;

            properties["SourceTable"] = csMap.GetTableByName(Table.Name);

            return properties;
        }


    }
}
