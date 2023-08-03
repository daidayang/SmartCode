using System;
using System.Collections.Generic;

namespace Northwind.Core.Domain
{
    /// <summary>
    /// Region object for NHibernate mapped table Region.
    /// </summary>
    [Serializable]
    public class Region : DomainObject<System.Int32>
    {


        private System.String _RegionDescription;
        private IList<Territory> _Territorieses = new List<Territory>();

        public Region()
        {
        }

        public Region(System.Int32 id)
        {
            base.id = id;
        }

         public virtual System.String RegionDescription {
             get { return _RegionDescription; }
             set { _RegionDescription = value;}
         }

         public virtual IList<Territory> Territorieses{
             get { return _Territorieses; }
             set { _Territorieses = value; }
         }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

     }
}
