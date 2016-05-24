using BouncyToken.Utility;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.IO;

namespace BouncyToken
{
	public class RsaSha : IJwtAlgorithm
	{
		string method = "";

		public RsaSha(EJwtAlgorithm method = EJwtAlgorithm.RS256)
		{
			switch (method)
			{
				case EJwtAlgorithm.RS256:
					this.method = "SHA-256withRSA";
					break;
				case EJwtAlgorithm.RS384:
					this.method = "SHA-384withRSA";
					break;
				case EJwtAlgorithm.RS512:
					this.method = "SHA-512withRSA";
					break;
				default:
					throw new System.Exception("Invalid Algorithm");
			}
		}

		public string Sign(byte[] input, JwtKey key)
		{
			ISigner signer = SignerUtilities.GetSigner(method);
			signer.Init(true, key.PrivateKey);

			signer.BlockUpdate(input, 0, input.Length);
			byte[] signedBytes = signer.GenerateSignature();
			return Helpers.Base64UrlEncode(signedBytes);
		}

		public bool Verify(byte[] signature, byte[] input, JwtKey key)
		{
			ISigner signer = SignerUtilities.GetSigner(method);
			signer.Init(false, key.PublicKey);

			signer.BlockUpdate(input, 0, input.Length);

			return signer.VerifySignature(signature);
		}
	}
}
