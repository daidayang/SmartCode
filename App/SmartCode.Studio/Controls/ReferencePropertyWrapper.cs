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
    internal class ReferencePropertyWrapper: NamedPropertyWrapper
    {
        public ReferencePropertyWrapper(ExplorerTreeNode treeNode, ReferenceSchema reference)
            : base(treeNode, reference)
        {
        }

        private ReferenceSchema CurrentReference
        {
            get
            {
                return NamedObject as ReferenceSchema;
            }
        }

        [DisplayText("PropOnDeleteCascade", "PropOnDeleteCascadeDesc", "CascadeCategory")]
        public bool OnDeleteCascade
        {
            get { return CurrentReference.OnDeleteCascade; }
            set { CurrentReference.OnDeleteCascade = value; }
        }

        [DisplayText("PropOnUpdateCascade", "PropOnUpdateCascadeDesc", "CascadeCategory")]
        public bool OnUpdateCascade
        {
            get { return CurrentReference.OnUpdateCascade; }
            set { CurrentReference.OnUpdateCascade = value; }
        }

        [DisplayText("PropAlignment", "PropAlignmentDesc", "CascadeCategory")]
        public SmartCode.Model.ReferenceSchema.AlignmentType Alignment
        {
            get { return CurrentReference.Alignment; }
            set { CurrentReference.Alignment = value; }
        }

        [DisplayText("PropParentTable", "PropParentTableDesc", "CascadeCategory")]
        public string ParentTable
        {
            get { return CurrentReference.ParentTable.Name; }
        }

        [DisplayText("PropChildTable", "PropChildTableDesc", "CascadeCategory")]
        public string ChildTable
        {
            get { return CurrentReference.ChildTable.Name; }
        }
    }
}
