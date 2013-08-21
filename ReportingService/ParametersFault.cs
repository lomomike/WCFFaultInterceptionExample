using System.Runtime.Serialization;

namespace ReportingService
{
    [DataContract]
    public class ParametersFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}