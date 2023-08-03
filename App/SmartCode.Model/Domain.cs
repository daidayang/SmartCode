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

namespace SmartCode.Model
{
    [Serializable()]
    public class Domain : NamedObject
    {
        public OnRemoveObjectDelegate OnRemoveTable;

        private DatabaseSchema databaseSchema;
        private IDictionary<String,ControlBase> controls;

        private Domain()
            : base("")
        {
        }

        public Domain(DatabaseSchema databaseSchema)
            : base(databaseSchema.ConnectionInfo.Host )
        {
            if (databaseSchema == null)
            {
                throw new ArgumentNullException(String.Format(SmartCode.Model.Properties.Resources.NullArgument, "databaseSchema"));
            }
            this.databaseSchema = databaseSchema;
            this.controls = new Dictionary<String, ControlBase>();
            LoadProfiles();
        }

        public Domain(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.databaseSchema = (DatabaseSchema)Info.GetValue("databaseSchema", typeof(DatabaseSchema));
            this.controls = (IDictionary<String, ControlBase>)Info.GetValue("clientEditors", typeof(IDictionary<String, ControlBase>));
        }

        private void LoadProfiles()
        {
            ControlBase clientProfile = new TextBox();
            this.controls.Add(clientProfile.Name, clientProfile);

            clientProfile = new CheckBox();
            this.controls.Add(clientProfile.Name, clientProfile);

            clientProfile = new ComboBox();
            this.controls.Add(clientProfile.Name, clientProfile);

            clientProfile = new Image();
            this.controls.Add(clientProfile.Name, clientProfile);

            clientProfile = new ListBox();
            this.controls.Add(clientProfile.Name, clientProfile);

            clientProfile = new OptionButton();
            this.controls.Add(clientProfile.Name, clientProfile);

            clientProfile = new Popup();
            this.controls.Add(clientProfile.Name, clientProfile);

            clientProfile = new TextBox("DateTime");
            ((TextBox)clientProfile).IsDateTime = true;
            this.controls.Add(clientProfile.Name, clientProfile);

        }

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("databaseSchema", this.databaseSchema);
            Info.AddValue("clientEditors", this.controls);
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// 
        /// </summary>
        public DatabaseSchema DatabaseSchema
        {
            get { return databaseSchema; }
        }

        public IDictionary<String, ControlBase> Controls
        {
            get { return controls; }
        }

        #endregion

        public bool RemoveTable(TableSchema table)
        {
            if (this.databaseSchema.RemoveTable(table))
            {
                if (this.OnRemoveTable != null)
                {
                    OnRemoveTable(this, table);
                }
                return true;
            }
            return false;
        }

        public void RemoveControl(ControlBase control)
        {
            foreach (TableSchema table in this.databaseSchema.Tables )
            {
                foreach (ColumnSchema column in table.Columns())
                {
                    if (column.Control == control)
                    {
                        column.Control = this.controls["TextBox"];
                    }
                }
            }

            this.controls.Remove(control.Name);
            
        }
    }
}
