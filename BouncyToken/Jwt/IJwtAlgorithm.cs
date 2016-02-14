namespace BouncyToken
{
	public interface IJwtAlgorithm
	{
		string Sign(byte[] input, JwtKey key);
		bool Verify(byte[] signature, byte[] input, JwtKey key);
	}
}
