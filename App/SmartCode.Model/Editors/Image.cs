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
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SmartCode.Model.Profile
{
	/// <summary>
	/// This style lets you display an image if the data in the associated column is an image instead of text
	/// </summary>
	[Serializable()]
	public class Image:ControlBase
	{
		private bool border;
        internal Image()
            : base("Image")
		{
		}

        public Image(string name)
            : base(name, true)
        {
        }

		public Image(SerializationInfo Info, StreamingContext ctxt) :base(Info,ctxt)
		{
			border	= (bool)Info.GetValue("border", typeof(bool));
		}

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

			Info.AddValue ("border",border );			
		}

		public bool Border 
		{
			get 
			{
				return this.border;
			}
			set 
			{
				this.border = value;
			}
		}
	}
}
