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
using SmartCode.Model;

namespace SmartCode.Studio.Controls
{
    internal class DomainPropertyWrapper : NamedPropertyWrapper
    {
        public DomainPropertyWrapper(ExplorerTreeNode treeNode, Domain domain)
            : base(treeNode, domain)
        {
        }

        private Domain CurrentDomain
        {
            get
            {
                return NamedObject as Domain;
            }
        }

        [DisplayText("PropLocation", "PropLocationDesc", "DatabaseSchemaCategory")]
        public string Location
        {
            get { return CurrentDomain.DatabaseSchema.ConnectionInfo.Location; }
            set { CurrentDomain.DatabaseSchema.ConnectionInfo.Location = value; }
        }

        [DisplayText("PropHost", "PropHostDesc", "DatabaseSchemaCategory")]
        public string Host
        {
            get { return CurrentDomain.DatabaseSchema.ConnectionInfo.Host; }
        }

        [DisplayText("PropProvider", "PropProviderDesc", "DatabaseSchemaCategory")]
        public string Provider
        {
            get { return CurrentDomain.DatabaseSchema.ConnectionInfo.Provider; }
        }

        [DisplayText("PropPort", "PropPortDesc", "DatabaseSchemaCategory")]
        public int Port
        {
            get { return CurrentDomain.DatabaseSchema.ConnectionInfo.Port; }
        }

        [DisplayText("PropDatabase", "PropDatabaseDesc", "DatabaseSchemaCategory")]
        public string Database
        {
            get { return CurrentDomain.DatabaseSchema.ConnectionInfo.Database; }
        }

        [DisplayText("PropUser", "PropUserDesc", "DatabaseSchemaCategory")]
        public string User
        {
            get { return CurrentDomain.DatabaseSchema.ConnectionInfo.User; }
        }

        //Ugly no?
        [DisplayText("PropPassword", "PropPasswordDesc", "DatabaseSchemaCategory")]
        public string Password
        {
            get { return CurrentDomain.DatabaseSchema.ConnectionInfo.Password; }
        }

    }
}
