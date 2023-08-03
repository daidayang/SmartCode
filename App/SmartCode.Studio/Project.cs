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
using SmartCode.Model;
using SmartCode.Studio.Templates;

namespace SmartCode.Studio
{
    [Serializable]
    public class Project: ISerializable
    {
        private string fileName;
        private Domain domain;
        private IDictionary<string, LibraryInfo> libraries;

        public Project()
        {
        }

        public Project(Domain domain)
        {
            this.domain = domain;
            this.libraries = new Dictionary<string, LibraryInfo>();

        }

        public Project(SerializationInfo Info, StreamingContext ctxt)
        {
            this.fileName = (string)Info.GetValue("fileName", typeof(string));
            this.domain = (Domain)Info.GetValue("domain", typeof(Domain));
            this.libraries = (IDictionary<string, LibraryInfo>)Info.GetValue("libraries", typeof(IDictionary<string, LibraryInfo>));
        }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo Info, StreamingContext context)
        {
            Info.AddValue("fileName", this.fileName);
            Info.AddValue("domain", this.domain);
            Info.AddValue("libraries", this.libraries);
        }

        #endregion

        public void AddLibrary(LibraryInfo library)
        {
            this.libraries.Add(library.AssemblyQualifiedName, library);
        }

        public bool RemoveLibrary(LibraryInfo library)
        {
            return this.libraries.Remove(library.AssemblyQualifiedName);
        }


        #region Properties

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public Domain Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        public IDictionary<string, LibraryInfo> Libraries
        {
            get { return libraries; }
        }


        #endregion
    }
}
