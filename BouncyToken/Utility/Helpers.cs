using System;

namespace BouncyToken.Utility
{
	public class Helpers
	{
		public static string Base64UrlEncode(byte[] input)
		{
			string result = Convert.ToBase64String(input);

			result = result.Split('=')[0];
			result = result.Replace('+', '-');
			result = result.Replace('/', '_');

			return result;
		}

		public static byte[] Base64UrlDecode(string input)
		{
			input = input.Replace('-', '+');
			input = input.Replace('_', '/');

			switch (input.Length % 4)
			{
				case 0:
					break;
				case 2:
					input += "==";
					break;
				case 3:
					input += "=";
					break;
				default:
					throw new InvalidTokenException(ETokenError.Malformed);
			}
			return Convert.FromBase64String(input);
		}

		
	}
}
