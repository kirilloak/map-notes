using System;
using System.Linq;
using System.Web.Http;
using Autofac;
using MapNotes.BLL;
using MapNotes.BLL.Abstract.Managers;

namespace MapNotes.Web.Controllers.Api
{
    public class TestController : ApiController
    {
        [Route("api/test/rebuild")]
        public string Test()
        {
            var noteManager = IoC.Instance.Resolve<INoteManager>();

            noteManager.RebuildIndex("2831333e-7f61-40dc-bc4d-ef85c6277dd2", null);

            return DateTime.Now.ToLongTimeString();
        }

        [Route("api/test/getnearest")]
        public string Test2()
        {
            var noteManager = IoC.Instance.Resolve<INoteManager>();

            var lat = 55.01483239352023;
            var lng = 82.95087408212277;
            var userId = "2831333e-7f61-40dc-bc4d-ef85c6277dd2";

            var notes = noteManager.GetNearest(userId, lat, lng, 100);

            return notes.Count().ToString();
        }
    }
}
