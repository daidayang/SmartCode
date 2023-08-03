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
    ///  ReferenceJoin stores for a ReferenceSchema object the mapping between the column 
    ///  in the Parent Entity and the foreign key column in the child Entity
    /// </summary>
    [Serializable()]
    public class ReferenceSchema : NamedObject, ICloneable
    {
        public enum AlignmentType
        {
            Left,
            Right,
            Inner,
        }
        private IList<ReferenceJoin> joins;

        private bool onDeleteCascade;
        private bool onUpdateCascade;

        private TableSchema parentTable;
        private TableSchema childTable;

        private AlignmentType alignment;

        #region Constructor
   
        private ReferenceSchema()
            : base("")
        {
        }

        public ReferenceSchema(string name, TableSchema parentTable, TableSchema childTable)
            : base(name)
        {
            this.joins = new List<ReferenceJoin>();
            this.parentTable = parentTable;
            this.childTable = childTable;
            this.alignment = AlignmentType.Left;
        }

        public ReferenceSchema(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.joins = (IList<ReferenceJoin>)Info.GetValue("joins", typeof(IList<ReferenceJoin>));
            this.onDeleteCascade = (bool)Info.GetValue("onDeleteCascade", typeof(bool));
            this.onUpdateCascade = (bool)Info.GetValue("onUpdateCascade", typeof(bool));
            this.parentTable = (TableSchema)Info.GetValue("parentTable", typeof(TableSchema));
            this.childTable = (TableSchema)Info.GetValue("childTable", typeof(TableSchema));
            this.alignment = (AlignmentType)Info.GetValue("alignment", typeof(AlignmentType));
        }
     
        #endregion

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("joins", this.joins);
            Info.AddValue("onDeleteCascade", this.onDeleteCascade);
            Info.AddValue("onUpdateCascade", this.onUpdateCascade);

            Info.AddValue("parentTable", this.parentTable);
            Info.AddValue("childTable", this.childTable);
            Info.AddValue("alignment", this.alignment);
        }

        #endregion

        #region Properties

        /// <summary>
        /// If foreign key is cascade on delete;
        /// </summary>
        public bool OnDeleteCascade
        {
            get { return onDeleteCascade; }
            set { onDeleteCascade = value; }
        }

        /// <summary>
        /// if foreign key is cascade on update;
        /// </summary>
        public bool OnUpdateCascade
        {
            get { return onUpdateCascade; }
            set { onUpdateCascade = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public AlignmentType Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }


        public IList<ReferenceJoin> Joins
        {
            get { return joins; }
        }

        public TableSchema ParentTable
        {
            get { return parentTable; }
        }

        public TableSchema ChildTable
        {
            get { return childTable; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add a new Join.
        /// </summary>
        /// <param name="referenceJoin"></param>
        public void AddJoin(ReferenceJoin referenceJoin)
        {
            if (referenceJoin == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "referenceJoin"));
            }
            this.joins.Add(referenceJoin);
        }

        /// <summary>
        /// Adds a reference, ie. a mapping between a local column (in the table that owns this foreign key) and a remote column.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public void AddNewJoin(ColumnSchema parent, ColumnSchema child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "parent"));
            }
            if (child == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "child"));
            }
            this.parentTable = parent.Table;
            this.childTable  = child.Table;
            ReferenceJoin newJoin = new ReferenceJoin(this, parent, child);
            this.joins.Add(newJoin);
            if (this.OnAddObject != null)
            {
                this.OnAddObject(this, newJoin);
            }
        }

        /// <summary>
        /// Removes the given reference.
        /// </summary>
        /// <param name="referenceJoin"></param>
        /// <returns></returns>
        public bool RemoveJoin(ReferenceJoin referenceJoin)
        {
            if (referenceJoin == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "referenceJoin"));
            }
            if (this.OnRemoveObject != null)
            {
                this.OnRemoveObject(this, referenceJoin);
            }
            return this.joins.Remove(referenceJoin);
        }
        #endregion



        #region ICloneable Members

        public object Clone()
        {
            ReferenceSchema reference = new ReferenceSchema(Name, this.parentTable, this.childTable);
            reference.Code = Code;
            reference.Caption = base.Caption;
            reference.OnDeleteCascade = this.onDeleteCascade;
            reference.OnUpdateCascade = this.onUpdateCascade;
            reference.alignment = this.alignment;

            foreach (ReferenceJoin join in this.joins)
            {
                reference.AddJoin(join.Clone() as ReferenceJoin);
            }
            return reference;
        }

        #endregion

    }
}
