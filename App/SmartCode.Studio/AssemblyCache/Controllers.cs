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

    internal class Controllers
    {
        [DllImport("Fusion.dll", EntryPoint="CreateAssemblyEnum", CharSet=CharSet.Auto)]
        public static extern int GetEnumerator(out IAssemblyEnum ppAsmCache, IntPtr pAppCtx, IntPtr pName, CacheFlags dwFlags, IntPtr pvReserved);
    }
}

