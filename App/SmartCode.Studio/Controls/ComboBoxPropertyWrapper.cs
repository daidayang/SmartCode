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
    internal class ComboBoxPropertyWrapper : ControlPropertyWrapper
    {
        ComboBox comboBox;
        internal ComboBoxPropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode, control)
        {
            this.comboBox = control as ComboBox;
        }

        [DisplayText("PropStyle", "ProUnavailabelDesc", "CustomCategory")]
        public SmartCode.Model.Profile.ComboBox.StyleEnum Style
        {
            get { return comboBox.ComboStyle; }
            set { comboBox.ComboStyle = value; }
        }

        [DisplayText("PropLength", "ProUnavailabelDesc", "CustomCategory")]
        public int Length
        {
            get { return comboBox.Length; }
            set { comboBox.Length = value; }
        }

        [DisplayText("PropSorted", "ProUnavailabelDesc", "CustomCategory")]
        public bool Sorted
        {
            get { return comboBox.Sorted; }
            set { comboBox.Sorted = value; }
        }

    }
}
