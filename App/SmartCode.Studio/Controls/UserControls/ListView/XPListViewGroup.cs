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
using System.ComponentModel; 

namespace SmartCode.Studio.Controls.UserControls.XPListView
{
	[TypeConverter(typeof(XPListViewGroupConverter))] 
	public class XPListViewGroup { 
		private string _text; 
		private int _index; 

		public XPListViewGroup() { 
		} 

		public XPListViewGroup(string text, int index) { 
			_text = text; 
			_index = index; 
		} 

		public XPListViewGroup(string text) { 
			_text = text; 
		} 

		public string GroupText { 
			get { 
				return _text; 
			} 
			set { 
				_text = value; 
			} 
		} 

		public int GroupIndex { 
			get { 
				return _index; 
			} 
			set { 
				_index = value; 
			} 
		} 

		public override string ToString() { 
			return _text; 
		} 
	}
}
