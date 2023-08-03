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

namespace SmartCode.Studio.Controls
{
    internal enum TreeNodeType
    {
        None,
        Column,
        Domain,
        Table,
        View,
        Reference,
        ReferenceJoin,
        Control,
    }
}
