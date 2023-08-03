using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// CustomerDemographic object for NHibernate mapped table CustomerDemographics.
    /// </summary>
    [Serializable]
    public class CustomerDemographic : DomainObject<System.String>
    {


        private System.String _CustomerDesc;
        private IList<CustomerCustomerDemo> _Customerses = new List<CustomerCustomerDemo>();

        public CustomerDemographic()
        {
        }

        public CustomerDemographic(System.String id)
        {
            base.id = id;
        }

         public virtual System.String CustomerDesc {
             get { return _CustomerDesc; }
             set { _CustomerDesc = value;}
         }

         public virtual IList<CustomerCustomerDemo> Customerses{
             get { return _Customerses; }
             set { _Customerses = value; }
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
