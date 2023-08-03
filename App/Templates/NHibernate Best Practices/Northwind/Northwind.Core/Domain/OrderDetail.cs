using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// OrderDetail object for NHibernate mapped table Order Details.
    /// </summary>
    [Serializable]
    public class OrderDetail : DomainObject<OrderDetail.DomainObjectID>
    {

        [Serializable]
        public class DomainObjectID
        {
            public DomainObjectID() {}

            private System.Int32 _OrderID;
            private System.Int32 _ProductID;

            public DomainObjectID(System.Int32 orderID, System.Int32 productID)
            {
                _OrderID = orderID;
                _ProductID = productID;
            }

         public System.Int32 OrderID {
             get { return _OrderID; }
             protected set { _OrderID = value;}
         }

         public System.Int32 ProductID {
             get { return _ProductID; }
             protected set { _ProductID = value;}
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
                 if (this.OrderID != that.OrderID) return false;
                 if (this.ProductID != that.ProductID) return false;

                 return true;
             }

         }

            public override int GetHashCode()
            {
                return OrderID.GetHashCode() ^ ProductID.GetHashCode();
            }

        }

        private System.Decimal _UnitPrice;
        private System.Int16 _Quantity;
        private System.Double _Discount;
        private Order _OrderIDOrders;
        private Product _ProductIDProducts;

        public OrderDetail()
        {
        }

        public OrderDetail(DomainObjectID id)
        {
            base.id = id;
        }

         public virtual System.Int32 OrderID {
             get { return base.id.OrderID; }
         }

         public virtual System.Int32 ProductID {
             get { return base.id.ProductID; }
         }

         public virtual System.Decimal UnitPrice {
             get { return _UnitPrice; }
             set { _UnitPrice = value;}
         }

         public virtual System.Int16 Quantity {
             get { return _Quantity; }
             set { _Quantity = value;}
         }

         public virtual System.Double Discount {
             get { return _Discount; }
             set { _Discount = value;}
         }

         public virtual Order OrderIDOrders{
             get { return _OrderIDOrders; }
             set { _OrderIDOrders = value;}
         }

         public virtual Product ProductIDProducts{
             get { return _ProductIDProducts; }
             set { _ProductIDProducts = value;}
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
