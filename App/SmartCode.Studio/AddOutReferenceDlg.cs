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

namespace SmartCode.Studio
{
    public partial class AddOutReferenceDlg : Form
    {
        TableSchema m_parentTable;
        TableSchema m_childTable;
        ReferenceSchema m_reference;


        public AddOutReferenceDlg()
        {
            InitializeComponent();
        }

        public AddOutReferenceDlg(TableSchema parentTable)
            : this()
        {
            this.uiLblParentTable.Text = parentTable.Name;
            m_parentTable = parentTable;
        }

        private void uiCmbChildTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxElement cmbElement = uiCmbChildTable.SelectedItem as ComboBoxElement;
            if (cmbElement != null)
            {
                m_childTable = cmbElement.Table;

                this.uiLBChildTable.Items.Clear();
                this.uiLVReferenceJoins.Items.Clear();
                //Carga el child list box
                foreach (ColumnSchema column in cmbElement.Table.Columns())
                {
                    ListBoxElement liE = new ListBoxElement(column);
                    this.uiLBChildTable.Items.Add(liE);
                }

                uiTxtReferenceName.Text = "FK_" + m_parentTable.Code  + "_" + m_childTable.Code;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (uiTxtReferenceName.Text.Trim().Length > 0)
            {
                m_reference = new ReferenceSchema(uiTxtReferenceName.Text, m_parentTable, m_childTable);

                foreach (ListViewItem li in this.uiLVReferenceJoins.Items)
                {
                    ListViewElement liE = li as ListViewElement;
                    if (liE != null)
                    {
                        ReferenceJoin referenceJoin = new ReferenceJoin(m_reference, liE.ParentColumn, liE.ChildColumn);
                        m_reference.AddJoin(referenceJoin);
                    }
                }
                m_parentTable.AddOutReference(m_reference);
                m_childTable.AddInReference(m_reference);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter the Reference name");
                uiTxtReferenceName.Focus();
            }
        }

        private void uiAddJoin_Click(object sender, EventArgs e)
        {
            ListBoxElement liParentElement = this.uiLBParentTable.SelectedItem as ListBoxElement;
            ListBoxElement liChildElement = this.uiLBChildTable.SelectedItem as ListBoxElement;

            if (liParentElement == null)
            {
                MessageBox.Show("Please select the Parent Column");
                return;
            }
            if (liChildElement == null)
            {
                MessageBox.Show("Please select the Child Column");
                return;
            }

            ListViewElement li = new ListViewElement(liParentElement.Column, liChildElement.Column);
            this.uiLVReferenceJoins.Items.Add(li);
        }

        private void uiRemoveJoin_Click(object sender, EventArgs e)
        {
            if (this.uiLVReferenceJoins.SelectedItems.Count > 0)
            {
                ListViewElement li = this.uiLVReferenceJoins.SelectedItems[0] as ListViewElement;
                if (li != null)
                {
                    this.uiLVReferenceJoins.Items.Remove(li);
                }
            }
            else
            {
                MessageBox.Show("Please select the Reference Join to remove.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel ;
            this.Close();
        }

        private void AddOutReference_Load(object sender, EventArgs e)
        {
            //Carga el parent list box
            foreach (ColumnSchema column in m_parentTable.Columns())
            {
                ListBoxElement liE = new ListBoxElement(column);
                this.uiLBParentTable.Items.Add(liE);
            }

            //Carga el combo  box
            foreach (TableSchema table in SmartStudio.MainForm.CurrentProject.Domain.DatabaseSchema.Tables)
            {
                this.uiCmbChildTable.Items.Add(new ComboBoxElement(table));
            }
        }

        public ReferenceSchema Reference
        {
            get { return m_reference; }
        }

        private class ListBoxElement 
        {
            private ColumnSchema m_column;

            public ListBoxElement(ColumnSchema column)
            {
                m_column = column;
            }

            public ColumnSchema Column
            {
                get { return m_column; }
            }

            public override string ToString()
            {
                return m_column.Name ;
            }
        }


        private class ComboBoxElement
        {
            private TableSchema m_table;

            public ComboBoxElement(TableSchema table)
            {
                m_table = table;
            }

            public TableSchema Table
            {
                get { return m_table; }
            }

            public override string ToString()
            {
                return m_table.Name;
            }
        }

        private class ListViewElement : ListViewItem 
        {
            private ColumnSchema m_parentColumn;
            private ColumnSchema m_childColumn;

            public ListViewElement(ColumnSchema parentColumn, ColumnSchema childColumn)
                : base(parentColumn.Name)
            {
                m_parentColumn = parentColumn;
                m_childColumn = childColumn;

                base.SubItems.Add(childColumn.Name);
            }

            public ColumnSchema ParentColumn
            {
                get { return m_parentColumn; }
            }

            public ColumnSchema ChildColumn
            {
                get { return m_childColumn; }
            }

        }

    }
}