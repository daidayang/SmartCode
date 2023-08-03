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
	/// This style lets you select or deselect a displayed choice by clicking in the box graphic.
	/// </summary>
	[Serializable()]
	public class CheckBox:ControlBase
	{
		private string text;
		private string dataValueForOn;
		private string dataValueForOff;
		private bool   leftText = true;


        internal CheckBox()
            : base("CheckBox")
		{
		}

        public CheckBox(string name)
            : base(name, true)
        {
        }

        public CheckBox(SerializationInfo Info, StreamingContext ctxt)
            : base(Info, ctxt)
        {
            text = (string)Info.GetValue("text", typeof(string));
            dataValueForOn = (string)Info.GetValue("dataValueForOn", typeof(string));
            dataValueForOff = (string)Info.GetValue("dataValueForOff", typeof(string));
            leftText = (bool)Info.GetValue("leftText", typeof(bool));

        }

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
        {
            base.GetObjectData(Info, ctxt);

			Info.AddValue ("text",text );
			Info.AddValue ("dataValueForOn",dataValueForOn );
			Info.AddValue ("dataValueForOff",dataValueForOff );
			Info.AddValue ("leftText",leftText ); 			
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
		public string DataValueForOn 
		{
			get 
			{
				return this.dataValueForOn;
			}
			set 
			{
				this.dataValueForOn = value;
			}
		}
		public string DataValueForOff 
		{
			get 
			{
				return this.dataValueForOff;
			}
			set 
			{
				this.dataValueForOff = value;
			}
		}


		public string Text 
		{
			get 
			{
				return this.text;
			}
			set 
			{
				this.text = value;
			}
		}
	}
}
