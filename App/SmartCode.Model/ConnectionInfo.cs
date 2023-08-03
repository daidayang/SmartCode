using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace SmartCode.Model
{
    [Serializable()]
    public class ConnectionInfo : IdentifiedObject
    {

        private string location;
        private string provider;
        private string host;
        private int port = 0;
        private string database = "";
        private string user = "";
        private string password = "";

        private  ConnectionInfo()
        {

        }

        public ConnectionInfo(string location)
        {
            this.location = location;
            this.ParseLocation(this.location);
        }

        public ConnectionInfo(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            this.location = (string)Info.GetValue("location", typeof(string));
            this.provider = (string)Info.GetValue("provider", typeof(string));
            this.host = (string)Info.GetValue("host", typeof(string));
            this.port = (int)Info.GetValue("port", typeof(int));
            this.database = (string)Info.GetValue("database", typeof(string));
            this.user = (string)Info.GetValue("user", typeof(string));
            this.password = (string)Info.GetValue("password", typeof(string));
        }

        #region ISerializable Members

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

            Info.AddValue("location", this.location);
            Info.AddValue("provider", this.provider);
            Info.AddValue("host", this.host);
            Info.AddValue("port", this.port);
            Info.AddValue("database", this.database);
            Info.AddValue("user", this.user);
            Info.AddValue("password", this.password);
        }

        #endregion

        #region Properties

        /// <summary>
        /// get/set the location of connection
        /// Expected form provider/[[user[:password]@]host[:port]]/database
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Host
        {
            get { return this.host; }
            set { this.host = value; }
        }

        public string Provider
        {
            get { return this.provider; }
            set { this.provider = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Get/Set Database name
        /// </summary>
        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion

        #region Utils

        private void ParseLocation(string location)
        {
            try
            {
                Regex URLextractor = new Regex(
                  @"^(?'provider'[^:]*)/" +
                  @"((?'username'[^:@]*)" +
                  @"(:(?'password'[^@]*))?@)?" +
                  @"(?'host'[^:/]*)" +
                  @"(:(?'port'\d+))?" +
                  @"/(?'database'[^?]*)?");
                Match match = URLextractor.Match(location);
                if (!match.Success)
                {
                    throw new Exception("This Location cannot be parsed.");
                }
                this.provider = match.Result("${provider}");
                this.user = match.Result("${username}");
                this.password = match.Result("${password}");
                this.database = match.Result("${database}");
                this.host = match.Result("${host}");
                this.port = 0;

                if (match.Result("${port}").Length != 0)
                {
                    this.port = Int32.Parse(match.Result("${port}"));
                }
            }
            catch (Exception e)
            {
                throw new Exception("This URL cannot be parsed.", e);
            }
        }
        #endregion
    }
}
