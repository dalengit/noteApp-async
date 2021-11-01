using System;

namespace NoteApp.Models
{
    // Defines note structure 
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string NoteText { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }

        // Constructor
        public void Update(string title, string text)
        {
            Title = title;
            NoteText = text;
            LastUpdated = DateTime.Now;
        }
    }
}
