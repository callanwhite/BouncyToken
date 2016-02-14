using BouncyToken.Utility;
using System;
using System.Text;

namespace BouncyToken
{
	public class JsonWebToken
	{
		public static IJsonSerializer JsonSerializer { get; set; }

		public static string Decode(string token, byte[] key, bool verify = true)
		{
			string[] tokenParts = token.Split('.');
			if (tokenParts.Length != 3)
			{
				throw new ArgumentException("Invalid Json Web Token");
			}

			string header = Encoding.UTF8.GetString(Helpers.Base64UrlDecode(tokenParts[0]));
			string payload = Encoding.UTF8.GetString(Helpers.Base64UrlDecode(tokenParts[1]));
			string signature = tokenParts[2];

			return null;
		}

		public static string Decode(string token, string key, bool verify = true)
		{
			return Decode(token, Encoding.UTF8.GetBytes(key), verify);
		}

		public static bool Verify(string token)
		{
			return true;
		}
	}
}
