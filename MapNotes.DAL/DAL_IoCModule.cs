using Autofac;
using MapNotes.DAL.Abstract;
using MapNotes.DAL.Concrete;

// ReSharper disable All

namespace MapNotes.DAL
{
    public class DAL_IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<NoteRepository>().As<INoteRepository>();
        }
    }
}
