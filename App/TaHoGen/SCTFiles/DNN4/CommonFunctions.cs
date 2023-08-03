

string ObjectQualifier = "";
private string strippedTablePrefixes = "";

public string GetRawStoredProcName(string moduleName, string keyword)
{
	return moduleName + keyword;
}

public string GetRawStoredProcName(string moduleName, string keyword, string columnName)
{
	return moduleName + keyword + "By" + columnName;
}

public string GetStoredProcName(string moduleName, string keyword)
{
	return GetTableOwner() + GetObjectQualifier() + moduleName + keyword;
}

public string GetMethodName(string methodName, string keyword)
{
	return	keyword + methodName;
}

public string GetMethodName(string moduleName, string keyword, bool makePlural)
{
	string methodName = "";
	
	if(makePlural)
		methodName = GetMethodName(moduleName, keyword) + "s";
	else
		methodName = GetMethodName(moduleName, keyword);
		
	return methodName;
}

public string GetMethodName(string methodName, string keyword, string fieldName)
{
	return keyword + methodName + "By" + fieldName;
}

public string GetTableOwner()
{
	return "{databaseOwner}";
}

public string GetObjectQualifier()
{
	return "{objectQualifier}" + ObjectQualifier;
}

public string GetTableName(TableSchema table)
{
	string tableName = table.Name;
	
	if( ObjectQualifier.Length > 0 )
	{
		return tableName.Replace(ObjectQualifier, "");
	}
	else
	{
	
		string[] strips = StrippedTablePrefixes.Split(new char[] {',', ';'});		
		foreach(string strip in strips)
		{
			
			if (tableName.StartsWith(strip))
			{
				tableName = tableName.Remove(0, strip.Length);
				continue;
			}
		}
		
		return tableName;
	}
}

public string GetPrimaryKeyParameters(TableSchema table, bool includeTypes, string vbOrCsharp)
{
	string parameters = "";
	
	if (table.PrimaryKey != null)
	{
		for( int i = 0; i < table.PrimaryKey.MemberColumns.Count; i++ )
		{
			if(parameters.Length == 0)
			{
				if( includeTypes && vbOrCsharp == "vb" )
					parameters = parameters + "ByVal ";

				parameters = parameters + GetCamelCaseName(table.PrimaryKey.MemberColumns[i].Name);
					
				if( includeTypes )
					parameters = parameters + (vbOrCsharp == "vb" ? " As " + GetVBVariableType(table.PrimaryKey.MemberColumns[i]) : GetCSharpVariableType(table.PrimaryKey.MemberColumns[i]) + " ");				
			}
			else
			{
				parameters = parameters + ", ";
				
				if( includeTypes && vbOrCsharp == "vb" )
					parameters = parameters + "ByVal ";					

				parameters = parameters + GetCamelCaseName(table.PrimaryKey.MemberColumns[i].Name);
				
				if( includeTypes )
					parameters = parameters + (vbOrCsharp == "vb" ? " As " + GetVBVariableType(table.PrimaryKey.MemberColumns[i]) : GetCSharpVariableType(table.PrimaryKey.MemberColumns[i]) + " ");				
			}
		}
	}
	else
	{
		throw new ApplicationException("This template will only work on tables with a primary key.");
	}
	return parameters;
}

public string GetModuleIdParameter(TableSchema table, bool includeType, bool prependComma, string vbOrCSharp)
{	
	
	return "";
}

/*
public string GetFKMemberColumns(TableKeySchema fk)
{
	string s="";
	for( int j = 0; j < fk.ForeignKeyMemberColumns.Count; j++)
	{
		s += GetSqlParameterStatement(fk.ForeignKeyMemberColumns[j]);
		if (j < fk.ForeignKeyMemberColumns.Count - 1) s += ", \n\t";
	}
	return s;
}
*/

public string GetFKMemberColumnsWhere(TableKeySchema fk)
{
	string s="";
	for( int j = 0; j < fk.ForeignKeyMemberColumns.Count; j++)
	{
		s += "["+fk.ForeignKeyMemberColumns[j].Name + "]=@" + fk.ForeignKeyMemberColumns[j].Name ;
		if (j < fk.ForeignKeyMemberColumns.Count - 1) s += " AND ";
	}
	return s;
}

public string GetMemberVariableDeclarationStatement(ColumnSchema column)
{
	return GetMemberVariableDeclarationStatement("protected", column);
}

public string GetMemberVariableDeclarationStatement(string protectionLevel, ColumnSchema column)
{
	string statement = "Private " + GetMemberVariableName(column) + " as " + GetVBVariableType(column);
	
	string defaultValue = GetMemberVariableDefaultValue(column);
	if (defaultValue != "")
	{
		statement += " = " + defaultValue;
	}
	
	return statement;
}

