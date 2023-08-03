using System;
using System.Collections.Generic;

namespace Northwind.Core.DataInterfaces
{
    public interface IDaoFactory
    {
        ICategoryDao GetCategoryDao();
        ICustomerCustomerDemoDao GetCustomerCustomerDemoDao();
        ICustomerDemographicDao GetCustomerDemographicDao();
        ICustomerDao GetCustomerDao();
        IEmployeeDao GetEmployeeDao();
        IEmployeeTerritoryDao GetEmployeeTerritoryDao();
        IOrderDetailDao GetOrderDetailDao();
        IOrderDao GetOrderDao();
        IProductDao GetProductDao();
        IRegionDao GetRegionDao();
        IShipperDao GetShipperDao();
        ISupplierDao GetSupplierDao();
        ITerritoryDao GetTerritoryDao();
    }
}
