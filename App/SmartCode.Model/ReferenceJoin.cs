/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SmartCode.Model
{
    /// <summary>
    ///  ReferenceJoin stores for a Reference object the mapping between the column 
    ///  in the Parent Entity and the foreign key column in the child Entity
    /// </summary>
    [Serializable()]
    public class ReferenceJoin : NamedObject, ICloneable
    {
        #region Fields
        private ReferenceSchema parentReference;


        private ColumnSchema parentColumn;
        private ColumnSchema childColumn;

        private IList<ColumnSchema> lov;

        #endregion

        #region Constructors
        private ReferenceJoin()
            : base("")
        {

        }

        public ReferenceJoin(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.parentReference = (ReferenceSchema)Info.GetValue("parentReference", typeof(ReferenceSchema));
            this.parentColumn = (ColumnSchema)Info.GetValue("parentColumn", typeof(ColumnSchema));
            this.childColumn = (ColumnSchema)Info.GetValue("childColumn", typeof(ColumnSchema));
            this.lov = (IList<ColumnSchema>)Info.GetValue("lov", typeof(IList<ColumnSchema>));
        }
       
        /// <summary>
        /// Creates a new reference between the two given columns.
        /// </summary>
        /// <param name="localColumn"></param>
        /// <param name="foreignColumn"></param>
        public ReferenceJoin(ReferenceSchema parentReference, ColumnSchema parentColumn, ColumnSchema childColumn)
            : base(parentColumn.Name + "->" + childColumn.Name)
        {
            this.parentReference = parentReference;
            this.parentColumn = parentColumn;
            this.childColumn = childColumn;
            this.lov = new List<ColumnSchema>();
        }
        #endregion

        #region Properties

        public ReferenceSchema ParentReference
        {
            get { return parentReference; }
        }

        public ColumnSchema ParentColumn
        {
            get { return parentColumn; }
        }

        public ColumnSchema ChildColumn
        {
            get { return childColumn; }
        }

        public IList<ColumnSchema> LOV
        {
            get { return lov; }
        }

        #endregion


        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(this.parentColumn.Name);
            result.Append(" -> ");
            result.Append(this.childColumn.Name);

            return result.ToString ();
        }

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("parentReference", this.parentReference);
            Info.AddValue("parentColumn", this.parentColumn);
            Info.AddValue("childColumn", this.childColumn);
            Info.AddValue("lov", this.lov);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new ReferenceJoin(this.parentReference, this.parentColumn, this.childColumn);
        }

        #endregion
    }
}
