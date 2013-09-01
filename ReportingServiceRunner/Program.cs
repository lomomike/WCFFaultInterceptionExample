using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportingService;
using ReportingService.Unity;

namespace ReportingServiceRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            IReportService service = ServiceFactory.Create();
            var guid = service.PostReportToQueue(0, new List<ReportParameter>());
        }
    }
}
