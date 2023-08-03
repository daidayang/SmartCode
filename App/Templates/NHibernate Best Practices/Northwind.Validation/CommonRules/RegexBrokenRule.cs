using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Northwind.Validation.CommonRules
{
    public class RegexBrokenRule : BrokenRule
    {
        object m_domainObject;
        string m_regExp;
        string m_propertyName;

        internal RegexBrokenRule(object domainObject, string propertyName, string regExp, string ruleName, string description)
            :base(ruleName, description)
        {
            m_domainObject = domainObject;
            m_regExp = regExp;
            m_propertyName = propertyName;
        }

        public override bool Invoke()
        {
            PropertyInfo pi = m_domainObject.GetType().GetProperty(m_propertyName);
            Match m = Regex.Match(pi.GetValue(m_domainObject, null).ToString(), m_regExp);

            return m.Success;
        }
    }
}
