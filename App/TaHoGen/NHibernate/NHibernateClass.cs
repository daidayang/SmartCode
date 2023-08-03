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
    public class NHibernateClass : ASPStyleTemplates
    {
        public NHibernateClass()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "Generates a C# class for use with NHibernate.";
            base.Name = "NHibernateClass";
            base.OutputFolder = @"NHibernate\Classes";
        }

        public override string OutputFileName()
        {
            return Table.Code + ".cs";
        }

        public override string SCTFileName
        {
            get { return "/NHibernate/NHibernate.class.sct"; }
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
