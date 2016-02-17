using System;
using BouncyToken;
using System.Collections.Generic;
using System.IO;

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

			JwtKey key = JwtKey.LoadKey(System.Text.Encoding.UTF8.GetBytes("supersecret"), EJwtAlgorithm.HS256);
			JwtKey letmeinPrivate = JwtKey.LoadKey(File.ReadAllBytes("Keys/letmein.priv"), EJwtAlgorithm.RS256, "letmein", true);
			JwtKey letmeinPublic = JwtKey.LoadKey(File.ReadAllBytes("Keys/letmein.pub"), EJwtAlgorithm.RS256);

			string token = JsonWebToken.Encode(testData, letmeinPrivate, EJwtAlgorithm.RS256);
			Console.WriteLine(token);
			JsonWebToken.Decode(token, letmeinPublic, true);
			Console.ReadLine();
		}
	}
}
