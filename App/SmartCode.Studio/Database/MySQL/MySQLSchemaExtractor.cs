



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
using System.Collections;
using System.Data;
using SmartCode.Model;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using SmartCode.Studio.Database.Info;

namespace SmartCode.Studio.Database.MySQL
{
    public class MySQLSchemaExtractor : SchemaExtractor
    {
        const string DATABASE = "INFORMATION_SCHEMA";
        Hashtable allColumns;
        Hashtable allLookup;
        ArrayList allKeys;
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="driver">Driver to which this instance will be bound.</param>
        /// <param name="connection">Connection through which this instance should operate.</param>


        public MySQLSchemaExtractor(Driver driver)
            :
            base(driver)
        {

        }

        public override string[] GetAllTables()
        {
            ArrayList allTables = new ArrayList();

            MySqlConnection cnn = new MySqlConnection(MySqlDBConnection());
            cnn.Open();

            string commandText = @"SELECT Table_Name FROM information_schema.TABLES where(Table_Schema=" + '"' + Driver.DatabaseSchema.ConnectionInfo.Database + '"' + " and (Table_Type=" + '"' + "Base Table" + '"' + "))";

            MySqlCommand cmd = new MySqlCommand(commandText, cnn);

            List<string> lstTables = new List<string>();

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lstTables.Add(reader.GetString(0));
                    //allTables.Add(reader.GetString(0));
                }
            }

