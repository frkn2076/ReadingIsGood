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
}
