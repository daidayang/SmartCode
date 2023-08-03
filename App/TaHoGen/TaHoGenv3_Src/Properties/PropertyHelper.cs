using System;
using System.ComponentModel;
using System.Reflection;
namespace TaHoGen
{
	/// <summary>
	/// A helper class to extract the Name,
	/// Description, Category, and Default value from a PropertyInfo object.
	/// </summary>
	public class PropertyHelper
	{
		private PropertyInfo _propertyInfo = null;
		public PropertyHelper(PropertyInfo pi)
		{
			_propertyInfo = pi;
		}
		public string Name
		{
			get { return _propertyInfo.Name; }
		}
		public string Description
		{
			get
			{
				string result = string.Empty;

				if (_propertyInfo != null)
				{
					result = GetDescription(_propertyInfo);
				}
				return result;
			}
		}
		public string Category
		{
			get
			{
				string result = string.Empty;
				if (_propertyInfo != null)
					result = GetCategoryName(_propertyInfo);

				return result;
			}
		}
		public object DefaultValue
		{
			get
			{
				object result = null;

				if (_propertyInfo != null)
					result = GetDefaultValue(_propertyInfo);

				return result;
			}
		}
		public System.Type PropertyType
		{
			get { return _propertyInfo.PropertyType; }
		}
		private string GetDescription(PropertyInfo pi)
		{
			string result = string.Empty;

			Attribute descriptionAttribute = GetCustomAttribute(typeof(DescriptionAttribute), pi);

			if (descriptionAttribute == null)
				return string.Empty;

			DescriptionAttribute desc = descriptionAttribute as DescriptionAttribute;

			if (desc == null)
				return string.Empty;

			result = desc.Description;

			return result;

		}
		private object GetDefaultValue(PropertyInfo pi)
		{
			object result = null;

			// Find the default value attribute
			Attribute defaultvalueAttribute = GetCustomAttribute(typeof(DefaultValueAttribute), pi);

			if (defaultvalueAttribute == null)
				return null;

			// if it exists, get the name of the category 
			DefaultValueAttribute defaultValue = defaultvalueAttribute as DefaultValueAttribute;

			if (defaultValue == null)
				return null;

			result = defaultValue.Value;


			return result;
		}
		private string GetCategoryName(PropertyInfo pi)
		{
			string result = string.Empty;

			// Find the category attribute
			Attribute categoryAttribute = GetCustomAttribute(typeof(CategoryAttribute), pi);

			if (categoryAttribute == null)
				return string.Empty;

			// if it exists, get the name of the category 
			CategoryAttribute category = categoryAttribute as CategoryAttribute;

			if (category == null)
				return string.Empty;

			result = category.Category;


			return result;
		}
		private System.Attribute GetCustomAttribute(System.Type attributeType, PropertyInfo pi)
		{
			System.Attribute result = null;
			
			// Is the attribute even defined on this property? 
			if (!pi.IsDefined(attributeType, true))
				return null;

			// Search for the attribute that matches the attribute type
			foreach(System.Attribute attr in pi.GetCustomAttributes(true))
			{
				if (attr.GetType() == attributeType)
					return attr;
			}
			
			return result;
		}
	}
}
