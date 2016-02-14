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
			string token = JsonWebToken.Encode(testData, System.Text.Encoding.UTF8.GetBytes("supersecret"), "pass");
			Console.WriteLine(token);
			Console.ReadLine();
		}
	}
}
