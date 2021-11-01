using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteApp.Data;
using NoteApp.Features.Create;
using NoteApp.Models;

namespace NoteApp.Features
{
    // Interface defining the methods we want in NoteApp
    public interface INoteService
    {
        Task<Guid> AddNote(NoteRequest noteRequest);
        Task<Note> GetById(Guid id);
        public Task<IEnumerable<Note>> List();
        Task<Note> UpdateNote(UpdateNoteRequest updateRequest, Guid id);
        Task DeleteNote(Guid id);
    }
}
