using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Category object for NHibernate mapped table Categories.
    /// </summary>
    [Serializable]
    public class Category : DomainObject<System.Int32>
    {


        private System.String _CategoryName;
        private System.String _Description;
        private System.Byte[] _Picture;
        private IList<Product> _Productses = new List<Product>();

        public Category()
        {
        }

        public Category(System.Int32 id)
        {
            base.id = id;
        }

         public virtual System.String CategoryName {
             get { return _CategoryName; }
             set { _CategoryName = value;}
         }

         public virtual System.String Description {
             get { return _Description; }
             set { _Description = value;}
         }

         public virtual System.Byte[] Picture {
             get { return _Picture; }
             set { _Picture = value;}
         }

         public virtual IList<Product> Productses{
             get { return _Productses; }
             set { _Productses = value; }
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
