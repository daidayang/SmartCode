using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
namespace TaHoGen
{
	internal struct VisitorCacheEntry
	{
		public VisitorCacheEntry(Type visitorType, Type visitableType)
		{
			VisitorType = visitorType;
			VisitableType = visitableType;
		}
		public Type VisitorType;
		public Type VisitableType;
	}
	[AttributeUsage(AttributeTargets.Method)]
	public class VisitorMethodAttribute : Attribute
	{
	}
	public interface IVisitor
	{
	}
	public interface IVisitable
	{
		void Accept(IVisitor v);
	}
	public class Visitable : IVisitable
	{
		private static readonly Hashtable _cache;
		private object _visitableElement ;

        static Visitable()
        {
            _cache = Hashtable.Synchronized(new Hashtable());
        }

		public Visitable()
		{
            _visitableElement = null;
			_visitableElement = this;
		}
		public Visitable(object visitableElement)
		{
            this._visitableElement = null;
            this._visitableElement = visitableElement;

		}
		public object Element
		{
			get { return _visitableElement;  } 
			set { _visitableElement = value; }
		}
		public void Accept(IVisitor v)
		{
            if (v != null)
            {
                v.GetType();
                this.Element.GetType();
                MethodInfo info = this.FindVisitMethod(v);
                if (info != null)
                {
                    Type parameterType = info.GetParameters()[0].ParameterType;
                    info.Invoke(v, new object[] { this.Element });
                }
            }

		}

		public static void Accept(IVisitor visitor, IList visitables)
		{
			foreach(object visitable in visitables)
			{
				Visitable v = new Visitable(visitable);
				v.Accept(visitor);
			}
		}
		private MethodInfo FindVisitMethod(IVisitor v)
		{
			// Use the results from the previous searches
			Type visitorType = v.GetType();
			Debug.Assert(_visitableElement != null);
			VisitorCacheEntry entry = new VisitorCacheEntry(visitorType, _visitableElement.GetType());

			if (_cache.Contains(entry))
				return _cache[entry] as MethodInfo;


			BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo[] methods = visitorType.GetMethods(flags);

			MethodInfo result = null;
			foreach(MethodInfo method in methods)
			{
				// We're only interested in visitor methods
				if (!method.IsDefined(typeof(VisitorMethodAttribute), false))
					continue;

				// The method must have one parameter that either matches this type
				// or it has to be compatible with this type
				ParameterInfo[] parameters = method.GetParameters();

				if (parameters == null || parameters.Length != 1)
					continue;

				if (!parameters[0].ParameterType.IsAssignableFrom(Element.GetType()))
					continue;
			
				// Cache the result for later use
				_cache[entry] = method;

				result = method;
				break;
				
			}
			return result;
		}
	}
}
