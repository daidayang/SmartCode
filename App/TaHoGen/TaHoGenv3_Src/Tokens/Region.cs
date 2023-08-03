using System;

namespace TaHoGen.Tokens
{
#if REGIONS_ENABLED
	public class Region : Token
	{
		private string _name;
		public Region(string regionName, string text) : base(text)
		{
			_name = regionName;
		}
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
	}
#endif
}
