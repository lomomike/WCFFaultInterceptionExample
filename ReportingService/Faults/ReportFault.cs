using System.Runtime.Serialization;

namespace ReportingService.Faults
{
    [DataContract]
    public class ReportFault
    {
        [DataMember]
        public string  Message { get; set; }

        [DataMember]
        public long Id { get; set; }
    }
}