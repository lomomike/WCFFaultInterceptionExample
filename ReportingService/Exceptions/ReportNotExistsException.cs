using System;
using System.Runtime.Serialization;

namespace ReportingService.Exceptions
{
    [Serializable]
    public class ReportNotExistsException : Exception
    {
        public long Id { get; private set; }

        public ReportNotExistsException(string message)
            : this(0, message)
        {
        }

        public ReportNotExistsException(long id, string message )
            : base(message)
        {
            Id = id;
        }

        public ReportNotExistsException(long id, string message, Exception inner)
            : base(message, inner)
        {
            Id = id;
        }

        protected ReportNotExistsException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
            Id = info.GetInt64("Id");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Id", Id);
        }

    }
}