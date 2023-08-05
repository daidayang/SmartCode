using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using SmartCode.Model;
using System.Data.OleDb;

namespace SmartCode.Studio.Database
{
    internal class TypesFactory
    {
        internal static IDictionary<String, SqlType> GetSQLTypes(String provider)
        {
            IDictionary<String, SqlType> sqlTypes = new Dictionary<String, SqlType>(30);

            switch (provider)
            {
                case "mssql":
                case "mssql2005":
                    sqlTypes.Add("bit", SqlType.Boolean);
                    sqlTypes.Add("char", SqlType.AnsiChar);
                    sqlTypes.Add("nvarchar", SqlType.VarChar);
                    sqlTypes.Add("nvarchar(max)", SqlType.VarCharMax);
                    sqlTypes.Add("text", SqlType.AnsiText);
                    sqlTypes.Add("sysname", SqlType.VarChar);
                    sqlTypes.Add("ntext", SqlType.Text);
                    sqlTypes.Add("nchar", SqlType.Char);
                    sqlTypes.Add("varchar", SqlType.AnsiVarChar);
                    sqlTypes.Add("varchar(max)", SqlType.AnsiVarCharMax);
                    sqlTypes.Add("binary", SqlType.Binary);
                    sqlTypes.Add("varbinary", SqlType.VarBinary);
                    sqlTypes.Add("datetime", SqlType.DateTime);
                    sqlTypes.Add("date", SqlType.Date);
                    sqlTypes.Add("decimal", SqlType.Decimal);
                    sqlTypes.Add("numeric", SqlType.Decimal);
                    sqlTypes.Add("float", SqlType.Double);
                    sqlTypes.Add("real", SqlType.Float);
                    sqlTypes.Add("smalldatetime", SqlType.SmallDateTime);
                    sqlTypes.Add("tinyint", SqlType.Byte);
                    sqlTypes.Add("smallint", SqlType.Int16);
                    sqlTypes.Add("int", SqlType.Int32);
                    sqlTypes.Add("int identity", SqlType.Int32);
                    sqlTypes.Add("uniqueidentifier", SqlType.GUID);
                    sqlTypes.Add("bigint", SqlType.Int64);
                    sqlTypes.Add("image", SqlType.Image);
                    sqlTypes.Add("varbinary(max)", SqlType.VarBinaryMax);
                    sqlTypes.Add("money", SqlType.Money);
                    sqlTypes.Add("smallmoney", SqlType.SmallMoney);
                    sqlTypes.Add("timestamp", SqlType.TimeStamp);
                    sqlTypes.Add("unknown", SqlType.Unknown);
                    break;
                //case "msaccess":
                //    sqlTypes = new Hashtable(23);
                //    sqlTypes.Add(typeof(System.Boolean), SqlType.Boolean);
                //    sqlTypes.Add(typeof(System.Byte), SqlType.Byte);
                //    sqlTypes.Add(typeof(System.DateTime), SqlType.DateTime);
                //    sqlTypes.Add(typeof(System.Decimal), SqlType.Decimal);
                //    sqlTypes.Add(typeof(System.Double), SqlType.Double);
                //    sqlTypes.Add(typeof(System.Guid), SqlType.GUID);
                //    sqlTypes.Add(typeof(System.Int16), SqlType.Int16);
                //    sqlTypes.Add(typeof(System.Int32), SqlType.Int32);
                //    sqlTypes.Add(typeof(System.String), SqlType.VarChar);
                //    sqlTypes.Add(typeof(System.Byte[]), SqlType.VarBinary);

                //    sqlTypes.Add(OleDbType.Binary, SqlType.Binary);
                //    sqlTypes.Add(OleDbType.Boolean, SqlType.Boolean);
                //    sqlTypes.Add(OleDbType.TinyInt, SqlType.Byte);
                //    sqlTypes.Add(OleDbType.Date, SqlType.DateTime);
                //    sqlTypes.Add(OleDbType.Numeric, SqlType.Decimal);
                //    sqlTypes.Add(OleDbType.Decimal, SqlType.Decimal);
                //    sqlTypes.Add(OleDbType.Double, SqlType.Double);
                //    sqlTypes.Add(OleDbType.Guid, SqlType.GUID);
                //    sqlTypes.Add(OleDbType.SmallInt, SqlType.Int16);
                //    sqlTypes.Add(OleDbType.Integer, SqlType.Int32);
                //    sqlTypes.Add(OleDbType.VarBinary, SqlType.VarBinary);
                //    sqlTypes.Add(OleDbType.VarChar, SqlType.VarChar);
                //    sqlTypes.Add(OleDbType.WChar, SqlType.VarChar);
                    break;
                case "oracle":
                    sqlTypes.Add("NVARCHAR2", SqlType.VarChar);
                    sqlTypes.Add("NCLOB", SqlType.Text);
                    sqlTypes.Add("NCHAR", SqlType.Char);
                    sqlTypes.Add("VARCHAR ", SqlType.AnsiVarChar);
                    sqlTypes.Add("VARCHAR2", SqlType.AnsiVarChar);
                    sqlTypes.Add("LONG", SqlType.AnsiVarChar);
                    sqlTypes.Add("CLOB", SqlType.AnsiText);
                    sqlTypes.Add("CHAR", SqlType.AnsiChar);
                    sqlTypes.Add("RAW", SqlType.Binary);
                    sqlTypes.Add("LONG RAW", SqlType.VarBinary);
                    sqlTypes.Add("NUMBER(1,0)", SqlType.Boolean);
                    sqlTypes.Add("DATE", SqlType.DateTime);
                    sqlTypes.Add("NUMBER(28,10)", SqlType.Decimal);
                    sqlTypes.Add("DOUBLE PRECISION", SqlType.Double);
                    sqlTypes.Add("FLOAT", SqlType.Double);
                    sqlTypes.Add("FLOAT(126)", SqlType.Double);
                    sqlTypes.Add("REAL", SqlType.Float);
                    sqlTypes.Add("FLOAT(63)", SqlType.Float);
                    sqlTypes.Add("NUMBER(3,0)", SqlType.Byte);
                    sqlTypes.Add("NUMBER(5,0)", SqlType.Int16);
                    sqlTypes.Add("NUMBER(10,0)", SqlType.Int32);
                    sqlTypes.Add("NUMBER(11,0)", SqlType.UInt32);
                    sqlTypes.Add("NUMBER(18,0)", SqlType.TimeStamp);
                    sqlTypes.Add("NUMBER(19,0)", SqlType.Int64);
                    sqlTypes.Add("NUMBER(20,0)", SqlType.UInt64);
                    sqlTypes.Add("BLOB", SqlType.Image);
                    sqlTypes.Add("NUMBER(15,4)", SqlType.Money);
                    sqlTypes.Add("NUMBER(6,4)", SqlType.SmallMoney);
                    sqlTypes.Add("ROWID", SqlType.Int64);
                    sqlTypes.Add("fallback for a nonexistent type", SqlType.Unknown);
                    break;
                case "mysql":
                    sqlTypes.Add("BIT", SqlType.Boolean);
                    sqlTypes.Add("BOOL", SqlType.Boolean);
                    sqlTypes.Add("BOOLEAN", SqlType.Boolean);
                    sqlTypes.Add("CHAR", SqlType.AnsiChar);
                    sqlTypes.Add("TINYBLOB", SqlType.AnsiText);
                    sqlTypes.Add("TINYTEXT", SqlType.AnsiText);
                    sqlTypes.Add("TEXT", SqlType.Text);
                    sqlTypes.Add("ENUM", SqlType.AnsiVarCharMax);
                    sqlTypes.Add("SET", SqlType.AnsiVarCharMax);
                    sqlTypes.Add("VARCHAR", SqlType.AnsiVarChar);
                    sqlTypes.Add("BINARY", SqlType.Binary);
                    sqlTypes.Add("VARBINARY", SqlType.VarBinary);
                    sqlTypes.Add("DATE", SqlType.DateTime);
                    sqlTypes.Add("DATETIME", SqlType.DateTime);
                    sqlTypes.Add("TIMESTAMP", SqlType.TimeStamp);
                    sqlTypes.Add("TIME", SqlType.DateTime);
                    sqlTypes.Add("YEAR", SqlType.DateTime);
                    sqlTypes.Add("DECIMAL", SqlType.Decimal);
                    sqlTypes.Add("DOUBLE", SqlType.Double);
                    sqlTypes.Add("DOUBLE PRECISION", SqlType.Float);
                    sqlTypes.Add("FLOAT", SqlType.Float);
                    sqlTypes.Add("smalldatetime", SqlType.SmallDateTime);
                    sqlTypes.Add("TINYINT", SqlType.Byte);
                    sqlTypes.Add("SMALLINT", SqlType.Int16);
                    sqlTypes.Add("MEDIUMINT", SqlType.Int32);
                    sqlTypes.Add("INT", SqlType.Int32);
                    sqlTypes.Add("INTEGER", SqlType.Int32);
                    sqlTypes.Add("BIGINT", SqlType.Int64);
                    sqlTypes.Add("unknown", SqlType.Unknown);
                    break;
                default:
                    throw new Exception("Invalid Provider Type");
            }
            return sqlTypes;
        }

