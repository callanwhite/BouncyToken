using Org.BouncyCastle.OpenSsl;

namespace BouncyToken.Utility
{
	public class Password : IPasswordFinder
	{
		char[] password = new char[0];
		public Password(string password)
		{
			this.password = password.ToCharArray();
		}

		public char[] GetPassword()
		{
			return password;
		}
	}
}
