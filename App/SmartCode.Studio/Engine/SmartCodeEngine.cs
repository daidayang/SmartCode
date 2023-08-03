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
using SmartCode.Studio.Templates;
using SmartCode.Model;
using System.Reflection;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace SmartCode.Studio.Engine
{
    internal class SmartCodeEngine
    {
        private CodeGenerationDlg sender;
        private Delegate senderDelegate = null;

        private Project project;

        internal SmartCodeEngine(CodeGenerationDlg sender, Delegate senderDelegate, Project project)
        {
            this.sender = sender;
            this.senderDelegate = senderDelegate;
            this.project = project;
        }

        /// Method for ThreadStart delegate
        /// </summary>
        internal void RunProcess()
        {
            Thread.CurrentThread.IsBackground = true;
            this.RunCodeEngine();
        }


        internal void RunCodeEngine()
        {
            OutputInfo outputInfo = new OutputInfo();
            try
            {
                foreach (KeyValuePair<string, LibraryInfo> pair in project.Libraries)
                {
                    LibraryInfo library = pair.Value;

                    //Optimization 
                    Assembly assembly = Utils.TemplatesLoader.LoadTemplateAssembly(library);   
 
                    foreach (TemplateInfo template in library.Templates)
                    {
                        if (!template.IsProjectTemplate)
                        {
                            for (int i = 0; i < template.AssignedObjects.Count; i++)
                            {
                                try
                                {
                                    if (template.AssignedObjects[i] is NamedObject)
                                    {
                                        outputInfo = SmartCodeEngine.GenerateCode(assembly, template, this.project.Domain, template.AssignedObjects[i]);
                                        this.sender.BeginInvoke(this.senderDelegate, new object[] { new GenerationArgs(template, outputInfo, "Success", true, false) });

                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.sender.BeginInvoke(this.senderDelegate, new object[] { new GenerationArgs(template, outputInfo, ex.Message, false, false) });
                                }
                            }
                        }
                        else if (template.Run)
                        {
                            try
                            {

                                outputInfo = SmartCodeEngine.GenerateCode(assembly, template, this.project.Domain, null);
                                this.sender.BeginInvoke(this.senderDelegate, new object[] { new GenerationArgs(template, outputInfo, "Success", true, false) });
                            }
                            catch (Exception ex)
                            {
                                this.sender.BeginInvoke(this.senderDelegate, new object[] { new GenerationArgs(template, outputInfo, ex.Message, false, false) });
                            }
                        }
                    }
                }

            }
            finally
            {
                //Process.Start("Explorer", this.folderBrowserDialog.SelectedPath);
                //The end
                this.sender.BeginInvoke(this.senderDelegate, new object[] { new GenerationArgs(null, outputInfo, "Success", true, true) });
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="assemblyName"></param>
        /// <param name="domain"></param>
        /// <param name="namedObject"></param>
        /// <returns></returns>
        private static OutputInfo GenerateCode( Assembly assembly, TemplateInfo template,  Domain domain, NamedObject namedObject)
        {
            try
            {
                OutputInfo outputInfo = new OutputInfo();
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass && (type.Name == template.ClassName))
                    {
                        object handle = Activator.CreateInstance(type);
                        object obj = type.InvokeMember("Run", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, handle, new object[] { domain, namedObject});
                        ArrayList returnValues = (ArrayList)obj;
                        outputInfo.Code = returnValues[0].ToString();
                        outputInfo.FileName = returnValues[1].ToString();
                        outputInfo.Folder = returnValues[2].ToString();
                        outputInfo.CreateFile = returnValues[3].ToString() == "True";
                    }
                }
                return outputInfo;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }
    }
}
