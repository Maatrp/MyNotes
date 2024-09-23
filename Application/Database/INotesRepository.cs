using MyNotes.Domain.Entities;

namespace MyNotes.Application.Database
{
    public interface INotesRepository
    {
        bool CreateNote(Note note); 

        Note GetNote(int id);

        bool DelNote(int id);

        bool UpdateNote(Note note);

        List<Note> GetNotes();

    }
}