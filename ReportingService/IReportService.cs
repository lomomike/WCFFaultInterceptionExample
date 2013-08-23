using System;
using System.Collections.Generic;
using System.ServiceModel;
using ReportingService.Faults;

namespace ReportingService
{
    [ServiceContract]
    public interface IReportService
    {
        [OperationContract]
        [FaultContract(typeof(ParametersFault))]
        [FaultContract(typeof(ReportFault))]
        Guid PostReportToQueue(long id, List<ReportParameter> parameters);

        [OperationContract]
        [FaultContract(typeof(ReportFault))]
        bool CheckReportIsReady(Guid reportId);
    }
}