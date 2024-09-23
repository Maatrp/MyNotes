using MyNotes.Domain.Entities;

namespace MyNotes.Application.Database
{
    public interface INotesRepository
    {
        bool CreateNote(Note note); 

        Note GetNote(int id);

        bool DelNote(int id);

        Note UpdateNote(int id);

        List<Note> GetNotes(List<int> ids);

    }
}