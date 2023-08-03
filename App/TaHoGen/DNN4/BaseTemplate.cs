using System;
using System.Collections.Generic;
using System.Text;
using TaHoGen;
using System.IO;
using System.Reflection;
using TaHoGen.Targets;
using SmartCode.Template;

namespace DNN4
{
    public abstract class BaseTemplate : TemplateBase
    {

        protected PropertyTable GetCustomPropertyTable()
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
