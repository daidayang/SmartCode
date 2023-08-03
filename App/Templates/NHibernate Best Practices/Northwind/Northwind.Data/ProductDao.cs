using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;

namespace Northwind.Data
{
    public class ProductDao : AbstractNHibernateDao<Product, System.Int32>, IProductDao
    {
    }
}
