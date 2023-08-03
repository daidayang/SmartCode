using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Shipper object for NHibernate mapped table Shippers.
    /// </summary>
    [Serializable]
    public class Shipper : DomainObject<System.Int32>
    {


        private System.String _CompanyName;
        private System.String _Phone;
        private IList<Order> _Orderses = new List<Order>();

        public Shipper()
        {
        }

        public Shipper(System.Int32 id)
        {
            base.id = id;
        }

         public virtual System.String CompanyName {
             get { return _CompanyName; }
             set { _CompanyName = value;}
         }

         public virtual System.String Phone {
             get { return _Phone; }
             set { _Phone = value;}
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
