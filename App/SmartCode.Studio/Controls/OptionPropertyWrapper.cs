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
    internal class OptionPropertyWrapper : ControlPropertyWrapper
    {
        OptionButton option;
        internal OptionPropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode, control)
        {
            this.option = control as OptionButton;
        }

        [DisplayText("PropColumnsAcross", "ProUnavailabelDesc", "CustomCategory")]
        public int ColumnsAcross
        {
            get { return option.ColumnsAcross; }
            set { option.ColumnsAcross = value; }
        }

        [DisplayText("PropLeftText", "ProUnavailabelDesc", "CustomCategory")]
        public bool LeftText
        {
            get { return option.LeftText; }
            set { option.LeftText = value; }
        }

    }
}
