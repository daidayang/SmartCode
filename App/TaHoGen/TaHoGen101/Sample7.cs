using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using System.IO;
using TaHoGen;
using System.Diagnostics;
using TaHoGen.Targets;
using System.Reflection;

namespace TaHoGen101
{
    public class Sample7 : TemplateBase
    {
        public Sample7()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "Create a list of properties from a database table.";
            base.Name = "Sample7";
            base.OutputFolder = @"TaHoGen101";
        }

        public override string OutputFileName()
        {
            return Table.Code + "Sample7.cs";
        }


        public override void ProduceCode()
        {

            //Read the contents of the template
            StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + "/TaHoGen101/Sample7.sct");

            // Read the contents of the template
            Assembly templateAssembly = TemplateCompiler.Compile(reader.ReadToEnd(), "Sample7.dll", true);
            // Did it succeed?
            if (templateAssembly == null)
            {
                throw new Exception("Template Compilation Failed!");
            }
            Type templateType = templateAssembly.GetTypes()[0];

            // Set the properties for the template
            PropertyTable properties = new PropertyTable();
            properties["CurrentDomain"] = Domain;
            properties["SourceTable"] = Table;

            // Instantiate the template and assign the properties at the same time
            object[] args = new object[] { properties };
            ITextGenerator generator = Activator.CreateInstance(templateType, args) as ITextGenerator;

            // Write to the string
            StringTarget output = new StringTarget();

            // Attach the output of the generator to the console
            output.Attach(generator);

            // Generate the output itself
            output.Write();
            string code = output.ToString();
            WriteLine(code);
        }

    }
}
