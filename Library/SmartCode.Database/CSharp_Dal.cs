using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

using StoreProcedures.Utils;

namespace SmartCode.Database
{
    public class CSharp_Dal : TemplateBase
    {
        // Constructor.
        public CSharp_Dal()
        {
            this.CreateOutputFile = true;
            this.Description = "Generates a C# class file DAL Layer";
            this.Name = "C# Dal Class";
            this.OutputFolder = @"Dal";
        }

        public override string OutputFileName()
        {
            return Table.Name + "DalGC.cs";
        }

        public override void ProduceCode()
        {
            int LineCount = 0;
            StringBuilder sb = new StringBuilder();
            string InputParams = "";
            string ParameterAssigment = "";
            bool ReturnIdentity = false;

            bool HasActive = false;
            bool HasHotelID = false;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.Name.ToLower() == "hotelid" && (column.Comment == null || column.Comment.ToLower() != "noscript"))
                    HasHotelID = true;

                if (column.Name.ToLower() == "active")
                    HasActive = true;
            }

            if (Table.PrimaryKeyColumns().Count == 1)
            {
                if (Table.PrimaryKeyColumns()[0].IsIdentity)
                {
                    ReturnIdentity = true;
                }
            }

            int cnt = 0;
            string dataClass = string.Format("Core.ResService.BusinessEntities.{0}Info", Table.Name);
            string tbInitial = Table.Name.Substring(0, 1).ToLower();

            //StringBuilder sb = new StringBuilder("        const string SQLSELECT = \"");
            //bool firstrow = true;
            //foreach (ColumnSchema column in Table.Columns()) {
            //    if (!firstrow)
            //        sb.Append(",");
            //    sb.AppendFormat("{0}.{1}", tbInitial, column.Name);
            //    firstrow = false;
            //}
            //sb.Append("\";");

            //            if (Table.PrimaryKeyColumns().Count > 0)
            //            {
            WriteLine("using System;");
            WriteLine("using System.Collections;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.Data;");
            WriteLine("using System.Data.Common;");
            WriteLine("using System.Data.SqlClient;");
            WriteLine("using System.Text;");
            WriteLine();
            //WriteLine("using Microsoft.Practices.EnterpriseLibrary.Data;");
            //WriteLine("using Microsoft.Practices.EnterpriseLibrary.Data.Sql;");
            //WriteLine();
            WriteLine("using Core.ResService.BusinessEntities;");
            WriteLine();
            WriteLine("namespace Core.ResService.DataAccess");
            WriteLine("{");
            WriteLine("    public partial class {0}Dal : SqlBase", Table.Name);
            WriteLine("    {");
            WriteLine();
            WriteLine("		#region SQL Statements");
            WriteLine();

            if (HasHotelID)
            {
                WriteLine("        private const string SQL_SELECTBYHOTEL = \"{0}_SelByHotel\";", Table.Name);
                WriteLine("        private const string SQL_DELETEBYHOTEL = \"{0}_DelByHotel\";", Table.Name);
                if (HasActive)
                    WriteLine("        private const string SQL_DEACTIVATEBYHOTEL = \"{0}_DeActivateByHotel\";", Table.Name);

            }

            WriteLine("	    private const string SQL_INSERT = \"{0}_add\";", Table.Name);

