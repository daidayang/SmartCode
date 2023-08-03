using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;
using Northwind.Data;

namespace Northwind.Data
{
    public class EmployeeTerritoryDao : AbstractNHibernateDao<EmployeeTerritory, EmployeeTerritory.DomainObjectID>, IEmployeeTerritoryDao
    {
    }
}
