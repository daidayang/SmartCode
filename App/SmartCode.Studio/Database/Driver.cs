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
using SmartCode.Studio.Database.Info;
using System.Data;
using SmartCode.Model;

namespace SmartCode.Studio.Database
{
    public abstract class Driver
    {
        private SmartCode.Model.Domain model;
        private SchemaExtractor extractor;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="databaseSchema">Information about default connection.</param>
        /// <param name="domain">Model, to which this driver belongs.</param>
        public Driver(DatabaseSchema databaseSchema)
            : base()
        {
            if (databaseSchema == null)
                throw new ArgumentNullException("DatabaseSchema is null.");

            this.model = new SmartCode.Model.Domain(databaseSchema);
        }

        /// <summary>
        /// Information about this driver.
        /// </summary>
        public DatabaseSchema DatabaseSchema
        {
            get { return model.DatabaseSchema; }
        }

        /// <summary>
        /// Model for the Driver
        /// </summary>
        public SmartCode.Model.Domain Model
        {
            get { return model; }
        }

        /// <summary>
        /// Creates new IDbConnection instance.
        /// </summary>
        /// <returns>Connection instance.</returns>
        public abstract IDbConnection CreateConnection();

        /// <summary>
        /// return the extractor
        /// </summary>
        public SchemaExtractor Extractor
        {
            get
            {
                if (extractor == null)
                {
                    extractor = CreateExtractor();
                }

                return extractor;
            }
        }

        protected abstract SchemaExtractor CreateExtractor();

        /// <summary>
        /// Configures IDbConnection.
        /// </summary>
        /// <param name="connection">Connection to configure.</param>
        public abstract void ConfigureConnection(IDbConnection connection);

        /// <summary>
        /// Test IDbConnection
        /// </summary>
        /// <param name="connection">Connection to test.</param>
        public abstract void TestConnection();

        /// <summary>
        /// Creates, opens and configures new <see cref="IDbConnection"/> instance.
        /// </summary>
        /// <returns>Connection instance.</returns>
        public IDbConnection OpenConnection()
        {
            IDbConnection connection = CreateConnection();
            connection.Open();
            ConfigureConnection(connection);
            return connection;
        }

    }
}
