using System.Runtime.Serialization;

namespace ReportingService.Faults
{
    [DataContract]
    public class ParametersFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}