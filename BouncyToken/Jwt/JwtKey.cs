using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
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
	}
}
