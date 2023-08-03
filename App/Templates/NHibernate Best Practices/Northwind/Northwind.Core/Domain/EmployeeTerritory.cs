using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// EmployeeTerritory object for NHibernate mapped table EmployeeTerritories.
    /// </summary>
    [Serializable]
    public class EmployeeTerritory : DomainObject<EmployeeTerritory.DomainObjectID>
    {

        [Serializable]
        public class DomainObjectID
        {
            public DomainObjectID() { }

            private System.Int32 _EmployeeID;
            private System.String _TerritoryID;

            public DomainObjectID(System.Int32 employeeID, System.String territoryID)
            {
                _EmployeeID = employeeID;
                _TerritoryID = territoryID;
            }

            public System.Int32 EmployeeID
            {
                get { return _EmployeeID; }
                protected set { _EmployeeID = value; }
            }

            public System.String TerritoryID
            {
                get { return _TerritoryID; }
                protected set { _TerritoryID = value; }
            }

            public override bool Equals(object obj)
            {
                if (obj == this) return true;
                if (obj == null) return false;

                DomainObjectID that = obj as DomainObjectID;
                if (that == null)
                {
                    return false;
                }
                else
                {
                    if (this.EmployeeID != that.EmployeeID) return false;
                    if (this.TerritoryID != that.TerritoryID) return false;
                    return true;
                }
            }

            public override int GetHashCode()
            {
                return EmployeeID.GetHashCode() ^ TerritoryID.GetHashCode();
            }

        }

        private Employee _EmployeeIDEmployees;
        private Territory _TerritoryIDTerritories;

        public EmployeeTerritory()
        {
        }

        public EmployeeTerritory(DomainObjectID id)
        {
            base.id = id;
        }

        public virtual System.Int32 EmployeeID
        {
            get { return base.id.EmployeeID; }
        }

        public virtual System.String TerritoryID
        {
            get { return base.id.TerritoryID; }
        }

        public virtual Employee EmployeeIDEmployees
        {
            get { return _EmployeeIDEmployees; }
            set { _EmployeeIDEmployees = value; }
        }

        public virtual Territory TerritoryIDTerritories
        {
            get { return _TerritoryIDTerritories; }
            set { _TerritoryIDTerritories = value; }
        }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

    }
}
