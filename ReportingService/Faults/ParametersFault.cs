﻿using System;
using System.Runtime.Serialization;

namespace ReportingService.Faults
{
    [DataContract]
    [Serializable]
    public class ParametersFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}