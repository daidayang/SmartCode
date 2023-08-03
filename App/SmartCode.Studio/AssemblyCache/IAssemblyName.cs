/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace SmartCode.Studio.AssemblyCache
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    [ComImport, Guid("CD193BC0-B4BC-11d2-9833-00C04FC31D2E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAssemblyName
    {
        void SetProperty_StubOnly_InvalidArgList_Do_Not_Call();
        void GetProperty_StubOnly_InvalidArgList_Do_Not_Call();
        void Finalize();
        void GetDisplayName(StringBuilder displayName, ref uint cch, NameDisplayFlags flags);
        void BindToObject_StubOnly_InvalidArgList_Do_Not_Call();
        void GetName(ref uint cch, StringBuilder name);
        void GetVersion(out uint versionHi, out uint versionLow);
        void IsEqual(IAssemblyName pName, uint cmpFlags);
        IAssemblyName Clone();
    }
}

