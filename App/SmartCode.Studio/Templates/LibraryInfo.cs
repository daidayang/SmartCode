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

namespace SmartCode.Studio.Templates
{
    [Serializable]
    public class LibraryInfo : ISerializable
    {
        private string assemblyName;
        private string assemblyQualifiedName;
        private IList<TemplateInfo> templates;


        public LibraryInfo(string assemblyName, string assemblyQualifiedName)
        {
            this.assemblyName = assemblyName;
            this.assemblyQualifiedName = assemblyQualifiedName;
            this.templates = new List<TemplateInfo>();
        }

        public LibraryInfo(SerializationInfo Info, StreamingContext ctxt)
        {
            this.assemblyName = (string)Info.GetValue("assemblyName", typeof(string));
            this.assemblyQualifiedName = (string)Info.GetValue("assemblyQualifiedName", typeof(string));
            this.templates = (IList<TemplateInfo>)Info.GetValue("templates", typeof(IList<TemplateInfo>));
        }
        #region Properties

        public string AssemblyName
        {
            get
            {
                return this.assemblyName;
            }
            set
            {
                this.assemblyName = value;
            }
        }

        public string AssemblyQualifiedName
        {
            get
            {
                return this.assemblyQualifiedName;
            }
        }

        public bool IsFromFile
        {
            get
            {
                return assemblyQualifiedName.ToLower().StartsWith("file:");
            }
        }

        public IList<TemplateInfo> Templates
        {
            get { return templates; }
        }
        #endregion

        public void AddTemplate(TemplateInfo template)
        {
            this.templates.Add(template);
        }

        public bool RemoveTemplate(TemplateInfo template)
        {
            return this.templates.Remove(template);
        }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo Info, StreamingContext context)
        {
            Info.AddValue("assemblyName", this.assemblyName);
            Info.AddValue("assemblyQualifiedName", this.assemblyQualifiedName);
            Info.AddValue("templates", this.templates);
        }

        #endregion
    }
}
