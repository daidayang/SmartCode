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
    internal class PopupPropertyWrapper : ControlPropertyWrapper
    {

        Popup popup;
        internal PopupPropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode, control)
        {
            this.popup = control as Popup;
        }

        [DisplayText("PropWindowsHeight", "ProUnavailabelDesc", "CustomCategory")]
        public int WindowsHeight
        {
            get { return popup.WindowsHeight; }
            set { popup.WindowsHeight = value; }
        }

        [DisplayText("PropWindowsWidth", "ProUnavailabelDesc", "CustomCategory")]
        public int WindowsWidth
        {
            get { return popup.WindowsWidth; }
            set { popup.WindowsWidth = value; }
        }

        [DisplayText("PropIsModal", "ProUnavailabelDesc", "CustomCategory")]
        public bool IsModal
        {
            get { return popup.IsModal; }
            set { popup.IsModal = value; }
        }

    }
}
