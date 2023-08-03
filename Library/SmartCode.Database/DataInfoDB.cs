using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace SmartCode.Database
{
    public class DataInfoDB : TemplateBase
    {
        // Constructor.
        public DataInfoDB()
        {
            this.CreateOutputFile = true;
            this.Description = "Generates a C# class file for a database table";
            this.Name = "C# Database Class";
            this.OutputFolder = @"Data";
        }

        public override string OutputFileName()
        {
            return Table.Name + "InfoDB.cs";
        }

        public override void ProduceCode()
        {
            string tbInitial = Table.Name.Substring(0, 1).ToLower();

            bool HasXML = false;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.Description != null)
                {
                    if (column.Description.ToLower().IndexOf("xml") >= 0)
                        HasXML = true;
                }
            }

            StringBuilder sb = new StringBuilder("        public const string SQLSELECT = \"");
            bool firstrow = true;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (!firstrow)
                    sb.Append(",");
                sb.AppendFormat("{0}.{1}", tbInitial, column.Name);
                firstrow = false;
            }
            sb.Append("\";");

            sb.Append("\n        public const string SQLINSERTCOLUMNS = \"");
            firstrow = true;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsIdentity)
                    continue;

                if (!firstrow)
                    sb.Append(",");
                sb.Append(column.Name);
                firstrow = false;
            }
            sb.Append("\";");

            //            if (Table.PrimaryKeyColumns().Count > 0)
            //            {
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.Data;");
            WriteLine("using System.Data.SqlClient;");
            WriteLine("using System.Text;");
            if (HasXML)
                WriteLine("using System.Xml.Serialization;");
            WriteLine();
            WriteLine("namespace Core.ResService.BusinessEntities");
            WriteLine("{");
            WriteLine();
            //                WriteLine("    [Serializable]");

            WriteLine("    public partial class {0}Info", RemoveLstFromTableName(Table.Name));
            WriteLine("    {");
            WriteLine();
            WriteLine(sb.ToString());
            WriteLine();
            WriteLine("        #region Database fields");

            foreach (ColumnSchema column in Table.Columns())
            {
                //                    string dataType = GetDateTypeWithEnum(column);
                WriteLine("        private {0} _{1};", GetDateTypeWithEnum(column), column.Name);
            }
            WriteLine("        #endregion");


            //WriteLine();
            //WriteLine("        #region More fields");
            //WriteLine("        #endregion");

            WriteLine();
            WriteLine("        #region GETs and SETs");
            foreach (ColumnSchema column in Table.Columns())
            {
                WriteLine();
                WriteLine("        public {0} {1}", GetDateTypeWithEnum(column), column.Name);
                WriteLine("        {");
                WriteLine("            get {{ return _{0}; }}", column.Name);
                WriteLine("            set {{ _{0} = value; }}", column.Name);
                WriteLine("        }");
            }
            WriteLine("        #endregion");
            WriteLine();

            WriteLine();
            WriteLine();
            WriteLine("        public static {0}Info LoadDbRecord(IDataReader rdr)", RemoveLstFromTableName(Table.Name));
            WriteLine("        {");
            WriteLine("            {0}Info obj = null;", RemoveLstFromTableName(Table.Name));
            WriteLine();
            WriteLine("            if (rdr == null)");
            WriteLine("                return null;");
            WriteLine();
            WriteLine("            if (rdr.Read())");
            WriteLine("            {");
            WriteLine("                obj = new {0}Info();", RemoveLstFromTableName(Table.Name));
            int cnt = 0;
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.Comment != null && column.Comment.Length > 0 && column.Name.ToLower() != "hotelid")
                {
                    WriteLine("                obj.{0} = ({3})(rdr.{1}({2}));", column.Name, GetGetMethordName(column.NetDataType),
                        cnt++, column.Comment);
                }
                else
                {
                    WriteLine("                obj.{0} = rdr.{1}({2});", column.Name, GetGetMethordName(column.NetDataType), cnt++);
                }
            }
            WriteLine("            }");
            WriteLine("            return obj;");
            WriteLine("        }");


            WriteLine("        public static List<{0}Info> LoadDbRecords(IDataReader rdr)", RemoveLstFromTableName(Table.Name));
            WriteLine("        {");
            WriteLine("            List<{0}Info> ret = new List<{0}Info>();", RemoveLstFromTableName(Table.Name));
            WriteLine();
            WriteLine("            while (true)");
            WriteLine("            {");
            WriteLine("                {0}Info prog = LoadDbRecord(rdr);", RemoveLstFromTableName(Table.Name));
            WriteLine("                if (prog == null)");
            WriteLine("                    break;");
            WriteLine();
            WriteLine("                ret.Add(prog);");
            WriteLine("            }");
            WriteLine("            return ret;");
            WriteLine("        }            ");
            WriteLine();



            WriteLine("    }");
            WriteLine("}");
            //            }
            //            else
            //            {
            //                WriteLine("-- Entity " + Entity.Name + " does not have a primary key.");
            //            }
        }

        private string GetGetMethordName(string nettype)
        {
            return nettype.Replace("System.", "Get");
        }

        private string RemoveLstFromTableName(string name)
        {
            name = name.Replace("Lst_", "");
            name = name.Replace("lst_", "");
            return name;
        }

        private string GetDateTypeWithEnum(ColumnSchema col)
        {
            if (col.Comment != null && col.Comment.Length > 0 && col.Name.ToLower() != "hotelid")
                return col.Comment;
            else
                return col.NetDataType;
        }
    }
}
