using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Validation.CommonRules
{
    internal class StringRequiredBrokenRule : AnonymousBrokenRules
    {
		string m_value;

        public StringRequiredBrokenRule(string ruleID, string value)
            : this(ruleID, "", value)
        {
        }

        public StringRequiredBrokenRule(string ruleID, string description, string value)
			: base(ruleID, description )
		{
			this.m_value = value;
			base.RuleDelegate += new AnonymousRulesDelegate( TrimDelegate);
		}

		internal bool TrimDelegate()
		{
			return m_value != null && m_value.Trim().Length != 0;
		}
    }
}
