using Autofac;
using TransformationLogic.Interfaces;
using TransformationLogic.Services;

namespace TransformationLogic
{
    public class TransformationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransformationService>().As<ITransformationService>();
        }
    }
}
