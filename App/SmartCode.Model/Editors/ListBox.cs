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
	/// This style lets you choose items from a predefined list (this style is often used to display a list of valid values).
	/// </summary>
	[Serializable()]
	public class ListBox:ControlBase
	{
		private int columns;
		private bool multipleSel;
		private bool sorted;

        internal ListBox()
            : base("ListBox")
		{
		}
        public ListBox(string name)
            : base(name, true)
        {
        }

		public ListBox(SerializationInfo Info, StreamingContext ctxt):base(Info,ctxt)
		{
			columns	= (int)Info.GetValue("columns", typeof(int));
			multipleSel	= (bool)Info.GetValue("multipleSel", typeof(bool));
			sorted	= (bool)Info.GetValue("sorted", typeof(bool));

		}

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

			Info.AddValue ("columns",columns );
			Info.AddValue ("multipleSel",multipleSel );
			Info.AddValue ("sorted",sorted );			
		}

		public int ColumnsNum 
		{
			get 
			{
				return this.columns;
			}
			set 
			{
				this.columns = value;
			}
		}
		public bool MultipleSel 
		{
			get 
			{
				return this.multipleSel;
			}
			set 
			{
				this.multipleSel = value;
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
