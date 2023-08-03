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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartCode.Studio.Templates;
using SmartCode.Model;
using System.Threading;

namespace SmartCode.Studio.Engine
{
    public partial class CodeGenerationDlg : Form
    {
        delegate void CodeGenerationDelegate(GenerationArgs args);

        private SmartCodeEngine codeEngine;
        private ICodeOutput codeOutput;

        internal  CodeGenerationDlg()
        {
            this.InitializeComponent();
        }

        internal CodeGenerationDlg(ICodeOutput codeOutput)
            : this()
        {
            this.codeOutput = codeOutput;
            this.ConfigureProgressBar();
            CodeGenerationDelegate showProgress = new CodeGenerationDelegate(EngineProgress);
            codeEngine = new SmartCodeEngine(this, showProgress, SmartCode.Studio.SmartStudio.MainForm.CurrentProject);
        }

        private void CodeGenerationDlg_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(codeEngine.RunProcess));
            t.IsBackground = true;
            t.Start();
        }

        private void EngineProgress(GenerationArgs args)
        {
            if (args.StatusDone)
            {
                MessageBox.Show("The Code engine was successful executed", "Message", MessageBoxButtons.OK);
            }
            else
            {
                codeOutput.WriteToOutput(args.Output, args.Template );
                UpdateInfo(args.Output, args.Message, args.Template, args.Success );
            }
        }
 

        public void AddMessageToListView(bool success, string message, string fullFileName)
        {
            ListViewItem li;
            if (success)
            {
                li = new ListViewItem(new string[] { string.Empty, fullFileName, message }, 0);
                li.ImageIndex = 0;
            }
            else
            {
                li = new ListViewItem(new string[] { string.Empty, message, fullFileName }, 1);
                li.ImageIndex = 1;
            }
            this.uiLiResume.Items.Add(li);
        }
    
        private void uiLiResume_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem li = ((ListView)sender).SelectedItems[0];
                MessageBox.Show(li.Text.ToString() + "\n" + li.SubItems[1].Text.ToString() + "\n" + li.SubItems[2].Text.ToString(), "Code Generation Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
            }
        }

        public void UpdateInfo(OutputInfo output, string message, TemplateInfo template, bool success)
        {
            this.uiProgressBar.PerformStep();
            if (success)
            {
                if (output.CreateFile)
                {
                    this.AddMessageToListView(true, "Created successfully ", output.Folder + @"\" + output.FileName);
                }
                else
                {
                    this.AddMessageToListView(true, "Template successfully executed", template.Name);
                }
            }
            else
            {
                this.AddMessageToListView(false, "Error in Template '" + template.Name, message);
            }
        }

        internal void ConfigureProgressBar()
        {
            int total = 0;
            foreach (KeyValuePair<string, LibraryInfo> pair in SmartCode.Studio.SmartStudio.MainForm.CurrentProject.Libraries)
            {
                foreach (TemplateInfo template in pair.Value.Templates)
                {
                    total += template.AssignedObjects.Count;
                    if (template.Run)
                    {
                        total++;
                    }
                }
            }
            this.uiProgressBar.Value = 0;
            this.uiProgressBar.Minimum = 0;
            this.uiProgressBar.Maximum = total;
            this.uiProgressBar.Step = 1;
        }

    }
}