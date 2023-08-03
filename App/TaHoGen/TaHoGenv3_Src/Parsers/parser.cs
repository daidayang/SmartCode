using System;
using System.IO;
using Interop.CodeSmithParser;
using TaHoGen.Tokens;
using TaHoGen.Collections;
namespace TaHoGen
{
	internal class CallbackHandler : IParserCallback
	{

		private TokenCollection _tokens = new TokenCollection();
		#region IParserCallback Members

		public void OnParseComplete()
		{

		}

		public void OnBeginParse()
		{

		}

		public void OnMarkupText(MarkupTypeEnum MarkupType, string bstrText)
		{
			Token newToken = null;
			string text = bstrText;
			switch(MarkupType)
			{
				case MarkupTypeEnum.Comment:
					newToken = new Comment(text);
					break;
				case MarkupTypeEnum.Directive:
					newToken = new Directive(text);
					break;
				case MarkupTypeEnum.Expression:
					newToken = new Expression(text);
					break;
				case MarkupTypeEnum.IncludeFile:
                    string fileName = SmartCode.Template.TemplateBase.TemplatesBaseDirectory + text.Replace("%%", "/");

                    bool exists = false;
                    foreach (Token var in _tokens)
                    {
                        if (var.Text == fileName)
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        //newToken = new Include(fileName);
                        // Read the file into memory and insert its contents
                        // into the existing stream.
                        if (!File.Exists(fileName))
                            throw new FileNotFoundException("Unable to find the include file!", fileName);

                        //CMarkupParserClass parser = new CMarkupParserClass();
                        //parser.Parse(fileText, this);

                       // newToken = new Include(fileName);
                       // _tokens.Add(new Include(fileName));
                        newToken = new Include(fileName);
                    }
					break;
				case MarkupTypeEnum.LiteralCode:
					newToken = new Code(text);
					break;
				case MarkupTypeEnum.LiteralText:
					newToken = new TextToken(text);
					break;
				case MarkupTypeEnum.ScriptBlock:
					newToken = new ScriptBlock(text);
					break;
				default:
					break;
			}
			if (newToken != null)
				_tokens.Add(newToken);
		}

		#endregion
		public TokenCollection ParsedTokens
		{
			get { return _tokens; }
		}
	}

	public class Parser : IParser
	{
		public Parser()
		{
		}
		#region IParser Members

		public Token[] Parse(string text)
		{
			CMarkupParserClass parser = new CMarkupParserClass();

			// Parse the text and let the handler collect the results
			CallbackHandler handler = new CallbackHandler();
			parser.Parse(text, handler);

			TokenCollection parsedTokens = handler.ParsedTokens;

			// Copy the results, and we're done
			Token[] results = new Token[parsedTokens.Count];

			if (parsedTokens.Count > 0)
			{
				parsedTokens.CopyTo(results);
			}
			return results;
		}

		#endregion
	}
}
