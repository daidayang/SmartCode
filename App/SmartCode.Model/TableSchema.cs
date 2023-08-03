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
using SmartCode.Model.Profile;
using System.Collections;

namespace SmartCode.Model
{
    [Serializable()]
    public class TableSchema : NamedObject, ICloneable
    {
        public OnRemoveObjectDelegate OnRemoveColum;
        public OnRemoveObjectDelegate OnRemoveInReferences;
        public OnRemoveObjectDelegate OnRemoveOutReferences;

        #region Fields
        private string catalog = null;
        private string schema = null;
        private bool isTable;

        private Domain domain;
        private IDictionary<string, ColumnSchema> columns;

        private IList<ReferenceSchema> inReferences;
        private IList<ReferenceSchema> outReferences;
        private TableClientProfile clientProfile;

        #endregion

        #region Constructor
        private TableSchema()
            : base("")
        {

        }

        private TableSchema(string name)
            : base(name)
        {
            this.schema = name;
            this.columns = new Dictionary<String, ColumnSchema>();
            this.inReferences = new List<ReferenceSchema>();
            this.outReferences = new List<ReferenceSchema>();
            this.clientProfile = new TableClientProfile();
        }

        public TableSchema(Domain domain, string name)
            : base(name)
        {
            this.domain = domain;
            this.isTable = true;
            this.schema = name;
            this.columns = new Dictionary<String, ColumnSchema>();
            this.inReferences = new List<ReferenceSchema>();
            this.outReferences = new List<ReferenceSchema>();
            this.clientProfile = new TableClientProfile();
        }

        public TableSchema(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.domain = (Domain)Info.GetValue("domain", typeof(Domain));

            this.isTable = (bool)Info.GetValue("isTable", typeof(bool));
            this.catalog = (string)Info.GetValue("catalog", typeof(string));
            this.schema = (string)Info.GetValue("schema", typeof(string));


            this.columns = (IDictionary<string, ColumnSchema>)Info.GetValue("columns", typeof(IDictionary<string, ColumnSchema>));
            this.inReferences = (IList<ReferenceSchema>)Info.GetValue("inReferences", typeof(IList<ReferenceSchema>));
            this.outReferences = (IList<ReferenceSchema>)Info.GetValue("outReferences", typeof(IList<ReferenceSchema>));
            this.clientProfile = (TableClientProfile)Info.GetValue("clientProfile", typeof(TableClientProfile));

        }
        #endregion

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("domain", this.domain);

            Info.AddValue("isTable", this.isTable);
            Info.AddValue("catalog", this.catalog);
            Info.AddValue("schema", this.schema);
            Info.AddValue("columns", this.columns);
            Info.AddValue("inReferences", this.inReferences);
            Info.AddValue("outReferences", this.outReferences);
            Info.AddValue("clientProfile", this.clientProfile);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set the catalog of this table as read from the database.
        /// </summary>
        public String Catalog
        {
            get { return catalog; }
            set { catalog = value; }
        }

        /// <summary>
        /// Get/Set the Schema of this table as read from the database.
        /// </summary>
        public String Schema
        {
            get { return schema; }
            set { schema = value; }
        }

        /// <summary>
        /// Is view or is table type
        /// </summary>
        public bool IsTable
        {
            get { return isTable; }
            set { isTable = value; }
        }

        public IDictionary<string, ColumnSchema> ColumnSchemaCollection
        {
            get { return columns; }
        }

        public IList<ReferenceSchema> InReferences
        {
            get { return inReferences; }
        }

        public IList<ReferenceSchema> OutReferences
        {
            get { return outReferences; }
        }

        public TableClientProfile ClientProfile
        {
            get { return clientProfile; }
        }

