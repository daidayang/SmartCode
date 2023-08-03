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

namespace SmartCode.Model.Utils
{
    internal class SharedUtils
    {
        private static readonly string[] keyWordArray = new string[]{
		    "abstract", "event", "new", "struct","as", "explicit", "null", "switch","base", "extern", "object", "this",
		    "bool", "false", "operator", "throw","break", "finally", "out", "true","byte", "fixed", "override", "try",
		    "case", "float", "params", "typeof","catch", "for", "private", "uint","char", "foreach", "protected", "ulong",
		    "checked", "goto", "public", "unchecked","class", "if", "readonly", "unsafe","const", "implicit", "ref", "ushort",
		    "continue", "in", "return", "using","decimal", "int", "sbyte", "virtual","default", "interface", "sealed", "volatile",
		    "delegate", "internal", "short", "void","do", "is", "sizeof", "while","double", "lock", "stackalloc",
		    "else", "long", "static", "enum", "namespace", "string"
	    };
    }
}
