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
using SmartCode.Studio.Database;
using SmartCode.Studio.Database.MSSQL;
using SmartCode.Studio.Controls;
using SmartCode.Model;
using System.Collections;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using SmartCode.Studio.AssemblyCache;
using SmartCode.Studio.Templates;
using SmartCode.Studio.Utils;
using SmartCode.Studio.Engine;
using System.Diagnostics;
using SmartCode.Model.Profile;
using System.Globalization;

namespace SmartCode.Studio
{
    public partial class SmartStudio : Form
    {
        private Driver driver;
        private Project currentProject;
        private static SmartCode.Studio.SmartStudio mainForm;
        private static Preferences m_preferences;

        private ExplorerTreeNode currentNode;

        public SmartStudio()
        {
            InitializeComponent();

            if (SmartStudio.Preferences != null)
            {
                SmartStudio.Preferences.Load();
                this.ReloadMenuRecentProjects();
            }
        }

        internal static Preferences Preferences
        {
            get
            {
                if (SmartCode.Studio.SmartStudio.m_preferences == null)
                {
                    SmartCode.Studio.SmartStudio.m_preferences = new Preferences();
                }
                return SmartCode.Studio.SmartStudio.m_preferences;
            }
        }

        public Project CurrentProject
        {
            get { return currentProject; }
        }

        internal static SmartCode.Studio.SmartStudio MainForm
        {
            get
            {
                if (SmartCode.Studio.SmartStudio.mainForm == null)
                {
                    SmartCode.Studio.SmartStudio.mainForm = new SmartCode.Studio.SmartStudio();
                }
                return SmartCode.Studio.SmartStudio.mainForm;
            }
        }

        /// <summary>
        /// Create a new Project
        /// </summary>
        private void CreateNewProject()
        {
            DriverDlg dlg = new DriverDlg();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                this.driver = dlg.SelectedDriver;
                EntitiesDlg entitiesDlg = new EntitiesDlg(this.driver);
                if (entitiesDlg.ShowDialog(this) == DialogResult.OK)
                {
                    string[] selectedTables = entitiesDlg.GetSelectedTables();
                    string[] selectedViews = entitiesDlg.GetSelectedViews();
                    if (this.BuildDomain(selectedTables, selectedViews))
                    {
                        this.currentProject.Domain.Name = dlg.DomainName;
                        this.currentProject.Domain.Code = this.currentProject.Domain.Name.Replace("-", "").Replace("_", "").Replace(" ",String.Empty);
                        DisplayProperties(null);
                        Common.BuildTreeFromDomain(this.uiTvExplorer, this.currentProject.Domain, true);
                        SetControls(true);
                        
                    }
                }
                entitiesDlg.Dispose();

            }
            dlg.Dispose();
        }

        /// <summary>
        /// Build Domain from the tables and views
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="views"></param>
        /// <returns></returns>
        private bool BuildDomain(string[] tables, string[] views)
        {
            BuildDomainDlg dlg = new BuildDomainDlg(this.driver,  tables, views);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Domain domain = dlg.GetDomain();
                this.currentProject = new Project(domain);
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        private void SetControls(bool enabled)
        {
            this.saveAsToolStripMenuItem.Enabled = enabled;
            this.saveToolStripMenuItem.Enabled = enabled;
            this.loadLibraryToolStripMenuItem.Enabled = enabled;
            this.viewLibrariesToolStripMenuItem.Enabled = enabled;
            this.generateToolStripMenuItem.Enabled = enabled;
            this.uiBtnSave.Enabled = enabled;
            this.uiBtnSaveAs.Enabled = enabled;
            this.uiBtnAddLibraries.Enabled = enabled;
            this.uiBtnLibrarySetting.Enabled = enabled;
            this.uiBtnGenerate.Enabled = enabled;
            this.refreshProjectToolStripMenuItem.Enabled = enabled;
            this.addControlToolStripMenuItem.Enabled = enabled;
        }

        /// <summary>
        /// Return all tables defined into domain
        /// </summary>
        /// <returns></returns>
        internal ArrayList GetAllTables()
        {
            return this.currentProject.Domain.DatabaseSchema.GetAllTables();
        }

        /// <summary>
        /// Return all views defined into domain
        /// </summary>
        /// <returns></returns>
        internal ArrayList GetAllViews()
        {
            return this.currentProject.Domain.DatabaseSchema.GetAllViews();
        }

        private void SetSelectedPropertyObject(object p)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        private void uiTvExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.uiTxtCaption.Text = string.Empty;
            this.currentNode = ((TreeView)sender).SelectedNode as ExplorerTreeNode;
            this.CleanPanel();
            if (currentNode == null)
            {
                return;
            }

            this.DisplayProperties(this.currentNode);
            this.SetMainPanel(this.currentNode);
            SetExplorerToolBar(this.currentNode);
        }

