using System;
using System.Runtime.Serialization;

namespace ReportingService.Common.Faults
{
    [DataContract]
    public class ParametersFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}