public string GetReaderAssignmentStatement(ColumnSchema column, int index)
{
	string statement = "if (!reader.IsDBNull(" + index.ToString() + ")) ";
	statement += GetMemberVariableName(column) + " = ";
	
	if (column.Name.EndsWith("TypeCode")) statement += "(" + column.Name + ")";
	
	statement += "reader." + GetReaderMethod(column) + "(" + index.ToString() + ");";
	
	return statement;
}

public string GetCamelCaseName(string value)
{
	return value.Substring(0, 1).ToLower() + value.Substring(1);
}

public string GetMemberVariableName(ColumnSchema column)
{
	string propertyName = GetPropertyName(column);
	string memberVariableName = "_" + GetCamelCaseName(propertyName);
	
	return memberVariableName;
}

public string GetPropertyName(ColumnSchema column)
{
	string propertyName = column.Name;
	
	//if (propertyName == column.Table.Name + "Name") return "Name";
	//if (propertyName == column.Table.Name + "Description") return "Description";
	
	if (propertyName.EndsWith("TypeCode")) propertyName = propertyName.Substring(0, propertyName.Length - 4);
	
	return propertyName;
}
		
public string GetMemberVariableDefaultValue(ColumnSchema column)
{
	switch (column.DataType)
	{
		case DbType.Guid:
		{
			return "Guid.Empty";
		}
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "string.Empty";
		}
		default:
		{
			return "";
		}
	}
}

public string GetCSharpVariableType(ColumnSchema column)
{
	if (column.Name.EndsWith("TypeCode")) return column.Name;
	
	switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte()";
		case DbType.Boolean: return "bool";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "int";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		default:
		{
			return "__UNKNOWN__" + column.NativeType;
		}
	}
}

public string GetVBVariableType(ColumnSchema column)
{
	if (column.Name.EndsWith("TypeCode")) return column.Name;
	
	switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte()";
		case DbType.Boolean: return "Boolean";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "Integer";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "String";
		case DbType.StringFixedLength: return "String";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		default:
		{
			return "__UNKNOWN__" + column.NativeType;
		}
	}
}

public string GetReaderMethod(ColumnSchema column)
{
	switch (column.DataType)
	{
		case DbType.Byte:
		{
			return "GetByte";
		}
		case DbType.Int16:
		{
			return "GetInt16";
		}
		case DbType.Int32:
		{
			return "GetInt32";
		}
		case DbType.Int64:
		{
			return "GetInt64";
		}
		case DbType.AnsiStringFixedLength:
		case DbType.AnsiString:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "GetString";
		}
		case DbType.Boolean:
		{
			return "GetBoolean";
		}
		case DbType.Guid:
		{
			return "GetGuid";
		}
		case DbType.Currency:
		case DbType.Decimal:
		{
			return "GetDecimal";
		}
		case DbType.DateTime:
		case DbType.Date:
		{
			return "GetDateTime";
		}
		default:
		{
			return "__SQL__" + column.DataType;
		}
	}
}

public string GetSqlDbType(ColumnSchema column)
{
	switch (column.NativeType)
	{
		case "bigint": return "BigInt";
		case "binary": return "Binary";
		case "bit": return "Bit";
		case "char": return "Char";
		case "datetime": return "DateTime";
		case "decimal": return "Decimal";
		case "float": return "Float";
		case "image": return "Image";
		case "int": return "Int";
		case "money": return "Money";
		case "nchar": return "NChar";
		case "ntext": return "NText";
		case "numeric": return "Decimal";
		case "nvarchar": return "NVarChar";
		case "real": return "Real";
		case "smalldatetime": return "SmallDateTime";
		case "smallint": return "SmallInt";
		case "smallmoney": return "SmallMoney";
		case "sql_variant": return "Variant";
		case "sysname": return "NChar";
		case "text": return "Text";
		case "timestamp": return "Timestamp";
		case "tinyint": return "TinyInt";
		case "uniqueidentifier": return "UniqueIdentifier";
		case "varbinary": return "VarBinary";
		case "varchar": return "VarChar";
		default: return "__UNKNOWN__" + column.NativeType;
	}
}

public string GetPrimaryKeyType(TableSchema table)
{
	if (table.PrimaryKey != null)
	{
		if (table.PrimaryKey.MemberColumns.Count == 1)
		{
			return GetCSharpVariableType(table.PrimaryKey.MemberColumns[0]);
		}
		else
		{
			throw new ApplicationException("This template will not work on primary keys with more than one member column.");
		}
	}
	else
	{
		throw new ApplicationException("This template will only work on tables with a primary key.");
	}
}

public string GetPrimaryKeyName(TableSchema table)
{
	if (table.PrimaryKey != null)
	{
		if (table.PrimaryKey.MemberColumns.Count == 1)
		{
			return table.PrimaryKey.MemberColumns[0].Name;
		}
		else
		{
			throw new ApplicationException("This template will not work on primary keys with more than one member column.");
		}		
	}
	else
	{
		throw new ApplicationException("This template will only work on tables with a primary key.");
	}
}

