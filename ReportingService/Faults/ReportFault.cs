using System;
using System.Runtime.Serialization;

namespace ReportingService.Faults
{
    [DataContract]
    [Serializable]
    public class ReportFault
    {
        [DataMember]
        public string  Message { get; set; }

        [DataMember]
        public long Id { get; set; }
    }
}