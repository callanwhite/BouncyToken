using BouncyToken.Utility;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace BouncyToken
{
	public class JsonWebToken
	{
		public static IJsonSerializer JsonSerializer = new JsonSerializer();

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

		public static string Encode(object payload, byte[] key, string secret, IDictionary<string, object> extraHeaders = null)
		{
			string[] parts = new string[3];
			Dictionary<string, object> header = extraHeaders == null ? new Dictionary<string, object>() : new Dictionary<string, object>(extraHeaders);
			header.Add("typ", "JWT");
			header.Add("alg", "HS256");

			parts[0] = Helpers.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(header)));
			parts[1] = Helpers.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload)));

			Console.WriteLine("Header: " + parts[0]);
			Console.WriteLine("Payload: " + parts[1]);

			Org.BouncyCastle.Crypto.Parameters.KeyParameter kp = new Org.BouncyCastle.Crypto.Parameters.KeyParameter(key);
			IMac mac = MacUtilities.GetMac("HmacSHA256");
			mac.Init(kp);
			mac.Reset();
			var b = Encoding.UTF8.GetBytes(parts[0] + "." + parts[1]);
			mac.BlockUpdate(b, 0, b.Length);
			var ob = new byte[mac.GetMacSize()];
			mac.DoFinal(ob, 0);
			parts[2] = Helpers.Base64UrlEncode(ob);

			return string.Join(".", parts);
		}
	}
}
