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
using System.ComponentModel;

namespace SmartCode.Model
{
    [Serializable]
    public class IdentifiedObject : ISerializable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Guid objectID;
        private VersionInfo version;

        internal IdentifiedObject()
        {
            this.objectID = Guid.NewGuid();

            //The new object model is based in the version 1,1 
            //Please increment the version minor revision for any new property added to the model
            this.version = new VersionInfo(1,1);
        }

        public IdentifiedObject(SerializationInfo Info, StreamingContext ctxt)
        {
            this.objectID = (Guid)Info.GetValue("objectID", typeof(Guid));
            try
            {
                this.version = (VersionInfo)Info.GetValue("version", typeof(VersionInfo));
            }
            catch
            {
                this.version = new VersionInfo();
            }
        }

        public Guid ObjectID
        {
            get { return objectID; }
        }

        public VersionInfo Version
        {
            get { return version; }
        }

        #region ISerializable Members

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("objectID", this.objectID);
            info.AddValue("version", this.version);
        }

        #endregion


        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
