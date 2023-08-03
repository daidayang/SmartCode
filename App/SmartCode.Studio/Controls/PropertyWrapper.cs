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
using System.ComponentModel;
using System.Reflection;

namespace SmartCode.Studio.Controls
{
    internal abstract class PropertyWrapper : ICustomTypeDescriptor
    {
        internal delegate void PropertyChangedHandler(object sender, EventArgs e);
        protected ExplorerTreeNode explorerNode;

        protected PropertyWrapper(ExplorerTreeNode treeNode)
        {
            this.explorerNode = treeNode;
        }

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            object[] customAttributes = base.GetType().GetCustomAttributes(true);
            Attribute[] attributes = new Attribute[customAttributes.Length];
            for (int i = 0; i < customAttributes.Length; i++)
            {
                attributes[i] = (Attribute)customAttributes[i];
            }
            return new AttributeCollection(attributes);
        }

        public string GetClassName()
        {
            return base.GetType().Name;
        }

        public string GetComponentName()
        {
            return string.Empty;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return new EventDescriptorCollection(null);
        }

        public EventDescriptorCollection GetEvents()
        {
            return new EventDescriptorCollection(null);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);
            PropertyInfo[] info = base.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < info.Length; i++)
            {
                properties.Add(new CustomPropertyDescriptor(info[i], this.IsReadOnly));
            }
            return properties;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return this.GetProperties(null);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        internal virtual bool IsReadOnly
        {
            get
            {
                return !this.explorerNode.EditEnabled;
            }
        }
    }
}