            lstTables.Sort();
            return lstTables.ToArray();
            //return (string[])allTables.ToArray(typeof(string));
        }

        public override string[] GetAllViews()
        {
            ArrayList allViews = new ArrayList();

            MySqlConnection cnn = new MySqlConnection(MySqlDBConnection());
            cnn.Open();

            string commandText = @"SELECT Table_Name FROM information_schema.TABLES where(Table_Schema=" + '"' + Driver.DatabaseSchema.ConnectionInfo.Database + '"' + " and (Table_Type=" + '"' + "View" + '"' + "))";

            MySqlCommand cmd = new MySqlCommand(commandText, cnn);

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allViews.Add(reader.GetString(0));
                }
            }
            return (string[])allViews.ToArray(typeof(string));
        }

        public override SmartCode.Studio.Database.Info.ColumnInfo[] GetColumns(string tableName)
        {
            if (this.allColumns == null)
            {
                BuildColumnsCache();
            }
            return (ColumnInfo[])((ArrayList)this.allColumns[tableName]).ToArray(typeof(ColumnInfo));
        }

        public override SmartCode.Studio.Database.Info.ConstraintInfo[] GetConstraints(string tableName)
        {
            if (this.allLookup == null)
                BuildLookupCache();

            ArrayList constraints = this.allLookup[tableName] as ArrayList;
            return (constraints == null ? new ConstraintInfo[0] : (ConstraintInfo[])constraints.ToArray(typeof(ConstraintInfo)));

        }

        public override KeyInfo[] GetKeys(string tableName)
        {
            if (this.allKeys == null)
                BuildKeysCache();

            return (this.allKeys == null ? new KeyInfo[0] : (KeyInfo[])this.allKeys.ToArray(typeof(KeyInfo)));

        }


        private string MySqlDBConnection()
        {
            StringBuilder connString = new StringBuilder();
            connString.AppendFormat("server={0};user id={1};database={2};password={3}", Driver.DatabaseSchema.ConnectionInfo.Host, Driver.DatabaseSchema.ConnectionInfo.User, DATABASE, Driver.DatabaseSchema.ConnectionInfo.Password);
            return connString.ToString();
        }

        private void BuildKeysCache()
        {
            this.allKeys = new ArrayList();
            string commandText = "";

            if (Driver.DatabaseSchema.ConnectionInfo.Provider == "mysql")
            {
                commandText = "select kcu.TABLE_SCHEMA, kcu.TABLE_NAME, kcu.CONSTRAINT_NAME, tc.CONSTRAINT_TYPE, kcu.COLUMN_NAME, kcu.ORDINAL_POSITION " +
                                 " from INFORMATION_SCHEMA.TABLE_CONSTRAINTS as tc " +
                                 " join INFORMATION_SCHEMA.KEY_COLUMN_USAGE as kcu " +
                                 "   on kcu.CONSTRAINT_SCHEMA = tc.CONSTRAINT_SCHEMA " +
                                 "  and kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME " +
                                 "  and kcu.TABLE_SCHEMA = tc.TABLE_SCHEMA " +
                                 "  and kcu.TABLE_NAME = tc.TABLE_NAME " +
                                 " where ((tc.CONSTRAINT_TYPE = 'PRIMARY KEY') and (kcu.TABLE_SCHEMA =" + '"' + Driver.DatabaseSchema.ConnectionInfo.Database + '"' +       //[Added by Julio C. Aragon]
                                 " )) order by kcu.TABLE_SCHEMA, kcu.TABLE_NAME, tc.CONSTRAINT_TYPE, kcu.CONSTRAINT_NAME, kcu.ORDINAL_POSITION";

            }



            MySqlConnection cnn = new MySqlConnection(MySqlDBConnection());
            cnn.Open();

            MySqlCommand cmd = new MySqlCommand(commandText, cnn);


            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    KeyInfo key = new KeyInfo();
                    key.ColumnName = (string)reader["COLUMN_NAME"];
                    key.TableName = (string)reader["TABLE_NAME"];
                    this.allKeys.Add(key);
                }
            }

        }

        private void BuildColumnsCache()
        {
            this.allColumns = new Hashtable();


            string commandText = @"SELECT table_catalog,table_schema,table_name,column_name
                    ,is_nullable,data_type,extra,column_type,column_key,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,NUMERIC_SCALE FROM information_schema.`COLUMNS`
                     WHERE table_schema=" + '"' + Driver.DatabaseSchema.ConnectionInfo.Database + '"';


            MySqlConnection cnn = new MySqlConnection(MySqlDBConnection());
            cnn.Open();

            MySqlCommand cmd = new MySqlCommand(commandText, cnn);

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tableName = (string)reader["TABLE_NAME"];
                    if ( tableName == "rtgBillingInfo")
                    {
                        int a = 1;
                        a = a + 2;
                    }

                    IList curTableList = allColumns[tableName] as IList;
                    if (curTableList == null)
                    {
                        curTableList = new ArrayList();
                        this.allColumns[tableName] = curTableList;
                    }

                    ColumnInfo newColumn = new ColumnInfo();
                    newColumn.Name = (string)reader["COLUMN_NAME"];

                    // SqlType detection
                    string dataType = (string)reader["DATA_TYPE"];
                    dataType = dataType.ToUpper();

                    newColumn.OriginalSQLType = dataType;

                    if (sqlTypes.ContainsKey(dataType))
                    {
                        newColumn.SqlType = (SqlType)sqlTypes[dataType];
                    }
                    else
                    {
                        newColumn.SqlType = (SqlType)sqlTypes["unknown"];
                    }

                    if (NetDataTypes.ContainsKey(newColumn.SqlType))
                    {
                        newColumn.NetDataType = (String)NetDataTypes[newColumn.SqlType];
                    }
                    else
                    {
                        newColumn.NetDataType = "unknown";
                    }
                    // Parameters
                    if ((newColumn.SqlType == SqlType.Char) ||
                      (newColumn.SqlType == SqlType.AnsiChar) ||
                      (newColumn.SqlType == SqlType.VarChar) ||
                      (newColumn.SqlType == SqlType.AnsiVarChar) ||
                      (newColumn.SqlType == SqlType.Text) ||
                      (newColumn.SqlType == SqlType.Binary) ||
                      (newColumn.SqlType == SqlType.VarBinary))
                    {
                        newColumn.Size = Convert.ToInt32((UInt64)reader["CHARACTER_MAXIMUM_LENGTH"]);
                    }
                    else if (newColumn.SqlType == SqlType.Decimal)
                    {
                        //newColumn.Size = (byte)reader["NUMERIC_PRECISION"];    //[Changed by Fredy Muñoz] The Size field was set with the Precision value because there wasn't a Precision field to use.
                        newColumn.Precision = Convert.ToInt32((UInt64)reader["NUMERIC_PRECISION"]);    //[Added by Fredy Muñoz] 
                        newColumn.Scale = Convert.ToInt32((UInt64)reader["NUMERIC_SCALE"]);
                    }

                    if (newColumn.Size == -1)
                    {
                        switch (newColumn.SqlType)
                        {
                            case SqlType.VarChar:
                                newColumn.SqlType = SqlType.VarCharMax;
                                newColumn.Size = 0;
                                break;
                            case SqlType.AnsiVarChar:
                                newColumn.SqlType = SqlType.AnsiVarCharMax;
                                newColumn.Size = 0;
                                break;
                            case SqlType.VarBinary:
                                newColumn.SqlType = SqlType.VarBinaryMax;
                                newColumn.Size = 0;
                                break;
                            default:
                                break;
                        }
                    }

                    if (reader["IS_NULLABLE"] != DBNull.Value)
                    {
                        string s = (string)reader["IS_NULLABLE"];
                        newColumn.AllowNull = ("yes" == s.ToLower());
                    }

                    //[Changed by Julio C. Aragon] 
                    /* if (reader["extra"] != DBNull.Value)
                      {
                          string s = (string)reader["extra"];
                          newColumn.DefaultValue = s;
                      } 
                      newColumn.AutoIncrement = (int)reader["IS_IDENTITY"] == 1; */

                    curTableList.Add(newColumn);
                }
            }

        }

        private void BuildLookupCache()
        {
            this.allLookup = new Hashtable();

            string query = "select " +
              "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME, " + /*0*/
              "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME, " + /*1*/
              "INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.DELETE_RULE, " + /*2*/
              "INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.UPDATE_RULE, " + /*3*/
              "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME, " + /*4*/
              "INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME, " + /*5*/
              "unique_usage.COLUMN_NAME " + /*6*/
              "from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE " +
              "inner join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS " +
              "on  " +
              "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME =  " +
              "INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.CONSTRAINT_NAME " +
              "inner join INFORMATION_SCHEMA.TABLE_CONSTRAINTS " +
              "on " +
              "INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME =  " +
              "INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.UNIQUE_CONSTRAINT_NAME " +
              "inner join INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE unique_usage " +
              "on " +
              "INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME = " +
              "unique_usage.CONSTRAINT_NAME";

            MySqlConnection cnn = new MySqlConnection(MySqlDBConnection());
            cnn.Open();

            try
            {

                MySqlCommand cmd = new MySqlCommand(query, cnn);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = (string)reader[0];

                        IList allConstraints = this.allLookup[tableName] as IList;
                        if (allConstraints == null)
                        {
                            this.allLookup[tableName] = allConstraints = new ArrayList();
                        }

                        ConstraintInfo constraintInfo = new ConstraintInfo();
                        constraintInfo.Name = (string)reader[1];

                        constraintInfo.OnDeleteCascade = reader[2].ToString().StartsWith("CASCADE");
                        constraintInfo.OnUpdateCascade = reader[3].ToString().StartsWith("CASCADE");

                        String constraintKeys = (string)reader[4];
                        constraintInfo.Columns = constraintKeys.Split(",".ToCharArray());
                        constraintInfo.PrimaryKeyTableColumns = new string[] { (string)reader[6] };
                        constraintInfo.PrimaryKeyTable = (string)reader[5];

                        allConstraints.Add(constraintInfo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



    }
}
