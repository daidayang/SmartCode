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
    public class Sample3 : TemplateBase
    {
        public Sample3()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "This template demonstrates using expression write and code constructs";
            base.Name = "Sample3";
            base.OutputFolder = @"TaHoGen101";
        }

        public override string OutputFileName()
        {
            return Table.Code + "Sample3.txt";
        }

        public override void ProduceCode()
        {

            // Read the contents of the template
            StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + "/TaHoGen101/Sample3.sct");
            // Compile it into a single assembly
            Assembly templateAssembly = TemplateCompiler.Compile(reader.ReadToEnd(), true);
            // Did it succeed?
            if (templateAssembly == null)
            {
                Console.WriteLine("Template Compilation Failed!");
                return;
            }
            Type templateType = templateAssembly.GetTypes()[0];

            // This *should* work
            Debug.Assert(templateType != null);

            // Set the properties for the template
            PropertyTable properties = new PropertyTable();
            properties["Type"] = "string";
            properties["Name"] = "MyProperty";
            properties["ReadOnly"] = false;

            // Instantiate the template and assign the properties at the same time
            object[] args = new object[] { properties };
            ITextGenerator generator = Activator.CreateInstance(templateType, args) as ITextGenerator;

            // We should have a valid generator at this point
            Debug.Assert(generator != null);

            // Write to the string
            StringTarget output = new StringTarget();

            // Attach the output of the generator to the console
            output.Attach(generator);

            // Generate the output itself
            output.Write();

            string myCode = output.ToString();

            WriteLine(myCode);
        }
    }
}
