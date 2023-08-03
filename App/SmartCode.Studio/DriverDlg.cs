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
using SmartCode.Model;
using SmartCode.Studio.Database.Access;

namespace SmartCode.Studio
{
    public partial class DriverDlg : Form
    {
        Driver selectedDriver;
        ///Domain domain = new Domain(

        public DriverDlg()
        {
            InitializeComponent();

        }

        private void uiOK_Click(object sender, EventArgs e)
        {
            setDriver();
        }

        private void setDriver()
        {
            if (IsValid())
            {
                try
                {
                    this.selectedDriver = DriverFactory.GetDriver(this.uiLocation.Text);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private bool IsValid()
        {
            return true;
        }

        public Driver SelectedDriver
        {
            get { return selectedDriver; }
        }

        public string DomainName
        {
            get { return this.uiDomainName.Text ; }
        }

        private void uiDriverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //provider/[[user[:password]@]host[:port]]/database
            switch (this.uiDriverType.SelectedIndex)
            {
                case 0:
                    this.uiLocation.Text = @"provider/[[user[:password]@]host[:port]]/database";
                    break;
                case 1:
                    this.uiLocation.Text = @"mssql/(local)/Northwind";
                    break;
                case 2:
                    this.uiLocation.Text = @"mssql/user:password@(local)/Northwind";
                    break;
                case 3:
                    this.uiLocation.Text = @"mssql2005/(local)/Northwind";
                    break;
                case 4:
                    this.uiLocation.Text = @"mssql2005/user:password@(local)/Northwind";
                    break;
                case 5:
                    this.uiLocation.Text = @"msaccess/(local)/C:\\SampleDB.mdb";
                    break;
                case 6:
                    this.uiLocation.Text = @"msaccess/admin:password@(local)/C:\\SampleDB.mdb";
                    break;
                case 7:
                    this.uiLocation.Text = @"oracle/user:password@server/database";
                    break;
                case 8:
                    this.uiLocation.Text = @"mysql/root:password@localhost/DatabaseName";
                    break;
                default:
                    break;
            }
        }

        private void uiTest_Click(object sender, EventArgs e)
        {
            try
            {
                setDriver();
                SelectedDriver.TestConnection();
                MessageBox.Show("Connection attemp successful.", "Alert", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message , "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } 
    }
}