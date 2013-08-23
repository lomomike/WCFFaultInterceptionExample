using System;
using System.Runtime.Serialization;

namespace ReportingService.Exceptions
{
    [Serializable]
    public class ParametersValidationException : Exception
    {
        public ParametersValidationException()
        {
        }

        public ParametersValidationException(string message)
            : base(message)
        {
        }

        public ParametersValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ParametersValidationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}