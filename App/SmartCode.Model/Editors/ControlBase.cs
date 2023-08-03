/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */


namespace SmartCode.Model.Profile
{

	using System;
	using System.Collections;
	using System.Runtime.Serialization;
	using System.Runtime.Serialization.Formatters.Binary;
	using SmartCode.Model;
	using System.ComponentModel;

	[Serializable()]
    public class ControlBase : IdentifiedObject
	{

		public enum JustifyEnum {LEFT,RIGHT,CENTER,}

		private string name;
        private bool required;
        private bool isVisible;
        private JustifyEnum justify;

        private decimal height;
        private decimal width;
        private bool isCustomControl;

		internal ControlBase(string name):
            this(name, false)
		{
            
		}

        public ControlBase(string name, bool isCustomControl)
        {
            Name = name;
            this.isVisible = true;
            this.Justify = SmartCode.Model.Profile.ControlBase.JustifyEnum.LEFT;
            this.Height = 20;
            this.isCustomControl = isCustomControl;
        }

		public ControlBase(SerializationInfo Info, StreamingContext ctxt)
		{
			name		= (string)Info.GetValue("name", typeof(string));
			required	= (bool)Info.GetValue("required", typeof(bool));
			isVisible	= (bool)Info.GetValue("isVisible", typeof(bool));
			justify		= (JustifyEnum)Info.GetValue("justify", typeof(JustifyEnum));
			height		= (decimal)Info.GetValue("height", typeof(decimal));
			width		= (decimal)Info.GetValue("width", typeof(decimal));

            try
            {
                isCustomControl = (bool)Info.GetValue("isCustomControl", typeof(bool));
            }
            catch 
            {
                isCustomControl = false;
            }
            
		}

        public override void GetObjectData(SerializationInfo Info, StreamingContext ctxt)
		{
            base.GetObjectData(Info, ctxt);

			Info.AddValue ("name", Name);
			Info.AddValue ("required", Required);
			Info.AddValue ("isVisible", Visible);
			Info.AddValue ("justify", Justify);
			Info.AddValue ("height", Height);
            Info.AddValue("width", Width);
            Info.AddValue("isCustomControl", IsCustomControl);		
		}

 	
		public string Name 
		{
			get 
			{
				return this.name;
			}
			set 
			{
				this.name = value;
			}
		}


		[Category("Editor")]
		[Description(@"None")]
		[Browsable(true)]
		public bool Required 
		{
			get 
			{
				return this.required;
			}
			set 
			{
				this.required = value;
			}
		}

		[Category("Editor")]
		[Description(@"None")]
		[Browsable(true)]
		public bool Visible 
		{
			get 
			{
				return this.isVisible;
			}
			set 
			{
				this.isVisible = value;
			}
		}

		[Category("Editor")]
		[Description(@"None")]
		[Browsable(true)]
		public JustifyEnum Justify 
		{
			get 
			{
				return this.justify;
			}
			set 
			{
				this.justify = value;
			}
		}
 
		[Category("Editor")]
		[Description(@"None")]
		[Browsable(true)]
		public decimal Height 
		{
			get 
			{
				return this.height;
			}
			set 
			{
				this.height = value;
			}
		}

		[Category("Editor")]
		[Description(@"None")]
		[Browsable(true)]
		public decimal Width 
		{
			get 
			{
				return this.width;
			}
			set 
			{
				this.width = value;
			}
		}

        public bool IsCustomControl
        {
            get { return isCustomControl; }
            set { isCustomControl = value; }
        }
	}


}