            if (Table.PrimaryKeyColumns().Count > 0)
            {
                WriteLine("     private const string SQL_SELECT = \"{0}_Sel\";", Table.Name);
                WriteLine("     private const string SQL_UPDATE = \"{0}_edt\";", Table.Name);
                WriteLine("		private const string SQL_DELETE = \"{0}_del\";", Table.Name);
                if (HasActive)
                    WriteLine("		private const string SQL_DEACTIVATE = \"{0}_DeActivate\";", Table.Name);

            }
            WriteLine(@"


		#endregion

        #region Constructors

        public {0}Dal() {{ }}

        public {0}Dal(SqlConnection conn) : base(conn) {{ }}

        public {0}Dal(SqlConnection conn, SqlTransaction trans) : base(conn, trans) {{ }}

        #endregion", Table.Name);
            WriteLine();

            #region SQL Parameters

            WriteLine("         #region SQL Parameters");
            WriteLine();
            foreach (ColumnSchema column in Table.Columns())
            {
                WriteLine("         private const string PARAM_{0} = \"@{1}\";", column.Name.ToUpper(), column.Name);
            }
            if (HasActive)
                WriteLine("         private const string PARAM_ACTIVEONLY = \"@ActiveOnly\";");
            WriteLine();
            WriteLine("         #endregion");
            WriteLine();

            #endregion

            #region UPDATEs

            WriteLine();
            WriteLine("        #region	UPDATEs");

            #region Insert

            WriteLine();
            WriteLine(@"
        public {2} Insert( Core.ResService.BusinessEntities.{0}Info {1} )
		{{
			SqlParameter[] Param_Insert = GetParameters_Insert();", Table.Name, Table.Name.ToLower(), (ReturnIdentity) ? "int" : "void");
            cnt = 0;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (!column.IsIdentity)
                {
                    if (column.Comment != null && column.Comment.Length > 0 && column.Name.ToLower() != "hotelid")
                        WriteLine("            Param_Insert[{0}].Value = ({3}){1}.{2};", cnt++, Table.Name.ToLower(), column.Name, column.NetDataType);
                    else
                    {
                        if (column.NetDataType == "System.String")
                        {
                            WriteLine("            if ( {0}.{1} == null )", Table.Name.ToLower(), column.Name);
                            WriteLine("                Param_Insert[{0}].Value = string.Empty;", cnt);
                            WriteLine("            else");
                            WriteLine("                Param_Insert[{0}].Value = {1}.{2};", cnt++, Table.Name.ToLower(), column.Name);
                        }
                        else
                            WriteLine("            Param_Insert[{0}].Value = {1}.{2};", cnt++, Table.Name.ToLower(), column.Name);
                    }
                }
            }
            WriteLine("            SQLHelper.ExecuteNonQuery(base._internalConnection, base._internalADOTransaction, CommandType.StoredProcedure, SQL_INSERT, Param_Insert);");

            if (ReturnIdentity)
                WriteLine("            return (int)(Param_Insert[{0}].Value);", cnt);

            WriteLine("		}");

            #endregion

            if (Table.PrimaryKeyColumns().Count > 0)
            {
                #region Building function and sql command parameters

                sb = new StringBuilder();
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    if (column.Comment != null && column.Comment.Length > 0 && column.Name.ToLower() != "hotelid")
                        sb.AppendFormat("{0} {1},", column.Comment, Lower1stChar(column.Name));
                    else
                        sb.AppendFormat("{0} {1},", column.NetDataType, Lower1stChar(column.Name));
                }
                InputParams = Common.Substring(sb.ToString(), (",").Length);

                sb = new StringBuilder();
                cnt = 0;
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    sb.AppendFormat("            Params[{0}].Value = {1};\n", cnt++, Lower1stChar(column.Name));
                }
                ParameterAssigment = sb.ToString();

                #endregion

                #region Update

                WriteLine();
                WriteLine(@"
        public int Update( Core.ResService.BusinessEntities.{0}Info {1} )
		{{
			SqlParameter[] Param_Update = GetParameters_Update();", Table.Name, Table.Name.ToLower());
                cnt = 0;
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (column.NetDataType == "System.String")
                    {
                        WriteLine("            if ( {0}.{1} == null )", Table.Name.ToLower(), column.Name);
                        WriteLine("                Param_Update[{0}].Value = string.Empty;", cnt);
                        WriteLine("            else");
                        WriteLine("                Param_Update[{0}].Value = {1}.{2};", cnt++, Table.Name.ToLower(), column.Name);
                    }
                    else
                        WriteLine("            Param_Update[{0}].Value = {1}.{2};", cnt++, Table.Name.ToLower(), column.Name);
                }
                WriteLine("            return SQLHelper.ExecuteNonQuery( base._internalConnection, base._internalADOTransaction, CommandType.StoredProcedure, SQL_UPDATE, Param_Update);");
                WriteLine("		}");

                #endregion

                #region Delete

                LineCount = 0;
                sb = new StringBuilder();
                sb.AppendFormat("DELETE FROM {0} WHERE ", Table.Name);
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    if (LineCount == 0)
                        sb.AppendFormat("[{0}]=@{1} ", column.Name, column.Code);
                    else
                        sb.AppendFormat("AND [{0}]=@{1} ", column.Name, column.Code);
                    LineCount++;
                }


                WriteLine();
                WriteLine(@"
		public int Delete( {0} )
		{{
            const string sql = ""{2}""; 

            SqlParameter[] Params = GetParameters_Delete();
			{1}

            return SQLHelper.ExecuteNonQuery(base._internalConnection, base._internalADOTransaction, CommandType.Text, sql, Params);
		}}", InputParams, ParameterAssigment, sb.ToString());
                WriteLine();

                #endregion

                if (HasActive)
                {
                    #region Deactivate

                    LineCount = 0;
                    sb = new StringBuilder();
                    sb.AppendFormat("UPDATE {0} SET Active=0 WHERE ", Table.Name);
                    foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                    {
                        if (LineCount == 0)
                            sb.AppendFormat("[{0}]=@{1} ", column.Name, column.Code);
                        else
                            sb.AppendFormat("AND [{0}]=@{1} ", column.Name, column.Code);
                        LineCount++;
                    }

                    WriteLine();
                    WriteLine(@"
		public int DeActivate( {0} )
		{{
            const string sql = ""{2}""; 

            SqlParameter[] Params = GetParameters_Delete();
			{1}

            return SQLHelper.ExecuteNonQuery(base._internalConnection, base._internalADOTransaction, CommandType.Text, sql, Params);
		}}", InputParams, ParameterAssigment, sb.ToString());
                    WriteLine();

                    #endregion
                }
            }

            #region Delete By Hotel

            if (HasHotelID)
            {
                WriteLine();
                WriteLine(@"
        public int DeleteByHotel(int hotelId)
        {{
            const string sql = ""DELETE FROM {0} WHERE HotelID=@HotelID""; 

            SqlParameter[] Param_DeleteByHotel = GetParameters_ByHotelID();
			Param_DeleteByHotel[0].Value = hotelId;

            return SQLHelper.ExecuteNonQuerySingleParm(base._internalConnection, base._internalADOTransaction,
                CommandType.Text, sql, Param_DeleteByHotel[0]);

        }}", Table.Name);
                WriteLine();
            }

            #endregion

            if (HasActive)
            {
                #region DeActivate By Hotel

                if (HasHotelID)
                {
                    WriteLine();
                    WriteLine(@"
        public int DeActivateByHotel(int hotelId)
        {{
            const string sql = ""UPDATE {0} SET Active=0 WHERE HotelID=@HotelID""; 

            SqlParameter[] Param_DeleteByHotel = GetParameters_ByHotelID();
			Param_DeleteByHotel[0].Value = hotelId;

            return SQLHelper.ExecuteNonQuerySingleParm(base._internalConnection, base._internalADOTransaction,
                CommandType.Text, sql, Param_DeleteByHotel[0]);

        }}", Table.Name);
                    WriteLine();
                }

                #endregion
            }

            WriteLine();
            WriteLine("            		#endregion");

            #endregion

            #region SELECTs

            WriteLine();
            WriteLine("		   #region SELECTs");

            #region Select By Hotel

            if (HasHotelID)
            {
                WriteLine();
                if (HasActive)
                    WriteLine(@"
        public List<Core.ResService.BusinessEntities.{0}Info> SelectByHotel(int hotelId, bool activeOnly)
        {{
            List<Core.ResService.BusinessEntities.{0}Info> ret = null;

            SqlParameter[] Params = GetParameters_ByHotelIDActiveOnly();
			Params[0].Value = hotelId;
			Params[1].Value = activeOnly;

            using (SqlDataReader rdr = SQLHelper.ExecuteReader(base._internalConnection, base._internalADOTransaction, CommandType.StoredProcedure, SQL_SELECTBYHOTEL, Params)) 
            {{
                ret = Core.ResService.BusinessEntities.{0}Info.LoadDbRecords(rdr);
            }}
            return ret;
        }}", Table.Name);
                else
                    WriteLine(@"
        public List<Core.ResService.BusinessEntities.{0}Info> SelectByHotel(int hotelId)
        {{
            List<Core.ResService.BusinessEntities.{0}Info> ret = null;

            SqlParameter[] Param_SelectByHotel = GetParameters_ByHotelID();
			Param_SelectByHotel[0].Value = hotelId;

            using (SqlDataReader rdr = SQLHelper.ExecuteReaderSingleParm(base._internalConnection, base._internalADOTransaction, CommandType.StoredProcedure, SQL_SELECTBYHOTEL, Param_SelectByHotel[0])) 
            {{
                ret = Core.ResService.BusinessEntities.{0}Info.LoadDbRecords(rdr);
            }}
            return ret;
        }}", Table.Name);


                WriteLine();
            }

            #endregion

            #region Select by Primary Key

            if (Table.PrimaryKeyColumns().Count > 0)
            {
                WriteLine();
                WriteLine(@"
        public Core.ResService.BusinessEntities.{0}Info Select({1})
        {{
            Core.ResService.BusinessEntities.{0}Info ret = null;

            SqlParameter[] Params = GetParameters_Select();
            {2}

            using (SqlDataReader rdr = SQLHelper.ExecuteReader(base._internalConnection, base._internalADOTransaction, CommandType.StoredProcedure, SQL_SELECT, Params)) 
            {{
                ret = Core.ResService.BusinessEntities.{0}Info.LoadDbRecord(rdr);
            }}
            return ret;
        }}", Table.Name, InputParams, ParameterAssigment);
                WriteLine();
            }

            #endregion

            WriteLine();
            WriteLine("        #endregion");

            #endregion



            #region Building Parameters

            WriteLine();
            WriteLine("		#region Build Parameters");
            WriteLine();

            #region SelectByHotel

            if (HasHotelID)
            {
                WriteLine(@"

        private static SqlParameter[] GetParameters_ByHotelID() 
		{{
			SqlParameter[] parms = SQLHelper.GetCacheParameters(""BYHOTELID"");

			if (parms == null) 
			{{
				parms = new SqlParameter[] {{
											   new SqlParameter(PARAM_HOTELID,			SqlDbType.Int, 4)
										   }};
				SQLHelper.CacheParameters(""BYHOTELID"", parms);
			}}
			return parms;
		}}", Table.Name.ToUpper());

                if (HasActive)
                {
                    WriteLine(@"

        private static SqlParameter[] GetParameters_ByHotelIDActiveOnly() 
		{{
			SqlParameter[] parms = SQLHelper.GetCacheParameters(""BYHOTELIDACTIVEONLY"");

			if (parms == null) 
			{{
				parms = new SqlParameter[] {{
											   new SqlParameter(PARAM_HOTELID,			SqlDbType.Int, 4),
											   new SqlParameter(PARAM_ACTIVEONLY,	    SqlDbType.Bit, 1)
										   }};
				SQLHelper.CacheParameters(""BYHOTELIDACTIVEONLY"", parms);
			}}
			return parms;
		}}", Table.Name.ToUpper());
                }
            }

            #endregion

            #region Insert

            WriteLine(@"
		private static SqlParameter[] GetParameters_Insert() 
		{
			SqlParameter[] parms = SQLHelper.GetCacheParameters(SQL_INSERT);

			if (parms == null) 
			{
				parms = new SqlParameter[] {");

            cnt = 0;
            sb = new StringBuilder();
            foreach (ColumnSchema column in Table.Columns())
            {
                if (!column.IsIdentity)
                {
                    sb.Append(BuildParamStatement(column));
                    cnt++;
                }
            }

            if (ReturnIdentity)
            {
                WriteLine(sb.ToString());
                WriteLine("                                               new SqlParameter(\"@RETURN_VALUE\", SqlDbType.Int)");
                WriteLine("										   };");
                WriteLine("                parms[{0}].Direction = ParameterDirection.ReturnValue;", cnt);
            }
            else
            {
                WriteLine(Common.Substring(sb.ToString(), ((",").Length + ("\n").Length)));
                WriteLine("										   };");
            }

            WriteLine(@"				SQLHelper.CacheParameters(SQL_INSERT, parms);
			}
			return parms;
		}");

            #endregion

            if (Table.PrimaryKeyColumns().Count > 0)
            {
                #region Select

                WriteLine();
                WriteLine(@"
		private static SqlParameter[] GetParameters_Select() 
		{
			SqlParameter[] parms = SQLHelper.GetCacheParameters(SQL_SELECT);

			if (parms == null) 
			{
				parms = new SqlParameter[] {");

                sb = new StringBuilder();
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                    sb.Append(BuildParamStatement(column));

                WriteLine(Common.Substring(sb.ToString(), ((",").Length + ("\n").Length)));

                WriteLine(@"										   };
				SQLHelper.CacheParameters(SQL_SELECT, parms);
			}
			return parms;
		}");

                #endregion

                #region Update

                WriteLine();
                WriteLine(@"
		private static SqlParameter[] GetParameters_Update() 
		{
			SqlParameter[] parms = SQLHelper.GetCacheParameters(SQL_UPDATE);

			if (parms == null) 
			{
				parms = new SqlParameter[] {");

                sb = new StringBuilder();
                foreach (ColumnSchema column in Table.Columns())
                {
                    sb.Append(BuildParamStatement(column));
                }

                WriteLine(Common.Substring(sb.ToString(), ((",").Length + ("\n").Length)));

                WriteLine(@"										   };
				SQLHelper.CacheParameters(SQL_UPDATE, parms);
			}
			return parms;
		}");

                #endregion

                #region Delete

                WriteLine();
                WriteLine(@"
		private static SqlParameter[] GetParameters_Delete() 
		{
			SqlParameter[] parms = SQLHelper.GetCacheParameters(SQL_DELETE);

			if (parms == null) 
			{
				parms = new SqlParameter[] {");

                sb = new StringBuilder();
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                    sb.Append(BuildParamStatement(column));

                WriteLine(Common.Substring(sb.ToString(), ((",").Length + ("\n").Length)));

                WriteLine(@"										   };
				SQLHelper.CacheParameters(SQL_DELETE, parms);
			}
			return parms;
		}");

                #endregion
            }

            WriteLine();
            WriteLine("		#endregion");
            WriteLine();

            #endregion

            WriteLine();
            WriteLine("    }");
            WriteLine("}");
        }

        private string GetGetMethordName(string nettype)
        {
            return nettype.Replace("System.", "Get");
        }

        private string BuildParamStatement(ColumnSchema column)
        {
            string FirstPart = "                                               new SqlParameter(PARAM_";
            switch (column.OriginalSQLType)
            {
                case "int":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.Int", 4);
                    break;
                case "smallint":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.SmallInt", 2);
                    break;
                case "tinyint":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.TinyInt", 1);
                    break;
                case "datetime":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.DateTime", 8);
                    break;
                case "smalldatetime":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.SmallDateTime", 4);
                    break;
                case "char":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.Char", column.Length);
                    break;
                case "nchar":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.NChar", column.Length);
                    break;
                case "varchar":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.VarChar", column.Length);
                    break;
                case "nvarchar":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.NVarChar", column.Length);
                    break;
                case "real":
                    return string.Format("{0}{1},			{2}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.Real");
                    break;
                case "bit":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.Bit", 1);
                    break;
                case "float":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.Float", 8);
                    break;
                case "text":
                    return string.Format("{0}{1},                 {2}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.Text");
                case "ntext":
                    return string.Format("{0}{1},                 {2}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.NText");
                default:
                    return string.Format("{0}{1},			{2}.{3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType", column.OriginalSQLType);
                    break;
            }
            return "";
        }

        private string Lower1stChar(string name)
        {
            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }
    }
}


/*

*/