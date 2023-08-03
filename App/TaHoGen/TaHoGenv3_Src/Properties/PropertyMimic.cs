using System;
using System.ComponentModel;
using System.Reflection;

namespace TaHoGen
{
	/// <summary>
	/// An extension of the PropertyTable class that assumes the properties of any
	/// type that it is given.
	/// </summary>
	/// 
	[Serializable]
	public class PropertyMimic : PropertyTable
	{
		private System.Type _subject = null;
		public PropertyMimic() : base()
		{
			
		}
		public PropertyMimic(System.Type subject) : this()
		{
			_subject = subject;
			
			Mimic(Subject);
		}
		public System.Type Subject
		{
			get { return _subject; }
		}
		
		public void Mimic(System.Type subject)
		{
			Mimic(subject, null);
		}
		public void Mimic(System.Type subject, PropertyInfoPredicate predicate)
		{
			
			// Read the properties of the subject
			PropertyInfo[] pi = subject.GetProperties();

			// If there aren't any properties, do nothing
			if (pi == null)
				return;
			
			Mimic(pi, predicate);
			
		}
		public void Mimic(PropertyInfo[] pi)
		{
			Mimic(pi, null);
		}
		public void Mimic(PropertyInfo[] pi, PropertyInfoPredicate predicate)
		{
			if (pi == null)
				return;

			// Clear the current list of properties (if any)
			if (Properties.Count > 0)
			{
				Properties.Clear();
				Clear();
			}

			foreach(PropertyInfo property in pi)
			{
				// Skip the property if it fails the predicate condition
				if (predicate != null && predicate(property) == false)
					continue;

				// Create a property that looks like the type's property
				PropertySpec spec = CreatePropertySpec(property);

				// Add it to the list of properties
				if (spec != null)
					Properties.Add(spec);
			}
		}
		public override string GetClassName()
		{
			if (_subject == null)
				return base.GetClassName ();

			return _subject.FullName;
		}

		private PropertySpec CreatePropertySpec(PropertyInfo pi)
		{
			string name = pi.Name;
			Type propertyType = pi.PropertyType;
			
			// Get the name, category, description, type,
			// and default value of the property
			PropertyHelper helper = new PropertyHelper(pi);

			PropertySpec result = new PropertySpec(pi.Name, pi.PropertyType);
			
			if (helper.Category.Length > 0)
				result.Category = helper.Category;

			if (helper.DefaultValue != null)
				result.DefaultValue = helper.DefaultValue;

			if (helper.Description != null)
				result.Description = helper.Description;

	
			return result;
		}

		
	}
}
