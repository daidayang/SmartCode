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
using System.IO;
using SmartCode.Studio.Utils;
using System.Diagnostics;

namespace SmartCode.Studio.Engine
{
    internal class FileOutput : ICodeOutput
    {
        string selectedPath;
        internal FileOutput(string selectedPath)
        {
            this.selectedPath = selectedPath;
        }
        #region ICodeOutput Members

        public void WriteToOutput(OutputInfo ouputInfo, SmartCode.Studio.Templates.TemplateInfo template)
        {
            string rootPath = this.selectedPath + @"\" + template.OutputFolder;
            if (template.CreateOutputFile)
            {
                new FileInfo(rootPath + @"\" + ouputInfo.FileName);
                Common.CreateAndWriteToFile(rootPath, ouputInfo.FileName, ouputInfo.Code);
            }
        }


        public void ShowOutput()
        {
            Process.Start("Explorer", selectedPath);
        }

        #endregion
    }
}
