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

            DateTime timeCreation = DateTime.Now;
            DateTime timeUpdated = DateTime.Now;

            // Creamos la nota
            Note note = new Note(title, text, timeCreation, timeUpdated);

            _notesRepository.CreateNote(note);

            return note;
        }

        public Note GetNote(int id)
        {
            if (id.IsNullOrEmpty()) {
                throw new NullReferenceException("El id no es correcto.");
            }

            Note note= _notesRepository.GetNote(id);

            return note;

        }
    }
}
