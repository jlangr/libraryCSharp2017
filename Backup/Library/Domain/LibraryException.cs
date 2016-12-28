using System;

namespace Library.Domain
{
    public class LibraryException: ApplicationException
    {
        public LibraryException(string message) : base(message)
        {
        }
    }
}
