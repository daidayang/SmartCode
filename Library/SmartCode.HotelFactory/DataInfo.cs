using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace SmartCode.Database
{
    public class DataInfo : TemplateBase
    {
        // Constructor.
        public DataInfo()
        {
            this.CreateOutputFile = true;
            this.Description = "Generates a C# class file wraps the info db class";
            this.Name = "C# Data Class";
            this.OutputFolder = @"Data";
        }

        public override string OutputFileName()
        {
            return Table.Name + "Info.cs";
        }

        public override void ProduceCode()
        {
           
//            if (Table.PrimaryKeyColumns().Count > 0)
//            {
                WriteLine("using System;");
                WriteLine("using System.Collections.Generic;");
                WriteLine("using System.Text;");
                WriteLine();
                WriteLine("namespace Core.ResService.BusinessEntities");
                WriteLine("{");
                WriteLine();
//                WriteLine("    [Serializable]");

                WriteLine("    public partial class {0}Info", Table.Name);
                WriteLine("    {");
                WriteLine();
                WriteLine("        #region More fields");
                WriteLine("        #endregion");
                WriteLine();
                WriteLine("    }");
                WriteLine("}");
//            }
//            else
//            {
//                WriteLine("-- Entity " + Entity.Name + " does not have a primary key.");
//            }
        }

    }
}
