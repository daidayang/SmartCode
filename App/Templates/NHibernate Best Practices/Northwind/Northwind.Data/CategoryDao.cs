using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Core.DataInterfaces;
using Northwind.Core.Domain;

namespace Northwind.Data
{
    public class CategoryDao : AbstractNHibernateDao<Category, System.Int32>, ICategoryDao
    {

    }
}
