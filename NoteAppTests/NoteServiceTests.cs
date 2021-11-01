using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.Features;
using NoteApp.Features.Create;
using NoteApp.Models;
using NUnit.Framework;

namespace NoteAppTests
{
    public class NoteServiceTests
    {
        private DataContext _dataContext;
        private NoteService _subject;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("Notes");
            _dataContext = new DataContext(builder.Options);

            _subject = new NoteService(_dataContext);
        }

        [Test]
        public void Adds_note()
        {
            var note = new NoteRequest()
            {
                Title = "Title",
                NoteText = "I am a note and I am going into the db!"
            };

            var result = _subject.AddNote(note);

            var addedNote = _dataContext.Notes.Find(result);

            Assert.That(addedNote.NoteText, Is.EqualTo(note.NoteText));
        }

        [Test]
        public void Deletes_notes()
        {
            var note = new Note()
            {
                Id = Guid.NewGuid(),
                Title = "Delete Test",
                NoteText = "Note deleted test"
            };
            _dataContext.Notes.Add(note);
            _dataContext.SaveChanges();

            _subject.DeleteNote(note.Id);

            var deletedNote = _dataContext.Notes.Find(note.Id);

            Assert.That(deletedNote, Is.Null);
        }

        [Test]

        public void Updates_note()
        {
            var note = new Note()
            {
                Id = Guid.NewGuid(),
                Title = "Note Test",
                NoteText = "Note Added test",
                LastUpdated = new DateTime(2020, 01, 01)
            };

            // Note added
            _dataContext.Notes.Add(note);
            _dataContext.SaveChanges();

            // Update note request
            var updatedNoteRequest = new UpdateNoteRequest()
            {
                Title = "Updated Note",
                NoteText = "Updated note test"
            };

            var updatedNote = _subject.UpdateNote(updatedNoteRequest, note.Id);

            Assert.That(updatedNote.Title, Is.EqualTo(updatedNoteRequest.Title));
            Assert.That(updatedNote.NoteText, Is.EqualTo(updatedNoteRequest.NoteText));
            Assert.That(updatedNote.LastUpdated.Value.Date, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Gets_note_by_id()
        {
            var note = new Note()
            {
                Id = Guid.NewGuid(),
                Title = "Note Test",
                NoteText = "Note Added test"
            };

            _dataContext.Notes.Add(note);
            _dataContext.SaveChanges();

            var result = _subject.GetById(note.Id);

            Assert.That(result.Id, Is.EqualTo(note.Id));
            Assert.That(result.Title, Is.EqualTo(note.Title));
            Assert.That(result.NoteText, Is.EqualTo(note.NoteText));
        }

        [Test]

        public void Lists_notes()
        {
            var newNote = new Note()
            {
                Id = Guid.NewGuid(),
                Title = "New note",
                NoteText = "New note",
                LastUpdated = new DateTime(2020, 01, 01)
            };

            var newNote2 = new Note()
            {
                Id = Guid.NewGuid(),
                Title = "New Note 2",
                NoteText = "New note 2",
                LastUpdated = new DateTime(2020, 01, 01)
            };

            var listedNotes = new List<Note>()
            {
                newNote,
                newNote2,
            };

            _dataContext.Add(newNote);
            _dataContext.Add(newNote2);
            _dataContext.SaveChanges();

            var result = _subject.List();

            Assert.That(result, Is.EqualTo(listedNotes));
            var id = listedNotes.Single(x => x.Id == newNote.Id).Id;
        }
    }
}