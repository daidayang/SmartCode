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

namespace SmartCode.Model.Mapping.CS
{
    /// <summary>
    /// Map Object Model to SchemaObjectBase
    /// </summary>
    public abstract class SchemaObjectBase
    {
        private DatabaseSchema m_database;
        private string m_description;
        private string m_name;
        private System.Collections.Generic.IDictionary<String,ExtendedProperty> m_extendedProperties;


        public SchemaObjectBase():
            this(string.Empty, string.Empty)
        {
            m_extendedProperties = new Dictionary<String, ExtendedProperty>();

            ExtendedProperty exProperty = new ExtendedProperty("CS_Description", "get", System.Data.DbType.String);
            m_extendedProperties.Add(exProperty.Name, exProperty);
        }

        public SchemaObjectBase(string name, string description)
        {
            this.m_name = name;
            this.m_description = description;
        }

        public SchemaObjectBase(string name, string description, DatabaseSchema database):
            this(name, description)
        {
            this.m_database = database;
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public DatabaseSchema Database
        {
            get { return m_database; }
            set { m_database = value; }
        }


        public System.Collections.Generic.IDictionary<String, ExtendedProperty> ExtendedProperties
        {
            get { 
                return m_extendedProperties; 
            }
            set { 
                m_extendedProperties = value;
            }
        }
    }
}
