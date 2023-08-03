using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

using StoreProcedures.Utils;

namespace SmartCode.Database
{
    public class CSharp_Bll : TemplateBase
    {
        // Constructor.
        public CSharp_Bll()
        {
            this.CreateOutputFile = true;
            this.Description = "Generates a C# class file BLL Layer";
            this.Name = "C# bll Class";
            this.OutputFolder = @"Bll";
        }

        public override string OutputFileName()
        {
            return Table.Name + "BllGC.cs";
        }

        public override void ProduceCode()
        {
            StringBuilder sb = new StringBuilder();
            string HotelIDText = "";
            string InputParams = "";
            string InputParams2 = "";
            string ParameterAssigment = "";
            bool ReturnIdentity = false;

            bool HasActive = false;
            bool HasHotelID = false;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.Name.ToLower() == "hotelid" && column.Comment.ToLower() != "noscript")
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
            WriteLine("namespace Core.ResService.BusinessLogic");
            WriteLine("{");
            WriteLine("    public partial class {0}Bll : BusinessLogicBase", Table.Name);
            WriteLine("    {");
            WriteLine();


            if (Table.PrimaryKeyColumns().Count > 0)
            {
                #region Building function and sql command parameters

                sb = new StringBuilder();
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    sb.AppendFormat("{0} {1},", column.NetDataType, Lower1stChar(column.Name));
                    if (column.Name.ToLower() == "hotelid")
                        HotelIDText = Lower1stChar(column.Name);
                }
                InputParams = Common.Substring(sb.ToString(), (",").Length);

                sb = new StringBuilder();
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    sb.AppendFormat("{0},", Lower1stChar(column.Name));
                }
                InputParams2 = Common.Substring(sb.ToString(), (",").Length);

                if (HotelIDText=="")
                {
                    InputParams = string.Format("int hotelID, {0}", InputParams);
                    HotelIDText = "hotelID";
                }
                #endregion

            }


            #region SELECTs

            WriteLine();
            WriteLine("		   #region SELECTs");

            #region Select By Hotel

            if (HasHotelID)
            {
                WriteLine();
                if (HasActive)
                    WriteLine(@"
        public List<Core.ResService.BusinessEntities.{0}Info> SelectByHotel(int hotelID, bool activeOnly)
        {{
            Core.ResService.DataAccess.{0}Dal dal{0} = new Core.ResService.DataAccess.{0}Dal();
            dal{0}.Open(hotelID);

            try
            {{
                return dal{0}.SelectByHotel(hotelID, activeOnly);
            }}
            catch (Exception e)
            {{
                _ErrorMessage = e.Message;
                _ErrorCode = -1;
                log.ErrorFormat(""Error: Source=BusinessLogic.{0}.SelectByHotel Message={0}"", _ErrorMessage);
            }}
            finally
            {{
                dal{0}.Close();
            }}

            return null;
        }}", Table.Name);
                else
                    WriteLine(@"
        public List<Core.ResService.BusinessEntities.{0}Info> SelectByHotel(int hotelID)
        {{
            Core.ResService.DataAccess.{0}Dal dal{0} = new Core.ResService.DataAccess.{0}Dal();
            dal{0}.Open(hotelID);

            try
            {{
                return dal{0}.SelectByHotel(hotelID);
            }}
            catch (Exception e)
            {{
                _ErrorMessage = e.Message;
                _ErrorCode = -1;
                log.ErrorFormat(""Error: Source=BusinessLogic.{0}.SelectByHotel Message={0}"", _ErrorMessage);
            }}
            finally
            {{
                dal{0}.Close();
            }}

            return null;
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
            Core.ResService.DataAccess.{0}Dal dal{0} = new Core.ResService.DataAccess.{0}Dal();
            dal{0}.Open({3});

            try
            {{
                return dal{0}.Select({2});
            }}
            catch (Exception e)
            {{
                _ErrorMessage = e.Message;
                _ErrorCode = -1;
                log.ErrorFormat(""Error: Source=BusinessLogic.{0}.Select Message={0}"", _ErrorMessage);
            }}
            finally
            {{
                dal{0}.Close();
            }}

            return null;
        }}", Table.Name, InputParams, InputParams2, HotelIDText);
                WriteLine();
            }

            #endregion

            WriteLine();
            WriteLine("        #endregion");

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
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.DateTime", 4);
                    break;
                case "smalldatetime":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.SmallDateTime", 2);
                    break;
                case "char":
                    return string.Format("{0}{1},			{2}, {3}),\n", FirstPart, column.Name.ToUpper(), "SqlDbType.Char", column.Length);
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