using System;

namespace NoteApp.Features.Create
{
    // Defines shape of note request when creating and updating
    public class NoteRequest
    {
        public string Title { get; set; }
        public string NoteText { get; set; }
    }

    public class UpdateNoteRequest
    {
        public string Title { get; set; }
        public string NoteText { get; set; }
    }
}
