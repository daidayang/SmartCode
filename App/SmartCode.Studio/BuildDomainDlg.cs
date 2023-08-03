/*
 * Copyright © 2005-2007 
 * Danilo Mendez <danilo.mendez@kontac.net>
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
using SmartCode.Studio.Model;
using System.Threading;
using SmartCode.Model;

namespace SmartCode.Studio
{
    public partial class BuildDomainDlg : Form
    {
        delegate void ShowProgressDelegate(int totalMessages, int i, string messagesSoFar, bool statusDone);

        private Driver driver;

        private string[] tables;
        private string[] views;

        BuildDomain buildModel;

        private BuildDomainDlg()
        {
            InitializeComponent();
        }

        internal BuildDomainDlg(Driver driver,  string[] tables, string[] views)
            :this()
        {
            this.driver = driver;
            this.tables = tables;
            this.views = views;

            ShowProgressDelegate showProgress = new ShowProgressDelegate(ShowProgress);
            buildModel = new BuildDomain(this, showProgress);

        }

        private void BuildModelDlg_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(buildModel.RunProcess));
            t.IsBackground = true; 
            t.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalMessages"></param>
        /// <param name="messagesSoFar"></param>
        /// <param name="done"></param>
        private void ShowProgress(int totalMessages, int i, string messagesSoFar, bool done)
        {
            progressBar1.Maximum = totalMessages ;

            uiMessage.Text = messagesSoFar;
            progressBar1.Value = i;
            if (done)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        public Domain GetDomain()
        {
            return buildModel.Domain;
        }
        public Driver Driver
        {
            get { return driver; }
        }

        public string[] Views
        {
            get { return views; }
        }

        public string[] Tables
        {
            get { return tables; }
        }
    }
}