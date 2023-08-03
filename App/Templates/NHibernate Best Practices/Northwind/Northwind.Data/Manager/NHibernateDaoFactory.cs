using System;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;

namespace Northwind.Data
{
    public class NHibernateDaoFactory : IDaoFactory
    {
        public ICategoryDao GetCategoryDao()
        {
            return new CategoryDao();
        }
        public ICustomerCustomerDemoDao GetCustomerCustomerDemoDao()
        {
            return new CustomerCustomerDemoDao();
        }
        public ICustomerDemographicDao GetCustomerDemographicDao()
        {
            return new CustomerDemographicDao();
        }
        public ICustomerDao GetCustomerDao()
        {
            return new CustomerDao();
        }
        public IEmployeeDao GetEmployeeDao()
        {
            return new EmployeeDao();
        }
        public IEmployeeTerritoryDao GetEmployeeTerritoryDao()
        {
            return new EmployeeTerritoryDao();
        }
        public IOrderDetailDao GetOrderDetailDao()
        {
            return new OrderDetailDao();
        }
        public IOrderDao GetOrderDao()
        {
            return new OrderDao();
        }
        public IProductDao GetProductDao()
        {
            return new ProductDao();
        }
        public IRegionDao GetRegionDao()
        {
            return new RegionDao();
        }
        public IShipperDao GetShipperDao()
        {
            return new ShipperDao();
        }
        public ISupplierDao GetSupplierDao()
        {
            return new SupplierDao();
        }
        public ITerritoryDao GetTerritoryDao()
        {
            return new TerritoryDao();
        }
    }
}
