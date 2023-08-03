using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Employee object for NHibernate mapped table Employees.
    /// </summary>
    [Serializable]
    public class Employee : DomainObject<System.Int32>
    {


        private System.String _LastName;
        private System.String _FirstName;
        private System.String _Title;
        private System.String _TitleOfCourtesy;
        private System.DateTime? _BirthDate;
        private System.DateTime? _HireDate;
        private System.String _Address;
        private System.String _City;
        private System.String _Region;
        private System.String _PostalCode;
        private System.String _Country;
        private System.String _HomePhone;
        private System.String _Extension;
        private System.Byte[] _Photo;
        private System.String _Notes;
        private System.Int32? _ReportsTo;
        private System.String _PhotoPath;
        private Employee _ReportsToEmployees;
        private IList<Employee> _Employeeses = new List<Employee>();
        private IList<EmployeeTerritory> _Territorieses = new List<EmployeeTerritory>();
        private IList<Order> _Orderses = new List<Order>();

        public Employee()
        {
        }

        public Employee(System.Int32 id)
        {
            base.id = id;
        }

         public virtual System.String LastName {
             get { return _LastName; }
             set { _LastName = value;}
         }

         public virtual System.String FirstName {
             get { return _FirstName; }
             set { _FirstName = value;}
         }

         public virtual System.String Title {
             get { return _Title; }
             set { _Title = value;}
         }

         public virtual System.String TitleOfCourtesy {
             get { return _TitleOfCourtesy; }
             set { _TitleOfCourtesy = value;}
         }

         public virtual System.DateTime? BirthDate {
             get { return _BirthDate; }
             set { _BirthDate = value;}
         }

         public virtual System.DateTime? HireDate {
             get { return _HireDate; }
             set { _HireDate = value;}
         }

         public virtual System.String Address {
             get { return _Address; }
             set { _Address = value;}
         }

         public virtual System.String City {
             get { return _City; }
             set { _City = value;}
         }

         public virtual System.String Region {
             get { return _Region; }
             set { _Region = value;}
         }

         public virtual System.String PostalCode {
             get { return _PostalCode; }
             set { _PostalCode = value;}
         }

         public virtual System.String Country {
             get { return _Country; }
             set { _Country = value;}
         }

         public virtual System.String HomePhone {
             get { return _HomePhone; }
             set { _HomePhone = value;}
         }

         public virtual System.String Extension {
             get { return _Extension; }
             set { _Extension = value;}
         }

         public virtual System.Byte[] Photo {
             get { return _Photo; }
             set { _Photo = value;}
         }

         public virtual System.String Notes {
             get { return _Notes; }
             set { _Notes = value;}
         }

         public virtual System.Int32? ReportsTo {
             get { return _ReportsTo; }
             set { _ReportsTo = value;}
         }

         public virtual System.String PhotoPath {
             get { return _PhotoPath; }
             set { _PhotoPath = value;}
         }

         public virtual Employee ReportsToEmployees{
             get { return _ReportsToEmployees; }
             set { _ReportsToEmployees = value;}
         }

         public virtual IList<Employee> Employeeses{
             get { return _Employeeses; }
             set { _Employeeses = value; }
         }

         public virtual IList<EmployeeTerritory> Territorieses{
             get { return _Territorieses; }
             set { _Territorieses = value; }
         }

         public virtual IList<Order> Orderses{
             get { return _Orderses; }
             set { _Orderses = value; }
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
