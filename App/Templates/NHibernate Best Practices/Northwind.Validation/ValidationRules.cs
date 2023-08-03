using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Core.Domain;

namespace Northwind.Validation
{

    /// <summary>
    /// Maintains the list of validation rules associated with an object
    /// </summary>
    [Serializable()]
    public abstract class ValidationRules
    {
        /// <summary>
        /// List of all defined rules.
        /// </summary>
        private BrokenRules m_allBrokenRules = new BrokenRules();

        /// <summary>
        /// List of all broken rules.
        /// </summary>
        private BrokenRules m_brokenRules = new BrokenRules();

        /// <summary>
        /// Dummy Contructor neccessary for serialization
        /// </summary>
        internal ValidationRules()
        {
        }

        /// <summary>
        /// Load All Broken Rule List.
        /// </summary>
        internal abstract void LoadRules();

        /// <summary>
        /// Adds a rule to the list of validated rules.
        /// </summary>
        /// <param name="rule"></param>
        protected void AddRule(BrokenRule rule)
        {
            this.m_allBrokenRules.Add(rule);
        }

        /// <summary>
        /// Validates a list of rules.
        /// </summary>
        /// <remarks>
        /// This method calls the Invoke method on each rule in the list, if the rule fails, it is addedd to _brokenRules list
        /// </remarks>
        private void ValidateRuleList()
        {
            foreach (BrokenRule rule in this.m_allBrokenRules)
            {
                if (rule.Invoke())
                    this.m_brokenRules.Remove(rule);
                else
                    this.m_brokenRules.Add(rule);
            }
        }

        #region Validation Status

        /// <summary>
        /// Returns a value indicating whether the doamin object is valid.
        /// </summary>
        /// <remarks>If one or more rules are broken, the object is assumed to be invalid and
        /// false is return.  Otherwise, True is returned.
        /// </remarks>
        /// <returns>A value indicating whether any rules are broken.</returns>
        internal bool IsValid
        {
            get
            {               

                //Load all rules.
                LoadRules();
                //Validate each rule, calling the Invoke method
                ValidateRuleList();

                return this.m_brokenRules.Count == 0;
            }
        }

        /// <summary>
        /// Return a list that contains all of the invalid rules.
        /// </summary>
        public BrokenRules GetBrokenRules()
        {
            return this.m_brokenRules;
        }

        #endregion
    }
}
