using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;

namespace Northwind.Data
{
    public class ShipperDao : AbstractNHibernateDao<Shipper, System.Int32>, IShipperDao
    {
    }
}
