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
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using SmartCode.Model.Profile;
using SmartCode.Model;

namespace SmartCode.Studio.Controls.EditorWrapper
{
    class LOVEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc;
        private string selectedColumns;
        System.Windows.Forms.ListView uiListView;
        

        public override UITypeEditorEditStyle
               GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            edSvc = provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            ReferenceJoinPropertyWrapper joinWrapper = context.Instance as ReferenceJoinPropertyWrapper;
            ColumnPropertyWrapper columnWrapper = context.Instance as ColumnPropertyWrapper;
            uiListView = new System.Windows.Forms.ListView();
            uiListView.View = View.List;
            uiListView.CheckBoxes = true;
            uiListView.BorderStyle = BorderStyle.None;


            if (edSvc != null && joinWrapper != null)
            {
                string[] currentValues = joinWrapper.LOV.Split(',');

                ReferenceSchema parentReference = joinWrapper.CurrentReferenceJoin.ParentReference;
                foreach (ColumnSchema column in parentReference.ParentTable.Columns())
                {
                    ListViewItem li = new ListViewItem(column.Name);
                    li.Tag = column;
                    uiListView.Items.Add(li);
                    foreach (string s in currentValues)
                    {
                        if (s == column.Name)
                        {
                            li.Checked = true;
                            break;
                        }
                    }
                }
            }
            else if (edSvc != null && columnWrapper != null)
            {
                string[] currentValues = columnWrapper.LOV.Split(',');

                foreach (ReferenceSchema parentReference in columnWrapper.CurrentColumn.Table.InReferences)
                {
                    foreach (ReferenceJoin join in parentReference.Joins)
                    {
                        if (join.ChildColumn == columnWrapper.CurrentColumn)
                        {
                            foreach (ColumnSchema column in parentReference.ParentTable.Columns())
                            {
                                ListViewItem li = new ListViewItem(column.Name);
                                li.Tag = column;
                                uiListView.Items.Add(li);
                                foreach (string s in currentValues)
                                {
                                    if (s == column.Name)
                                    {
                                        li.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            uiListView.Leave += new EventHandler(listBox_Leave);
            //listBox.close += new EventHandler(this.TextChanged);
            this.edSvc.DropDownControl(uiListView);
            return string.IsNullOrEmpty(selectedColumns) ? "" : selectedColumns.Substring(0, selectedColumns.Length - 1);
        }

        void listBox_Leave(object sender, EventArgs e)
        {
            if (this.edSvc != null)
            {
                selectedColumns = "";
                foreach (ListViewItem li in uiListView.CheckedItems)
                {
                    ColumnSchema parentColumn = li.Tag as ColumnSchema;
                    if (parentColumn != null)
                    {
                        selectedColumns += parentColumn.Name + ",";
                    }
                }
                this.edSvc.CloseDropDown();
            }
        }
    }
}
