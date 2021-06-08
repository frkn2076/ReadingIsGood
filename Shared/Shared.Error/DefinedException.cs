using System;

namespace Shared.Error
{
    public class DefinedException : Exception
    {
        public int Code { get; set; }
        public DefinedException(string message) : base(message) { }
        public DefinedException(string message, int code) : base(message) => Code = code;
    }

    public class SomethingWentWrongDuringDatabaseOperationException : DefinedException
    {
        public SomethingWentWrongDuringDatabaseOperationException() : base("An error occured while executing database transaction", 700) { }
    }

    public class UserClaimNotFoundException : DefinedException
    {
        public UserClaimNotFoundException(string claimKey) : base($"The given claim key '{claimKey}' not found in the context.", 800) { }
    }
}
