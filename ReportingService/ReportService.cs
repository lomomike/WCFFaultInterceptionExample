using System;
using System.Collections.Generic;
using System.ServiceModel;
using ReportingService.Exceptions;
using ReportingService.Faults;

namespace ReportingService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ReportService : IReportService
    {
        private readonly ReportRepository _repository = new ReportRepository();
        private readonly ReportPreparationQueue _queue = new ReportPreparationQueue();

        public virtual Guid PostReportToQueue(long id, List<ReportParameter> parameters)
        {
            IReport report = _repository.Find(id);
            if (report == null)
            {
                throw new ReportNotExistsException(id, "Отчет не найден во внешнем хранилище.");
            }

            // Метод выбрасывает исключение при 
            // передачи некорректных параметров отчета.
            CheckParams(report, parameters);

            return _queue.EnqueuePrepare(report, parameters);
        }

        public virtual bool CheckReportIsReady(Guid reportId)
        {
            return _queue.CheckReady(reportId);
        }

        #region Private methods
        private void CheckParams(IReport report, List<ReportParameter> parameters)
        {
            if (new Random().Next() % 3 == 0)
                throw new ParametersValidationException("Переданы некорректные параметры.");
        }
        #endregion Private methods
    }
}