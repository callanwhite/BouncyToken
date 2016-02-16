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

		static Dictionary<EJwtAlgorithm, IJwtAlgorithm> hashAlgorithms = new Dictionary<EJwtAlgorithm, IJwtAlgorithm>
		{
			{ EJwtAlgorithm.HS256, new HmacSha(EJwtAlgorithm.HS256) },
			{ EJwtAlgorithm.HS384, new HmacSha(EJwtAlgorithm.HS384) },
			{ EJwtAlgorithm.HS512, new HmacSha(EJwtAlgorithm.HS512) },
			{ EJwtAlgorithm.RS256, new RsaSha(EJwtAlgorithm.RS256) },
			{ EJwtAlgorithm.RS384, new RsaSha(EJwtAlgorithm.RS384) },
			{ EJwtAlgorithm.RS512, new RsaSha(EJwtAlgorithm.RS512) },
		};

		public static string Decode(string token, JwtKey key, bool verify = true)
		{
			string[] tokenParts = token.Split('.');
			if (tokenParts.Length != 3)
			{
				throw new ArgumentException("Invalid Json Web Token");
			}

			string header = Encoding.UTF8.GetString(Helpers.Base64UrlDecode(tokenParts[0]));
			string payload = Encoding.UTF8.GetString(Helpers.Base64UrlDecode(tokenParts[1]));
			Console.WriteLine(header + "::");
			Console.WriteLine(payload + "::");
			if (verify)
			{
				Dictionary<string, object> headerJson = JsonSerializer.Deserialize<Dictionary<string, object>>(header);	
				EJwtAlgorithm algorithm = (EJwtAlgorithm)Enum.Parse(typeof(EJwtAlgorithm), headerJson["alg"] as string);
				Console.WriteLine(tokenParts[2]);
				byte[] signature = Helpers.Base64UrlDecode(tokenParts[2]);
				Console.WriteLine("Sig:" + tokenParts[2]);
				byte[] toVerify = Encoding.UTF8.GetBytes(tokenParts[0] + "." + tokenParts[1]);

				bool valid = hashAlgorithms[algorithm].Verify(signature, toVerify, key);
				Console.WriteLine(valid);
			}


			return payload;
		}

		public static T Decode<T>(string token, JwtKey key, bool verify = true)
		{
			string payloadJson = Decode(token, key, verify);
			return JsonSerializer.Deserialize<T>(payloadJson);
		}

		public static bool Verify(string token)
		{
			return true;
		}

		public static string Encode(object payload, JwtKey key, EJwtAlgorithm algorithm = EJwtAlgorithm.HS256, IDictionary<string, object> extraHeaders = null)
		{
			string[] tokenParts = new string[3];
			Dictionary<string, object> header = extraHeaders == null ? new Dictionary<string, object>() : new Dictionary<string, object>(extraHeaders);
			header.Add("typ", "JWT");
			header.Add("alg", algorithm.ToString());

			tokenParts[0] = Helpers.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(header)));
			tokenParts[1] = Helpers.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload)));

			Console.WriteLine("Header: " + tokenParts[0]);
			Console.WriteLine("Payload: " + tokenParts[1]);

			byte[] bytesToSign = Encoding.UTF8.GetBytes(tokenParts[0] + "." + tokenParts[1]);
			tokenParts[2] = hashAlgorithms[algorithm].Sign(bytesToSign, key);

			return string.Join(".", tokenParts);
		}
	}
}
