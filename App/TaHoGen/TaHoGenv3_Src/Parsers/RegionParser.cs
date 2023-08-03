using System;
using System.Collections;
using TaHoGen.Tokens;
using Interop.RegionParser;
namespace TaHoGen.Parsers
{
#if REGIONS_ENABLED
	public class RegionParser : IParser
	{
		#region IParser Members

		public TaHoGen.Tokens.Token[] Parse(string text)
		{
			Interop.RegionParser.CParserClass parser = new CParserClass();

			CallbackHandler callback = new CallbackHandler();
			parser.Parse(text, callback);
			return callback.GetTokens();
		}

		#endregion

		private class CallbackHandler : Interop.RegionParser.IParserCallback
		{
			private ArrayList _tokens = new ArrayList();
			private string _currentTokenName;
			#region IParserCallback Members

			public void OnParse(string text, Interop.RegionParser.MergeTokenType tokenType)
			{
				Token newToken = null;
				switch (tokenType)
				{
					case MergeTokenType.RegionName:
						_currentTokenName = text;
						break;
					case MergeTokenType.Region:
						newToken = new Region(_currentTokenName, text);
						break;
					case MergeTokenType.Nonregion:
						newToken = new NonRegion(text);
						break;
					default:
						break;
				}
				if (newToken != null)
					_tokens.Add(newToken);
			}

			public void OnBeginParse()
			{
				// TODO:  Add CallbackHandler.OnBeginParse implementation
			}

			public void OnEndParse()
			{
				// TODO:  Add CallbackHandler.OnEndParse implementation
			}

			#endregion 

			public Token[] GetTokens()
			{
				Token[] results = new Token[_tokens.Count];
				_tokens.CopyTo(results);
				return results;	
			}
		}

	}
#endif
}
