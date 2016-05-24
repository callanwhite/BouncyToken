using BouncyToken.Utility;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace BouncyToken
{
	public class HmacSha : IJwtAlgorithm
	{
		string method = "";

		public HmacSha(EJwtAlgorithm method = EJwtAlgorithm.HS256)
		{
			switch (method)
			{
				case EJwtAlgorithm.HS256:
					this.method = "HmacSha256";
					break;
				case EJwtAlgorithm.HS384:
					this.method = "HmacSha384";
					break;
				case EJwtAlgorithm.HS512:
					this.method = "HmacSha512";
					break;
				default:
					throw new System.Exception("Invalid Algorithm");
			}
		}

		public string Sign(byte[] input, JwtKey key)
		{
			byte[] signedBytes = MacUtilities.CalculateMac(method, key.PrivateKey, input);

			return Helpers.Base64UrlEncode(signedBytes);
		}

		public bool Verify(byte[] signature, byte[] input, JwtKey key)
		{
			System.Console.WriteLine("@" + Helpers.Base64UrlEncode(signature));
			System.Console.WriteLine("@" + Sign(input, key));
			return Helpers.Base64UrlEncode(signature).Equals(Sign(input, key));
		}
	}
}
