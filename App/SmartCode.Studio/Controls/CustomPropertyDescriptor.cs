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
using System.ComponentModel;
using System.Reflection;

namespace SmartCode.Studio.Controls
{

    internal class CustomPropertyDescriptor : PropertyDescriptor
    {

        private string category;
        private string description;
        private string displayName;
        private PropertyInfo propInfo;
        private bool readOnly;

        public CustomPropertyDescriptor(PropertyInfo propInfo, bool readOnly)
            : base(propInfo.Name, CustomPropertyDescriptor.ToAttrArray(propInfo.GetCustomAttributes(true)))
        {
            this.propInfo = propInfo;
            this.readOnly = readOnly;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return this.propInfo.GetValue(component, null);
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            this.propInfo.SetValue(component, value, null);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        internal static Attribute[] ToAttrArray(object[] objects)
        {
            Attribute[] attributes = new Attribute[objects.Length + 1];
            for (int i = 0; i < objects.Length; i++)
            {
                attributes[i] = (Attribute)objects[i];
            }
            attributes[objects.Length] = new RefreshPropertiesAttribute(RefreshProperties.All);
            return attributes;
        }


        public override string Category
        {
            get
            {
                if (this.category == null)
                {
                    this.category = ((DisplayTextAttribute)this.Attributes[typeof(DisplayTextAttribute)]).Category;
                }
                return this.category;
            }
        }

        public override Type ComponentType
        {
            get
            {
                return this.propInfo.ReflectedType;
            }
        }

        public override string Description
        {
            get
            {
                if (this.description == null)
                {
                    this.description = ((DisplayTextAttribute)this.Attributes[typeof(DisplayTextAttribute)]).Description;
                }
                return this.description;
            }
        }

        public override string DisplayName
        {
            get
            {
                if (this.displayName == null)
                {
                    this.displayName = ((DisplayTextAttribute)this.Attributes[typeof(DisplayTextAttribute)]).Name;
                }
                return this.displayName;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                if (!this.readOnly && this.propInfo.CanWrite)
                {
                    return ((ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)]).IsReadOnly;
                }
                return true;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return this.propInfo.PropertyType;
            }
        }


    }
}
