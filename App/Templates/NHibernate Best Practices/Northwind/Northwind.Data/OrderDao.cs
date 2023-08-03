using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;

namespace Northwind.Data
{
    public class OrderDao : AbstractNHibernateDao<Order, System.Int32>, IOrderDao
    {
    }
}
