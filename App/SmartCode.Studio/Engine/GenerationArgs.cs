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
using SmartCode.Studio.Templates;

namespace SmartCode.Studio.Engine
{
    internal class GenerationArgs: System.EventArgs 
    {
        private TemplateInfo template;
        private OutputInfo output;
        private String message;
        private bool success;
        private bool statusDone;


        internal GenerationArgs(TemplateInfo template, OutputInfo output, String message, bool success, bool statusDone)
        {
            this.template = template;
            this.output = output;
            this.message = message;
            this.success = success;
            this.statusDone = statusDone;

        }

        public TemplateInfo Template
        {
            get { return template; }
        }

        public OutputInfo Output
        {
            get { return output; }
        }

        public String Message
        {
            get { return message; }
        }

        public bool Success
        {
            get { return success; }
        }

        public bool StatusDone
        {
            get { return statusDone; }
        }
    }
}
