using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Autofac;
using MapNotes.BLL;
using MapNotes.BLL.Abstract.Managers;
using MapNotes.DTO.ApiModels.Request.Note;
using MapNotes.DTO.ApiModels.Response.Note;
using MapNotes.DTO.Models.Note;
using Microsoft.AspNet.Identity;

namespace MapNotes.Web.Controllers.Api
{
    [Authorize]
    public class MapController : ApiController
    {
        [HttpPost]
        [Route("api/notes")]
        public HttpResponseMessage CreateNote([FromBody]CreateNoteRequest request)
        {
            var noteManager = IoC.Instance.Resolve<INoteManager>();
            var userId = User.Identity.GetUserId();

            var model = new NoteModel
            {
                Title = request.Title,
                Latitude = request.Lattitude,
                Longitude = request.Longitude,
                UserId = userId
            };

            var noteId = noteManager.Repository.Create(model);
            noteManager.RebuildIndex(userId, noteId);

            Thread.Sleep(1000);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

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
    }
}
