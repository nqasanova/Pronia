using System;
namespace Pronia.Exceptions
{
    public class IdentityCookieException : ApplicationException
    {
        public IdentityCookieException(string? message)
            : base(message)
        {

        }
    }
}