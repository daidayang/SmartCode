



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
using System.Data.OracleClient;

namespace SmartCode.Studio.Database.Oracle
{
    class OracleSchemaExtractor : SchemaExtractor
    {

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="driver">Driver to which this instance will be bound.</param>
        /// <param name="connection">Connection through which this instance should operate.</param>
        public OracleSchemaExtractor(Driver driver)
            :
            base(driver)
        {
        }

        #region Base Implemetation

        public override string[] GetAllTables()
        {
            ArrayList allTables = new ArrayList();

            IDbCommand cmd = CreateCommand();
            cmd.Connection.Open();

            cmd.CommandText = "Select * from ALL_TABLES" + AddOwner(" WHERE ", "OWNER");

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //string owner = reader.GetString(1); The array is zero based! [Changed by Vladimir Chuprinskiy]
                    string owner = reader.GetString(reader.GetOrdinal("OWNER"));
                    if (owner == "INFORMATION_SCHEMA" || owner == "sys")
                    {
                        continue;
                    }
                    //allTables.Add(reader.GetString(2)); [Changed by Vladimir Chuprinskiy]
                    allTables.Add(reader.GetString(reader.GetOrdinal("TABLE_NAME")));
                }
            }

            return (string[])allTables.ToArray(typeof(string));

        }

        public override string[] GetAllViews()
        {
            ArrayList allViews = new ArrayList();

            IDbCommand cmd = CreateCommand();
            cmd.Connection.Open();

            cmd.CommandText = "Select * from ALL_VIEWS" + AddOwner(" WHERE ", "OWNER");

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //string owner = reader.GetString(1); [Changed by Vladimir Chuprinskiy]
                    string owner = reader.GetString(reader.GetOrdinal("OWNER"));
                    if (owner == "INFORMATION_SCHEMA" || owner == "sys")
                    {
                        continue;
                    }
                    //allTables.Add(reader.GetString(2)); [Changed by Vladimir Chuprinskiy]
                    allViews.Add(reader.GetString(reader.GetOrdinal("VIEW_NAME")));
                }
            }
            return (string[])allViews.ToArray(typeof(string));
        }

        public override SmartCode.Studio.Database.Info.ColumnInfo[] GetColumns(string tableName)
        {
            ArrayList columns = new ArrayList();
            OracleCommand cmd = (OracleCommand)this.CreateCommand();

            cmd.Connection.Open(); //[Added by Vladimir Chuprinskiy] That was missing!

            cmd.CommandText = "Select COLUMN_NAME, DATA_TYPE, DATA_LENGTH, DATA_PRECISION, DATA_SCALE, NULLABLE, CHARACTER_SET_NAME from  ALL_TAB_COLUMNS where TABLE_NAME = :TableName \n" + this.AddOwner(" AND ", "OWNER") + " order by COLUMN_ID";
            cmd.Parameters.Add("TableName", OracleType.NVarChar, tableName.Length).Value = tableName;

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ColumnInfo columnInfo = new ColumnInfo();
                    columnInfo.Name = Convert.ToString(reader["COLUMN_NAME"]);
                    string dataType = Convert.ToString(reader["DATA_TYPE"]);
                    if (dataType == "NUMBER")
                    {
                        //dataType = string.Concat(new object[] { dataType, "(", Convert.ToInt32(reader["DATA_PRECISION"]), ",", Convert.ToInt32(reader["DATA_SCALE"]), ")" });
                        //[Changed by Vladimir Chuprinskiy] DATA_PRECISION and DATA_SCALE might be NULL for INTEGER
                        if (reader["DATA_PRECISION"] != DBNull.Value && (reader["DATA_SCALE"] != DBNull.Value))
                            dataType = string.Concat(new object[] { dataType, "(", Convert.ToInt32(reader["DATA_PRECISION"]), ",", Convert.ToInt32(reader["DATA_SCALE"]), ")" });
                    }
                    if (dataType == "FLOAT")
                    {
                        dataType = string.Concat(new object[] { dataType, "(", Convert.ToInt32(reader["DATA_PRECISION"]), ")" });
                    }
                    if (this.sqlTypes.ContainsKey(dataType))
                    {
                        columnInfo.SqlType = (SqlType)this.sqlTypes[dataType];
                    }
                    else
                    {
                        columnInfo.SqlType = (SqlType)this.sqlTypes["fallback for a nonexistent type"];
                    }
                    if (columnInfo.SqlType == SqlType.Char || columnInfo.SqlType == SqlType.AnsiChar || columnInfo.SqlType == SqlType.VarChar || columnInfo.SqlType == SqlType.AnsiVarChar)
                    {
                        columnInfo.Size = Convert.ToInt32(reader["DATA_LENGTH"]) / 2;
                    }
                    else if ((columnInfo.SqlType == SqlType.Binary) || (columnInfo.SqlType == SqlType.VarBinary))
                    {
                        columnInfo.Size = Convert.ToInt32(reader["DATA_LENGTH"]);
                    }
                    else if (columnInfo.SqlType == SqlType.Decimal)
                    {
                        //columnInfo.Size = Convert.ToInt32(reader["DATA_PRECISION"]);    //[Changed by Fredy Muñoz] The Size field was set with the Precision value because there wasn't a Precision field to use.
                        //columnInfo.Precision = Convert.ToInt32(reader["DATA_PRECISION"]);    //[Added by Fredy Muñoz] 

                        //[Changed by Vladimir Chuprinskiy] DATA_PRECISION and DATA_SCALE might be NULL for INTEGER
                        if (reader["DATA_PRECISION"] != DBNull.Value)
                            columnInfo.Precision = Convert.ToInt32(reader["DATA_PRECISION"]);

                        if (reader["DATA_SCALE"] != DBNull.Value)
                            columnInfo.Scale = Convert.ToInt32(reader["DATA_SCALE"]);
                    }
                    if (reader["NULLABLE"] != DBNull.Value)
                    {
                        string text2 = Convert.ToString(reader["NULLABLE"]);
                        columnInfo.AllowNull = "N" != text2;
                    }
                    columns.Add(columnInfo);
                }
            }


            bool hastAutoIncrement = HasAutoincrement(tableName);
            for (int i = 0; i < columns.Count; i++)
            {
                ColumnInfo columnInfo = (ColumnInfo)columns[i];
                if ((i == 0) && hastAutoIncrement)
                {
                    columnInfo.AutoIncrement = true;
                    hastAutoIncrement = false;
                }
                else
                {
                    columnInfo.AutoIncrement = false;
                }
                columns[i] = columnInfo;
            }
            return (ColumnInfo[])columns.ToArray(typeof(ColumnInfo));

        }

        public override SmartCode.Studio.Database.Info.ConstraintInfo[] GetConstraints(string tableName)
        {
            ConstraintInfo[] arrayOfConstratint = new ConstraintInfo[0];
            ArrayList listOfConstratin = new ArrayList();

            OracleCommand allConstrsCmd = null;
            IDataReader allConstrsReader = null;

            OracleCommand allConstrsColsCmd = null;
            IDataReader allConstrsColsReader = null;

            try
            {
                try
                {
                    allConstrsColsCmd = (OracleCommand)this.CreateCommand();

                    //[Changed by Vladimir Chuprinskiy] We might have the same constraint names in different schemas
                    //allConstrsColsCmd.CommandText = "  Select COLUMN_NAME ColumnName    From ALL_CONS_COLUMNS   Where CONSTRAINT_NAME = :ConstraintName";
                    allConstrsColsCmd.CommandText = "  Select COLUMN_NAME ColumnName    From USER_CONS_COLUMNS   Where CONSTRAINT_NAME = :ConstraintName" + AddOwner(" AND ", "OWNER");

                    allConstrsCmd = (OracleCommand)this.CreateCommand();

                    //[Changed by Vladimir Chuprinskiy] DISTINCT required, or we get a Cartesian product
                    //allConstrsCmd.CommandText = " Select A.CONSTRAINT_NAME CONSTRAINTNAME, B.TABLE_NAME PRIMARYKEYTABLE, A.R_CONSTRAINT_NAME PrimaryKeyName,A.DELETE_RULE DeleteRule From ALL_CONSTRAINTS A, ALL_CONSTRAINTS B Where A.TABLE_NAME = :TableName and B.CONSTRAINT_NAME = A.R_CONSTRAINT_NAME and A.CONSTRAINT_TYPE='R' and A.STATUS='ENABLED'";
                    allConstrsCmd.CommandText = " Select Distinct(A.CONSTRAINT_NAME) CONSTRAINTNAME, B.TABLE_NAME PRIMARYKEYTABLE, A.R_CONSTRAINT_NAME PrimaryKeyName,A.DELETE_RULE DeleteRule From USER_CONSTRAINTS A, USER_CONSTRAINTS B Where A.TABLE_NAME = :TableName and B.CONSTRAINT_NAME = A.R_CONSTRAINT_NAME and A.CONSTRAINT_TYPE='R' and A.STATUS='ENABLED'";

                    allConstrsCmd.Parameters.Add("TableName", OracleType.NVarChar, tableName.Length).Value = tableName;
                    allConstrsCmd.Connection.Open(); //[Added by Vladimir Chuprinskiy] 
                    allConstrsReader = allConstrsCmd.ExecuteReader();
                    while (allConstrsReader.Read())
                    {
                        ConstraintInfo constraintInfo = new ConstraintInfo();
                        constraintInfo.Name = allConstrsReader.GetString(allConstrsReader.GetOrdinal("ConstraintName"));
                        constraintInfo.OnDeleteCascade = "CASCADE" == allConstrsReader.GetString(allConstrsReader.GetOrdinal("DeleteRule"));
                        constraintInfo.OnUpdateCascade = false;

                        ArrayList constraintNameList = new ArrayList();
                        allConstrsColsCmd.Parameters.Clear();
                        allConstrsColsCmd.Parameters.Add("ConstraintName", OracleType.NVarChar, constraintInfo.Name.Length).Value = constraintInfo.Name;
                        try
                        {
                            allConstrsColsCmd.Connection.Open();//[Added by Vladimir Chuprinskiy] 
                            allConstrsColsReader = allConstrsColsCmd.ExecuteReader();
                            while (allConstrsColsReader.Read())
                            {
                                constraintNameList.Add(allConstrsColsReader.GetString(allConstrsColsReader.GetOrdinal("ColumnName")));
                            }
                        }
                        finally
                        {
                            if (allConstrsColsReader != null)
                            {
                                allConstrsColsReader.Close();
                            }
                            allConstrsColsCmd.Connection.Close();
                            allConstrsColsReader = null;
                        }

                        constraintInfo.Columns = (string[])constraintNameList.ToArray(typeof(string));
                        ArrayList tempList = new ArrayList();
                        constraintInfo.PrimaryKeyTable = allConstrsReader.GetString(allConstrsReader.GetOrdinal("PrimaryKeyTable"));

                        string keyName = allConstrsReader.GetString(allConstrsReader.GetOrdinal("PrimaryKeyName"));
                        allConstrsColsCmd.Parameters.Clear();
                        allConstrsColsCmd.Parameters.Add("ConstraintName", OracleType.NVarChar, keyName.Length).Value = keyName;
                        try
                        {
                            allConstrsColsCmd.Connection.Open();//[Added by Vladimir Chuprinskiy] 
                            allConstrsColsReader = allConstrsColsCmd.ExecuteReader();
                            while (allConstrsColsReader.Read())
                            {
                                tempList.Add(allConstrsColsReader.GetString(allConstrsColsReader.GetOrdinal("ColumnName")));
                            }
                        }
                        finally
                        {
                            if (allConstrsColsReader != null)
                            {
                                allConstrsColsReader.Close();
                            }
                            allConstrsColsCmd.Connection.Close();
                            allConstrsColsReader = null;
                        }
                        constraintInfo.PrimaryKeyTableColumns = (string[])tempList.ToArray(typeof(string));
                        listOfConstratin.Add(constraintInfo);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot get foreign key info: (" + ex.Message + ").");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (allConstrsReader != null)
                {
                    allConstrsReader.Close();
                }
            }
            if (listOfConstratin.Count > 0)
            {
                arrayOfConstratint = (ConstraintInfo[])listOfConstratin.ToArray(typeof(ConstraintInfo));
            }
            return arrayOfConstratint;

        }

        public override SmartCode.Studio.Database.Info.KeyInfo[] GetKeys(string tableName)
        {
            ArrayList arrayOfKeys = new ArrayList();
            OracleCommand cmd = (OracleCommand)this.CreateCommand();

            cmd.Connection.Open(); //[Added by Vladimir Chuprinskiy] That was missing!

            cmd.CommandText = "Select * from ALL_INDEXES where TABLE_NAME = :TableName" + this.AddOwner(" AND ", "OWNER");
            cmd.Parameters.Add("TableName", OracleType.NVarChar, tableName.Length).Value = tableName;

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    KeyInfo keyInfo = new KeyInfo();
                    keyInfo.ColumnName = reader.GetString(1);
                    keyInfo.TableName = tableName;
                    arrayOfKeys.Add(keyInfo);
                }
            }
            return (KeyInfo[])arrayOfKeys.ToArray(typeof(KeyInfo));

        }

        #endregion

        private string AddOwner(string prefix, string ownerField)
        {
            if ((Driver.DatabaseSchema.ConnectionInfo.Database != null) && (Driver.DatabaseSchema.ConnectionInfo.Database != string.Empty))
            {
                return string.Format("{0}{1}='{2}'", prefix, ownerField, Driver.DatabaseSchema.ConnectionInfo.User.ToUpper());
            }
            return null;
        }

        private bool HasAutoincrement(string tableName)
        {
            bool hastAutoIncrement = false;
            OracleCommand cmd = (OracleCommand)this.CreateCommand();

            cmd.Connection.Open(); //[Added by Vladimir Chuprinskiy] That was missing!

            cmd.CommandText = "Select INCREMENT_BY from ALL_SEQUENCES where SEQUENCE_NAME = :SeqName" + AddOwner(" AND ", "SEQUENCE_OWNER");
            cmd.Parameters.Add("SeqName", OracleType.NVarChar, tableName.Length + 4).Value = "SEQ_" + tableName;

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    hastAutoIncrement = reader.GetDecimal(0) == new decimal(1);
                }
            }
            return hastAutoIncrement;
        }





    }
}
