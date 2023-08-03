/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace SmartCode.Studio.AssemblyCache
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Text;
    using SmartCode.Studio.Templates;

    internal class CacheAssemblies
    {
        internal CacheAssemblies()
        {
        }

        internal bool GetGACCache(ArrayList al)
        {
            if (this.GetGACCache(al, CacheFlags.CACHE_GAC))
            {
                return this.GetGACCache(al, CacheFlags.CACHE_ZAP);
            }
            return false;
        }

        private bool GetGACCache(ArrayList al, CacheFlags flags)
        {
            IAssemblyEnum assemblyEnum;
            bool addAssembly = true;
            if (Controllers.GetEnumerator(out assemblyEnum, IntPtr.Zero, IntPtr.Zero, flags, IntPtr.Zero) != 0)
            {
                return false;
            }
            IAssemblyName assemblyName = null;
            int i = 0;
            StringBuilder sbAssemblyName = new StringBuilder(0x100);
            StringBuilder sbAssemblyQualifiedName = new StringBuilder(0x100);
            while (true)
            {
                int iNext = assemblyEnum.GetNextAssembly(IntPtr.Zero, out assemblyName, 0);
                if (iNext == 0)
                {
                    addAssembly = true;
                    uint capacity = (uint) sbAssemblyName.Capacity;
                    assemblyName.GetName(ref capacity, sbAssemblyName);

                    uint capacity2 = (uint) sbAssemblyQualifiedName.Capacity;
                    assemblyName.GetDisplayName(sbAssemblyQualifiedName, ref capacity2, NameDisplayFlags.PUBLIC_KEY | NameDisplayFlags.VERSION | NameDisplayFlags.KEY_TOKEN | NameDisplayFlags.CULTURE);
                   
                    LibraryInfo library = new LibraryInfo(sbAssemblyName.ToString(), sbAssemblyQualifiedName.ToString());
                    for (int j = 0; j < al.Count; j++)
                    {
                        if (((LibraryInfo)al[j]).AssemblyName == library.AssemblyName)
                        {
                            addAssembly = false;
                            break;
                        }
                    }
                    if (addAssembly)
                    {
                        al.Add(library);
                        i++;
                    }
                    Marshal.ReleaseComObject(assemblyName);
                    assemblyName = null;
                }
                if (iNext != 0)
                {
                    return true;
                }
            }
        }

    }
}

