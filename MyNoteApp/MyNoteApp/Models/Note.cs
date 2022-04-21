namespace MyNoteApp.Models
{
    public class Note
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string NoteId { get; set; } //for telling apart notes

        public Note(string title, DateTime date, string text)
        {
            Title = title;  
            Date = date;
            Text = text;
            NoteId = Guid.NewGuid().ToString();
        }

        
    }
}
