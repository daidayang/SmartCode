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
    public class ModuleController : BaseTemplate
    {
        public ModuleController()
        {
            base.IsProjectTemplate = false;
            base.CreateOutputFile = true;
            base.Description = "";
            base.Name = "ModuleController";
            base.OutputFolder = @"DNN4\ModuleController";
        }

        public override string OutputFileName()
        {
            return Table.Code + ".vb";
        }

        public override void ProduceCode()
        {

            StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + "/DNN4/ModuleController.vb.sct");

            // Read the contents of the template
            Assembly templateAssembly = TemplateCompiler.Compile(reader.ReadToEnd());
            if (templateAssembly == null)
            {
                throw new Exception("Template Compilation Failed!");
            }
            Type templateType = templateAssembly.GetTypes()[0];

            PropertyTable properties = base.GetCustomPropertyTable();

            object[] args = new object[] { properties };
            ITextGenerator generator = Activator.CreateInstance(templateType, args) as ITextGenerator;

            StringTarget output = new StringTarget();
            output.Attach(generator);
            output.Write();
            WriteLine(output.ToString());
        }


    }
}
