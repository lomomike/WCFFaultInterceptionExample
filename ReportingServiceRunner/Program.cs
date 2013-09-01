using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using ReportingService;
using ReportingService.Unity;
using log4net;

namespace ReportingServiceRunner
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        

        static void Main(string[] args)
        {
            IReportService server = null;
            try
            {
                server = ServiceFactory.Create(); ;
            }
            catch (Exception exception)
            {
                log.Fatal("Ошибка создания экземпляра сервиса. Выход", exception);
                Environment.ExitCode = 1;
                return;
            }

            //упаковка в WCF хост
            ServiceHost host = new ServiceHost(server);
            try
            {
                host.Open();
            }
            catch (Exception exception)
            {
                log.Error("Ошибка инициализации WCF сервиса. Выход", exception);
                return;
            }

            Console.WriteLine("Нажмите любую клавишу для то, чтобы остановить службу.");
            Console.ReadKey();

            Stop(host);
            log.Info("Сервер построения отчетов остановлен.");
        }

        private static void Stop(ServiceHost host)
        {
            try
            {
                host.Close();
            }
            catch (Exception ee)
            {
                log.Error("Ошибка при остановке хоста", ee);
                Environment.ExitCode = 3;
            }

            try
            {
                IDisposable disposable = host.SingletonInstance as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            catch (Exception ee)
            {
                log.Error("Ошибка при освобождении ресурсов сервиса", ee);
            }
        }
    }
}
