using MyNotes.Domain.Entities;

namespace MyNotes.Application.Services
{
    public interface INotesService
    {
        public Note CreateNote(string title, List<string> text);

        public Note GetNote(int id);

        public bool DelNote(int id);

        public Note UpdateNote(int id, string title, List<string> text);

        List<Note> GetNotes();


    }
}
