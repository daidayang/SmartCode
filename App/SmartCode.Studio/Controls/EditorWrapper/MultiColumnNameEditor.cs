using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using SmartCode.Model;

namespace SmartCode.Studio.Controls.EditorWrapper
{
    class MultiColumnNameEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc;
        System.Windows.Forms.CheckedListBox combo;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        private class CompareTo
        {
            private string s;
            public CompareTo(string s)
            {
                this.s = s;
            }

            public bool IsEqual(string s)
            {
                return this.s == s;
            }
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            edSvc = provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            TablePropertyWrapper tableWrapper = context.Instance as TablePropertyWrapper;
            string[] values = value.ToString().Split(',');

            combo = new System.Windows.Forms.CheckedListBox();
            if (edSvc != null && tableWrapper != null)
                foreach (ColumnSchema column in tableWrapper.CurrentTable.Columns())
                {
                    CompareTo comparer = new CompareTo(column.Name);
                    combo.Items.Add(column.Name, Array.Exists<string>(values, comparer.IsEqual));
                }
            this.edSvc.DropDownControl(combo);

            StringBuilder returnValue = new StringBuilder();
            foreach (object item in combo.CheckedItems)
            {
                returnValue.AppendFormat("{0},", item.ToString());
            }
            if (returnValue.Length > 0)
                returnValue.Length -= 1;
            return returnValue.ToString();
        }
    }
}
