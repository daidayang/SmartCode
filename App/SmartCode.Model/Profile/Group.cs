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
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace SmartCode.Model.Profile
{
    [Serializable()]
    public class Group : IdentifiedObject
    {
        string name;
        private IList<ColumnSchema> columns;

        private Group()
            : base()
        {
        }

        public Group(string name)
            : base()
        {
            this.name = name;
            this.columns = new List<ColumnSchema>();
        }

        public Group(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            name = (string)Info.GetValue("name", typeof(string));
            columns = (IList<ColumnSchema>)Info.GetValue("columns", typeof(IList<ColumnSchema>));
        }

        public void AddColumn(ColumnSchema column)
        {
            this.columns.Add(column);
        }


        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);
            Info.AddValue("name", name);
            Info.AddValue("columns", columns);
        }

        #endregion
    }
}
