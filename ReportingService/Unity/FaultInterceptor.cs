using System;
using System.ServiceModel;
using Microsoft.Practices.Unity.InterceptionExtension;
using ReportingService.Exceptions;
using ReportingService.Faults;
using log4net;

namespace ReportingService.Unity
{
    public class FaultInterceptor : ICallHandler
    {
        private const string logRootName = "ReportingService.ReportService";

        #region Регистратор
        
        private static readonly ILog log = LogManager.GetLogger(logRootName);
        #endregion

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {

            IMethodReturn result = getNext()(input, getNext);
            Exception e = result.Exception;
            if (e == null)
                return result;

            // Логируем все возникшие сбои
            log.Debug(input.MethodBase.Name, e);

            //Любые контрактные исключения пропускаем 
            if (e is FaultException)
                return result;

            var paramException = e as ParametersValidationException;
            if (paramException != null)
            {
                var fault = new ParametersFault {Message = paramException.Message};
                var exception = new FaultException<ParametersFault>(fault);
                return input.CreateExceptionMethodReturn(exception);
            }

            var reportException = e as ReportNotExistsException;
            if (reportException != null)
            {
                 var fault = new ReportFault
                     {
                         Message = reportException.Message,
                         Id = reportException.Id
                     };
                var exception = new FaultException<ReportFault>(fault);
                return input.CreateExceptionMethodReturn(exception);
            }
            return result;
        }

        public int Order { get; set; }
    }
}