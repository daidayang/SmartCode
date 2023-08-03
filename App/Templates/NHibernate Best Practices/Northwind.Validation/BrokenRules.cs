using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Northwind.Validation
{

    [Serializable()]
    public class BrokenRules : BindingList<BrokenRule>
    {
        /// <summary>
        /// Dummy Contructor neccessary for serialization
        /// </summary>
        public BrokenRules()
        {
        }

        /// <summary>
        /// Add a broken rule to the list
        /// </summary>
        new public void Add(BrokenRule rule)
        {
            Remove(rule);
            Add(rule);
        }

        /// <summary>
        /// Removes a broken rule from the list
        /// </summary>
        new public void Remove(BrokenRule rule)
        {

            for (int index = Count - 1; index >= 0; index--)
            {
                if (this[index].RuleName == rule.RuleName)
                {
                    RemoveAt(index);
                    break;
                }
            }

        }
    }
}
