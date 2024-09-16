using MyNotes.Application.Database;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Extensions;

namespace MyNotes.Application.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;

        public NotesService(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public Note CreateNote(string title, List<string> text)
        {

            if (title.IsNullOrEmpty() && text.IsNullOrEmpty())
            {
                throw new NullReferenceException("El título y el texto de la nota no pueden estar vacíos.");
            }

            // Creamos la nota
            Note _note = new Note(title, text);

            return _note;
        }



    }
}
