using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Product object for NHibernate mapped table Products.
    /// </summary>
    [Serializable]
    public class Product : DomainObject<System.Int32>
    {


        private System.String _ProductName;
        private System.Int32? _SupplierID;
        private System.Int32? _CategoryID;
        private System.String _QuantityPerUnit;
        private System.Decimal? _UnitPrice;
        private System.Int16? _UnitsInStock;
        private System.Int16? _UnitsOnOrder;
        private System.Int16? _ReorderLevel;
        private System.Boolean _Discontinued;
        private Category _CategoryIDCategories;
        private Supplier _SupplierIDSuppliers;
        private IList<OrderDetail> _OrderDetailses = new List<OrderDetail>();

        public Product()
        {
        }

        public Product(System.Int32 id)
        {
            base.id = id;
        }

        public virtual System.String ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        public virtual System.Int32? SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }

        public virtual System.Int32? CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        public virtual System.String QuantityPerUnit
        {
            get { return _QuantityPerUnit; }
            set { _QuantityPerUnit = value; }
        }

        public virtual System.Decimal? UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }

        public virtual System.Int16? UnitsInStock
        {
            get { return _UnitsInStock; }
            set { _UnitsInStock = value; }
        }

        public virtual System.Int16? UnitsOnOrder
        {
            get { return _UnitsOnOrder; }
            set { _UnitsOnOrder = value; }
        }

        public virtual System.Int16? ReorderLevel
        {
            get { return _ReorderLevel; }
            set { _ReorderLevel = value; }
        }

        public virtual System.Boolean Discontinued
        {
            get { return _Discontinued; }
            set { _Discontinued = value; }
        }

        public virtual Category CategoryIDCategories
        {
            get { return _CategoryIDCategories; }
            set { _CategoryIDCategories = value; }
        }

        public virtual Supplier SupplierIDSuppliers
        {
            get { return _SupplierIDSuppliers; }
            set { _SupplierIDSuppliers = value; }
        }

        public virtual IList<OrderDetail> OrderDetailses
        {
            get { return _OrderDetailses; }
            set { _OrderDetailses = value; }
        }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

    }
}
