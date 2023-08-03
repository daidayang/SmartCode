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
using SmartCode.Studio.Properties;
using System.Globalization;

namespace SmartCode.Studio.Controls
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class DisplayTextAttribute : Attribute
    {

        private string categoryID;
        private string descriptionID;
        private string nameID;

        public DisplayTextAttribute(string nameID, string descriptionID, string categoryID)
        {
            this.nameID = nameID;
            this.descriptionID = descriptionID;
            this.categoryID = categoryID;
        }


        public string Category
        {
            get
            {
                try
                {
                    return Resources.ResourceManager.GetString(this.categoryID, CultureInfo.CurrentUICulture);
                }
                catch
                {
                    return this.categoryID;
                }
            }
        }

        public string Description
        {
            get
            {
                try
                {
                    return Resources.ResourceManager.GetString(this.descriptionID, CultureInfo.CurrentUICulture);
                }
                catch
                {
                    return this.descriptionID;
                }
            }
        }

        public string Name
        {
            get
            {
                try
                {
                    return Resources.ResourceManager.GetString(this.nameID, CultureInfo.CurrentUICulture);
                }
                catch
                {
                    return this.nameID;
                }
            }
        }

    }
}
