/*
 * Copyright � 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
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

namespace SmartCode.Model
{
    /// <summary>
    /// Enumerates supported SQL column types.
    /// Storage size is specified for Microsoft SQL Server 2000\MSDE, it
    /// can differ on other database servers.
    /// </summary>
    public enum SqlType
    {
        /// <summary>
        /// An unknown type.
        /// </summary>
        Unknown,
        /// <summary>
        /// Boolean (bit).
        /// </summary>
        Boolean,
        /// <summary>
        /// Signed Byte (8 bit integer).
        /// </summary>
        SByte,
        /// <summary>
        /// Unsigned byte.
        /// </summary>
        Byte,
        /// <summary>
        /// Small integer (16 bit integer).
        /// </summary>
        Int16,
        /// <summary>
        /// Unsigned small integer (word).
        /// </summary>
        UInt16,
        /// <summary>
        /// Integer (32 bit integer).
        /// </summary>
        Int32,
        /// <summary>
        /// Unsigned integer.
        /// </summary>
        UInt32,
        /// <summary>
        /// Long integer (64 bit integer).
        /// </summary>
        Int64,
        /// <summary>
        /// Unsigned long integer.
        /// </summary>
        UInt64,
        /// <summary>
        /// Numeric data type with fixed precision and scale.
        /// </summary>
        Decimal,

        // Real

        /// <summary>
        /// Floating point number data from �3.40E + 38 through 3.40E + 38. 
        /// Storage size is 4 bytes.
        /// </summary>
        Float,
        /// <summary>
        /// Floating point number data from - 1.79E + 308 through 1.79E + 308.
        /// Storage size is 8 bytes.
        /// </summary>
        Double,

        // Money

        /// <summary>
        /// Monetary data values from - 214,748.3648 through +214,748.3647, 
        /// with accuracy to a ten-thousandth of a monetary unit. 
        /// Storage size is 4 bytes. 
        /// </summary>
        SmallMoney,
        /// <summary>
        /// Monetary data values from -2^63 (-922,337,203,685,477.5808) through
        /// 2^63 - 1 (+922,337,203,685,477.5807), with accuracy to a ten-thousandth of a monetary unit. 
        /// Storage size is 8 bytes.
        /// </summary>
        Money,

        // DateTime

        /// <summary>
        /// Date and time data from January 1, 1900, through June 6, 2079, 
        /// with accuracy to the minute. smalldatetime values with 29.998 seconds 
        /// or lower are rounded down to the nearest minute; values with 29.999 
        /// seconds or higher are rounded up to the nearest minute.
        /// Storage size is 4 bytes. 
        /// </summary>
        SmallDateTime,
        /// <summary>
        /// Date and time data from January 1, 1753 through December 31, 9999, 
        /// to an accuracy of one three-hundredth of a second (equivalent to 3.33 
        /// milliseconds or 0.00333 seconds). Values are rounded to increments 
        /// of .000, .003, or .007 seconds.
        /// Storage size is 8 bytes. 
        /// </summary>
        DateTime,

        Date,
        // String

        /// <summary>
        /// Fixed-length non-Unicode character data with length of n bytes. 
        /// n must be a value from 1 through 8,000. Storage size is n bytes. 
        /// The SQL-92 synonym for char is character.
        /// </summary>
        AnsiChar,
        /// <summary>
        /// Variable-length non-Unicode character data with length of n bytes. 
        /// n must be a value from 1 through 8,000. Storage size is the actual 
        /// length in bytes of the data entered, not n bytes. The data entered 
        /// can be 0 characters in length. The SQL-92 synonyms for varchar are 
        /// char varying or character varying.
        /// </summary>
        AnsiVarChar,
        /// <summary>
        /// Variable-length non-Unicode data in the code page of the server and 
        /// with a maximum length of 231-1 (2,147,483,647) characters.
        /// </summary>
        AnsiText,
        /// <summary>
        /// Variable-length non-Unicode data in the code page of the server and 
        /// with a maximum length of 231-1 (2,147,483,647) characters.
        /// </summary>
        AnsiVarCharMax,
        /// <summary>
        /// Fixed-length Unicode character data of n characters. 
        /// n must be a value from 1 through 4,000. Storage size is two times n bytes. 
        /// The SQL-92 synonyms for nchar are national char and national character.
        /// </summary>
        Char,
        /// <summary>
        /// Variable-length Unicode character data of n characters. 
        /// n must be a value from 1 through 4,000. Storage size, in bytes, is two times 
        /// the number of characters entered. The data entered can be 0 characters in length. 
        /// The SQL-92 synonyms for nvarchar are national char varying and national character varying.
        /// </summary>
        VarChar,
        /// <summary>
        /// Variable-length Unicode data with a maximum length of 230 - 1 (1,073,741,823) 
        /// characters. Storage size, in bytes, is two times the number of characters entered. 
        /// The SQL-92 synonym for ntext is national text.
        /// </summary>
        Text,
        /// <summary>
        /// Variable-length Unicode data with a maximum length of 230 - 1 (1,073,741,823) 
        /// characters. Storage size, in bytes, is two times the number of characters entered. 
        /// </summary>
        VarCharMax,

        // Binary

        /// <summary>
        /// Fixed-length binary data of n bytes. n must be a value from 1 through 8,000. 
        /// Storage size is n+4 bytes. 
        /// </summary>
        Binary,
        /// <summary>
        /// Variable-length binary data of n bytes. n must be a value from 1 through 8,000. 
        /// Storage size is the actual length of the data entered + 4 bytes, not n bytes. 
        /// The data entered can be 0 bytes in length. 
        /// The SQL-92 synonym for varbinary is binary varying.
        /// </summary>
        VarBinary,
        /// <summary>
        /// Variable-length binary data from 0 through 231-1 (2,147,483,647) bytes. 
        /// </summary>
        Image,
        /// <summary>
        /// Variable-length binary data from 0 through 231-1 (2,147,483,647) bytes. 
        /// </summary>
        VarBinaryMax,

        // Other

        /// <summary>
        /// A data type that stores values of various SQL Server-supported data types, 
        /// except text, ntext, image, timestamp, and sql_variant. 
        /// </summary>
        Variant,
        /// <summary>
        /// Data type that exposes automatically generated binary numbers, which are 
        /// guaranteed to be unique within a database. timestamp is used typically as 
        /// a mechanism for version-stamping table rows. The storage size is 8 bytes.
        /// </summary>
        TimeStamp,
        /// <summary>
        /// A globally unique identifier (GUID). 
        /// </summary>
        GUID
    }
}
