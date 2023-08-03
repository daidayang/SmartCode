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
using System.Data;

namespace SmartCode.Model.Mapping.CS
{
    /// <summary>
    /// Map Object Model to SchemaObjectBase
    /// </summary>
    public class DataObjectBase : SchemaObjectBase
    {
        private bool m_allowDBNull;
        private DbType m_dataType;
        private string m_nativeType;
        private byte m_precision;
        private int m_scale;
        private int m_size;
        private Type systemType;

        public bool AllowDBNull
        {
            get { return m_allowDBNull; }
            set { m_allowDBNull = value; }
        }

        public DbType DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }

        public string NativeType
        {
            get { return m_nativeType; }
            set { m_nativeType = value; }
        }

        public byte Precision
        {
            get { return m_precision; }
            set { m_precision = value; }
        }

        public int Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        public int Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        public Type SystemType
        {
            get { return systemType; }
            set { systemType = value; }
        }


    }
}
