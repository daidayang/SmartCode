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
namespace SmartCode.Template
{
    interface ITemplateBase
    {
        string Code { get; }
        bool CreateOutputFile { get; }
        string Description { get; }
        bool IsProjectTemplate { get; }
        string Name { get; }
        string OutputFileName();
        string OutputFolder { get; }
        void ProduceCode();
        System.Collections.ArrayList Run(SmartCode.Model.Domain domain, SmartCode.Model.NamedObject entity);
    }
}
