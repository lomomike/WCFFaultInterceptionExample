using System;
using System.Collections.Generic;

namespace ReportingService
{
    internal class ReportRepository
    {
        private static readonly List<long> existedReport = new List<long> { 1, 3, 4, 5, 6, 7, 12, 14 };

        public IReport Find(long id)
        {
            if (existedReport.Contains(id))
                return new Report(id);
            return null;
        }
    }

   
}