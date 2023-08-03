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
    public class CSSortedList : ASPStyleTemplates
    {
        public CSSortedList()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "Generates a strongly-typed collection based on a HashTable.";
            base.Name = "CSSortedList";
            base.OutputFolder = @"CSSortedList";
        }

        public override string OutputFileName()
        {
            return Table.Code + "CSSortedList.cs";
        }

        public override string SCTFileName
        {
            get { return "/CollectionGen/CSSortedList.sct"; }
        }

        public override PropertyTable GetCustomPropertyTable()
        {
            PropertyTable properties = new PropertyTable();

            properties["ClassNamespace"] = Domain.Code;
            properties["KeyType"] = "System.String";
            properties["ItemType"] = "System.String";
            properties["ClassName"] = Table.Code;

            return properties;
        }


    }
}
