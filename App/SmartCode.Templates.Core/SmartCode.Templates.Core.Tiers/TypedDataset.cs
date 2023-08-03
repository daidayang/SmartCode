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
using SmartCode.Template;
using SmartCode.Model;

namespace SmartCode.Templates.Core.Tiers
{
    public class TypedDataset : TemplateBase
    {
        public TypedDataset()
        {
            CreateOutputFile = true;
            Description = "The common entity for all Tiers in XML format";
            Name = "Typed Dataset";
            OutputFolder = @"Common";
        }

        public override string OutputFileName()
        {
            return base.Entity.Name + "DS.xsd";
        }

        public override void ProduceCode()
        {
            if (! Table.IsTable)
            {
                throw new Exception ("' The used for this class is only available for tables, not for views.");
            }

            WriteLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            WriteLine(@"<xs:schema id=""" + Table.Code + @"DS"" targetNamespace=""http://tempuri.org/" + Table.Code + @"DS.xsd"" elementFormDefault=""qualified""");
            WriteLine(@"	attributeFormDefault=""qualified"" xmlns=""http://tempuri.org/" + Table.Code + @"DS.xsd"" xmlns:mstns=""http://tempuri.org/" + Table.Code + @"DS.xsd""");
            WriteLine(@"	xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata"" xmlns:msprop=""urn:schemas-microsoft-com:xml-msprop"" >");
            WriteLine(@"	<xs:element name=""" + Table.Code + @"DS"" msdata:IsDataSet=""true"">");
            WriteLine(@"		<xs:complexType>");
            WriteLine(@"			<xs:choice maxOccurs=""unbounded"">");
            WriteLine(@"				<xs:element name=""" + Table.Code + @""">");
            WriteLine(@"					<xs:complexType>");
            WriteLine(@"						<xs:sequence>");
            foreach (ColumnSchema column in Table.Columns())
            {
                if (column.IsIdentity)
                {
                    WriteLine(@"				<xs:element name=""" + column.Code + @""" msdata:AutoIncrement=""true"" type=""" + GetXMLMappings(column) + @""" />");
                }
                else
                {
                    if (column.IsRequired)
                    {
                        if (ColumnIsString(column))
                        {
                            WriteLine(@"				<xs:element name=""" + column.Code + @""" type=""" + GetXMLMappings(column) + @"""  minOccurs=""0"" msprop:nullValue=""_null""/>");
                        }
                        else
                        {
                            WriteLine(@"				<xs:element name=""" + column.Code + @""" type=""" + GetXMLMappings(column) + @"""  minOccurs=""0""/>");
                        }
                    }
                    else
                    {
                        WriteLine(@"				<xs:element name=""" + column.Code + @""" type=""" + GetXMLMappings(column) + @"""/>");
                    }

                }
                if (column.GetLOV().Count > 0)
                {
                    foreach (ColumnSchema referenceColumn in column.GetLOV())
                    {
                        WriteLine(@"				<xs:element name=""" + column.Code + "_" + referenceColumn.Code + @""" type=""" + GetXMLMappings(referenceColumn) + @"""  minOccurs=""0"" msprop:nullValue=""_null""/>");
                    }
                }
            }
            WriteLine(@"						</xs:sequence>");
            WriteLine(@"					</xs:complexType>");
            WriteLine(@"				</xs:element>");
            WriteLine(@"			</xs:choice>");
            WriteLine(@"		</xs:complexType>");

            if (Table.PrimaryKeyColumns().Count > 0)
            {
                WriteLine(@"		<xs:unique name=""" + Table.Code + @"PK"" msdata:PrimaryKey=""true"">");
                WriteLine(@"			<xs:selector xpath="".//mstns:" + Table.Code + @""" />");
                foreach (ColumnSchema column in Table.PrimaryKeyColumns())
                {
                    WriteLine(@"			<xs:field xpath=""mstns:" + column.Code + @""" />");
                }
                WriteLine(@"		</xs:unique>");
            }
            WriteLine(@"	</xs:element>");
            WriteLine(@"</xs:schema>");
           
        }

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/ms190942.aspx
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetXMLMappings(ColumnSchema column)
        {
            switch (column.SqlType)
            {
                case SqlType.AnsiText:
                case SqlType.AnsiChar:
                case SqlType.AnsiVarChar :
                case SqlType.AnsiVarCharMax:
                case SqlType.VarChar:
                case SqlType.VarCharMax:
                    return "xs:string";
                case SqlType.Binary:
                    return "xs:base64Binary";
                case SqlType.Boolean:
                    return "xs:boolean";
                case SqlType.Byte:
                    return "xs:base64Binary";
                case SqlType.Char:
                    return "xs:string";
                case SqlType.DateTime:
                case SqlType.SmallDateTime:
                    return "xs:dateTime";
                case SqlType.Decimal:
                    return "xs:decimal";
                case SqlType.Double:
                case SqlType.Float:
                    return "xs:double";
                case SqlType.GUID:
                    return "xs:string";
                case SqlType.Image:
                    return "xs:base64Binary";
                case SqlType.Int16:
                case SqlType.Int32:
                case SqlType.UInt16:
                case SqlType.UInt32:
                    return "xs:int";
                case SqlType.Int64 :
                case SqlType.UInt64:
                    return "xs:long";
                case SqlType.Money:
                case SqlType.SmallMoney:
                    return "xs:decimal";
                case SqlType.SByte:
                    return "xs:base64Binary";
                case SqlType.Text:
                    return "xs:string";
                case SqlType.TimeStamp:
                    return "xs:base64Binary ";
                case SqlType.VarBinary:
                case SqlType.VarBinaryMax:
                    return "xs:base64Binary";
                default:
                    return "xs:string";
            }
        }

        public bool ColumnIsString(ColumnSchema column)
        {
            switch (column.SqlType)
            {
                case SqlType.AnsiText:
                case SqlType.AnsiChar:
                case SqlType.AnsiVarChar:
                case SqlType.AnsiVarCharMax:
                case SqlType.VarChar:
                case SqlType.VarCharMax:
                case SqlType.Char:
                case SqlType.GUID:
                case SqlType.Text:
                    return true;
                default:
                    return false ;
            }
        }

    }
}
