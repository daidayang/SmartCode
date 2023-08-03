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

namespace SmartCode.Model.Mapping.CS
{
    /// <summary>
    /// Map Object Model to ViewSchema
    /// </summary>
    /// 
    public class ViewSchema : TabularObjectBase
    {
        IList<ViewColumnSchema> m_Columns;
        string m_FullName;
        string m_Owner;
        string m_ViewText;

        public ViewSchema()
        {
            m_Columns = new List<ViewColumnSchema>();
        }

        public IList<ViewColumnSchema> Columns
        {
            get { return m_Columns; }
            set { m_Columns = value; }
        }

        public override string FullName
        {
            get { return m_FullName; }
            set { m_FullName = value; }
        }

        public string Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        public string ViewText
        {
            get { return m_ViewText; }
            set { m_ViewText = value; }
        }

        public DateTime DateCreated
        {
            get
            {
                return DateTime.Now ;
            }
        }

    }
}
