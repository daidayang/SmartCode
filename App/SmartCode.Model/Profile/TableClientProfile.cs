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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace SmartCode.Model.Profile
{
	[Serializable()]
    public class TableClientProfile : IdentifiedObject
	{
		private string collectionName;
		private bool isPersistent;

		private bool allowUpdate;
		private bool allowDelete;
		private bool allowCopy;
		private bool allowInsert;

        private List<string> orderBy;

        //Used in tabs.?
		private IList<Group> m_Groups;
		private TableSchema m_oEntity;

        internal TableClientProfile()
            : base()
        {
            orderBy = new List<string>();
        }

        public TableClientProfile(TableSchema _Table)
            : base()
		{
            m_Groups = new List<Group>();
			m_oEntity = _Table;
            orderBy = new List<string>();
		}


        public TableClientProfile(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {

            collectionName = (string)Info.GetValue("collectionName", typeof(string));
            isPersistent = (bool)Info.GetValue("isPersistent", typeof(bool));
            allowUpdate = (bool)Info.GetValue("allowUpdate", typeof(bool));
            allowDelete = (bool)Info.GetValue("allowDelete", typeof(bool));
            allowCopy = (bool)Info.GetValue("allowCopy", typeof(bool));
            allowInsert = (bool)Info.GetValue("allowInsert", typeof(bool));

            m_Groups = (List<Group>)Info.GetValue("m_Groups", typeof(List<Group>));
            m_oEntity = (TableSchema)Info.GetValue("m_oEntity", typeof(TableSchema));

            if (Version.MinorRevision >= 1)
            {
                orderBy = (List<string>)Info.GetValue("orderBy", typeof(List<string>));
            }
        }

		public  string CollectionName
		{
			get 
			{
				return this.collectionName;
			}
			set 
			{
				this.collectionName = value;
			}
		}

		public  bool IsPersistent
		{
			get 
			{
				return this.isPersistent;
			}
			set 
			{
				this.isPersistent = value;
			}
		}
		public  bool AllowUpdate
		{
			get 
			{
				return this.allowUpdate;
			}
			set 
			{
				this.allowUpdate = value;
			}
		}
		public  bool AllowDelete
		{
			get 
			{
				return this.allowDelete;
			}
			set 
			{
				this.allowDelete = value;
			}
		}
		public  bool AllowCopy
		{
			get 
			{
				return this.allowCopy;
			}
			set 
			{
				this.allowCopy = value;
			}
		}
		public  bool AllowInsert
		{
			get 
			{
				return this.allowInsert;
			}
			set 
			{
				this.allowInsert = value;
			}
		}
        public IList<Group> Groups
		{
			get 
			{
				return this.m_Groups;
			}
			set 
			{
				this.m_Groups = value;
			}
		}
        public TableSchema Entity
		{
			get 
			{
				return this.m_oEntity;
			}
			set 
			{
				this.m_oEntity = value;
			}
		}

        public List<string> OrderBy
        {
            get
            {
                return orderBy;
            }
            set
            {
                orderBy = value;
            }
        }

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("collectionName", collectionName);
            info.AddValue("isPersistent", isPersistent);
            info.AddValue("allowUpdate", allowUpdate);
            info.AddValue("allowDelete", allowDelete);
            info.AddValue("allowCopy", allowCopy);
            info.AddValue("allowInsert", allowInsert);
            info.AddValue("m_Groups", m_Groups);
            info.AddValue("m_oEntity", m_oEntity);

            info.AddValue("orderBy", orderBy);
        }

        #endregion
    }
}
