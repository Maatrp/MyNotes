
namespace MyNotes.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<string> Text { get; set; }

        public DateTime TimeCreation { get; set; }

        public DateTime TimeUpdated { get; set; }

        public Note(int id,  string title, List<string> text, DateTime timeCreation, DateTime timeUpdated)
        {
            Id = id;
            Title = title;
            Text = text;
            TimeCreation = timeCreation;
            TimeUpdated = timeUpdated;
        }

        public Note(string title, List<string> text, DateTime timeCreation, DateTime timeUpdated)
        {

            Title = title;
            Text = text;
            TimeCreation = timeCreation;
            TimeUpdated = timeUpdated;
        }

        public Note(int id, string title, List<string> text, DateTime timeUpdated)
        {
            Id = id;
            Title = title;
            Text = text;
            TimeUpdated = timeUpdated;
        }

    }
}
