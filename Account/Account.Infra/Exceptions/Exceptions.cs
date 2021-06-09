using Shared.Error;

namespace Account.Infra.Exceptions
{
    public class AccountNotFoundException : DefinedException
    {
        public AccountNotFoundException() : base("Account not found", 100) { }
    }

    public class AccountAlreadyExistsException : DefinedException
    {
        public AccountAlreadyExistsException() : base("This account already exists.", 101) { }
    }

    public class RefreshTokenNotException : DefinedException
    {
        public RefreshTokenNotException() : base("Refresh token not found.", 102) { }
    }

    public class RefreshTokenIsNullOrWhiteSpaceException : DefinedException
    {
        public RefreshTokenIsNullOrWhiteSpaceException() : base("Refresh token is null or whitespace.", 103) { }
    }
}
