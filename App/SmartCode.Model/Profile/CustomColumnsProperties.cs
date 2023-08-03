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
    public class CustomColumnsProperties : IdentifiedObject
    {
        private bool deleteBy;
        private bool searchBy;

        public CustomColumnsProperties():
            base()
        {
            this.deleteBy = false;
            this.searchBy = false;
        }

        public CustomColumnsProperties(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            deleteBy = (bool)Info.GetValue("deleteBy", typeof(bool));
            searchBy = (bool)Info.GetValue("searchBy", typeof(bool));
        }

        public bool DeleteBy
        {
            get { return deleteBy; }
            set { deleteBy = value; }
        }

        public bool SearchBy
        {
            get { return searchBy; }
            set { searchBy = value; }
        }

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);
            Info.AddValue("deleteBy", deleteBy);
            Info.AddValue("searchBy", searchBy);
        }

        #endregion
    }
}
