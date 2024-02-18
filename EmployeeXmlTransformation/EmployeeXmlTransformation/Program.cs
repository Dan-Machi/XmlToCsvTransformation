using Autofac;
using EmployeeXmlTransformation;
using EmployeeXmlTransformation.Interfaces;

namespace EmplyeeXmlTransformation
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfiguration.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IAppService>();
                app.Run();
            }
        }
    }
}