using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Order object for NHibernate mapped table Orders.
    /// </summary>
    [Serializable]
    public class Order : DomainObject<System.Int32>
    {


        private System.String _CustomerID;
        private System.Int32? _EmployeeID;
        private System.DateTime? _OrderDate;
        private System.DateTime? _RequiredDate;
        private System.DateTime? _ShippedDate;
        private System.Int32? _ShipVia;
        private System.Decimal? _Freight;
        private System.String _ShipName;
        private System.String _ShipAddress;
        private System.String _ShipCity;
        private System.String _ShipRegion;
        private System.String _ShipPostalCode;
        private System.String _ShipCountry;
        private Customer _CustomerIDCustomers;
        private Employee _EmployeeIDEmployees;
        private Shipper _ShipViaShippers;
        private IList<OrderDetail> _OrderDetailses = new List<OrderDetail>();

        public Order()
        {
        }

        public Order(System.Int32 id)
        {
            base.id = id;
        }

         public virtual System.String CustomerID {
             get { return _CustomerID; }
             set { _CustomerID = value;}
         }

         public virtual System.Int32? EmployeeID {
             get { return _EmployeeID; }
             set { _EmployeeID = value;}
         }

         public virtual System.DateTime? OrderDate {
             get { return _OrderDate; }
             set { _OrderDate = value;}
         }

         public virtual System.DateTime? RequiredDate {
             get { return _RequiredDate; }
             set { _RequiredDate = value;}
         }

         public virtual System.DateTime? ShippedDate {
             get { return _ShippedDate; }
             set { _ShippedDate = value;}
         }

         public virtual System.Int32? ShipVia {
             get { return _ShipVia; }
             set { _ShipVia = value;}
         }

         public virtual System.Decimal? Freight {
             get { return _Freight; }
             set { _Freight = value;}
         }

         public virtual System.String ShipName {
             get { return _ShipName; }
             set { _ShipName = value;}
         }

         public virtual System.String ShipAddress {
             get { return _ShipAddress; }
             set { _ShipAddress = value;}
         }

         public virtual System.String ShipCity {
             get { return _ShipCity; }
             set { _ShipCity = value;}
         }

         public virtual System.String ShipRegion {
             get { return _ShipRegion; }
             set { _ShipRegion = value;}
         }

         public virtual System.String ShipPostalCode {
             get { return _ShipPostalCode; }
             set { _ShipPostalCode = value;}
         }

         public virtual System.String ShipCountry {
             get { return _ShipCountry; }
             set { _ShipCountry = value;}
         }

         public virtual Customer CustomerIDCustomers{
             get { return _CustomerIDCustomers; }
             set { _CustomerIDCustomers = value;}
         }

         public virtual Employee EmployeeIDEmployees{
             get { return _EmployeeIDEmployees; }
             set { _EmployeeIDEmployees = value;}
         }

         public virtual Shipper ShipViaShippers{
             get { return _ShipViaShippers; }
             set { _ShipViaShippers = value;}
         }

         public virtual IList<OrderDetail> OrderDetailses{
             get { return _OrderDetailses; }
             set { _OrderDetailses = value; }
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
