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
    public class Sample2 : TemplateBase
    {
        public Sample2()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "This template demonstrates using properties in your template.";
            base.Name = "Sample2";
            base.OutputFolder = @"TaHoGen101";
        }

        public override string OutputFileName()
        {
            return Table.Code + "Sample2.txt";
        }

        public override void ProduceCode()
        {
            // Read the contents of the template
            StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + "/TaHoGen101/Sample2.sct");

            Assembly templateAssembly = TemplateCompiler.Compile(reader.ReadToEnd(), true);
            // Did it succeed?
            if (templateAssembly == null)
            {
                Console.WriteLine("Template Compilation Failed!");
                return;
            }
            Type templateType = templateAssembly.GetTypes()[0];

            // Set the properties for the template
            PropertyTable properties = new PropertyTable();
            properties["TableName"] = Table.Name ;

            // Instantiate the template and assign the properties at the same time
            object[] args = new object[] { properties };
            ITextGenerator generator = Activator.CreateInstance(templateType, args) as ITextGenerator;

            // Write to the string
            StringTarget output = new StringTarget();

            // Attach the output of the generator to the console
            output.Attach(generator);

            // Generate the output itself
            output.Write();

            WriteLine(output.ToString());
        }
    }
}
