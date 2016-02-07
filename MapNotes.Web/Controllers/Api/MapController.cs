using System.Collections.Generic;
using System.Web.Http;
using Autofac;
using MapNotes.BLL;
using MapNotes.BLL.Abstract.Managers;
using MapNotes.DTO.Models;

namespace MapNotes.Web.Controllers.Api
{
    [Authorize]
    public class MapController : ApiController
    {
        [Route("api/notes")]
        public IEnumerable<NoteModel> GetNotes()
        {
            var noteManager = IoC.Instance.Resolve<INoteManager>();

            var notes = noteManager.Repository.GetAll();

            return notes;
        }
    }
}
