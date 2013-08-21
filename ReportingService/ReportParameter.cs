using System.Runtime.Serialization;

namespace ReportingService
{
    [DataContract]
    public class ReportParameter
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}