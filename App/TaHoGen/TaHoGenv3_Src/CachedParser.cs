using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading;
using TaHoGen.Tokens;
namespace TaHoGen
{
	internal class Hash
	{
		public static string Calculate(string data)
		{
			MD5 md5Hash = new MD5CryptoServiceProvider();
			
			// convert it to a byte array so that the algorithm can process it
			byte[] byteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(data);

			// compute the hash
			byte[] hashBytes = md5Hash.ComputeHash(byteArray);
			
			// then convert it back to a string and return the result
			return System.Text.ASCIIEncoding.ASCII.GetString(hashBytes);
		}
	}
	[Serializable]
	internal struct CacheEntry
	{
		public CacheEntry(string hash, Token[] tokens, DateTime expiration)
		{
			Hash = hash;
			Tokens = tokens;
			ExpirationDate = expiration;
		}
		public string Hash;
		public Token[] Tokens;
		public DateTime ExpirationDate;
	}
	internal class TokenCache
	{
		private static Hashtable _entries;
		private static readonly string cacheFileName = AppDomain.CurrentDomain.BaseDirectory + "tokencache.dat";
		static TokenCache()
		{
			LoadEntries();
		}
		~TokenCache()
		{
			SaveEntries();
		}
		public bool Contains(string hash)
		{
#if CACHING_ENABLED
			bool result = _entries.Contains(hash);

			if (result == false)
				return false;

			result &= _entries[hash].GetType() == typeof(CacheEntry);

			return result;
#else
			return false;
#endif
		}
		public void Store(CacheEntry entry)
		{
			_entries[entry.Hash] = entry;
		}
		public CacheEntry Retrieve(string hash)
		{
			object target = _entries[hash];

			CacheEntry result = (CacheEntry) target;
			return result;
		}
		private static void LoadEntries()
		{
			FileStream cacheFile = new FileStream(cacheFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				_entries = (Hashtable) formatter.Deserialize(cacheFile);
			}
			catch
			{
				_entries = new Hashtable();
			}

			_entries = Hashtable.Synchronized(_entries);
			cacheFile.Close();
		}
		private static void SaveEntries()
		{
			// Remove any expired entries
			ArrayList removeList = new ArrayList();
			foreach(object key in _entries.Keys)
			{
				
				object currentObject = _entries[key];
				
				// Entries from previous versions aren't backward compatible with
				// newer versions, so we're just going to have to remove them
				if (!currentObject.GetType().IsAssignableFrom(typeof(CacheEntry)))
				{
					removeList.Add(key);
					continue;
				}

				CacheEntry entry = (CacheEntry) currentObject;
				
				if (entry.ExpirationDate < DateTime.Now)
					removeList.Add(key);
			}
			foreach(object targetKey in removeList)
			{
				_entries.Remove(targetKey);
			}

			FileStream cacheFile = new FileStream(cacheFileName, FileMode.Create, FileAccess.ReadWrite);

			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(cacheFile, _entries);
			cacheFile.Close();
		}
	}
	public class CachedParser : IParser
	{
		private IParser _parser;
		private TokenCache _cache = new TokenCache();
		public CachedParser(IParser parser)
		{
			_parser = parser;
		}
		#region IParser Members

		public TaHoGen.Tokens.Token[] Parse(string text)
		{
			string tokenHash = Hash.Calculate(text);

			
			if (_cache.Contains(tokenHash))
			{
				// Use the existing entry
				CacheEntry entry = _cache.Retrieve(tokenHash);
#if DEBUG
				//Console.WriteLine("----------------------------------Cache Hit----------------------------------");
#endif
				return entry.Tokens;
			}

			Token[] tokens = _parser.Parse(text);

			// Cache the result.
			// Note: Cache entries will expire after two weeks
			CacheEntry newEntry = new CacheEntry(tokenHash, tokens, DateTime.Now.AddDays(14));
			_cache.Store(newEntry);

			return tokens;
		}

		#endregion
	}
}
