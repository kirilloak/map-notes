using System;
using System.Linq;
using System.Web.Http;
using Autofac;
using MapNotes.BLL;
using MapNotes.BLL.Abstract.Managers;
using MapNotes.DTO.ApiModels.Request.Note;
using MapNotes.DTO.ApiModels.Response.Note;
using Microsoft.AspNet.Identity;

namespace MapNotes.Web.Controllers.Api
{
    [Authorize]
    public class MapController : ApiController
    {
        [HttpPost]
        [Route("api/notes/getnearest")]
        public NearestNoteResponse GetNearestNotes([FromBody]GetNearestNoteRequest request)
        {
            var noteManager = IoC.Instance.Resolve<INoteManager>();
            var userId = User.Identity.GetUserId();

            var response = new NearestNoteResponse
            {
                Notes = noteManager.GetNearest(userId, request.Lattitude, request.Longitude, request.Distance)
            };

            return response;
        }

        [Route("api/test")]
        public string Test()
        {
            var noteManager = IoC.Instance.Resolve<INoteManager>();

            noteManager.RebuildIndex("082f486d-8e89-4f70-a6b6-b2738cbd9bda");

            return DateTime.Now.ToLongTimeString();
        }

        [Route("api/test2")]
        public string Test2()
        {
            var noteManager = IoC.Instance.Resolve<INoteManager>();

            var lat = 55.01483239352023;
            var lng = 82.95087408212277;
            var userId = "082f486d-8e89-4f70-a6b6-b2738cbd9bda";

            var notes = noteManager.GetNearest(userId, lat, lng, 100);

            return notes.Count().ToString();
        }
    }
}
