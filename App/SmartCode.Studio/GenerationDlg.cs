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
using SmartCode.Model;
using SmartCode.Studio.Templates;

namespace SmartCode.Studio
{
    public partial class GenerationDlg : Form
    {
        public GenerationDlg()
        {
            InitializeComponent();
        }

        private void uiGenerate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GenerationDlg_Load(object sender, EventArgs e)
        {
            LoadTables();
            LoadViews();
            LoadAllTemplates();
        }


        private void LoadTables()
        {
            if (this.uiChkTables.Checked)
            {
                System.Collections.ArrayList allTables = SmartCode.Studio.SmartStudio.MainForm.GetAllTables();
                foreach (TableSchema table in allTables)
                {
                    ListViewItem li = new ListViewItem(table.Name);
                    li.Tag = table;
                    li.ImageIndex = 3;
                    this.uiLVNamedObjects.Items.Add(li);
                }
            }
        }
        private void LoadViews()
        {
            if (this.uiChkViews.Checked)
            {
                System.Collections.ArrayList allViews = SmartCode.Studio.SmartStudio.MainForm.GetAllViews();
                foreach (TableSchema view in allViews)
                {
                    ListViewItem li = new ListViewItem(view.Name);
                    li.Tag = view;
                    li.ImageIndex = 3;
                    this.uiLVNamedObjects.Items.Add(li);
                }
            }
        }


        private void LoadAllTemplates()
        {
            this.uiTVEntityTemplates.Nodes.Clear();


            foreach (KeyValuePair<string, LibraryInfo> pair in SmartCode.Studio.SmartStudio.MainForm.CurrentProject.Libraries)
            {
                TreeNode libraryNode = new TreeNode(pair.Value.AssemblyName, 0, 0);
                libraryNode.Tag = pair.Value;
                this.uiTVEntityTemplates.Nodes.Add(libraryNode);

                foreach (TemplateInfo template in pair.Value.Templates)
                {
                    if (!template.IsProjectTemplate)
                    {
                        TreeNode templateNode = new TreeNode(template.Name, 1, 1);
                        templateNode.Tag = template;
                        libraryNode.Nodes.Add(templateNode);

                        foreach (NamedObject namedObject in template.AssignedObjects)
                        {
                            TreeNode namedNode = new TreeNode(namedObject.Name, 2, 2);
                            namedNode.Tag = namedObject;
                            templateNode.Nodes.Add(namedNode);
                        }
                    }
                    else
                    {
                        ListViewItem li = new ListViewItem(template.Name);
                        li.Tag = template;
                        li.ImageIndex = 2;
                        li.Checked = template.Run;
                        this.uiLVProjectTemplates.Items.Add(li);
                    }
                }

                libraryNode.ExpandAll();

            }
        }

        private void uiBtnAdd_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in this.uiLVNamedObjects.SelectedItems)
            {
                NamedObject namedObject = li.Tag as NamedObject;
                if (namedObject != null)
                {
                    foreach (TreeNode templateNode in this.uiTVEntityTemplates.SelectedNodes)
                    {
                        TemplateInfo template = templateNode.Tag as TemplateInfo;
                        if (template != null)
                        {
                            if (!template.AssignedObjects.Contains(namedObject))
                            {
                                template.AssignedObjects.Add(namedObject);
                                TreeNode namedNode = new TreeNode(namedObject.Name, 2, 2);
                                namedNode.Tag = namedObject;
                                templateNode.Nodes.Add(namedNode);
                                templateNode.ExpandAll();
                            }
                        }

                    }
                }
            }
        }

        private void uiBtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                System.Collections.ArrayList namedNodes = new System.Collections.ArrayList();
                foreach (TreeNode node in this.uiTVEntityTemplates.SelectedNodes)
                {
                    NamedObject obj = node.Tag as NamedObject;
                    if (obj != null)
                    {
                        namedNodes.Add(node);
                    }

                    TemplateInfo info = node.Tag as TemplateInfo;
                    if (info != null)
                    {
                        foreach (TreeNode namedNode in node.Nodes)
                        {
                            namedNodes.Add(namedNode);
                        }
                    }
                }
                RemoveNodes(namedNodes);
                
            }
            catch { }
        }

        private void RemoveNodes(System.Collections.ArrayList namedNodes)
        {

            foreach (TreeNode namedNode in namedNodes)
            {
                NamedObject obj = namedNode.Tag as NamedObject;
                if (obj != null)
                {
                    TemplateInfo parentTemplate = namedNode.Parent.Tag as TemplateInfo;
                    if (parentTemplate != null)
                    {
                        if (parentTemplate.AssignedObjects.Remove(obj))
                        {
                            this.uiTVEntityTemplates.SelectedNodes.Remove(namedNode);
                            namedNode.Remove();
                        }
                    }
                }
            }
        }

        private void uiLVNamedObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableArrows();
        }

        private void uiTVEntityTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EnableArrows();
        }


        /// <summary>
        /// TODO: Fix the bug
        /// </summary>
        private void EnableArrows()
        {

            //bool enableAdd = false;
            //bool enableRemove = false;

            //foreach (TreeNode node in this.uiTVEntityTemplates.SelectedNodes)
            //{
            //    if (node.Tag is TemplateInfo)
            //    {
            //        enableAdd = true;
            //    }
            //    if (node.Tag is NamedObject)
            //    {
            //        enableRemove = true;
            //    }
            //}

            //enableAdd &= uiTabTemplates.SelectedIndex == 0;
            //enableRemove &= uiTabTemplates.SelectedIndex == 0;

            //enableAdd &= uiLVNamedObjects.SelectedItems.Count > 0;

            //this.uiBtnAdd.Enabled = enableAdd;
            //this.uiBtnRemove.Enabled = enableRemove;
        }

        private void uiLVProjectTemplates_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewItem li = this.uiLVProjectTemplates.Items[e.Index];
            TemplateInfo template = li.Tag as TemplateInfo;
            if (template != null)
            {
                template.Run = e.NewValue == CheckState.Checked ;
            }

        }

        private void uiBtnRemoveAll_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList namedNodes = new System.Collections.ArrayList();
            foreach (TreeNode LibraryNode in this.uiTVEntityTemplates.Nodes)
            {
                foreach (TreeNode TemplateNode in LibraryNode.Nodes)
                {
                    foreach (TreeNode NamedNode in TemplateNode.Nodes)
                    {
                        if (NamedNode.Tag is NamedObject)
                        {
                            namedNodes.Add(NamedNode);
                        }
                    }
                }
            }
            this.RemoveNodes(namedNodes);
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            this.uiLVNamedObjects.Items.Clear();
            LoadTables();
            LoadViews();
        }

    }
}