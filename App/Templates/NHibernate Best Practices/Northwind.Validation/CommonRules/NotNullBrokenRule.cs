using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Validation.CommonRules
{
    internal class NotNullBrokenRule 
        : BrokenRule
    {
        object m_domain;
        internal NotNullBrokenRule(object domain, string ruleName, string description)
            :base(ruleName, description)
        {
            m_domain = domain;
        }

        public override bool Invoke()
        {
            return m_domain != null;
        }
    }
}
