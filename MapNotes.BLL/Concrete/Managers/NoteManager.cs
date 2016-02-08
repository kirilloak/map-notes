using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using MapNotes.BLL.Abstract.Managers;
using MapNotes.DAL.Abstract;
using MapNotes.DTO.ElasticModels;
using MapNotes.DTO.Models.Note;
using Nest;

namespace MapNotes.BLL.Concrete.Managers
{
    public class NoteManager : INoteManager
    {
        public INoteRepository Repository { get; set; }

        public NoteManager()
        {
            Repository = IoC.Instance.Resolve<INoteRepository>();
        }

        public IEnumerable<NearestNoteModel> GetNearest(string userId, double latitude, double longitude, int distance)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"), "mapnotes");
            var client = new ElasticClient(settings);

            var result = client.Search<NoteIndex>(s => s
                .Take(9999)
                .Index("mapnotes")
                .Query(q => q.Match(m => m.OnField(f => f.UserId).Query(userId)))
                .Filter(f => f.GeoDistance(l => l.Location, d => d.Distance(distance, GeoUnit.Kilometers)
                                    .Location(latitude, longitude)
                                    .Optimize(GeoOptimizeBBox.Indexed))));

            var notes = result.Documents.Select(x => new NearestNoteModel
            {
                Title = x.Title,
                Latitude = x.Location.Lat,
                Longitude = x.Location.Lon
            });

            return notes;
        }

        public void RebuildIndex(string userId, int? noteId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                var notes = new List<NoteModel>();

                if (noteId.HasValue)
                    notes.Add(Repository.GetById(noteId.Value));
                else
                    notes = Repository.GetByUserId(userId).ToList();
                

                var settings = new ConnectionSettings(new Uri("http://localhost:9200"), "mapnotes");
                var client = new ElasticClient(settings);


                var indexExists = client.IndexExists("mapnotes");
                if (!indexExists.Exists)
                {
                    client.CreateIndex(descriptor => descriptor.Index("mapnotes")
                            .AddMapping<NoteIndex>(m => m.Properties(p => p.GeoPoint(d => d.Name(f => f.Location).IndexLatLon()))));
                }

                foreach (var note in notes)
                {
                    client.Index(new NoteIndex
                    {
                        Id = note.Id,
                        UserId = note.UserId,
                        Title = note.Title,
                        Location = new Location(note.Latitude, note.Longitude)
                    });
                }
            }
        }
    }
}
