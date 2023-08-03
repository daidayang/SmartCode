using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
namespace TaHoGen
{
	public delegate bool PropertyInfoPredicate(PropertyInfo property);
	public delegate void LoadPropertyDelegate(PropertyBag bag);
	internal class PropertyPredicates
	{
		private PropertyPredicates() {}

		public static bool NullPredicate(PropertyInfo property)
		{
			// Do nothing
			return true;
		}
	}
	public class ReflectionHelper
	{
		public static void LoadProperties(object target, PropertyBag bag)
		{
			LoadProperties(target, bag, true);
		}
		public static void LoadProperties(object target, PropertyBag bag, bool checkType)
		{
			LoadProperties(target, bag, new PropertyInfoPredicate(PropertyPredicates.NullPredicate), checkType);
		}
		public static void LoadProperties(object target, PropertyBag bag, PropertyInfoPredicate predicate, bool checkType)
		{

			// Get the list of properties on the target object
			// and try to get the value for each one of its properties
			// from the property bag
			foreach(PropertyInfo pi in target.GetType().GetProperties())
			{
				// Only save the writeable properties
				if (!pi.CanWrite)
					continue;

				// If the property doesn't match the criteria, skip it
				if (!predicate(pi))
					continue;

				if (bag.HasProperty(pi.Name) == false)
					continue;

				object propValue = bag.GetPropertyValue(pi.Name);
			
				// if type checking is enabled, only set the property if the two
				// types are compatible
				if (propValue != null && 
					checkType == true && 
					!pi.PropertyType.IsAssignableFrom(propValue.GetType()))
					continue;
				
				SetProperty(target, pi.Name, propValue);
			}
		}
		public static PropertyBag SaveProperties(object target, PropertyInfoPredicate predicate)
		{
			PropertyMimic result = new PropertyMimic(target.GetType());
			
			// Get the list of properties and persist each one of their
			// values

			PropertyInfo[] propertyList = target.GetType().GetProperties();
			foreach(PropertyInfo pi in propertyList)
			{
				// Only save the readable properties
				if (!pi.CanRead)
					continue;

				// If the property doesn't match the criteria, skip it
				if (!predicate(pi))
					continue;

				result[pi.Name] = pi.GetValue(target, null);
			}
			return result;
		}
		
		public static void SetProperty(object target, string propertyName, object Value)
		{	
			Type thisType = target.GetType();
			
			PropertyInfo targetProperty = GetPropertyInternal(target, propertyName);
			if (targetProperty == null)
			{
				string msg = string.Format("{0}: Unable to set the value for property '{1}'; No such property found.", thisType.ToString(), propertyName);
				throw new Exception(msg);
			}
			
			
			try
			{
				if (targetProperty.CanWrite == false)
					throw new Exception(string.Format("{0} is a readonly property and cannot be set.", targetProperty.Name));

				targetProperty.SetValue(target, Value, null);
			}
			catch(Exception ex)
			{
				string msg = string.Format("{0}: Unable to set the value for property '{1}': {2}", thisType.ToString(), propertyName, ex.ToString());

				throw new Exception(msg, ex);
			}
		}
		public static object GetProperty(object target, string propertyName)
		{
			object result = null;
			PropertyInfo targetProperty = GetPropertyInternal(target, propertyName);

			if (targetProperty != null)
			{
				result = targetProperty.GetValue(target, null);
			}
			return result;
		}
		
		private static PropertyInfo GetPropertyInternal(object target, string propertyName)
		{
			if (target == null)
				throw new NullReferenceException("GetPropertyInternal(): parameter 'target' cannot be null.");
			// Search for the property
			PropertyInfo targetProperty = target.GetType().GetProperty(propertyName);

			return targetProperty;
		}
		
	}
}
