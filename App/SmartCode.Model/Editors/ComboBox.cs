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
	///This style lets you browse and choose items from a list that opens up when you click the down arrow to the right of the control. You can also type a new value in the control.
	/// </summary>
	[Serializable()]
	public class ComboBox : ControlBase
	{
        public enum StyleEnum { DropDown, Simple, DropDownList, }

		private StyleEnum comboStyle;
		private int       length;
		private bool	  sorted;

        internal ComboBox()
            : base("ComboBox")
		{
		}

        public ComboBox(string name)
            : base(name, true)
        {
        }

		public ComboBox(SerializationInfo Info, StreamingContext ctxt):base(Info,ctxt)
		{
			comboStyle	= (StyleEnum)Info.GetValue("comboStyle", typeof(StyleEnum));
			length	= (int)Info.GetValue("length", typeof(int));
			sorted	= (bool)Info.GetValue("sorted", typeof(bool));

		}

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
		{
            base.GetObjectData(Info, ctxt);

			Info.AddValue ("comboStyle",comboStyle );
			Info.AddValue ("length",length );
			Info.AddValue ("sorted",sorted ); 			
		}

		public StyleEnum  ComboStyle 
		{
			get 
			{
				return this.comboStyle;
			}
			set 
			{
				this.comboStyle = value;
			}
		}

		public int  Length 
		{
			get 
			{
				return this.length;
			}
			set 
			{
				this.length = value;
			}
		}

		public bool Sorted 
		{
			get 
			{
				return this.sorted;
			}
			set 
			{
				this.sorted = value;
			}
		}
	}
}
