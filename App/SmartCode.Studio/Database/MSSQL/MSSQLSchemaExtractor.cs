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
using System.Data.SqlClient;
using SmartCode.Studio.Database.Info;

namespace SmartCode.Studio.Database.MSSQL
{
    public class MSSQLSchemaExtractor : SchemaExtractor
    {
        Hashtable allColumns;
        Hashtable allLookup;
        ArrayList allKeys;
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="driver">Driver to which this instance will be bound.</param>
        /// <param name="connection">Connection through which this instance should operate.</param>
        public MSSQLSchemaExtractor(Driver driver)
            :
            base(driver)
        {
        }

        public override string[] GetAllTables()
        {
            ArrayList allTables = new ArrayList();

            IDbCommand cmd = CreateCommand();
            cmd.Connection.Open();

            cmd.CommandText = "SELECT TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME,TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_SCHEMA,TABLE_NAME";
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string owner = reader.GetString(1);
                    if (owner == "INFORMATION_SCHEMA" || owner == "sys")
                    {
                        continue;
                    }
                    allTables.Add(reader.GetString(2));
                }
            }

            return (string[])allTables.ToArray(typeof(string));
        }

        public override string[] GetAllViews()
        {
            ArrayList allViews = new ArrayList();
            IDbCommand cmd = CreateCommand();

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }
            cmd.CommandText = "select  u.name,  v.name,   substring(t.text, 1, 1),   t.text  from   sysusers u,   sysobjects v,   syscomments t where   t.id=v.id   and u.uid=v.uid   and v.type='V'";
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allViews.Add(reader.GetString(1));
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

        private void BuildKeysCache()
        {
            this.allKeys = new ArrayList();
            string commandText = "";

            if (Driver.DatabaseSchema.ConnectionInfo.Provider == "mssql")
            {
                commandText = "select kcu.TABLE_SCHEMA, kcu.TABLE_NAME, kcu.CONSTRAINT_NAME, tc.CONSTRAINT_TYPE, kcu.COLUMN_NAME, kcu.ORDINAL_POSITION " +
                                 " from INFORMATION_SCHEMA.TABLE_CONSTRAINTS as tc " +
                                 " join INFORMATION_SCHEMA.KEY_COLUMN_USAGE as kcu " +
                                 "   on kcu.CONSTRAINT_SCHEMA = tc.CONSTRAINT_SCHEMA " +
                                 "  and kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME " +
                                 "  and kcu.TABLE_SCHEMA = tc.TABLE_SCHEMA " +
                                 "  and kcu.TABLE_NAME = tc.TABLE_NAME " +
                                 //" where tc.CONSTRAINT_TYPE in ( 'PRIMARY KEY', 'UNIQUE' ) " + //[Changed by Fredy Muñoz] This line was replaced by the line next to it because it included the Unique Constraints as part of the Primary Key
                                 " where tc.CONSTRAINT_TYPE = 'PRIMARY KEY' " + //[Added by Fredy Muñoz]
                                 " order by kcu.TABLE_SCHEMA, kcu.TABLE_NAME, tc.CONSTRAINT_TYPE, kcu.CONSTRAINT_NAME, kcu.ORDINAL_POSITION";

            }
            else if (Driver.DatabaseSchema.ConnectionInfo.Provider == "mssql2005")
            {
                commandText = "select s.name as TABLE_SCHEMA, t.name as TABLE_NAME, k.name as CONSTRAINT_NAME, k.type_desc as CONSTRAINT_TYPE, c.name as COLUMN_NAME, ic.key_ordinal AS ORDINAL_POSITION " +
                              " from sys.key_constraints as k " +
                              " join sys.tables as t " +
                              "   on t.object_id = k.parent_object_id " +
                              " join sys.schemas as s " +
                              "   on s.schema_id = t.schema_id " +
                              " join sys.index_columns as ic " +
                              "   on ic.object_id = t.object_id " +
                              "  and ic.index_id = k.unique_index_id " +
                              " join sys.columns as c " +
                              "   on c.object_id = t.object_id " +
                              "  and c.column_id = ic.column_id " +
                              "where k.type_desc = 'PRIMARY_KEY_CONSTRAINT' " + //[Added by Fredy Muñoz] This line was added because it included the Unique Constraints as part of the Primary Key
                              "order by TABLE_SCHEMA, TABLE_NAME, CONSTRAINT_TYPE, CONSTRAINT_NAME, ORDINAL_POSITION ";
            }
            SqlCommand cmd = (SqlCommand)CreateCommand();
            cmd.Connection.Open();
            cmd.CommandText = commandText;

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
            SqlCommand cmd = (SqlCommand)CreateCommand();
            cmd.Connection.Open();
            //                            0             1           2               3                         4                5                 6                  7            8             9                                                                              10
            string commandText = "Select COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE, CHARACTER_SET_NAME, COLLATION_NAME, TABLE_NAME, COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') as IS_IDENTITY, COLUMN_DEFAULT\n" +
                 "  from  INFORMATION_SCHEMA.COLUMNS\n" +
                 "  order by ORDINAL_POSITION";

            cmd.CommandText = commandText;

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tableName = (string)reader["TABLE_NAME"];
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
                      (newColumn.SqlType == SqlType.Binary) ||
                      (newColumn.SqlType == SqlType.VarBinary))
                    {
                        newColumn.Size = (int)reader["CHARACTER_MAXIMUM_LENGTH"];
                    }
                    else if (newColumn.SqlType == SqlType.Decimal)
                    {
                        //newColumn.Size = (byte)reader["NUMERIC_PRECISION"];    //[Changed by Fredy Muñoz] The Size field was set with the Precision value because there wasn't a Precision field to use.
                        newColumn.Precision = (byte)reader["NUMERIC_PRECISION"];    //[Added by Fredy Muñoz] 
                        newColumn.Scale = (int)reader["NUMERIC_SCALE"];
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

                    //[Added by Fredy Muñoz] 
                    if (reader["COLUMN_DEFAULT"] != DBNull.Value)
                    {
                        string s = (string)reader["COLUMN_DEFAULT"];
                        newColumn.DefaultValue = s;
                    }

                    //BUG FIX ON July 26
                    if (reader["IS_IDENTITY"] != DBNull.Value)
                    {
                        newColumn.AutoIncrement = (int)reader["IS_IDENTITY"] == 1;
                    }
                    else
                    {
                        newColumn.AutoIncrement = false;
                    }

                    curTableList.Add(newColumn);
                }
            }

        }

        private void BuildLookupCache()
        {
            this.allLookup = new Hashtable();
            SqlCommand cmd = (SqlCommand)CreateCommand();

            cmd.Connection.Open();
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

            cmd.CommandText = query;
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

    }
}