        private void uiTvExplorer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode selectedNode = uiTvExplorer.GetNodeAt(p);
                if (selectedNode != null)
                {
                    // Highlight the node that the user clicked.
                    //The node is highlighted until the Menu is displayed on the screen
                    TreeNode m_OldSelectNode = uiTvExplorer.SelectedNode;
                    uiTvExplorer.SelectedNode = selectedNode;
                    if (selectedNode.Tag is TableSchema)
                    {
                        this.uiTableMenu.Show(uiTvExplorer, p);
                    }
                    else if (selectedNode.Tag is ColumnSchema)
                    {
                        this.uiColumnMenu.Show(uiTvExplorer, p);
                    }
                    else if (selectedNode.Tag is ReferenceSchema)
                    {
                        this.uiReferenceMenu.Show(uiTvExplorer, p);
                    }
                    else if (selectedNode.Tag is ReferenceJoin)
                    {
                        this.uiReferenceJoinMenu.Show(uiTvExplorer, p);
                    }
                    else if (selectedNode.Tag is ControlBase)
                    {
                        this.uiControlMenu.Show(uiTvExplorer, p);
                    }

                    // Highlight the selected node
                    uiTvExplorer.SelectedNode = m_OldSelectNode;
                    m_OldSelectNode = null;
                }
            }
        }

      

        private void SetExplorerToolBar(TreeNode tn)
        {
            bool enabled = false;

            enabled |= tn.Tag is ColumnSchema;
            enabled |= tn.Tag is ReferenceSchema;
            enabled |= tn.Tag is ReferenceJoin;
            enabled |= tn.Tag is TableSchema;

            if (tn.Tag is ControlBase)
            {
                enabled |= (tn.Tag as ControlBase).IsCustomControl;
            }

        }

        /// <summary>
        /// Set the main panel
        /// </summary>
        /// <param name="node"></param>
        internal void DisplayProperties(ExplorerTreeNode node)
        {
            if (node == null)
            {
                this.SetSelectedPropertyObject(null);
                this.uiTxtCaption.Text = string.Empty;
            }
            else
            {
                this.uiPGNamedObject.SelectedObject = node.PropertyWrapper;
                this.uiPGNamedObject.Refresh();
                this.uiTxtCaption.Text = node.Text;
            }

        }


        private void SetMainPanel(ExplorerTreeNode node)
        {
            if (node is ExplorerTreeNode)
            {
                this.uiPGNamedObject.Dock = DockStyle.Fill;
                this.uiPGNamedObject.Visible = true;
            }
        }

        private void CleanPanel()
        {
            foreach (Control ctl in this.uiPnlMain.Controls)
            {
                ctl.Visible = false;
            }
        }

        private void SmartStudio_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.CleanPanel();
            Splash splash = new Splash();
            splash.ShowDialog(this);
            splash.Dispose();
        }

        private void OpenProject()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Smart Code Project";
            dialog.Filter = "All files(*.scp)|*.scp";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.LoadProjectFromFilename(dialog.FileName);
                    
                    
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                dialog.Dispose();
            }

        }

  

        private void LoadProjectFromFilename(string fileName)
        {
            FileStream stream = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                stream = File.Open(fileName, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                this.currentProject = (Project)formatter.Deserialize(stream);
                this.currentProject.FileName = fileName;

                SmartStudio.Preferences.RecentProjects.Add(fileName);
                //TODO: Ask if Save
                RefreshExplorer();
            }
            catch (FileNotFoundException ex)
            {
                SmartStudio.Preferences.RecentProjects.Remove(fileName);
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                SmartStudio.Preferences.RecentProjects.Remove(fileName);
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                this.Cursor = Cursors.Default;
            }

        }

        public void SaveProject(bool saveAs)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;
            string fileName = "";
            try
            {
                if (string.IsNullOrEmpty(this.currentProject.FileName) || saveAs)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Title = "Save Project As...";
                    dialog.Filter = "All files(*.scp)|*.scp";
                    dialog.FilterIndex = 2;
                    dialog.RestoreDirectory = true;
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    fileName = dialog.FileName;
                }
                else
                {
                    fileName = this.currentProject.FileName;
                }
                stream = File.Open(fileName, FileMode.Create);
                this.currentProject.FileName = fileName;
                formatter.Serialize(stream, this.currentProject);
                SmartStudio.Preferences.RecentProjects.Add(fileName);
                MessageBox.Show("Save successful.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                this.Cursor = Cursors.Default;
            }
        }

        internal void LoadLibrariesFromGAC()
        {
            GACDlg dlg = new GACDlg();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ArrayList selectedLibraries = dlg.GetSelectedLibraries();
                LoadLibrariesFromList(selectedLibraries);
            }
        }


        internal void LoadLibrariesFromFileFolder()
        {
            System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.InitialDirectory = Path.GetDirectoryName(this.CurrentProject.FileName);
            dlg.Filter = "Libraries (*.dll)|*.dll|All Files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.Multiselect = true;
            dlg.Title = "Select Template Libraries";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ArrayList selectedLibraries = new ArrayList(dlg.FileNames.Length);
                foreach (string filename in dlg.FileNames)
                {
                    LibraryInfo lInfo = new LibraryInfo(Path.GetFileNameWithoutExtension(filename), "File:" + filename);
                    selectedLibraries.Add(lInfo);
                }
                this.LoadLibrariesFromList(selectedLibraries);
            }
        }

        private void LoadLibrariesFromList(ArrayList libList)
        {
            if (libList.Count == 0) return;

            TemplatesLoader libraryLoader = new TemplatesLoader();
            foreach (LibraryInfo library in libList)
            {
                try
                {
                    libraryLoader.LoadTemplates(library);
                    if (!this.currentProject.Libraries.Keys.Contains(library.AssemblyName))
                    {
                        this.currentProject.AddLibrary(library);
                    }
                    else
                    {
                        MessageBox.Show("The Library " + library.AssemblyName + " exists in the project, please Remove from View Libraries Dialog.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        internal void GenerateCode()
        {
            try
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowNewFolderButton = true;
                folderBrowserDialog.SelectedPath = SmartStudio.Preferences.LastDirectorySelected;
                folderBrowserDialog.Description = "Select the destination directory for code generation.";
                if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                {
                    SmartStudio.Preferences.LastDirectorySelected = folderBrowserDialog.SelectedPath;

                    ICodeOutput codeOuptut = new FileOutput(folderBrowserDialog.SelectedPath);
                    CodeGenerationDlg codeGenerationDlg = new CodeGenerationDlg(codeOuptut);
                    codeOuptut.ShowOutput();
                    codeGenerationDlg.ShowDialog(this);
                    codeGenerationDlg.Dispose();

                }
            }
            catch
            {

            }
        }

 
        private void NewProject_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void SaveProject_Click(object sender, EventArgs e)
        {
            SaveProject(false);
        }

        private void SaveAsProject_Click(object sender, EventArgs e)
        {
            SaveProject(true);
        }

        private void CloseProject_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadLibrary_Click(object sender, EventArgs e)
        {
            ViewLibraries();
        }

        private void ViewLibraries_Click(object sender, EventArgs e)
        {
            ViewLibraries();
        }

        internal void ViewLibraries()
        {
            LibrariesDlg dlg = new LibrariesDlg();
            dlg.ShowDialog(this);
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if (this.CurrentProject.Libraries.Count == 0)
            {
                string message = "There are not templates libraries loaded." + System.Environment.NewLine ;
                message += "All code generation is based on templates assigned to entities." + System.Environment.NewLine;
                message += "In the next windows please load templates libraries. They are localizated in the Global Assembly Cache.";
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewLibraries();
            }
            else
            {
                GenerationDlg dlg = new GenerationDlg();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    this.GenerateCode();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDlg dlg = new AboutDlg();
            dlg.ShowDialog(this);
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.kontac.net/forum");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The method or operation is not implemented.");
        }

 
        private void RefreshExplorer()
        {
            this.uiTvExplorer.Nodes.Clear();
            this.CleanPanel();
            DisplayProperties(null);
            Common.BuildTreeFromDomain(this.uiTvExplorer, this.currentProject.Domain, true);
            SetControls(true);
        }

        private void AddControl()
        {
            ControlsDlg dlg = new ControlsDlg();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                TreeNode controlsNode = this.uiTvExplorer.Nodes[0].Nodes[2];
                ExplorerTreeNode controlNode = new ExplorerTreeNode(dlg.control.Name, 7, 7, dlg.control, controlsNode);
                controlsNode.Nodes.Add(controlNode);
            }
        }

        private void RefreshProject_Click(object sender, EventArgs e)
        {
            Driver tempDriver = DriverFactory.GetDriver(this.currentProject.Domain.DatabaseSchema.ConnectionInfo.Location);

            EntitiesDlg entitiesDlg = new EntitiesDlg(tempDriver);
            if (entitiesDlg.ShowDialog(this) == DialogResult.OK)
            {
                string[] selectedTables = entitiesDlg.GetSelectedTables();
                string[] selectedViews = entitiesDlg.GetSelectedViews();

                BuildDomainDlg dlg = new BuildDomainDlg(tempDriver, selectedTables, selectedViews);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshProjectDlg refreshDlg = new RefreshProjectDlg(CurrentProject.Domain, dlg.GetDomain());
                    if (refreshDlg.ShowDialog(this) == DialogResult.OK )
                    {
                        RefreshExplorer();
                    }
                }
            }

            
        }

        private void addControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddControl();
        }

        private void ReloadMenuRecentProjects()
        {
            foreach (MenuItem item in this.fileMenuRecentProjects.DropDownItems)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }
            this.fileMenuRecentProjects.DropDownItems.Clear();
            if (SmartStudio.Preferences.RecentProjects.Count == 0)
            {
                this.fileMenuRecentProjects.DropDownItems.Add(new ToolStripMenuItem("No projects have been opened."));
            }
            else
            {
                for (int i = SmartStudio.Preferences.RecentProjects.Count - 1; i >= 0; i--)
                {
                    string file = SmartStudio.Preferences.RecentProjects[i];
                    ToolStripMenuItem menuItem = new ToolStripMenuItem();
                    menuItem.Text = "&" + i.ToString(CultureInfo.CurrentCulture) + " " + file;
                    menuItem.Click += new EventHandler(RecentProjectHandler);
                    menuItem.Tag = file;
                    this.fileMenuRecentProjects.DropDownItems.Add(menuItem);
                }
            }
        }

        void RecentProjectHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu != null)
            {
                LoadProjectFromFilename(menu.Tag.ToString());
            }
        }

        private void SmartStudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                SmartStudio.Preferences.Save();
                Application.Exit();
            }
            catch (Exception exception)
            {
                string text = "An error occurred saving user preferences, changes will be lost." + Environment.NewLine + exception.Message + Environment.NewLine + Environment.NewLine + "Do you still want to exit SmartCode?";
                if (MessageBox.Show(this, text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }

        }

        //Context Menu
        private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddColumnDlg dlg = new AddColumnDlg();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (this.currentNode != null)
                {
                    TableSchema table = this.currentNode.Tag as TableSchema;
                    IDictionary<SqlType, String> netDataTypes = TypesFactory.GetNetDataTypes();
                    if (table != null)
                    {
                        ColumnSchema column = new ColumnSchema(dlg.Name, table);
                        column.Code = dlg.Code;
                        column.IsPrimaryKey = dlg.IsPK;
                        column.IsRequired = !dlg.IsNull;
                        column.OriginalSQLType = dlg.OriginalType;
                        column.SqlType = dlg.SQLType;
                        column.NetDataType = netDataTypes[column.SqlType];

                        table.AddColumn(column);

                        ExplorerTreeNode columnNode = new ExplorerTreeNode(column.Name, 2, 2, column, this.currentNode);
                        this.currentNode.Nodes.Add(columnNode);
                    }
                }
            }
        }

        private void addOutReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentNode != null  )
            {
                TableSchema selectedTable = this.currentNode.Tag as TableSchema;
                if (selectedTable != null)
                {
                    AddOutReferenceDlg dlg = new AddOutReferenceDlg(selectedTable);
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        ReferenceSchema outReference = dlg.Reference;

                        ExplorerTreeNode outReferenceNode = new ExplorerTreeNode(outReference.Name, 4, 4, outReference, this.currentNode);
                        this.currentNode.Nodes.Add(outReferenceNode);

                        foreach (ReferenceJoin join in outReference.Joins)
                        {
                            ExplorerTreeNode joinNode = new ExplorerTreeNode(join.Name, 5, 5, join, outReferenceNode);
                            outReferenceNode.Nodes.Add(joinNode);
                        }
                    }
                }
            }
        }

        private void removeTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentNode != null && MessageBox.Show("Are you sure you want to delete the Table?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TableSchema selectedTable = this.currentNode.Tag as TableSchema;
                if (selectedTable != null)
                {
                    if (this.CurrentProject.Domain.RemoveTable(selectedTable))
                    {
                        return;
                    }
                }
            }
        }

        private void removeColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentNode != null && MessageBox.Show("Are you sure you want to delete the Column?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ColumnSchema column = this.currentNode.Tag as ColumnSchema;
                if (column != null)
                {
                    TableSchema table = this.currentNode.Parent.Tag as TableSchema;
                    if (table.RemoveColumn(column))
                    {
                        return;
                    }
                }
            }
        }

        private void removeReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentNode != null && MessageBox.Show("Are you sure you want to delete the Reference?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ReferenceSchema reference = this.currentNode.Tag as ReferenceSchema;
                if (reference != null)
                {
                    TableSchema table = this.currentNode.Parent.Tag as TableSchema;
                    if (table.RemoveOutReference(reference))
                    {
                        return;
                    }
                }
            }
        }

        private void removeReferenceJoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentNode != null && MessageBox.Show("Are you sure you want to delete the Reference Join?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ReferenceJoin join = this.currentNode.Tag as ReferenceJoin;
                if (join != null)
                {
                    ReferenceSchema refe = this.currentNode.Parent.Tag as ReferenceSchema;
                    if (refe.RemoveJoin(join))
                    {
                        return;
                    }
                }
            }
        }

        private void removeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentNode != null && MessageBox.Show("Are you sure you want to delete the Control?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ControlBase selectedControl = this.currentNode.Tag as ControlBase;
                if (selectedControl != null)
                {
                    this.CurrentProject.Domain.RemoveControl(selectedControl);
                    this.currentNode.Remove();
                }
            }
        }

  
 
    }
}