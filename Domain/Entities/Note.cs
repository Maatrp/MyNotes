
namespace MyNotes.Domain.Entities
{
    public class Note
    {
        public string Title { get; set; }

        public List<string> Text { get; set; }

        public Note(string title, List<string> text)
        {

            Title = title;
            Text = text;
        }

    }
}
