/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
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

namespace SmartCode.Studio
{
    public partial class LibrariesDlg : Form
    {
        private TreeNode rootNode;
        public LibrariesDlg()
        {
            InitializeComponent();
        }

        private void LibrariesDlg_Load(object sender, EventArgs e)
        {
            LoadLibraries();
        }

        private void LoadLibraries()
        {
            this.uiTVLibraries.Nodes.Clear();

            rootNode = new TreeNode("Libraries", 0, 0);
            this.uiTVLibraries.Nodes.Add(rootNode);

            foreach (KeyValuePair<string, LibraryInfo> pair in SmartCode.Studio.SmartStudio.MainForm.CurrentProject.Libraries)
            {
                TreeNode libraryNode = new TreeNode(pair.Key, 1, 1);
                libraryNode.Tag = pair.Value;
                rootNode.Nodes.Add(libraryNode);

                foreach (TemplateInfo template in pair.Value.Templates)
                {
                    TreeNode templateNode = new TreeNode(template.Name, 2, 2);
                    templateNode.Tag = template;
                    libraryNode.Nodes.Add(templateNode);
                }

            }
        }

        private void uiTVLibraries_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.uiRemove.Enabled = e.Node.Tag is LibraryInfo;
        }

        //private void uiAdd_Click(object sender, EventArgs e)
        //{
        //    SmartCode.Studio.SmartStudio.MainForm.LoadLibrariesFromGAC();
        //    this.LoadLibraries();
        //}

        private void uiAddFromGAC_Click(object sender, EventArgs e)
        {
            SmartCode.Studio.SmartStudio.MainForm.LoadLibrariesFromGAC();
            this.LoadLibraries();
        }

        void UiAddFromFolder_Click(object sender, System.EventArgs e)
        {
            SmartCode.Studio.SmartStudio.MainForm.LoadLibrariesFromFileFolder();
            this.LoadLibraries();
        }

        private void uiRemove_Click(object sender, EventArgs e)
        {
            LibraryInfo selectedLibrary = uiTVLibraries.SelectedNode.Tag as LibraryInfo;
            SmartCode.Studio.SmartStudio.MainForm.CurrentProject.Libraries.Remove(selectedLibrary.AssemblyQualifiedName);
            this.LoadLibraries();
        }
    }
}