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

namespace SmartCode.Model.Mapping.CS
{
    /// <summary>
    /// Map Object Model to IndexSchema
    /// </summary>
    public class ViewColumnSchema : DataObjectBase
    {
        ViewSchema m_View;

        public ViewSchema View
        {
            get { return m_View; }
            set { m_View = value; }
        }
    }
}
