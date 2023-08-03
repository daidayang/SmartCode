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

namespace SmartCode.Studio.Controls.EditorWrapper
{
    class ClientProfilerEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc;

        public override UITypeEditorEditStyle
               GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            edSvc =  provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            ColumnPropertyWrapper wrapper = context.Instance as ColumnPropertyWrapper;
            System.Windows.Forms.ListBox listBox = new System.Windows.Forms.ListBox();
            listBox.BorderStyle = BorderStyle.None;
            listBox.Items.Add("");

            if (edSvc != null && wrapper != null)
            {
                IDictionary<String, ControlBase> allControls = wrapper.GetAllControls();
                foreach (KeyValuePair<String, ControlBase> keyPair in wrapper.GetAllControls())
                {
                    listBox.Items.Add(keyPair.Key);
                }
            }
            listBox.SelectedValueChanged += new EventHandler(this.TextChanged);
            this.edSvc.DropDownControl(listBox);
            return listBox.Text;
        }

        private void TextChanged(object sender, EventArgs e)
        {
            if (this.edSvc != null)
            {
                this.edSvc.CloseDropDown();
            }
        }

    }
}
