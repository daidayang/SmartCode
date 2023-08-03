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
using System.Windows.Forms;
using SmartCode.Model;
using SmartCode.Model.Profile;

namespace SmartCode.Studio.Controls
{
    internal class ExplorerTreeNode : TreeNode
    {
        private bool editEnabled;

        private bool isDirty;
        private TreeNodeType nodeType;
        private PropertyWrapper propertyWrapper;


        internal ExplorerTreeNode(string text, int imageIndex, int selectedImageIndex, IdentifiedObject obj, TreeNode parentNode)
            : base(text, imageIndex, selectedImageIndex)
        {
            this.editEnabled = true;
            this.RefreshProperties(obj,  parentNode);
        }

        private void RefreshProperties(IdentifiedObject obj, TreeNode parentNode)
        {
            if (obj is Domain)
            {
                Domain domain = (Domain)obj;
                base.Tag = domain;
                this.nodeType = TreeNodeType.Domain;
                domain.OnRemoveTable += new OnRemoveObjectDelegate(OnRemoveTable);
                this.propertyWrapper = new DomainPropertyWrapper(this, domain);
            }
            else if (obj is ColumnSchema)
            {
                base.Tag = (ColumnSchema)obj;
                this.nodeType = TreeNodeType.Column;
                this.propertyWrapper = new ColumnPropertyWrapper(this, (ColumnSchema)obj);
            }
            else if (obj is TableSchema)
            {
                TableSchema table = (TableSchema)obj;
                base.Tag = table;
                if (table.IsTable)
                {
                    this.nodeType = TreeNodeType.Table;
                }
                else
                {
                    this.nodeType = TreeNodeType.View;
                }
                table.OnRemoveOutReferences += new OnRemoveObjectDelegate(OnRemoveOutReferences);
                table.OnRemoveColum += new OnRemoveObjectDelegate(OnRemoveColumn);
                this.propertyWrapper = new TablePropertyWrapper(this, (TableSchema)obj);

            }
            
            else if (obj is ReferenceSchema)
            {
                ReferenceSchema reference = (ReferenceSchema)obj;
                base.Tag = reference;
                this.nodeType = TreeNodeType.Reference ;
                reference.OnRemoveObject += new OnRemoveObjectDelegate(OnRemoveJoin);
                this.propertyWrapper = new ReferencePropertyWrapper(this, reference);
            }
            else if (obj is ReferenceJoin)
            {
                base.Tag = (ReferenceJoin)obj;
                this.nodeType = TreeNodeType.ReferenceJoin;
                this.propertyWrapper = new ReferenceJoinPropertyWrapper(this, (ReferenceJoin)obj);
            }
            else if (obj is ControlBase)
            {
                this.nodeType = TreeNodeType.Control;
                if (obj is SmartCode.Model.Profile.CheckBox)
                {
                    base.Tag = (SmartCode.Model.Profile.CheckBox)obj;
                    this.propertyWrapper = new CheckBoxPropertyWrapper(this, (SmartCode.Model.Profile.CheckBox)obj);
                }
                else if (obj is SmartCode.Model.Profile.TextBox)
                {
                    base.Tag = (SmartCode.Model.Profile.TextBox)obj;
                    this.propertyWrapper = new TextBoxPropertyWrapper(this, (SmartCode.Model.Profile.TextBox)obj);
                }
                else if (obj is SmartCode.Model.Profile.ComboBox)
                {
                    base.Tag = (SmartCode.Model.Profile.ComboBox)obj;
                    this.propertyWrapper = new ComboBoxPropertyWrapper(this, (SmartCode.Model.Profile.ComboBox)obj);
                }
                else if (obj is SmartCode.Model.Profile.Image )
                {
                    base.Tag = (SmartCode.Model.Profile.Image)obj;
                    this.propertyWrapper = new ImagePropertyWrapper(this, (SmartCode.Model.Profile.Image)obj);
                }
                else if (obj is SmartCode.Model.Profile.ListBox)
                {
                    base.Tag = (SmartCode.Model.Profile.ListBox)obj;
                    this.propertyWrapper = new ListBoxPropertyWrapper(this, (SmartCode.Model.Profile.ListBox)obj);
                }
                else if (obj is SmartCode.Model.Profile.OptionButton)
                {
                    base.Tag = (SmartCode.Model.Profile.OptionButton)obj;
                    this.propertyWrapper = new OptionPropertyWrapper(this, (SmartCode.Model.Profile.OptionButton)obj);
                }
                else if (obj is SmartCode.Model.Profile.Popup)
                {
                    base.Tag = (SmartCode.Model.Profile.Popup)obj;
                    this.propertyWrapper = new PopupPropertyWrapper(this, (SmartCode.Model.Profile.Popup)obj);
                }
                else
                {
                    this.propertyWrapper = new ControlPropertyWrapper(this, (ControlBase)obj);
                }
            }

        }

        private void OnRemoveTable(IdentifiedObject parent, IdentifiedObject arg)
        {
            if (arg is TableSchema)
            {
                TableSchema table = arg as TableSchema;

                TreeNode parentNode = null;
                if (table.IsTable)
                {
                    parentNode = this.Nodes[0];
                }
                else
                {
                    parentNode = this.Nodes[1];
                }


                for (int i = parentNode.Nodes.Count - 1; i >= 0; i--)
                {
                    TreeNode nd = parentNode.Nodes[i];
                    if (nd != null && nd.Tag == arg)
                    {
                        nd.Remove();
                    }
                }
            }

        }

        private void OnRemoveOutReferences(IdentifiedObject parent, IdentifiedObject arg)
        {
            for (int i = this.Nodes.Count - 1; i >=0; i--)
            {
                TreeNode nd = this.Nodes[i];
                if (nd.Tag == arg)
                {
                    nd.Remove();
                }
            }
            
        }

        private void OnRemoveJoin(IdentifiedObject parent, IdentifiedObject arg)
        {
            for (int i = this.Nodes.Count - 1; i >= 0; i--)
            {
                TreeNode nd = this.Nodes[i];
                if (nd.Tag == arg)
                {
                    nd.Remove();
                }
            }

        }


        private void OnRemoveColumn(IdentifiedObject parent, IdentifiedObject arg)
        {
            for (int i = this.Nodes.Count - 1; i >= 0; i--)
            {
                TreeNode nd = this.Nodes[i];
                if (nd.Tag == arg)
                {
                    nd.Remove();
                }
            }

        }

        internal bool EditEnabled
        {
            get { return editEnabled; }
        }

        internal bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        internal TreeNodeType NodeType
        {
            get { return nodeType; }
        }

        internal PropertyWrapper PropertyWrapper
        {
            get { return propertyWrapper; }
        }
    }
}
