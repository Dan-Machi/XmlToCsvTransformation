using Autofac;
using EmployeeXmlTransformation.Interfaces;
using EmployeeXmlTransformation.Services;
using TransformationLogic;

namespace EmployeeXmlTransformation
{
    public class ContainerConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppService>().As<IAppService>();
            builder.RegisterModule<TransformationModule>();
            builder.RegisterType<MapperService>().As<IMapperService>();

            return builder.Build();
        }
    }
}
