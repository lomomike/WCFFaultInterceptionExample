using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace ReportingService.Unity
{
    public static class ServiceFactory
    {
        private const string PolicyName = "FaultPolicy";

        private static readonly object _locker = new object();
        private static IUnityContainer _container;

        public static IReportService Create()
        {
            return Container.Resolve<IReportService>();
        }

        private static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (_locker)
                    {
                        if (_container == null)
                        {
                            _container = new UnityContainer();
                            _container.AddNewExtension<Interception>();

                            _container.Configure<Interception>()
                                            .AddPolicy(PolicyName)
                                            .AddMatchingRule<AlwaysMatchingRule>()
                                            .AddCallHandler<FaultInterceptor>();

                            _container.RegisterType<IReportService, ReportService>(
                                new TransientLifetimeManager(),
                                new Interceptor<VirtualMethodInterceptor>(),
                                new InterceptionBehavior<PolicyInjectionBehavior>(PolicyName));
                          
                        }
                    }
                }
                return _container;
            }
        }

        #region Вспомогательный класс

        /// <summary>
        /// Класс правила перехвата - перехватывает любые вызовы.
        /// </summary>
        class AlwaysMatchingRule : IMatchingRule
        {
            public bool Matches(MethodBase member)
            {
                return true;
            }
        }
        #endregion Вспомогательный класс
    }
}