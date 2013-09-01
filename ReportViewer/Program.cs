using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ReportViewer.ReportServiceReference;

namespace ReportViewer
{
    class Program
    {
        private static ReportServiceClient _client;

        static void Main(string[] args)
        {
            try
            {
                _client = new ReportServiceClient();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
                return;
            }

            try
            {
                foreach (var i in Enumerable.Range(0, 2).Reverse())
                {
                    Guid id = PrepareReport(i);
                    if (id != Guid.Empty)
                        CheckReport(id);
                }
                
                Console.ReadKey();
            }
            finally
            {
                if (_client != null)
                    _client.Close();
            }
        }
        
        private static void CheckReport(Guid id)
        {

             Process(i => _client.CheckReportIsReady(i), id);
        }

        private static Guid PrepareReport(long reportId)
        {
            return Process(i =>
                {
                    var paramanter = new List<ReportParameter>
                        {
                            new ReportParameter {Name = "Name", Value = "John"}
                        };
                    return _client.PostReportToQueue(i, paramanter.ToArray());

                }, reportId);
        }

        private static T Process<U, T>(Func<U, T> func, U argument)
        {
            try
            {
                return func(argument);
            }
            catch (FaultException<ReportFault> exception)
            {
                Console.WriteLine("ExceptionType: {0}", exception.GetType());
                Console.WriteLine("Fault type: {0}", exception.Detail.GetType());
                Console.WriteLine("Message: {0}", exception.Detail.Message);
                Console.WriteLine("Report template id: {0}", exception.Detail.Id);
                Console.WriteLine();
            }
            catch (FaultException<ParametersFault> exception)
            {
                Console.WriteLine("ExceptionType: {0}", exception.GetType());
                Console.WriteLine("Fault type: {0}", exception.Detail.GetType());
                Console.WriteLine("Message: {0}", exception.Detail.Message);
                Console.WriteLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Неизвестное исключение: {0} ", exception.Message);
                Console.WriteLine();

            }

            return default(T);
        }
    }
}
