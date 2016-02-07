using Autofac;
using MapNotes.BLL.Abstract.Helpers;
using MapNotes.BLL.Abstract.Managers;
using MapNotes.BLL.Concrete.Helpers;
using MapNotes.BLL.Concrete.Managers;

// ReSharper disable All

namespace MapNotes.BLL
{
    public class BLL_IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Helpers
            builder.RegisterType<RedisCacheHelper>().As<IRedisCacheHelper>().SingleInstance();

            // Managers
            builder.RegisterType<NoteManager>().As<INoteManager>();
        }
    }
}
