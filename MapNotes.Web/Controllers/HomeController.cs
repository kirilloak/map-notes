using System.Web.Mvc;
using Autofac;
using MapNotes.BLL;
using MapNotes.BLL.Abstract.Managers;
using Microsoft.AspNet.Identity;

namespace MapNotes.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(bool? updateIndex)
        {
            if (updateIndex.HasValue && updateIndex.Value)
            {
                var noteManager = IoC.Instance.Resolve<INoteManager>();
                noteManager.RebuildIndex(User.Identity.GetUserId(), null);
                Response.Redirect("/");
            }

            return View();
        }
    }
}