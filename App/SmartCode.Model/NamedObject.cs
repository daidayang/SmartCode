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
using SmartCode.Model.Utils;
using System.ComponentModel;

namespace SmartCode.Model
{
    public delegate void OnAddObjectDelegate(NamedObject container, NamedObject arg);
    public delegate void OnRemoveObjectDelegate(NamedObject container, NamedObject arg);

    [Serializable]
    public class NamedObject : IdentifiedObject
    {
        public OnAddObjectDelegate OnAddObject;
        public OnRemoveObjectDelegate OnRemoveObject;

        #region Fields

        private RestrictedLengthString name;
        private string caption;
        private string code;
        private string description;
        private string comment;

        #endregion

        #region Constructors
        private NamedObject()
            : base()
        {

        }
        public NamedObject(string name)
            : base()
        {
            this.name = new RestrictedLengthString(name);
            this.code = this.name.Value.Replace("-","").Replace("_","").Trim();
            this.caption = this.name.Value;
        }

        public NamedObject(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.name = (RestrictedLengthString)Info.GetValue("name", typeof(RestrictedLengthString));
            this.caption = Info.GetString("caption");
            this.code = Info.GetString("code");
            this.description = Info.GetString("description");
            this.comment = Info.GetString("comment");
        }

        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return name.Value;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "Name"));
                }
                if (value != this.name.Value)
                {
                    name = new RestrictedLengthString(value);
                    NotifyPropertyChanged("Name");
                }
            }
        }

      
        /// <summary>
        /// Indicates the default user-interface caption for this column.
        /// </summary>
        public string Caption
        {
            get { return caption; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "Caption"));
                }
                if (value != this.caption)
                {
                    caption = value;
                    NotifyPropertyChanged("Caption");
                }
            }
        }

        public string Code
        {
            get { return code; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "Code"));
                }
                if (value != this.code)
                {
                    code = value;
                    NotifyPropertyChanged("Code");
                }
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "Comment"));
                }
                if (value != this.comment)
                {
                    this.comment = value;
                    NotifyPropertyChanged("Comment");
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "Description"));
                }
                if (value != description)
                {
                    description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        #endregion

        #region Override Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Serializable Members


        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("name", this.name);
            Info.AddValue("caption", this.caption);
            Info.AddValue("code", this.code );
            Info.AddValue("description", this.description);
            Info.AddValue("comment", this.comment);
        }

        #endregion


     
    }
}
