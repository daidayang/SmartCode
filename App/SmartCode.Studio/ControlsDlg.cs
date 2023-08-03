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
using SmartCode.Model.Profile;

namespace SmartCode.Studio
{
    public partial class ControlsDlg : Form
    {
        internal  ControlBase control;

        public ControlsDlg()
        {
            InitializeComponent();
        }

        private void uiOK_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                switch (this.uiControlType.SelectedIndex)
                {
                    case 0:
                        control = new SmartCode.Model.Profile.TextBox(uiControlName.Text);
                        break;
                    case 1:
                        control = new SmartCode.Model.Profile.CheckBox(uiControlName.Text);
                        break;
                    case 2:
                        control = new SmartCode.Model.Profile.ComboBox(uiControlName.Text);
                        break;
                    case 3:
                        control = new SmartCode.Model.Profile.Image(uiControlName.Text);
                        break;
                    case 4:
                        control = new SmartCode.Model.Profile.ListBox(uiControlName.Text);
                        break;
                    case 5:
                        control = new SmartCode.Model.Profile.OptionButton(uiControlName.Text);
                        break;
                    case 6:
                        control = new SmartCode.Model.Profile.Popup(uiControlName.Text);
                        break;

                }
                SmartCode.Studio.SmartStudio.MainForm.CurrentProject.Domain.Controls.Add(control.Name, control);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ;
            }
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(uiControlName.Text))
            {
                MessageBox.Show("Please enter a name to the control.", "Error", MessageBoxButtons.OK);
                return false;
            }
            if (SmartCode.Studio.SmartStudio.MainForm.CurrentProject.Domain.Controls.ContainsKey(uiControlName.Text))
            {
                MessageBox.Show("There is a control with the same name.", "Error", MessageBoxButtons.OK);
                return false;
            }
            if (this.uiControlType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a control type.", "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
    }
}