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

			JwtKey key = JwtKey.LoadSymmetricKey(System.Text.Encoding.UTF8.GetBytes("supersecret"));
			string token = JsonWebToken.Encode(testData, key);
			Console.WriteLine(token);
			JsonWebToken.Decode(token, key, true);
			Console.ReadLine();
		}
	}
}
