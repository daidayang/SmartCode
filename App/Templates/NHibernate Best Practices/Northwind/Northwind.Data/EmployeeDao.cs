using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;

namespace Northwind.Data
{
    public class EmployeeDao : AbstractNHibernateDao<Employee, System.Int32>, IEmployeeDao
    {
    }
}
