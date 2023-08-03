using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using System.IO;
using TaHoGen;
using System.Diagnostics;
using TaHoGen.Targets;
using System.Reflection;

namespace CollectionGen
{
    public class CSVector : ASPStyleTemplates
    {
        public CSVector()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "Generates a strongly-typed collection based on a HashTable.";
            base.Name = "CSVector";
            base.OutputFolder = @"CSVector";
        }

        public override string OutputFileName()
        {
            return Table.Code + "CSVector.cs";
        }

        public override string SCTFileName
        {
            get { return "/CollectionGen/CSVector.sct"; }
        }

        public override PropertyTable GetCustomPropertyTable()
        {
            PropertyTable properties = new PropertyTable();

            properties["ClassNamespace"] = Domain.Code;
            properties["ItemType"] = "System.String";
            properties["ClassName"] = Table.Code;

            return properties;
        }


    }
}
