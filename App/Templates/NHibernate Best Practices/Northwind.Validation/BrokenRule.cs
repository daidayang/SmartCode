using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Validation
{
    [Serializable]
    public abstract class BrokenRule
    {
        private string m_ruleName;
        private string m_description;

        /// <summary>
        /// Dummy Contructor neccessary for serialization
        /// </summary>
        internal BrokenRule()
            :this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BrokenRule(string ruleName, string description)
        {
            this.m_ruleName = ruleName;
            this.m_description = description;
        }

        /// <summary>
        /// Invoke the rule has been followed.
        /// </summary>
        public abstract bool Invoke();

        /// <summary>
        /// For use in hashing algorithms 
        /// </summary>
        /// <returns>A hash code for the current rule.</returns>
        public override int GetHashCode()
        {
            return this.m_ruleName.GetHashCode();
        }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        public string RuleName
        {
            get { return m_ruleName; }
        }

        /// <summary>
        /// Gets descriptive text about this broken rule.
        /// </summary>
        public string Description
        {
            get { return m_description; }
        }
    }
}
