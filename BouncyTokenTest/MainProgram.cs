using System;
using BouncyToken;
using System.Collections.Generic;

namespace BouncyTokenTest
{
	class MainProgram
	{
		static Dictionary<string, object> testData = new Dictionary<string, object>
		{
			{ "username", "callan" },
		};

		static void Main(string[] args)
		{
			Console.WriteLine("BouncyTest");

			Console.WriteLine(System.IO.File.Exists("Keys/letmein.pem"));

			JwtKey key = JwtKey.LoadSymmetricKey(System.Text.Encoding.UTF8.GetBytes("supersecret"));
			JwtKey letmeinPrivate = JwtKey.LoadAsymmetricKeyPrivate("Keys/letmein.priv", "letmein");
			JwtKey letmeinPublic = JwtKey.LoadAsymmetricKeyPublic("Keys/letmein.pub");

			string token = JsonWebToken.Encode(testData, letmeinPrivate, EJwtAlgorithm.RS256);
			Console.WriteLine(token);
			JsonWebToken.Decode(token, letmeinPublic, true);
			Console.ReadLine();
		}
	}
}
