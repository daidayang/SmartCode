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
    internal class ImagePropertyWrapper : ControlPropertyWrapper
    {
        Image image;
        internal ImagePropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode, control)
        {
            this.image = control as Image;
        }

        [DisplayText("PropBorder", "ProUnavailabelDesc", "CustomCategory")]
        public bool Border
        {
            get { return image.Border; }
            set { image.Border  = value; }
        }

    }
}
