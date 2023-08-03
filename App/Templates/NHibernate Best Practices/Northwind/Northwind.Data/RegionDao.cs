using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;

namespace Northwind.Data
{
    public class RegionDao : AbstractNHibernateDao<Region, System.Int32>, IRegionDao
    {
    }
}
