using System;
namespace BouncyToken
{
	public enum ETokenError
	{
		Malformed,
		VerificationFailed,
	};

	public class InvalidTokenException : Exception
	{
		public ETokenError ErrorType { get; private set; }

		public InvalidTokenException(ETokenError eError, string error = null) : base(error)
		{
			this.ErrorType = eError;
		}
	}
}
