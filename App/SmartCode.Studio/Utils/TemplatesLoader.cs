/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Studio.AssemblyCache;
using System.Reflection;
using System.CodeDom.Compiler;
using SmartCode.Studio.Templates;
using System.Runtime.InteropServices;

namespace SmartCode.Studio.Utils
{
    public class TemplatesLoader 
    {
        private const string LONG_BASE_TYPE = "SmartCode.Template.TemplateBase";
        /*w/o namespaces*/
        private const string SHORT_BASE_TYPE = "TemplateBase"; 

        public TemplatesLoader()
        {
        }

        ~TemplatesLoader()
        {
            GC.SuppressFinalize(true);
        }

        public static Assembly LoadTemplateAssembly(LibraryInfo library)
        {
            Assembly assembly;
            try
            {
                if (library.IsFromFile)
                {
                    //AssemblyQualifiedName contains "file:" + fully qualified path to the assembly DLL file.
                    assembly = System.Reflection.Assembly.LoadFrom(library.AssemblyQualifiedName.Substring(5));
                    library.AssemblyName = assembly.GetName().Name;
                }
                else
                {
                    assembly = Assembly.Load(library.AssemblyQualifiedName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't load assembly: " + library.AssemblyName + Environment.NewLine + ex.Message);
            }

            return assembly;
        }

        private bool IsTemplate(Type type)
        {
            if (type.BaseType == null)
            {
                return false;
            }
            else if (type.BaseType.ToString() == LONG_BASE_TYPE || type.BaseType.ToString() == SHORT_BASE_TYPE)
            {
                return true;
            }
            else
            {
                return IsTemplate(type.BaseType);
            }
        }

        public void LoadTemplates(LibraryInfo libraryInfo)
        {
            IAssemblyEnum assemblies;
            Assembly assembly = LoadTemplateAssembly(libraryInfo);

            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    GC.Collect();
                    if (type.IsClass && !type.IsAbstract )
                    {
                        if (IsTemplate(type))
                        {
                            TemplateInfo templateInfo = new TemplateInfo(libraryInfo.AssemblyName, type.Name, type.Namespace);

                            object instance = Activator.CreateInstance(type);
                            templateInfo.Name = type.InvokeMember("Name", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, instance, null).ToString();
                            templateInfo.Description = type.InvokeMember("Description", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, instance, null).ToString();
                            templateInfo.OutputFolder = type.InvokeMember("OutputFolder", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, instance, null).ToString();

                            object members = type.InvokeMember("CreateOutputFile", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, instance, null);
                            templateInfo.CreateOutputFile = members.ToString() == "True";
                            members = type.InvokeMember("IsProjectTemplate", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, instance, null);
                            templateInfo.IsProjectTemplate = members.ToString() == "True";
                            libraryInfo.Templates.Add(templateInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {   // should get LoaderExceptions property. 
                throw new Exception("Couldn't load Templates from library: " + libraryInfo.AssemblyName
                                    + Environment.NewLine + ex.Message);
            }

            IAssemblyName asmName = null;
            int i = Controllers.GetEnumerator(out assemblies, IntPtr.Zero, IntPtr.Zero, CacheFlags.CACHE_GAC, IntPtr.Zero);
            StringBuilder sbLibrary = new StringBuilder(0x100);
            do
            {
                i = assemblies.GetNextAssembly(IntPtr.Zero, out asmName, 0);
                if (i == 0)
                {
                    uint capacity = (uint)sbLibrary.Capacity;
                    asmName.GetName(ref capacity, sbLibrary);
                    if (sbLibrary.ToString() == libraryInfo.AssemblyName)
                    {
                        Marshal.ReleaseComObject(asmName);
                        return;
                    }
                }
            }
            while (i == 0);
        }

    }
}
