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
    internal class ControlPropertyWrapper : PropertyWrapper
    {
        private ControlBase control;

        internal ControlPropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode)
        {
            this.control = control;
        }

        [DisplayText("PropName", "PropNameDesc", "CommonCategory")]
        public string Name
        {
            get
            {
                return this.control.Name;
            }
        }

        [DisplayText("PropRequired", "ProUnavailabelDesc", "CommonCategory")]
        public bool Required
        {
            get { return this.control.Required; }
            set { this.control.Required = value; }
        }

        [DisplayText("PropIsVisible", "ProUnavailabelDesc", "CommonCategory")]
        public bool IsVisible
        {
            get { return this.control.Visible; }
            set { this.control.Visible = value; }
        }

        [DisplayText("PropJustify", "ProUnavailabelDesc", "CommonCategory")]
        public SmartCode.Model.Profile.ControlBase.JustifyEnum Justify
        {
            get { return this.control.Justify; }
            set { this.control.Justify = value; }
        }

        [DisplayText("PropHeight", "ProUnavailabelDesc", "CommonCategory")]
        public decimal Height
        {
            get { return this.control.Height; }
            set { this.control.Height = value; }
        }

        [DisplayText("PropWidth", "ProUnavailabelDesc", "CommonCategory")]
        public decimal Width
        {
            get { return this.control.Width; }
            set { this.control.Width = value; }
        }


    }
}
