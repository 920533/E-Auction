using System;

namespace EAuction.Shared.Exceptions
{

    public class UserException : Exception
    {
        public UserException(string message)
            : base(message)
        {
        }

    }
    public class UserNotFounException : Exception
    {
        public UserNotFounException(string message)
            : base(message)
        {
        }
    }
}