public string GetSelectByColumnNameParameter(ColumnSchema column, bool includeTypes, bool wrapDbNullColumnsWithNullFunction, string vbOrCSharp)
{
	string parameter = "";
	bool allowDbNull = column.AllowDBNull;
	
	if( includeTypes && vbOrCSharp == "vb" )
		parameter = parameter + "ByVal ";
	
	if(allowDbNull && wrapDbNullColumnsWithNullFunction)
		parameter = parameter + string.Format("GetNull({0})", GetCamelCaseName(column.Name));
	else
		parameter = parameter + GetCamelCaseName(column.Name);
	
	if( includeTypes && vbOrCSharp == "vb" )
		parameter = parameter + " As " + GetVBVariableType(column) + " ";
	else if(includeTypes)
		parameter = parameter + GetCSharpVariableType(column) + " ";
		
	return parameter;
}

public string GetNonPrimaryKeyParameters(TableSchema table, bool includeTypes, bool wrapDbNullColumnsWithNullFunction, string vbOrCSharp)
{
    string parameters = "";
	if (table.PrimaryKey != null)
	{
		for( int i = 0; i < table.NonPrimaryKeyColumns.Count; i++ )
		{
			ColumnSchema column = table.NonPrimaryKeyColumns[i];
			bool allowDbNull = column.AllowDBNull;
			
			if(parameters.Length == 0)
			{
				if( includeTypes && vbOrCSharp == "vb" )
					parameters = parameters + "ByVal ";
				
				if(allowDbNull && wrapDbNullColumnsWithNullFunction)
					parameters = parameters + string.Format("GetNull({0})", GetCamelCaseName(table.NonPrimaryKeyColumns[i].Name));
				else
					parameters = parameters + GetCamelCaseName(table.NonPrimaryKeyColumns[i].Name);
				
				if( includeTypes && vbOrCSharp == "vb" )
					parameters = parameters + " As " + GetVBVariableType(table.NonPrimaryKeyColumns[i]) + " ";
				else if(includeTypes)
					parameters = parameters + GetCSharpVariableType(table.NonPrimaryKeyColumns[i]) + " ";
			}
			else
			{
				parameters = parameters + ", ";
				
				if( includeTypes && vbOrCSharp == "vb" )
					parameters = parameters + "ByVal ";
					
				
				if(allowDbNull && wrapDbNullColumnsWithNullFunction)
					parameters = parameters + string.Format("GetNull({0})", GetCamelCaseName(table.NonPrimaryKeyColumns[i].Name));
				else
					parameters = parameters + GetCamelCaseName(table.NonPrimaryKeyColumns[i].Name);
				
				if( includeTypes && vbOrCSharp == "vb" )
					parameters = parameters + " As " + GetVBVariableType(table.NonPrimaryKeyColumns[i]) + " ";
				else if(includeTypes)
					parameters = parameters + GetCSharpVariableType(table.NonPrimaryKeyColumns[i]) + " ";					
			}
		}
	}
	else
	{
		throw new ApplicationException("This template will only work on tables with a primary key.");
	}
	return parameters;
}

public string GetClassName(TableSchema table)
{
	if( ObjectQualifier.Length > 0 )
	{
		return (table.Name + "Controller").Replace(ObjectQualifier, "");
	}
	else
	{
		return (table.Name + "Controller");
	}
}

public string GetClassNameInfo(TableSchema table)
{
	if( ObjectQualifier.Length > 0 )
	{
		return (table.Name + "Info").Replace(ObjectQualifier, "");
	}
	else
	{
		return (table.Name + "Info");
	}
}

public string GetPrimaryKeyParametersForObject(TableSchema table, string obj)
{
    string parameters = "";
	if (table.PrimaryKey != null)
	{
		for( int i = 0; i < table.PrimaryKey.MemberColumns.Count; i++ )
		{
			if(parameters.Length == 0)
			{
				parameters = parameters + obj + "." + table.PrimaryKey.MemberColumns[i].Name;
			}
			else
			{
				parameters = parameters + ", ";
				parameters = parameters + obj + "." + table.PrimaryKey.MemberColumns[i].Name;
			}
		}
	}
	else
	{
		throw new ApplicationException("This template will only work on tables with a primary key.");
	}
	return parameters;
}

public string GetNonPrimaryKeyParametersForObject(TableSchema table, string obj)
{
    string parameters = "";

	for( int i = 0; i < table.NonPrimaryKeyColumns.Count; i++ )
	{
		if(parameters.Length == 0)
		{
			parameters = parameters + obj + "." + table.NonPrimaryKeyColumns[i].Name;
		}
		else
		{
			parameters = parameters + ", ";
			parameters = parameters + obj + "." + table.NonPrimaryKeyColumns[i].Name;
		}
	}
	
	return parameters;
}


[Category("DataSource")]
[Description("The table prefixes to strip from the classes name, delimited by comma.")]
public string StrippedTablePrefixes
{
	get {return this.strippedTablePrefixes;}
	set	{this.strippedTablePrefixes = value;}
}