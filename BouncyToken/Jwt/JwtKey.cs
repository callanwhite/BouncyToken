using BouncyToken.Utility;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System.IO;

namespace BouncyToken
{
	public class JwtKey
	{
		ICipherParameters key = null;
		public ICipherParameters Key { get { return key; } }

		public JwtKey(ICipherParameters key)
		{
			this.key = key;
		}

		public static JwtKey LoadSymmetricKey(byte[] keyBytes)
		{
			ICipherParameters key = new KeyParameter(keyBytes);
			return new JwtKey(key);
		}

		public static JwtKey LoadAsymmetricKeyPrivate(byte[] keyBytes, string secret = null)
		{
			MemoryStream memoryStream = new MemoryStream(keyBytes);
			StreamReader streamReader = new StreamReader(memoryStream);
			PemReader pemReader = secret == null ? new PemReader(streamReader) : new PemReader(streamReader, new Password(secret));
			AsymmetricCipherKeyPair key = (AsymmetricCipherKeyPair)pemReader.ReadObject();

			return new JwtKey(key.Private);
		}

		public static JwtKey LoadAsymmetricKeyPrivate(string keyPath, string secret = null)
		{
			return LoadAsymmetricKeyPrivate(File.ReadAllBytes(keyPath), secret);
		}

		public static JwtKey LoadAsymmetricKeyPublic(byte[] keyBytes)
		{
			MemoryStream memoryStream = new MemoryStream(keyBytes);
			StreamReader streamReader = new StreamReader(memoryStream);
			PemReader pemReader = new PemReader(streamReader);
			AsymmetricKeyParameter key = (AsymmetricKeyParameter)pemReader.ReadObject();

			return new JwtKey(key);
		}

		public static JwtKey LoadAsymmetricKeyPublic(string keyPath)
		{
			return LoadAsymmetricKeyPublic(File.ReadAllBytes(keyPath));
		}
	}
}
