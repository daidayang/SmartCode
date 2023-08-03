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
using Microsoft.Win32;

namespace SmartCode.Studio.Utils
{

    public sealed class Configuration
    {
        private const String SUB_KEY = "Software\\Kontac\\SmartCode\\1.1";

        static Configuration()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(SUB_KEY);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(SUB_KEY);
            }
        }
        private static int GetInt32(string keyName, int defaultValue)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(SUB_KEY);
            if (key != null)
            {
                object value = key.GetValue(keyName);
                key.Close();
                if (value != null)
                {
                    return (int)value;
                }
            }
            return defaultValue;
        }

        private static string GetString(string keyName, string defaultValue)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(SUB_KEY);
            if (key != null)
            {
                object value = key.GetValue(keyName);
                key.Close();
                if (value != null)
                {
                    return (string)value;
                }
            }
            return defaultValue;
        }

        public static string LastDirectorySelected
        {
            get
            {
                return Configuration.GetString("LastDirectorySelected", Environment.CurrentDirectory);
            }
            set
            {
                RegistryKey key = null;
                try
                {
                    key = Registry.CurrentUser.OpenSubKey(SUB_KEY, true);
                    if (key != null)
                    {
                        key.SetValue("LastDirectorySelected", value);
                    }
                }
                catch
                {
                    return;
                }
                finally
                {
                    if (key != null)
                    {
                        key.Close();
                    }
                }
            }
        }

        public static string GetRecentProjects(int i)
        {
            return Configuration.GetString("RecentProjects" + i, "");
        }

        public static void SetRecentProjects(int i, string fileName)
        {
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(SUB_KEY, true);
                if (key != null)
                {
                    key.SetValue("RecentProjects" + i, fileName);
                }
            }
            catch
            {
                return;
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
        }

     

    }
}
