using System;
using System.Collections.Generic;
using ReportingService.Exceptions;

namespace ReportingService
{
    internal class ReportPreparationQueue
    {
        public Guid EnqueuePrepare(IReport report, IList<ReportParameter> parameters)
        {
            return Guid.NewGuid();
        }

        public bool CheckReady(Guid reportId)
        {
            var message = string.Format("Отчет {0} не существует в очереди.", reportId);
            throw new ReportNotExistsException(message);
        }
    }
}