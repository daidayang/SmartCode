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
using System.Reflection;
using SmartCode.Model;

namespace SmartCode.Studio.Utils
{
	/// <summary>
	/// Summary description for Mapper.
	/// </summary>
	public class Mapper
	{
		public enum Behaviour
		{
			FieldsToFields,
			FieldsToProperties,
			PropertiesToFields,
			PropertiesToProperties,
			PropertiesPropertiesDataEntity
		};

		public Mapper()
		{
		}
		
		public static void Map( Object from, Object to )
		{
			Map( from, to, true, Behaviour.FieldsToFields );
		}

		public static void Map( Object from, Object to, Behaviour behaviour )
		{
			Map( from, to, true, behaviour );
		}

		public static void Map( Object from, Object to, bool caseSensitive, Behaviour behaviour )
		{
			switch ( behaviour )
			{
				case Behaviour.FieldsToFields:
					MapFieldsToFields( from, to, caseSensitive );
					break;
				case Behaviour.FieldsToProperties:
					MapFieldsToProperties( from, to, caseSensitive );
					break;
				case Behaviour.PropertiesToFields:
					MapPropertiesToFields( from, to, caseSensitive );
					break;
				case Behaviour.PropertiesToProperties:
					MapPropertiesToProperties( from, to, caseSensitive );
					break;
				case Behaviour.PropertiesPropertiesDataEntity:
					MapPropertiesToPropertiesDataEntity( from, to, caseSensitive );
					break;
			}
		}

		private static void MapFieldsToFields( Object from, Object to, bool caseSensitive )
		{
			FieldInfo toFieldInfo;
			try
			{
				Type fromType = from.GetType();
				Type toType = to.GetType();

				foreach( FieldInfo fromFieldInfo in fromType.GetFields() )
				{
					if( caseSensitive )
						toFieldInfo = toType.GetField( fromFieldInfo.Name );
					else
						toFieldInfo = toType.GetField( fromFieldInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase );

					// if field exists, set value
					if( toFieldInfo != null )
					{
						if ( fromFieldInfo.FieldType.IsArray )
						{
							toFieldInfo.SetValue( to, fromFieldInfo.GetValue( from ) );
						}
						else
						{
							toFieldInfo.SetValue( to, fromFieldInfo.GetValue( from ) );
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private static void MapFieldsToProperties( Object from, Object to, bool caseSensitive )
		{
			PropertyInfo toPropertyInfo;
			try
			{
				Type fromType = from.GetType();
				Type toType = to.GetType();
				

				foreach( FieldInfo fromFieldInfo in fromType.GetFields() )
				{
					//PropertyInfo toPropertyInfo;
					if( caseSensitive )
						toPropertyInfo = toType.GetProperty( fromFieldInfo.Name );
					else
						toPropertyInfo = toType.GetProperty( fromFieldInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase );

					// if field exists, set value
					if( toPropertyInfo != null )
					{
						//if ( fromFieldInfo.FieldType.IsArray )
						//{
						//	throw new NotImplementedException();
						//}
						//else
					{
						toPropertyInfo.SetValue( to, fromFieldInfo.GetValue( from ), null );
					}
					}
				}
			}
			catch ( Exception )
			{
				throw;
			}
		}

		private static void MapPropertiesToFields( Object from, Object to, bool caseSensitive )
		{
			FieldInfo toFieldInfo;
			Type fromType = from.GetType();
			Type toType = to.GetType();

			try
			{
				foreach( PropertyInfo fromPropertyInfo in fromType.GetProperties() )
				{
					if( caseSensitive )
						toFieldInfo = toType.GetField( fromPropertyInfo.Name );
					else
						toFieldInfo = toType.GetField( fromPropertyInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase );
				
					// if field exists, set value
					if( toFieldInfo != null )
					{
						toFieldInfo.SetValue( to, fromPropertyInfo.GetValue( from, null ) );
					}
				}
			}
			catch ( Exception )
			{
				throw;
			}

		}

		private static void MapPropertiesToProperties( Object from, Object to, bool caseSensitive )
		{
			Type fromType = from.GetType();
			Type toType = to.GetType();

			foreach( PropertyInfo fromPropertyInfo in fromType.GetProperties() )
			{
				PropertyInfo toPropertyInfo;
				if( caseSensitive )
					toPropertyInfo = toType.GetProperty( fromPropertyInfo.Name );
				else
					toPropertyInfo = toType.GetProperty( fromPropertyInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase );

				// if field exists, set value
				if( toPropertyInfo != null )
				{
					if (toPropertyInfo.PropertyType != fromPropertyInfo.PropertyType)
						continue;
					/*if ( fromPropertyInfo.PropertyType.IsArray )
					{
						throw new NotImplementedException();
					}
					else*/
					{
						
						if (toPropertyInfo.CanWrite)
							toPropertyInfo.SetValue( to, fromPropertyInfo.GetValue( from, null ), null );
					}
				}
			}
		}

		private static void MapPropertiesToPropertiesDataEntity( Object from, Object to, bool caseSensitive )
		{
			Type fromType = from.GetType();
			Type toType = to.GetType();

			foreach( PropertyInfo fromPropertyInfo in fromType.GetProperties() )
			{
				PropertyInfo toPropertyInfo;
				if( caseSensitive )
					toPropertyInfo = toType.GetProperty( fromPropertyInfo.Name );
				else
					toPropertyInfo = toType.GetProperty( fromPropertyInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase );

				
				// if field exists, set value
				if( toPropertyInfo != null )
				{
					if (toPropertyInfo.Name == "ID")
						continue;

					if (toPropertyInfo.PropertyType != fromPropertyInfo.PropertyType)
						continue;

					if ( fromPropertyInfo.PropertyType.IsArray )
					{
						throw new NotImplementedException();
					}
					else
					{
						if (toPropertyInfo.CanWrite)
						{
							object val = fromPropertyInfo.GetValue( from, null );
							if (val != null)
							toPropertyInfo.SetValue( to, fromPropertyInfo.GetValue( from, null ), null );
						}
					}
				}
			}
		}
		


	}
}
