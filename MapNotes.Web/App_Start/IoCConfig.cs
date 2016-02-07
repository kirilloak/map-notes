using MapNotes.BLL;
using MapNotes.DAL;

namespace MapNotes.Web
{
    public class IoCConfig
    {
        public static void Initialize()
        {
            IoC.Initialize(
                new BLL_IoCModule(),
                new DAL_IoCModule()
            );
        }
    }
}