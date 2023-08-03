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
    public class Sample1 : TemplateBase
    {
        public Sample1()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "This template demonstrates using expression write and code constructs";
            base.Name = "Sample1";
            base.OutputFolder = @"TaHoGen101";
        }

        public override string OutputFileName()
        {
            return Table.Code + "Sample1.txt";
        }

        public override void ProduceCode()
        {

            // Read the contents of the template
            StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + "/TaHoGen101/Sample1.sct");
            // Compile it into a single assembly
            Assembly templateAssembly = TemplateCompiler.Compile(reader.ReadToEnd(), true);
            // Did it succeed?
            if (templateAssembly == null)
            {
                Debug.WriteLine("Template Compilation Failed!");
                return;
            }
            Type templateType = templateAssembly.GetTypes()[0];

            // Instantiate the template and assign the properties at the same time
            ITextGenerator generator = Activator.CreateInstance(templateType) as ITextGenerator;

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
