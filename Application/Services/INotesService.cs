using MyNotes.Domain.Entities;

namespace MyNotes.Application.Services
{
    public interface INotesService
    {
        public Note CreateNote(string title, List<string> text);
    }
}