        /// <summary>
        /// Returns the number of columns in this table.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columns.Count;
            }
        }

        /// <summary>
        /// Get the parent domain.
        /// </summary>
        public Domain Domain
        {
            get { return domain; }
            set { this.domain = value; }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Add the given column.
        /// </summary>
        /// <param name="column"></param>
        public void AddColumn(ColumnSchema column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "column"));
            }
            this.columns.Add(column.Name, column);
        }

        /// <summary>
        /// Removes the given column.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool RemoveColumn(ColumnSchema column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "column"));
            }
            //Look for references
            foreach (TableSchema table in this.Domain.DatabaseSchema.Tables)
            {
                foreach (ReferenceSchema reference in table.OutReferences)
                {
                    for (int i = reference.Joins.Count -1; i >=0; i--)
                    {
                        ReferenceJoin join = reference.Joins[i];
                        if (join.ChildColumn == column || join.ParentColumn == column)
                        {
                            reference.RemoveJoin(join);
                        }
                    }
                    
                }

                foreach (ReferenceSchema reference in table.InReferences)
                {
                    for (int i = reference.Joins.Count - 1; i >= 0; i--)
                    {
                        ReferenceJoin join = reference.Joins[i];
                        if (join.ChildColumn == column || join.ParentColumn == column)
                        {
                            reference.RemoveJoin(join);
                        }
                    }
                }
            }

            if (this.OnRemoveColum != null)
            {
                this.OnRemoveColum(this, column);
            }
            return this.columns.Remove(column.Name );
        }

        public void AddInReference(ReferenceSchema reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "reference"));
            }
           
            this.inReferences.Add(reference);
        }

        public bool RemoveInReference(ReferenceSchema reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "reference"));
            }
            if (this.OnRemoveInReferences != null)
            {
                this.OnRemoveInReferences(this, reference);
            }
            return this.inReferences.Remove(reference);
        }

        public void AddOutReference(ReferenceSchema reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "reference"));
            }
           
            this.outReferences.Add(reference);
        }


        public bool RemoveOutReference(ReferenceSchema reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "reference"));
            }
            if (this.OnRemoveOutReferences != null)
            {
                this.OnRemoveOutReferences(this, reference);
            }
            return this.outReferences.Remove(reference);
        }

        /// <summary>
        /// Determines whether there is at least one primary key column on this table
        /// </summary>
        /// <returns><code>true</code> if there are one or more primary key columns</returns>
        public bool HasPrimaryKey()
        {
            foreach (KeyValuePair<string, ColumnSchema> pair in this.columns)
            {
                if (pair.Value.IsPrimaryKey)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Finds the column with the specified name, using case insensitive matching.
        /// Note that this method is not called getColumn(String) to avoid introspection problems.
        /// </summary>
        /// <param name="name">The name of the column</param>
        /// <returns>The column or <code>null</code> if there is no such column</returns>
        public ColumnSchema FindColumn(String name)
        {
            return FindColumn(name, false);
        }

        /// <summary>
        /// * Finds the column with the specified name, using case insensitive matching.
        /// Note that this method is not called getColumn(String) to avoid introspection problems.
        /// </summary>
        /// <param name="name">The name of the column</param>
        /// <param name="caseSensitive">Whether case matters for the names</param>
        /// <returns>The column or <code>null</code> if there is no such column</returns>
        public ColumnSchema FindColumn(string name, bool caseSensitive)
        {
            foreach (KeyValuePair<string, ColumnSchema> pair in this.columns)
            {
                ColumnSchema column = pair.Value;

                if (caseSensitive)
                {
                    if (column.Name.Equals(name))
                    {
                        return column;
                    }
                }
                else
                {
                    if (column.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return column;
                    }
                }
            }

            return null;
        }


        public IList<ColumnSchema> Columns()
        {
            IList<ColumnSchema> results = new List<ColumnSchema>(this.columns.Count);
            foreach (KeyValuePair<String, ColumnSchema> keyPair in this.columns)
            {
                results.Add(keyPair.Value);
            }
            return results;
        }

        /// <summary>
        /// The primary key columns of this table.
        /// </summary>
        /// <returns>The primary key columns</returns>
        public IList<ColumnSchema> PrimaryKeyColumns()
        {
            IList<ColumnSchema> values = new List<ColumnSchema>();
            foreach (KeyValuePair<string, ColumnSchema> pair in this.columns)
            {
                if (pair.Value.IsPrimaryKey)
                {
                    values.Add(pair.Value);
                }
            }
            return values;
        }
        /// <summary>
        /// The primary key columns of this table.
        /// </summary>
        /// <returns>The primary key columns</returns>
        public IList<ColumnSchema> NoPrimaryKeyColumns()
        {
            IList<ColumnSchema> values = new List<ColumnSchema>();
            foreach (KeyValuePair<string, ColumnSchema> pair in this.columns)
            {
                if (!pair.Value.IsPrimaryKey)
                {
                    values.Add(pair.Value);
                }
            }
            return values;
        }
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            TableSchema table = new TableSchema(Name);
            table.IsTable = this.isTable;
            foreach (ColumnSchema column in this.Columns())
            {
                ColumnSchema newColumn = column.Clone() as ColumnSchema;
                if (newColumn != null)
                {
                    table.ColumnSchemaCollection.Add(newColumn.Name, newColumn);
                    newColumn.Table = table;
                }
            }
            return table;
        }

        #endregion
    }
}
