using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Territory object for NHibernate mapped table Territories.
    /// </summary>
    [Serializable]
    public class Territory : DomainObject<System.String>
    {


        private System.String _TerritoryDescription;
        private System.Int32 _RegionID;
        private Region _RegionIDRegion;
        private IList<EmployeeTerritory> _Employeeses = new List<EmployeeTerritory>();

        public Territory()
        {
        }

        public Territory(System.String id)
        {
            base.id = id;
        }

         public virtual System.String TerritoryDescription {
             get { return _TerritoryDescription; }
             set { _TerritoryDescription = value;}
         }

         public virtual System.Int32 RegionID {
             get { return _RegionID; }
             set { _RegionID = value;}
         }

         public virtual Region RegionIDRegion{
             get { return _RegionIDRegion; }
             set { _RegionIDRegion = value;}
         }

         public virtual IList<EmployeeTerritory> Employeeses{
             get { return _Employeeses; }
             set { _Employeeses = value; }
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
