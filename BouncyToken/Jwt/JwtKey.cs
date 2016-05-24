using BouncyToken.Utility;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;

namespace BouncyToken
{
	public class JwtKey
	{
		//ICipherParameters key = null;
		//public ICipherParameters Key { get { return key; } }

		public ICipherParameters PrivateKey { get; private set; }
		public ICipherParameters PublicKey { get; private set; }
		

		public static JwtKey LoadKey(byte[] keyBytes, EJwtAlgorithm algorithm, string secret = null, bool isPrivate = false)
		{
			switch (algorithm)
			{
				case EJwtAlgorithm.HS256:
				case EJwtAlgorithm.HS384:
				case EJwtAlgorithm.HS512:
					return LoadSymmetricKey(keyBytes);
				case EJwtAlgorithm.RS256:
				case EJwtAlgorithm.RS384:
				case EJwtAlgorithm.RS512:
					return LoadPKCS8Key(keyBytes, secret, isPrivate);
				default:
					throw new Exception("Invalid key");
			}
		}

		static JwtKey LoadSymmetricKey(byte[] keyBytes)
		{
			ICipherParameters key = new KeyParameter(keyBytes);
			return new JwtKey
			{
				PrivateKey = key,
				PublicKey = key,
			};
		}

		static JwtKey LoadPKCS8Key(byte[] keyBytes, string secret = null, bool isPrivate = false)
		{
			MemoryStream memoryStream = new MemoryStream(keyBytes);
			StreamReader streamReader = new StreamReader(memoryStream);
			bool isPrivateWithSecret = secret != null && isPrivate;
			PemReader pemReader = isPrivateWithSecret? new PemReader(streamReader, new Password(secret)) : new PemReader(streamReader);

			//ICipherParameters key;
			JwtKey key = null;
			if (isPrivate)
			{
				AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
				key = new JwtKey
				{
					PrivateKey = keyPair.Private,
					PublicKey = keyPair.Public,
				};
			}
			else
			{
				key = new JwtKey
				{
					PublicKey = (AsymmetricKeyParameter)pemReader.ReadObject(),
				};
			}

			return key;
		}
	}
}
