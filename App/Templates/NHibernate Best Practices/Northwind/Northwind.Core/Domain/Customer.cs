using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Customer object for NHibernate mapped table Customers.
    /// </summary>
    [Serializable]
    public class Customer : DomainObject<System.String>
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
        private IList<CustomerCustomerDemo> _CustomerDemographicses = new List<CustomerCustomerDemo>();
        private IList<Order> _Orderses = new List<Order>();

        public Customer()
        {
        }

        public Customer(System.String id)
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

         public virtual IList<CustomerCustomerDemo> CustomerDemographicses{
             get { return _CustomerDemographicses; }
             set { _CustomerDemographicses = value; }
         }

         public virtual IList<Order> Orderses{
             get { return _Orderses; }
             set { _Orderses = value; }
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
