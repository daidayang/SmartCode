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

namespace SmartCode.Studio
{
    public partial class EntitiesDlg : Form
    {
        private Driver driver;

        internal EntitiesDlg()
        {
            InitializeComponent();
        }
        public EntitiesDlg(Driver driver)
            : this()
        {
            this.driver = driver;
        }

        private void EntitiesDlg_Load(object sender, EventArgs e)
        {
            LoadEntities();
        }

        private void LoadEntities()
        {
            try
            {
                string[] allTables = this.driver.Extractor.GetAllTables();
                string[] allViews = this.driver.Extractor.GetAllViews();

                foreach (string table in allTables)
                {
                    ListViewItem li = new ListViewItem(table);
                    li.Checked = true;
                    this.uiTables.Items.Add(li);
                }
                foreach (string view in allViews)
                {
                    ListViewItem li = new ListViewItem(view);
                    li.Checked = true;
                    this.uiViews.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void uiSelectAllTables_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in this.uiTables.Items)
            {
                li.Checked = true;
            }
        }

        private void uiUnselectAllTables_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in this.uiTables.Items)
            {
                li.Checked = false;
            }
        }

        private void uiSelectAllViews_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in this.uiViews.Items)
            {
                li.Checked = true;
            }
        }

        private void uiUnselectAllViews_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in this.uiViews.Items)
            {
                li.Checked = false;
            }
        }

        private void uiOK_Click(object sender, EventArgs e)
        {
            if (this.Isvalid())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool Isvalid()
        {
            return true;
        }

        public string[] GetSelectedTables()
        {
            string[] allTables = new string[this.uiTables.CheckedItems.Count];
            for (int i = 0; i < this.uiTables.CheckedItems.Count; i++)
            {
                allTables.SetValue(this.uiTables.CheckedItems[i].Text, i);
            }
            return allTables;
        }

        public string[] GetSelectedViews()
        {
            string[] allViews = new string[this.uiViews.CheckedItems.Count];
            for (int i = 0; i < this.uiViews.CheckedItems.Count; i++)
            {
                allViews.SetValue(this.uiViews.CheckedItems[i].Text, i);
            }
            return allViews;
        }
    }
}