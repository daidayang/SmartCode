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
    internal class ListBoxPropertyWrapper : ControlPropertyWrapper
    {

        ListBox listBox;
        internal ListBoxPropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode, control)
        {
            this.listBox = control as ListBox;
        }

        [DisplayText("PropColumnsNum", "ProUnavailabelDesc", "CustomCategory")]
        public int ColumnsNum
        {
            get { return listBox.ColumnsNum; }
            set { listBox.ColumnsNum = value; }
        }

        [DisplayText("PropMultipleSelect", "ProUnavailabelDesc", "CustomCategory")]
        public bool MultipleSelect
        {
            get { return listBox.MultipleSel; }
            set { listBox.MultipleSel = value; }
        }

        [DisplayText("PropSorted", "ProUnavailabelDesc", "CustomCategory")]
        public bool Sorted
        {
            get { return listBox.Sorted; }
            set { listBox.Sorted = value; }
        }
    }
}
