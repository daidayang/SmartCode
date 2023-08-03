using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SmartCode.Model.Mapping.CS
{
    public enum PropertyStateEnum
    {
        Unmodified,
        Dirty,
        New,
        Deleted,
        ReadOnly
    }

    public class ExtendedProperty
    {
        private PropertyStateEnum m_PropertyStateEnum;
        private string m_Name;
        private DbType m_dbType;
        private object m_Value;


        public ExtendedProperty()
        {
            this.m_PropertyStateEnum = PropertyStateEnum.New;
        }

        public ExtendedProperty(string name, object value, DbType dataType)
        {
            this.m_Name = name;
            this.m_Value = value;
            this.m_dbType = dataType;
            this.m_PropertyStateEnum = PropertyStateEnum.Unmodified;
        }

        public ExtendedProperty(string name, object value, DbType dataType, PropertyStateEnum state)
        {
            this.m_Name = name;
            this.m_Value = value;
            this.m_dbType = dataType;
            this.m_PropertyStateEnum = state;
        }

        public PropertyStateEnum PropertyStateEnum
        {
            get { return m_PropertyStateEnum; }
            set { m_PropertyStateEnum = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public DbType DbType
        {
            get { return m_dbType; }
            set { m_dbType = value; }
        }

        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }



    }
}
