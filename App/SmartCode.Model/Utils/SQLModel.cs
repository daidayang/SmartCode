using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCode.Model.Utils
{
   /// <summary>
   /// Contains Model state and behaviour specifically related to SQLServer.
   /// </summary>
   public class SQLModel
   {

      /// <summary>
      /// A static Dictionary that provides the SqlDbType names (properly cased) necessary
      /// to generate appropriate DBParameters.  May be used by Templates.
      /// The key is SmartCode.Model.SQLType.
      /// 
      /// </summary>
      public static Dictionary<SqlType, string> SqlDbTypeName
      {
         get
         {
            if (m_SqlDbTypeName == null)
            {
               m_SqlDbTypeName = new Dictionary<SqlType, string>(36);
               m_SqlDbTypeName[SqlType.AnsiChar] = "SqlDbType.Char";
               m_SqlDbTypeName[SqlType.AnsiVarChar] = "SqlDbType.VarChar";
               m_SqlDbTypeName[SqlType.AnsiVarCharMax] = "SqlDbType.VarChar";
               m_SqlDbTypeName[SqlType.AnsiText] = "SqlDbType.Text";
               m_SqlDbTypeName[SqlType.Binary] = "SqlDbType.Binary";
               m_SqlDbTypeName[SqlType.Boolean] = "SqlDbType.Bit";
               m_SqlDbTypeName[SqlType.Byte] = "SqlDbType.TinyInt";
               m_SqlDbTypeName[SqlType.Decimal] = "SqlDbType.Decimal";
               m_SqlDbTypeName[SqlType.Double] = "SqlDbType.Float";
               m_SqlDbTypeName[SqlType.Float] = "SqlDbType.Real";
               m_SqlDbTypeName[SqlType.DateTime] = "SqlDbType.DateTime";
               m_SqlDbTypeName[SqlType.GUID] = "SqlDbType.UniqueIdentifier";
               m_SqlDbTypeName[SqlType.Image] = "SqlDbType.Image";
               m_SqlDbTypeName[SqlType.Int16] = "SqlDbType.SmallInt";
               m_SqlDbTypeName[SqlType.Int32] = "SqlDbType.Int";
               m_SqlDbTypeName[SqlType.Int64] = "SqlDbType.BigInt";
               m_SqlDbTypeName[SqlType.Money] = "SqlDbType.Money";
               m_SqlDbTypeName[SqlType.SByte] = "SqlDbType.TinyInt";
               m_SqlDbTypeName[SqlType.SmallDateTime] = "SqlDbType.SmallDateTime";
               m_SqlDbTypeName[SqlType.SmallMoney] = "SqlDbType.SmallMoney";
               m_SqlDbTypeName[SqlType.Text] = "SqlDbType.NText";
               m_SqlDbTypeName[SqlType.TimeStamp] = "SqlDbType.Timestamp";
               m_SqlDbTypeName[SqlType.UInt16] = "SqlDbType.SmallInt";
               m_SqlDbTypeName[SqlType.UInt32] = "SqlDbType.Int";
               m_SqlDbTypeName[SqlType.UInt64] = "SqlDbType.BigInt";
               m_SqlDbTypeName[SqlType.Unknown] = "SqlDbType.Variant";
               m_SqlDbTypeName[SqlType.VarBinary] = "SqlDbType.VarBinary";
               m_SqlDbTypeName[SqlType.VarBinaryMax] = "SqlDbType.VarBinary";
               m_SqlDbTypeName[SqlType.Char] = "SqlDbType.NChar";
               m_SqlDbTypeName[SqlType.VarChar] = "SqlDbType.NVarChar";
               m_SqlDbTypeName[SqlType.VarCharMax] = "SqlDbType.NVarChar";
               m_SqlDbTypeName[SqlType.Variant] = "SqlDbType.Variant";
            }
            return m_SqlDbTypeName;
         }
      }
      private static Dictionary<SqlType, string> m_SqlDbTypeName = null;

   } //class SqlModel

}
