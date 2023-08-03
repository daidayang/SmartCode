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
using SmartCode.Model;
using SmartCode.Model.Profile;
using SmartCode.Studio.Controls.EditorWrapper;
using System.ComponentModel;
using System.Drawing.Design;

namespace SmartCode.Studio.Controls
{
    internal class CheckBoxPropertyWrapper : ControlPropertyWrapper
    {
        CheckBox checkBox;
        internal CheckBoxPropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode, control)
        {
            this.checkBox = control as CheckBox;
        }

        [DisplayText("PropText", "ProUnavailabelDesc", "CustomCategory")]
        public string Text
        {
            get { return checkBox.Text; }
            set { checkBox.Text = value; }
        }

        [DisplayText("PropDataValueForOn", "ProUnavailabelDesc", "CustomCategory")]
        public string DataValueForOn
        {
            get { return checkBox.DataValueForOn; }
            set { checkBox.DataValueForOn = value; }
        }

        [DisplayText("PropDataValueForOff", "ProUnavailabelDesc", "CustomCategory")]
        public string DataValueForOff
        {
            get { return checkBox.DataValueForOff; }
            set { checkBox.DataValueForOff = value; }
        }

        [DisplayText("PropLeftText", "ProUnavailabelDesc", "CustomCategory")]
        public bool LeftText
        {
            get { return checkBox.LeftText; }
            set { checkBox.LeftText = value; }
        }

    }
}
