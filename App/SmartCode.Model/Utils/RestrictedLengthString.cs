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
using System.Globalization;

namespace SmartCode.Model.Utils
{
    [Serializable]
    public class RestrictedLengthString
    {
        private const int MAX_LENGTH = 0xff;
        private string value;

        public RestrictedLengthString(string stringValue)
        {
            this.ValidateName(stringValue);
            this.value = stringValue;
        }

        public override bool Equals(object obj)
        {
            return this.value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public static implicit operator string(RestrictedLengthString r)
        {
            if (r == null)
            {
                throw new ArgumentNullException("r");
            }
            return r.Value;
        }

        public static implicit operator RestrictedLengthString(string s)
        {
            return new RestrictedLengthString(s);
        }

        public override string ToString()
        {
            return this.value;
        }

        private void ValidateName(string stringToCheck)
        {
            if (String.IsNullOrEmpty(stringToCheck) || stringToCheck.Length > MAX_LENGTH)
            {
                throw new ArgumentException("Invalid Parameter name", "Value");
            }
            for (int i = 0; i < stringToCheck.Length; i++)
            {
                char charToCheck = stringToCheck[i];
                if (charToCheck < ' ' || charToCheck > 0xfffd)
                {
                    throw new ArgumentOutOfRangeException("Value: " + charToCheck.ToString());
                }
            }
        }


        public static int MaxLength
        {
            get
            {
                return MAX_LENGTH;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.ValidateName(value);
                this.value = value;
            }
        }



    }
}
