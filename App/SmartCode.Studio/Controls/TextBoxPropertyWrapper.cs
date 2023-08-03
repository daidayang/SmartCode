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
    internal class TextBoxPropertyWrapper : ControlPropertyWrapper
    {
        TextBox textBox;
        internal TextBoxPropertyWrapper(ExplorerTreeNode treeNode, ControlBase control)
            : base(treeNode, control)
        {
            this.textBox = control as TextBox;
        }

        [DisplayText("PropLimit", "ProUnavailabelDesc", "CustomCategory")]
        protected int Limit
        {
            get { return textBox.Limit; }
            set { textBox.Limit = value; }
        }

        [DisplayText("PropPassword", "ProUnavailabelDesc", "CustomCategory")]
        protected string Password
        {
            get { return textBox.Password; }
            set { textBox.Password = value; }
        }

        [DisplayText("PropHideSelection", "ProUnavailabelDesc", "CustomCategory")]
        protected bool HideSelection
        {
            get { return textBox.HideSelection; }
            set { textBox.HideSelection = value; }
        }

        [DisplayText("PropBorder", "ProUnavailabelDesc", "CustomCategory")]
        protected bool Border
        {
            get { return textBox.Border; }
            set { textBox.Border = value; }
        }

        [DisplayText("PropAutoSelect", "ProUnavailabelDesc", "CustomCategory")]
        protected bool AutoSelect
        {
            get { return textBox.AutoSelect; }
            set { textBox.AutoSelect = value; }
        }

        [DisplayText("PropMultiline", "ProUnavailabelDesc", "CustomCategory")]
        protected bool Multiline
        {
            get { return textBox.Multiline; }
            set { textBox.Multiline = value; }
        }

        [DisplayText("PropHScrollBar", "ProUnavailabelDesc", "CustomCategory")]
        protected bool HScrollBar
        {
            get { return textBox.HScrollBar; }
            set { textBox.HScrollBar = value; }
        }

        [DisplayText("PropVScrollBar", "ProUnavailabelDesc", "CustomCategory")]
        protected bool VScrollBar
        {
            get { return textBox.VScrollBar; }
            set { textBox.VScrollBar = value; }
        }

        [DisplayText("PropReadOnly", "ProUnavailabelDesc", "CustomCategory")]
        protected bool ReadOnly
        {
            get { return textBox.ReadOnly; }
            set { textBox.ReadOnly = value; }
        }

        [DisplayText("PropEmptyIsNull", "ProUnavailabelDesc", "CustomCategory")]
        protected bool EmptyIsNull
        {
            get { return textBox.EmptyIsNull; }
            set { textBox.EmptyIsNull = value; }
        }

        [DisplayText("PropIsDateTime", "ProUnavailabelDesc", "CustomCategory")]
        protected bool IsDateTime
        {
            get { return textBox.IsDateTime; }
            set { textBox.IsDateTime = value; }
        }

        [DisplayText("PropMask", "ProUnavailabelDesc", "CustomCategory")]
        protected string Mask
        {
            get { return textBox.Mask; }
            set { textBox.Mask = value; }
        }
    }
}
