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
	/// This style lets you choose one item from a short list by clicking in the circular graphic.
	/// </summary>
	[Serializable()]
	public class OptionButton : ControlBase
	{
		private int columnsAcross;
		private bool leftText;

        internal OptionButton()
            : base("OptionButton")
		{
		}

        public OptionButton(string name)
            : base(name, true)
        {
        }

		public OptionButton(SerializationInfo Info, StreamingContext ctxt) :base(Info,ctxt)
		{
			columnsAcross	= (int)Info.GetValue("columnsAcross", typeof(int));
			leftText	= (bool)Info.GetValue("leftText", typeof(bool));
		}

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);		

			Info.AddValue ("columnsAcross",columnsAcross );
            Info.AddValue("leftText", leftText);
		}

		public bool LeftText 
		{
			get 
			{
				return this.leftText;
			}
			set 
			{
				this.leftText = value;
			}
		}

		public int ColumnsAcross 
		{
			get 
			{
				return this.columnsAcross;
			}
			set 
			{
				this.columnsAcross = value;
			}
		}
	}
}
