namespace ReportingService
{
    internal interface IReport
    {
    }

    internal class Report : IReport
    {
        public Report(long id)
        {
            Id = id;
        }

        protected long Id { get; private set; }
    }
}