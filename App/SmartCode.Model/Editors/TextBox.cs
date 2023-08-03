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
	/// This style corresponds to the Text Box control that lets you enter text. 
	/// This is the default edit style that ERnet assigns when you create a new edit style.
	/// </summary>
	[Serializable()]
	public class TextBox : ControlBase
	{
        private int limit;
        private string password;
        private bool hideSelection;
        private bool border;
        private bool autoSelect;
        private bool multiline;
        private bool hScrollBar;
        private bool vScrollBar;
        private bool readOnly;
        private bool emptyIsNull;
        private bool isDateTime;
        private string mask;
        
        internal TextBox()
            : base("TextBox")
		{
		}

        public TextBox(string name)
            : base(name, true)
        {
        }

		public TextBox(SerializationInfo Info, StreamingContext ctxt):base(Info,ctxt)
		{
			limit	= (int)Info.GetValue("limit", typeof(int));
			password	= (string)Info.GetValue("password", typeof(string));
			hideSelection	= (bool)Info.GetValue("hideSelection", typeof(bool));
			border	= (bool)Info.GetValue("border", typeof(bool));
			autoSelect	= (bool)Info.GetValue("autoSelect", typeof(bool));
			multiline	= (bool)Info.GetValue("multiline", typeof(bool));
			hScrollBar	= (bool)Info.GetValue("hScrollBar", typeof(bool));
			vScrollBar	= (bool)Info.GetValue("vScrollBar", typeof(bool));
			readOnly	= (bool)Info.GetValue("readOnly", typeof(bool));
            emptyIsNull = (bool)Info.GetValue("emptyIsNull", typeof(bool));
            isDateTime = (bool)Info.GetValue("isDateTime", typeof(bool));
            mask = (string)Info.GetValue("mask", typeof(string));
			
		}

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
		{
            base.GetObjectData(Info, ctxt);

			Info.AddValue ("limit",limit );
			Info.AddValue ("password",password );
			Info.AddValue ("hideSelection",hideSelection );
			Info.AddValue ("border",border );
			Info.AddValue ("autoSelect",autoSelect );
			Info.AddValue ("multiline",multiline );
			Info.AddValue ("hScrollBar",hScrollBar );
			Info.AddValue ("vScrollBar",vScrollBar );
			Info.AddValue ("readOnly",readOnly );
            Info.AddValue("emptyIsNull", emptyIsNull);
            Info.AddValue("isDateTime", isDateTime);
            Info.AddValue("mask", mask);		
		}

		public int  Limit 
		{
			get 
			{
				return this.limit;
			}
			set 
			{
				this.limit = value;
			}
		}

		public string Password 
		{
			get 
			{
				return this.password;
			}
			set 
			{
				this.password = value;
			}
		}


		
		public bool  HideSelection 
		{
			get 
			{
				return this.hideSelection;
			}
			set 
			{
				this.hideSelection = value;
			}
		}
		
		public bool  Border 
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

				
		public bool  AutoSelect 
		{
			get 
			{
				return this.autoSelect;
			}
			set 
			{
				this.autoSelect = value;
			}
		}				
		public bool   Multiline 
		{
			get 
			{
				return this.multiline;
			}
			set 
			{
				this.multiline = value;
			}
		}


		public bool    HScrollBar 
		{
			get 
			{
				return this.hScrollBar;
			}
			set 
			{
				this.hScrollBar = value;
			}
		}

		public bool  VScrollBar 
		{
			get 
			{
				return this.vScrollBar;
			}
			set 
			{
				this.vScrollBar = value;
			}
		}

		
		public bool  ReadOnly 
		{
			get 
			{
				return this.readOnly;
			}
			set 
			{
				this.readOnly = value;
			}
		}
		public bool  EmptyIsNull 
		{
			get 
			{
				return this.emptyIsNull;
			}
			set 
			{
				this.emptyIsNull = value;
			}
		}


        public bool IsDateTime
        {
            get { return isDateTime; }
            set { isDateTime = value; }
        }

        public string Mask
        {
            get { return mask; }
            set { mask = value; }
        }

	}
}















