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

namespace SmartCode.Studio.AssemblyCache
{
    [Flags]
    internal enum CacheFlags : uint
    {
        CACHE_ZAP = 1,
        CACHE_GAC = 2,
    }
}

