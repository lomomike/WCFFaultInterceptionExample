using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

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

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ReportService : IReportService
    {
        ReportRepository _repository = new ReportRepository();
        ReportPreparationQueue _queue = new ReportPreparationQueue();

        public Guid PostReportToQueue(long id, List<ReportParameter> parameters)
        {
            IReport report = _repository.Find(id);
            if (report == null)
            {
                // Проверка и формирование сбоя уровня коммуникации.
                throw new FaultException<ReportFault>(new ReportFault { Id = id, Message = @"Report doesn\'t exists." });
            }

            try
            {
                CheckParams(report, parameters);
            }
            catch (ParametersValidationException exception)
            {
                //Перехватываем исключение и преобразовавыем в нужный сбой
                var fault = new ParametersFault { Message = exception.Message };
                throw new FaultException<ParametersFault>(fault);

                //Что делать, если метод выбрасывает не одно исключение?
            }
            return _queue.Prepare(report, parameters);
        }

        private void CheckParams(IReport report, List<ReportParameter> parameters)
        {
            throw new ParametersValidationException("Parameters are invalid.");
        }

        public bool CheckReportIsReady(Guid reportId)
        {
            return false;
        }
    }

    [Serializable]
    public class ParametersValidationException : Exception
    {
        public ParametersValidationException()
        {
        }

        public ParametersValidationException(string message)
            : base(message)
        {
        }

        public ParametersValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ParametersValidationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    internal class ReportRepository
    {
        public IReport Find(long id)
        {
            return null;
        }
    }

    internal class ReportPreparationQueue
    {
        public Guid Prepare(IReport report, IList<ReportParameter> parameters)
        {
            return Guid.NewGuid();
        }
    }

    internal interface IReport
    {
    }
}