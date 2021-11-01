using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.Features.Create;
using NoteApp.Models;

namespace NoteApp.Features
{
    // Class implementing interface INoteService 
    public class NoteService : INoteService
    {
        // Field
        private readonly DataContext _dataContext;

        // Constructor
        public NoteService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Add note method
        public async Task<Guid> AddNote(NoteRequest noteRequest)
        {
            // Map data to note structure
            var note = new Note
            {
                Title = noteRequest.Title,
                NoteText = noteRequest.NoteText,
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid()
            };

            // Add note to db and save
            await _dataContext.Notes.AddAsync(note);
            await _dataContext.SaveChangesAsync();

            // Returns Note ID
            return note.Id;
        }

        // Find note by ID
        public async Task<Note> GetById(Guid id)
        {
            var note = _dataContext.Notes.FindAsync(id);

            return await note; 
        }

        // Return all notes as a list
        public async Task<IEnumerable<Note>> List()
        {
            var notes =  _dataContext.Notes.ToListAsync();

            return await notes;
        }

        // Update note method
        public async Task<Note> UpdateNote(UpdateNoteRequest updateRequest, Guid id)
        {
            
            // Find note
            var note = await _dataContext.Notes.FindAsync(id);
            // Update the note in current context 
           note.Update(updateRequest.Title, updateRequest.NoteText);
            // Update note in DB and save 
            _dataContext.Notes.Update(note);
            await _dataContext.SaveChangesAsync();
            // Find updated note
            var updatedNote = _dataContext.Notes.FindAsync(id);

            // Return updated note 
            return await updatedNote;
        }

        // Delete Method (arg - id) 
        public async Task DeleteNote(Guid id)
        {
            // Find
            var note = _dataContext.Notes.FindAsync(id);
            // Remove & Save
            _dataContext.Notes.Remove(await note);
            await _dataContext.SaveChangesAsync();
        }
    }
}
