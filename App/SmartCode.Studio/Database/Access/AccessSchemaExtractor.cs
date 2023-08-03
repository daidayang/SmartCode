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
using System.Data.OleDb;
using SmartCode.Studio.Database.Info;
using SmartCode.Model;
using System.Collections.Specialized;

namespace SmartCode.Studio.Database.Access
{
    internal class AccessSchemaExtractor : SchemaExtractor
    {
        IDbConnection connection ;
        public AccessSchemaExtractor(Driver driver)
            :base(driver)
        {
            connection = driver.CreateConnection();           
        }

        ~AccessSchemaExtractor()
        {
            if (connection != null && connection.State != ConnectionState.Closed )
            {
                try
                {
                    //connection.Close();
                }
                finally
                {
                    ;
                }
            }
        }

        public override string[] GetAllTables()
        {
            ArrayList allTables = new ArrayList();
            try
            {
                if (connection.State == ConnectionState.Closed )
                {
                    connection.Open();
                }

                DataTable schema = ((OleDbConnection)connection).GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[0]);

                DataColumn tableTypeColumn = schema.Columns["TABLE_TYPE"];
                DataColumn tableNameColumn = schema.Columns["TABLE_NAME"];

                foreach (DataRow schemaRow in schema.Rows)
                {
                    if (string.Compare(schemaRow[tableTypeColumn].ToString(), "TABLE") == 0)
                    {
                        string tbname = schemaRow[tableNameColumn].ToString();
                        allTables.Add(tbname);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't get the tables to extract model.", e);
            }
            return (string[])allTables.ToArray(typeof(string));
        }

        public override string[] GetAllViews()
        {
            ArrayList allViews = new ArrayList();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DataTable schema = ((OleDbConnection)connection).GetOleDbSchemaTable(OleDbSchemaGuid.Views, new object[0]);
                DataColumn tableTypeColumn = schema.Columns["TABLE_TYPE"];
                DataColumn tableNameColumn = schema.Columns["TABLE_NAME"];

                foreach (DataRow schemaRow in schema.Rows)
                {
                    string viewName = schemaRow[tableNameColumn].ToString();
                    allViews.Add(viewName);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't get the views to extract model.", e);
            }
            return (string[])allViews.ToArray(typeof(string));
        }

        public override SmartCode.Studio.Database.Info.ColumnInfo[] GetColumns(string tableName)
        {
            ArrayList allColumns = new ArrayList();
            Hashtable columnHashtable = new Hashtable();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DataTable schema = ((OleDbConnection)connection).GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName });
                DataColumn ordinalPosition = schema.Columns["ORDINAL_POSITION"];
                DataColumn dataType = schema.Columns["DATA_TYPE"];
                DataColumn column = schema.Columns["COLUMN_NAME"];
                DataColumn numericPrecisionColumn = schema.Columns["NUMERIC_PRECISION"];
                DataColumn allowDBNull = schema.Columns["IS_NULLABLE"];
                DataColumn columnSize = schema.Columns["CHARACTER_MAXIMUM_LENGTH"];
                DataColumn numericScale = schema.Columns["NUMERIC_SCALE"];

                schema.DefaultView.Sort = ordinalPosition.ColumnName;

                foreach (DataRowView schemaRow in schema.DefaultView)
                {
                    ColumnInfo info = new ColumnInfo();

                    info.Name = schemaRow[column.Ordinal].ToString();
                    info.AllowNull = (bool)schemaRow[allowDBNull.Ordinal];
                    //OleDbType colType = (OleDbType)schemaRow[dataType.Ordinal];

                    //info.OriginalSQLType = colType.ToString();

                    //if (sqlTypes[colType] != null)
                    //    info.SqlType = (SqlType)sqlTypes[colType];
                    //else
                        info.SqlType = SqlType.Unknown;

                    if (NetDataTypes.ContainsKey(info.SqlType))
                    {
                        info.NetDataType = (String)NetDataTypes[info.SqlType];
                    }
                    else
                    {
                        info.NetDataType = "unknown";
                    }

                    if ((info.SqlType == SqlType.VarChar)
                       || (info.SqlType == SqlType.VarBinary)
                       || (info.SqlType == SqlType.Binary)
                      )
                    {
                        info.Size = Convert.ToInt32(schemaRow[columnSize.Ordinal]);
                        if (info.SqlType == SqlType.VarChar)
                        {
                            info.SqlType = SqlType.Text;
                            info.Size = 0;
                        }
                    }
                    else if (info.SqlType == SqlType.Decimal)
                    {
                        //info.Size = Convert.ToInt32(schemaRow[numericPrecisionColumn.Ordinal]);    //[Changed by Fredy Muñoz] The Size field was set with the Precision value because there wasn't a Precision field to use.
                        info.Precision = Convert.ToInt32(schemaRow[numericPrecisionColumn.Ordinal]);    //[Added by Fredy Muñoz] 
                        info.Scale = Convert.ToInt32(schemaRow[numericScale.Ordinal]);
                    }
                    int index = allColumns.Add(info);
                    columnHashtable.Add(info.Name, index);
                }

