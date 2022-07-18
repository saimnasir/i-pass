using System;

namespace Patika.Shared.Exceptions
{
    public class GeneralException : BaseFatalException
    {
        public GeneralException(Exception ex) : base($"_general_:{ex.GetHashCode()}", ex.Message)
        {

        }
    }
}