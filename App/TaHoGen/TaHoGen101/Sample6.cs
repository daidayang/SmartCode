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
    //*****************************WE ARE USING PROJECT LEVEL TEMPLATE base.IsProjectTemplate = true ********************************
    //This Template will be displayed in the project Templates Tab, in the Setting Code Generator Dialog
    public class Sample6 : TemplateBase
    {
        public Sample6()
        {
            base.IsProjectTemplate = true;
            base.CreateOutputFile = true;
            base.Description = "Create an enum of tables.";
            base.Name = "Sample6";
            base.OutputFolder = @"TaHoGen101";
        }

        public override string OutputFileName()
        {
            return Domain.Code + "Sample6.cs";
        }


        public override void ProduceCode()
        {

            //Read the contents of the template
            StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + "/TaHoGen101/Sample6.sct");

            // Read the contents of the template
            Assembly templateAssembly = TemplateCompiler.Compile(reader.ReadToEnd(), "Sample6.dll", true);
            // Did it succeed?
            if (templateAssembly == null)
            {
                throw new Exception("Template Compilation Failed!");
            }
            Type templateType = templateAssembly.GetTypes()[0];

            // Set the properties for the template
            PropertyTable properties = new PropertyTable();
            properties["CurrentDomain"] = Domain;

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
