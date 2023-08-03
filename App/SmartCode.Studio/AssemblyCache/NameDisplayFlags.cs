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

    [Flags]
    internal enum NameDisplayFlags : uint
    {
        VERSION = 1,
        CULTURE = 2,
        KEY_TOKEN = 4,
        PUBLIC_KEY = 8,
    }
}

