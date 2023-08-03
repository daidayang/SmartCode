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

	[Serializable()]
	public class Popup : ControlBase
	{
		private int windowsHeight;
		private int windowsWidth;
		private bool isModal;

        internal Popup()
            : base("Popup")
        {
        }

        public Popup(string name)
            : base(name, true)
        {
        }

		public Popup(SerializationInfo Info, StreamingContext ctxt):base(Info,ctxt)
		{
			windowsHeight	= (int)Info.GetValue("windowsHeight", typeof(int));
            windowsWidth = (int)Info.GetValue("windowsWidth", typeof(int));
			isModal			= (bool)Info.GetValue("isModal", typeof(bool));
		}

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

			Info.AddValue ("windowsHeight",windowsHeight );
            Info.AddValue("windowsWidth", windowsWidth);
			Info.AddValue ("isModal",isModal );
		}

		public bool IsModal 
		{
			get 
			{
				return this.isModal;
			}
			set 
			{
				this.isModal = value;
			}
		}

		public int  WindowsHeight 
		{
			get 
			{
				return this.windowsHeight;
			}
			set 
			{
				this.windowsHeight = value;
			}
		}

		public int  WindowsWidth 
		{
			get 
			{
				return this.windowsWidth;
			}
			set 
			{
				this.windowsWidth = value;
			}
		}
	}
}