        internal static IDictionary<SqlType, String> GetNetDataTypes()
        {
            IDictionary<SqlType, String> netDataTypes = new Dictionary<SqlType, String>();

            netDataTypes.Add(SqlType.AnsiText, "System.String");
            netDataTypes.Add(SqlType.AnsiChar, "System.String");
            netDataTypes.Add(SqlType.AnsiVarChar, "System.String");
            netDataTypes.Add(SqlType.AnsiVarCharMax, "System.String");
            netDataTypes.Add(SqlType.Byte, "System.Byte");
            netDataTypes.Add(SqlType.Binary, "System.Byte[]");
            netDataTypes.Add(SqlType.Boolean, "System.Boolean");
            netDataTypes.Add(SqlType.Char, "System.String");
            netDataTypes.Add(SqlType.DateTime, "System.DateTime");
            netDataTypes.Add(SqlType.Date, "System.DateTime");
            netDataTypes.Add(SqlType.Decimal, "System.Decimal");
            netDataTypes.Add(SqlType.Double, "System.Double");
            netDataTypes.Add(SqlType.Float, "System.Double");
            netDataTypes.Add(SqlType.GUID, "System.Guid");
            netDataTypes.Add(SqlType.Int16, "System.Int16");
            netDataTypes.Add(SqlType.Int32, "System.Int32");
            netDataTypes.Add(SqlType.Int64, "System.Int64");
            netDataTypes.Add(SqlType.Image, "System.Byte[]");
            netDataTypes.Add(SqlType.Money, "System.Decimal");
            netDataTypes.Add(SqlType.SmallDateTime, "System.DateTime");
            netDataTypes.Add(SqlType.SmallMoney, "System.Decimal");
            netDataTypes.Add(SqlType.Text, "System.String");
            netDataTypes.Add(SqlType.TimeStamp, "System.DateTime");
            netDataTypes.Add(SqlType.Unknown, "System.String");
            netDataTypes.Add(SqlType.VarBinary, "System.Byte[]");
            netDataTypes.Add(SqlType.VarBinaryMax, "System.Byte[]");
            netDataTypes.Add(SqlType.VarChar, "System.String");
            netDataTypes.Add(SqlType.VarCharMax, "System.String");

            return netDataTypes;
        }
    }
}
