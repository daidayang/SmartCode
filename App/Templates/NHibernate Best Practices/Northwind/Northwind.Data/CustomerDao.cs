using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;

namespace Northwind.Data
{
    public class CustomerDao : AbstractNHibernateDao<Customer, System.String>, ICustomerDao
    {
    }
}
