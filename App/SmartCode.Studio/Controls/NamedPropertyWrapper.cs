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

namespace SmartCode.Studio.Controls
{
    internal abstract class NamedPropertyWrapper : PropertyWrapper
    {

        private NamedObject namedObject;

        public NamedPropertyWrapper(ExplorerTreeNode treeNode, NamedObject namedObject)
            : base(treeNode)
        {
            this.namedObject = namedObject;
        }

        [DisplayText("PropName", "PropNameDesc", "NamedCategory")]
        public string Name
        {
            get
            {
                return this.namedObject.Name;
            }
        }

        [DisplayText("PropCode", "PropCodeDesc", "NamedCategory")]
        public string Code
        {
            get
            {
                return this.namedObject.Code;
            }
            set
            {
                this.namedObject.Code = value;
            }
        }
        [DisplayText("PropCaption", "PropCaptionDesc", "NamedCategory")]
        public string Caption
        {
            get
            {
                return this.namedObject.Caption;
            }
            set
            {
                this.namedObject.Caption = value;
            }
        }

        [DisplayText("PropComment", "PropCommentDesc", "NamedCategory")]
        public string Comment
        {
            get
            {
                return this.namedObject.Comment;
            }
            set
            {
                this.namedObject.Comment = value;
            }
        }

        [DisplayText("PropDescription", "PropDescriptionDesc", "NamedCategory")]
        public string Description
        {
            get
            {
                return this.namedObject.Description;
            }
            set
            {
                this.namedObject.Description = value;
            }
        }

        protected NamedObject NamedObject
        {
            get { return namedObject; }
        }
    }
}
