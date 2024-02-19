using Autofac;
using Autofac.log4net;
using TransformationLogic.Interfaces;
using TransformationLogic.Services;

namespace TransformationLogic
{
    public class TransformationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransformationService>().As<ITransformationService>();
            var loggingModule = new Log4NetModule("log4net.config", true);
            builder.RegisterModule(loggingModule);
        }
    }
}
