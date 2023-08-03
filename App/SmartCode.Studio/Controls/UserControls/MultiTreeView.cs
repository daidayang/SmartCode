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
using System.Collections;
using System.Drawing;

namespace SmartCode.Studio.Controls.UserControls
{
    /// <summary>
    /// Tree view with multi selection
    /// </summary>
    class MultiTreeView: TreeView
    {
        
        protected ArrayList selectedNodes;
        protected TreeNode firstNode;
        protected TreeNode lastNode;

        public MultiTreeView()
        {
            this.selectedNodes = new ArrayList();
            base.Sorted = true;
        }

        protected bool IsParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
            {
                return true;
            }
            TreeNode tempNode = childNode;
            bool parent = false;
            while (!parent && (tempNode != null))
            {
                tempNode = tempNode.Parent;
                parent = tempNode == parentNode;
            }
            return parent;
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            if (Control.ModifierKeys == Keys.Control)
            {
                if (!this.selectedNodes.Contains(e.Node))
                {
                    this.selectedNodes.Add(e.Node);
                }
                else
                {
                    this.RemovePaintFromNodes();
                    this.selectedNodes.Remove(e.Node);
                }
                this.PaintSelectedNodes();
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
                Queue queue = new Queue();
                TreeNode node1 = this.firstNode;
                TreeNode node = e.Node;
                bool isParentNode = this.IsParent(this.firstNode, e.Node);
                if (!isParentNode)
                {
                    isParentNode = this.IsParent(node, node1);
                    if (isParentNode)
                    {
                        TreeNode tempNode = node1;
                        node1 = node;
                        node = tempNode;
                    }
                }
                if (isParentNode)
                {
                    for (TreeNode availableNode = node; availableNode != node1.Parent; availableNode = availableNode.Parent)
                    {
                        if (!this.selectedNodes.Contains(availableNode))
                        {
                            queue.Enqueue(availableNode);
                        }
                    }
                }
                else if ((node1.Parent == null && node.Parent == null) || (node1.Parent != null && node1.Parent.Nodes.Contains(node)))
                {
                    int i = node1.Index;
                    int j = node.Index;
                    if (j < i)
                    {
                        TreeNode tempNode = node1;
                        node1 = node;
                        node = tempNode;
                        i = node1.Index;
                        j = node.Index;
                    }
                    TreeNode pNode1 = node1;
                    while (i <= j)
                    {
                        if (!this.selectedNodes.Contains(pNode1))
                        {
                            queue.Enqueue(pNode1);
                        }
                        pNode1 = pNode1.NextNode;
                        i++;
                    }
                }
                else
                {
                    if (!this.selectedNodes.Contains(node1))
                    {
                        queue.Enqueue(node1);
                    }
                    if (!this.selectedNodes.Contains(node))
                    {
                        queue.Enqueue(node);
                    }
                }
                this.selectedNodes.AddRange(queue);
                this.PaintSelectedNodes();
                this.firstNode = e.Node;
            }
            else
            {
                if ((this.selectedNodes != null) && (this.selectedNodes.Count > 0))
                {
                    this.RemovePaintFromNodes();
                    this.selectedNodes.Clear();
                }
                this.selectedNodes.Add(e.Node);
            }
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);
            if (Control.ModifierKeys == Keys.Control && this.selectedNodes.Contains(e.Node))
            {
                e.Cancel = true;
                this.RemovePaintFromNodes();
                this.selectedNodes.Remove(e.Node);
                this.PaintSelectedNodes();
            }
            else
            {
                this.lastNode = e.Node;
                if (Control.ModifierKeys != Keys.Shift)
                {
                    this.firstNode = e.Node;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected void PaintSelectedNodes()
        {
            foreach (TreeNode node in this.selectedNodes)
            {
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
            }
        }

        protected void RemovePaintFromNodes()
        {
            if (this.selectedNodes.Count != 0)
            {
                try
                {
                    TreeNode selectedNode = (TreeNode) this.selectedNodes[0];
                    foreach (TreeNode node in this.selectedNodes)
                    {
                        node.BackColor = selectedNode.TreeView.BackColor;
                        node.ForeColor = selectedNode.TreeView.ForeColor;
                    }
                }
                catch
                {
                }
            }
        }

        public void RemovePaintNode(TreeNode nd)
        {
            if (this.selectedNodes.Count != 0)
            {
                TreeNode selectedNode = (TreeNode) this.selectedNodes[0];
                nd.BackColor = selectedNode.TreeView.BackColor;
                nd.ForeColor = selectedNode.TreeView.ForeColor;
            }
        }


        public ArrayList SelectedNodes
        {
            get
            {
                return this.selectedNodes;
            }
            set
            {
                this.RemovePaintFromNodes();
                this.selectedNodes.Clear();
                this.selectedNodes = value;
                this.PaintSelectedNodes();
            }
        }


    }
}

