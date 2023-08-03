using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Supplier object for NHibernate mapped table Suppliers.
    /// </summary>
    [Serializable]
    public class Supplier : DomainObject<System.Int32>
    {


        private System.String _CompanyName;
        private System.String _ContactName;
        private System.String _ContactTitle;
        private System.String _Address;
        private System.String _City;
        private System.String _Region;
        private System.String _PostalCode;
        private System.String _Country;
        private System.String _Phone;
        private System.String _Fax;
        private System.String _HomePage;
        private IList<Product> _Productses = new List<Product>();

        public Supplier()
        {
        }

        public Supplier(System.Int32 id)
        {
            base.id = id;
        }

         public virtual System.String CompanyName {
             get { return _CompanyName; }
             set { _CompanyName = value;}
         }

         public virtual System.String ContactName {
             get { return _ContactName; }
             set { _ContactName = value;}
         }

         public virtual System.String ContactTitle {
             get { return _ContactTitle; }
             set { _ContactTitle = value;}
         }

         public virtual System.String Address {
             get { return _Address; }
             set { _Address = value;}
         }

         public virtual System.String City {
             get { return _City; }
             set { _City = value;}
         }

         public virtual System.String Region {
             get { return _Region; }
             set { _Region = value;}
         }

         public virtual System.String PostalCode {
             get { return _PostalCode; }
             set { _PostalCode = value;}
         }

         public virtual System.String Country {
             get { return _Country; }
             set { _Country = value;}
         }

         public virtual System.String Phone {
             get { return _Phone; }
             set { _Phone = value;}
         }

         public virtual System.String Fax {
             get { return _Fax; }
             set { _Fax = value;}
         }

         public virtual System.String HomePage {
             get { return _HomePage; }
             set { _HomePage = value;}
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