                return (ColumnInfo[])allColumns.ToArray(typeof(ColumnInfo));
            }
            catch (Exception e)
            {
                throw new Exception("Could not extract columns (" + e.Message + ").", e);
            }
        }

        public override SmartCode.Studio.Database.Info.ConstraintInfo[] GetConstraints(string tableName)
        {
            try
            {
                ArrayList allConstraints = new ArrayList();
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                DataTable schema = ((OleDbConnection)connection).GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[0]);

                DataColumn constraintName = schema.Columns["FK_NAME"];
                DataColumn columnOrdinal = schema.Columns["ORDINAL"];
                DataColumn childTableName = schema.Columns["FK_TABLE_NAME"];
                DataColumn parentColumnName = schema.Columns["FK_COLUMN_NAME"];
                DataColumn updateRule = schema.Columns["UPDATE_RULE"];
                DataColumn deleteRule = schema.Columns["DELETE_RULE"];
                DataColumn parentTableName = schema.Columns["PK_TABLE_NAME"];
                DataColumn childColumnName = schema.Columns["PK_COLUMN_NAME"];

                schema.DefaultView.Sort = constraintName + "," + columnOrdinal.ColumnName;
                schema.DefaultView.RowFilter = childTableName.ColumnName + " = '" + tableName + "'";

                int allConstraintsIndex;
                HybridDictionary foreignKeyDictionary = new HybridDictionary();
                foreach (DataRowView schemaRow in schema.DefaultView)
                {
                    ConstraintInfo constraintInfo;

                    string foreignKeyName = schemaRow[constraintName.Ordinal].ToString();
                    if (foreignKeyDictionary[foreignKeyName] == null)
                    {
                        constraintInfo = new ConstraintInfo();
                        constraintInfo.Name = foreignKeyName;
                        constraintInfo.PrimaryKeyTable = schemaRow[parentTableName.Ordinal].ToString();
                        if (string.Compare(schemaRow[updateRule.Ordinal].ToString(), "cascade", true) == 0)
                            constraintInfo.OnUpdateCascade = true;
                        if (string.Compare(schemaRow[deleteRule.Ordinal].ToString(), "cascade", true) == 0)
                            constraintInfo.OnDeleteCascade = true;
                        constraintInfo.Columns = new string[0];
                        constraintInfo.PrimaryKeyTableColumns = new string[0];

                        allConstraintsIndex = allConstraints.Add(constraintInfo);
                        foreignKeyDictionary[foreignKeyName] = constraintInfo;
                    }
                    else
                    {
                        constraintInfo = (ConstraintInfo)foreignKeyDictionary[foreignKeyName];
                        allConstraintsIndex = -1;
                        for (int constraintIndex = 0; constraintIndex < allConstraints.Count; constraintIndex++)
                        {
                            if (((ConstraintInfo)allConstraints[constraintIndex]).Name == foreignKeyName)
                            {
                                allConstraintsIndex = constraintIndex;
                                break;
                            }
                        }
                    }

                    ArrayList allColInIdx = new ArrayList();
                    allColInIdx.AddRange(constraintInfo.Columns);

                    string foreignKeyColumnName = schemaRow[parentColumnName.Ordinal].ToString();
                    allColInIdx.Add(foreignKeyColumnName);

                    constraintInfo.Columns = (string[])allColInIdx.ToArray(typeof(string));

                    allColInIdx = new ArrayList();
                    allColInIdx.AddRange(constraintInfo.PrimaryKeyTableColumns);

                    string primaryKeyColumnName = schemaRow[childColumnName.Ordinal].ToString();
                    allColInIdx.Add(primaryKeyColumnName);

                    constraintInfo.PrimaryKeyTableColumns = (string[])allColInIdx.ToArray(typeof(string));


                    allConstraints.RemoveAt(allConstraintsIndex);
                    allConstraints.Add(constraintInfo);
                }

                return (ConstraintInfo[])allConstraints.ToArray(typeof(ConstraintInfo));
            }
            catch (Exception e)
            {
                throw new Exception("Error get constraint (" + e.Message + ").", e);
            }
        }

        public override SmartCode.Studio.Database.Info.KeyInfo[] GetKeys(string tableName)
        {
            return new SmartCode.Studio.Database.Info.KeyInfo[0];
        }
    }
}
