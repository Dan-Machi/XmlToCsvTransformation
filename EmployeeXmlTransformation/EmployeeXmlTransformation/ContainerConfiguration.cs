using Autofac;
using EmployeeXmlTransformation.Interfaces;
using EmployeeXmlTransformation.Services;

namespace EmployeeXmlTransformation
{
    public class ContainerConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppService>().As<IAppService>();

            return builder.Build();
        }
    }
}
