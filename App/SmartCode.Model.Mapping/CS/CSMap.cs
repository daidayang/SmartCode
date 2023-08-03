using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCode.Model.Mapping.CS
{
    public class CSMap
    {
        SmartCode.Model.Domain m_domain;

        DatabaseSchema m_DatabaseSchema;

        public DatabaseSchema DatabaseSchema
        {
            get
            {
                return m_DatabaseSchema;
            }
        }

        public CSMap(SmartCode.Model.Domain domain)
        {
            this.m_domain = domain;
        }

        public void RunMapToCS()
        {
            BuildDatabaseSchema();
        }

        private void BuildDatabaseSchema()
        {
            m_DatabaseSchema = new DatabaseSchema();
            m_DatabaseSchema.Name = this.m_domain.Code;
            m_DatabaseSchema.Description = this.m_domain.Description ;
            m_DatabaseSchema.Database = m_DatabaseSchema;

            loadTablesAndViews();

        }

        private void loadTablesAndViews()
        {
            //First load all tables and views
            foreach (SmartCode.Model.TableSchema  tableSchema in m_domain.DatabaseSchema.Tables)
            {
                if (tableSchema.IsTable)
                {
                    TableSchema table = new TableSchema();
                    table.Name = tableSchema.Name;
                    table.Description = tableSchema.Description;
                    table.Database = m_DatabaseSchema;
                    m_DatabaseSchema.Tables.Add(table);
                }
                else
                {
                    ViewSchema view = new ViewSchema();
                    view.Name = tableSchema.Name;
                    view.Description = tableSchema.Description;
                    view.Database = m_DatabaseSchema;
                    m_DatabaseSchema.Views.Add(view);
                }
            }

            //Load Columns
            foreach (SmartCode.Model.TableSchema tableSchema in m_domain.DatabaseSchema.Tables)
            {
                if (tableSchema.IsTable)
                {
                    TableSchema table = GetTableByName(tableSchema.Name);
                    foreach (SmartCode.Model.ColumnSchema column in tableSchema.Columns())
                    {
                        ColumnSchema newColumn = buildColumnSchema(column, table);
                        table.Columns.Add(newColumn);
                    }
                }
            }

            //Load Columns and Keys
            foreach (SmartCode.Model.TableSchema tableSchema in m_domain.DatabaseSchema.Tables)
            {
                if (tableSchema.IsTable)
                {
                    TableSchema table = GetTableByName(tableSchema.Name);

                    table.FullName = m_domain.DatabaseSchema.ConnectionInfo.Database + "." + tableSchema.Name;
                    table.Owner = m_domain.DatabaseSchema.ConnectionInfo.User;
                    table.HasPrimaryKey = tableSchema.HasPrimaryKey();

                    loadPrimaryKeySchema(tableSchema, table);
                }
            }

            foreach (SmartCode.Model.TableSchema tableSchema in m_domain.DatabaseSchema.Tables)
            {
                if (tableSchema.IsTable)
                {
                    TableSchema table = GetTableByName(tableSchema.Name);

                    loadForeignKeyColumns(tableSchema, table);
                    loadNonKeyColumns(tableSchema, table);
                    loadForeignKeys(tableSchema, table);
                }
            }
        }


        public TableSchema GetTableByName(string name)
        {
            foreach (TableSchema table in m_DatabaseSchema.Tables)
            {
                if (table.Name == name)
                {
                    return table;
                }
            }
            return null;
        }

        private ColumnSchema getColumnFromTable(string tableName, string columnName)
        {
            TableSchema table = GetTableByName(tableName);
            foreach (ColumnSchema column in table.Columns )
            {
                if (column.Name == columnName)
                {
                    return column;
                }
            }
            return null;
        }

        private void loadForeignKeys(SmartCode.Model.TableSchema tableSchema, TableSchema table)
        {
            foreach (SmartCode.Model.ReferenceSchema reference in tableSchema.InReferences)
            {
                TableSchema parentTable = GetTableByName(reference.ParentTable.Name);


                TableKeySchema keySchema = new TableKeySchema();
                keySchema.Name = reference.Name;
                keySchema.Description = reference.Description;
                keySchema.Database = m_DatabaseSchema;
                keySchema.ForeignKeyTable = table;
                keySchema.PrimaryKeyTable = parentTable;

                PrimaryKeySchema pkSchema = new PrimaryKeySchema();
                pkSchema.Name = reference.Name;
                pkSchema.Table = parentTable;

                foreach (SmartCode.Model.ReferenceJoin join in reference.Joins)
                {
                    ColumnSchema pkColumn = getColumnFromTable(parentTable.Name, join.ParentColumn.Name);
                    keySchema.PrimaryKeyMemberColumns.Add(pkColumn);

                    ColumnSchema fkColumn = getColumnFromTable(table.Name, join.ChildColumn.Name);
                    keySchema.ForeignKeyMemberColumns.Add(fkColumn);

                    pkSchema.MemberColumns.Add(pkColumn);

                }

                keySchema.PrimaryKey = pkSchema;

                if (!parentTable.Keys.Contains(keySchema))
                    parentTable.Keys.Add(keySchema);

                if (!parentTable.PrimaryKeys.Contains(keySchema))
                    parentTable.PrimaryKeys.Add(keySchema);

                if (!parentTable.ForeignKeys.Contains(keySchema))
                    table.ForeignKeys.Add(keySchema);

            }
        }

        private void loadPrimaryKeySchema(SmartCode.Model.TableSchema tableSchema, TableSchema table)
        {
            PrimaryKeySchema KeySchema = new PrimaryKeySchema();
            KeySchema.Name = table.Name;
            KeySchema.Description = table.Description;
            KeySchema.Database = m_DatabaseSchema;
            KeySchema.Table = table;

            foreach (SmartCode.Model.ColumnSchema column in tableSchema.PrimaryKeyColumns())
            {
                ColumnSchema newColumn = buildColumnSchema(column, table);
                KeySchema.MemberColumns.Add(newColumn);
            }
            table.PrimaryKey = KeySchema;
        }

        private void loadForeignKeyColumns(SmartCode.Model.TableSchema tableSchema, TableSchema table)
        {
            foreach (SmartCode.Model.ReferenceSchema reference in tableSchema.InReferences)
            {
                foreach (SmartCode.Model.ReferenceJoin join in reference.Joins )
                {
                    //ColumnSchema newColumn = buildColumnSchema(join.ChildColumn, join.ParentColumn);
                }
            }
        }

        private void loadNonKeyColumns(SmartCode.Model.TableSchema tableSchema, TableSchema table)
        {
            foreach (SmartCode.Model.ColumnSchema column in tableSchema.NoPrimaryKeyColumns())
            {
                ColumnSchema newColumn = buildColumnSchema(column, table);
                table.NonPrimaryKeyColumns.Add(newColumn);
                table.NonKeyColumns.Add(newColumn);
            }
        }

        private ColumnSchema buildColumnSchema(SmartCode.Model.ColumnSchema originalColumn, TableSchema table)
        {
            ColumnSchema column = new ColumnSchema();
            column.Name = originalColumn.Name;
            column.Description = originalColumn.Description;
            column.Database = m_DatabaseSchema;

            column.AllowDBNull = !originalColumn.IsRequired;
            column.NativeType = originalColumn.OriginalSQLType;
            column.Precision  = byte.Parse(originalColumn.Precision.ToString());
            column.Scale  = originalColumn.Scale ;
            column.Size  = originalColumn.Length ;
            column.SystemType = GetType(originalColumn.SqlType);
            column.DataType = GetDBType(originalColumn.SqlType);
            column.Table = table;
            column.IsForeignKeyMember = originalColumn.IsForeignKey;
            column.IsPrimaryKeyMember  = originalColumn.IsPrimaryKey;
            column.IsUnique  = false;

            return column;

        }

        #region Utils
        private Type GetType(SmartCode.Model.SqlType sqlType)
        {
            switch (sqlType)
            {
                case SmartCode.Model.SqlType.AnsiText:
                    return typeof(string);

                case SmartCode.Model.SqlType.Binary:
                    return typeof(byte[]);

                case SmartCode.Model.SqlType.Boolean:
                    return typeof(bool);

                case SmartCode.Model.SqlType.Money:
                case SmartCode.Model.SqlType.SmallMoney:
                    return typeof(decimal);

                case SmartCode.Model.SqlType.DateTime:
                case SmartCode.Model.SqlType.SmallDateTime:
                    return typeof(DateTime);

                case SmartCode.Model.SqlType.Decimal:
                    return typeof(decimal);

                case SmartCode.Model.SqlType.Double:
                    return typeof(double);

                case SmartCode.Model.SqlType.GUID:
                    return typeof(Guid);

                case SmartCode.Model.SqlType.Int16:
                    return typeof(short);

                case SmartCode.Model.SqlType.Int32:
                    return typeof(int);

                case SmartCode.Model.SqlType.Int64:
                    return typeof(long);

                case SmartCode.Model.SqlType.VarBinary:
                    return typeof(object);

                case SmartCode.Model.SqlType.SByte:
                    return typeof(sbyte);

                case SmartCode.Model.SqlType.TimeStamp:
                    return typeof(TimeSpan);

                case SmartCode.Model.SqlType.UInt16:
                    return typeof(ushort);

                case SmartCode.Model.SqlType.UInt32:
                    return typeof(uint);

                case SmartCode.Model.SqlType.UInt64:
                    return typeof(ulong);

                case SmartCode.Model.SqlType.Unknown:
                    return typeof(object);

                default:
                    return typeof(string);
            }

        }

        private System.Data.DbType GetDBType(SmartCode.Model.SqlType sqlType)
        {
            switch (sqlType)
            {
                case SmartCode.Model.SqlType.AnsiText:
                    return  System.Data.DbType.AnsiString;

                case SmartCode.Model.SqlType.Binary:
                    return System.Data.DbType.Binary;

                case SmartCode.Model.SqlType.Boolean:
                    return System.Data.DbType.Boolean;

                case SmartCode.Model.SqlType.Money:
                case SmartCode.Model.SqlType.SmallMoney:
                    return System.Data.DbType.Currency;

                case SmartCode.Model.SqlType.DateTime:
                case SmartCode.Model.SqlType.SmallDateTime:
                    return System.Data.DbType.DateTime;

                case SmartCode.Model.SqlType.Decimal:
                    return System.Data.DbType.Decimal ;

                case SmartCode.Model.SqlType.Double:
                    return System.Data.DbType.Double;

                case SmartCode.Model.SqlType.GUID:
                    return System.Data.DbType.Guid ;

                case SmartCode.Model.SqlType.Int16:
                    return System.Data.DbType.Int16;

                case SmartCode.Model.SqlType.Int32:
                    return System.Data.DbType.Int32;

                case SmartCode.Model.SqlType.Int64:
                    return System.Data.DbType.Int64;

                case SmartCode.Model.SqlType.VarBinary:
                    return System.Data.DbType.Binary;

                case SmartCode.Model.SqlType.SByte:
                    return System.Data.DbType.SByte;

                case SmartCode.Model.SqlType.TimeStamp:
                    return System.Data.DbType.Time;

                case SmartCode.Model.SqlType.UInt16:
                    return System.Data.DbType.UInt16;

                case SmartCode.Model.SqlType.UInt32:
                    return System.Data.DbType.UInt32;

                case SmartCode.Model.SqlType.UInt64:
                    return System.Data.DbType.UInt64;

                case SmartCode.Model.SqlType.Unknown:
                    return System.Data.DbType.Object;

                default:
                    return System.Data.DbType.AnsiString;
            }

        }

        #endregion
    }
}
