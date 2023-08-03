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

namespace SmartCode.Studio.Templates
{
    [Serializable]
    public class TemplateInfo : ISerializable
    {
        private string className;
        private string _namespace;
        private string assemblyName;
        private string outputFolder;
        private bool createFile;
        private bool isProjectTemplate;
        private string name;
        private string description;
        //If is project template can run independiently
        private bool run;


        private IList<NamedObject> namedObjects;



        public TemplateInfo(string assemblyName, string className, string _namespace)
        {
            this.assemblyName = assemblyName;
            this.className = className;
            this._namespace = _namespace;
            this.namedObjects = new List<NamedObject>();
        }

        public TemplateInfo(SerializationInfo Info, StreamingContext ctxt)
        {
            this.className = (string)Info.GetValue("className", typeof(string));
            this._namespace = (string)Info.GetValue("_namespace", typeof(string));
            this.assemblyName = (string)Info.GetValue("assemblyName", typeof(string));
            Name = (string)Info.GetValue("name", typeof(string));
            Description = (string)Info.GetValue("description", typeof(string));

            OutputFolder = (string)Info.GetValue("outputFolder", typeof(string));

            CreateOutputFile = (bool)Info.GetValue("createFile", typeof(bool));
            this.isProjectTemplate = (bool)Info.GetValue("isProjectTemplate", typeof(bool));
            this.run = (bool)Info.GetValue("run", typeof(bool));

            this.namedObjects = (IList<NamedObject>)Info.GetValue("namedObjects", typeof(IList<NamedObject>));
            
        }

        #region Properties
        
        public string ClassName
        {
            get{return this.className;}
        }

        public string NameSpace
        {
            get{return this._namespace;}
        }

        public string OutputFolder
        {
            get { return this.outputFolder; }
            set { this.outputFolder = value; }
        }

        public bool CreateOutputFile
        {
            get { return this.createFile; }
            set { this.createFile = value; }
        }

        public bool IsProjectTemplate
        {
            get { return this.isProjectTemplate; }
            set { this.isProjectTemplate = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public bool Run
        {
            get { return run; }
            set { run = value; }
        }

        public string FullName
        {
            get { return this.NameSpace + "." + this.className; }
        }

        public IList<NamedObject> AssignedObjects
        {
            get { return namedObjects; }
        }

        #endregion

        #region ISerializable Members

        public void GetObjectData(SerializationInfo Info, StreamingContext context)
        {
            Info.AddValue("className", this.className);
            Info.AddValue("_namespace", this._namespace);
            Info.AddValue("outputFolder", OutputFolder);
            Info.AddValue("createFile", CreateOutputFile);
            Info.AddValue("isProjectTemplate", this.isProjectTemplate);
            Info.AddValue("assemblyName", this.assemblyName);
            Info.AddValue("name", Name);
            Info.AddValue("description", Description);
            Info.AddValue("namedObjects", this.namedObjects);
            Info.AddValue("run", this.run);

            
        }

        #endregion

       
    }
}
