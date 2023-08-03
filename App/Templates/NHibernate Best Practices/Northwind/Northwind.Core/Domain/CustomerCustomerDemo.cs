using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// CustomerCustomerDemo object for NHibernate mapped table CustomerCustomerDemo.
    /// </summary>
    [Serializable]
    public class CustomerCustomerDemo : DomainObject<CustomerCustomerDemo.DomainObjectID>
    {

        [Serializable]
        public class DomainObjectID
        {
            public DomainObjectID() {}

            private System.String _CustomerID;
            private System.String _CustomerTypeID;

            public DomainObjectID(System.String customerID, System.String customerTypeID)
            {
                _CustomerID = customerID;
                _CustomerTypeID = customerTypeID;
            }

         public System.String CustomerID {
             get { return _CustomerID; }
             protected set { _CustomerID = value;}
         }

         public System.String CustomerTypeID {
             get { return _CustomerTypeID; }
             protected set { _CustomerTypeID = value;}
         }


         public override bool Equals(object obj)
         {
             if (obj == this) return true;
             if (obj == null) return false;

             DomainObjectID that = obj as DomainObjectID;
             if (that == null)
             {
                 return false;
             }
             else
             {
                 if (this.CustomerID != that.CustomerID) return false;
                 if (this.CustomerTypeID != that.CustomerTypeID) return false;

                 return true;
             }

         }

            public override int GetHashCode()
            {
                return CustomerID.GetHashCode() ^ CustomerTypeID.GetHashCode();
            }

        }

        private Customer _CustomerIDCustomers;
        private CustomerDemographic _CustomerTypeIDCustomerDemographics;

        public CustomerCustomerDemo()
        {
        }

        public CustomerCustomerDemo(DomainObjectID id)
        {
            base.id = id;
        }

         public virtual System.String CustomerID {
             get { return base.id.CustomerID; }
         }

         public virtual System.String CustomerTypeID {
             get { return base.id.CustomerTypeID; }
         }

         public virtual Customer CustomerIDCustomers{
             get { return _CustomerIDCustomers; }
             set { _CustomerIDCustomers = value;}
         }

         public virtual CustomerDemographic CustomerTypeIDCustomerDemographics{
             get { return _CustomerTypeIDCustomerDemographics; }
             set { _CustomerTypeIDCustomerDemographics = value;}
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
