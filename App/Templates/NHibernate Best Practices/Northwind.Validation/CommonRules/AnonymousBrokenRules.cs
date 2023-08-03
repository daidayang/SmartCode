using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Validation.CommonRules
{
    /// <summary>
    /// A simple type of domain object rule that uses a delegate for validation. 
    /// </summary>
    /// <returns>True if the rule has been followed, or false if it has been broken.</returns>
    /// <remarks>
    /// Usage:
    /// <code>
    ///     this.AddRule(new AnonymousBrokenRules("Name", "The customer name must be at least 5 letters long.", delegate { return this.Name &gt; 5; } ));
    /// </code>
    /// </remarks>
    [Serializable]
    public delegate bool AnonymousRulesDelegate();

    [Serializable]
    public class AnonymousBrokenRules : BrokenRule
    {
        private AnonymousRulesDelegate _ruleDelegate;

        private AnonymousBrokenRules()
            : base()
        {
        }

        public AnonymousBrokenRules(string ruleName, AnonymousRulesDelegate rulesDelegate)
            : this(ruleName, "", rulesDelegate)
        {
        }

        public AnonymousBrokenRules(string ruleName, string description)
            : base(ruleName, description)
        {
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public AnonymousBrokenRules(string ruleName, string description, AnonymousRulesDelegate rulesDelegate)
            :
            base(ruleName, description)
        {
            this.RuleDelegate = rulesDelegate;
        }

        /// <summary>
        /// Gets the delegate used to validate this rule.
        /// </summary>
        protected virtual AnonymousRulesDelegate RuleDelegate
        {
            get { return _ruleDelegate; }
            set { _ruleDelegate = value;}
        }

        /// <summary>
        /// Validates that the rule has not been broken.
        /// </summary>
        /// <returns>True if the rule has not been broken, or false if it has.</returns>
        public override bool Invoke()
        {
            return RuleDelegate();
        }
    }
}
