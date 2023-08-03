using System;
using System.Collections.Generic;
using System.Text;
using TaHoGen;
using System.IO;
using System.Reflection;
using TaHoGen.Targets;
using SmartCode.Template;

namespace NHibernate
{
    public abstract class ASPStyleTemplates : TemplateBase
    {
        public abstract string SCTFileName{get;}

        public abstract PropertyTable GetCustomPropertyTable();

        public override void ProduceCode()
        {
            

            StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + SCTFileName);

            // Read the contents of the template
            Assembly templateAssembly = TemplateCompiler.Compile(reader.ReadToEnd());
            if (templateAssembly == null)
            {
                throw new Exception("Template Compilation Failed!");
            }
            Type templateType = templateAssembly.GetTypes()[0];

            PropertyTable properties = GetCustomPropertyTable();
  

            object[] args = new object[] { properties };
            ITextGenerator generator = Activator.CreateInstance(templateType, args) as ITextGenerator;

            StringTarget output = new StringTarget();
            output.Attach(generator);
            output.Write();
            WriteLine(output.ToString());
        }


    }
}
