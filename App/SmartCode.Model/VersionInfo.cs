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
    [Serializable]
    public class VersionInfo : ISerializable
    {
        private int minorRevision;
        private int majorRevision;

        internal VersionInfo()
        {
            this.majorRevision = 1;
            this.minorRevision = 0;
        }

        public VersionInfo(int minorRevision, int majorRevision)
        {
            this.minorRevision = minorRevision;
            this.majorRevision = majorRevision;
        }

        public VersionInfo(SerializationInfo Info, StreamingContext ctxt)
        {
            this.minorRevision = Info.GetInt32("minorRevision");
            this.majorRevision = Info.GetInt32("majorRevision");
        }

        public int MinorRevision
        {
            get { return minorRevision; }
            set { minorRevision = value; }
        }

        public int MajorRevision
        {
            get { return majorRevision; }
            set { majorRevision = value; }
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}", this.majorRevision, this.minorRevision);
        }

        #region ISerializable Members

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("minorRevision", this.minorRevision);
            info.AddValue("majorRevision", this.majorRevision);
        }

        #endregion

    }
}
