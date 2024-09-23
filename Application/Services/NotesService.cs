using MyNotes.Application.Database;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Extensions;
using System.Collections.Generic;

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
            if (id.IsNullOrEmpty())
            {
                throw new NullReferenceException("El id no es correcto.");
            }

            Note note = _notesRepository.GetNote(id);

            return note;

        }

        public bool DelNote(int id)
        {
            bool isDeleted = false;

            if (id.IsNullOrEmpty())
            {
                throw new NullReferenceException("El id no es correcto.");
            }

            isDeleted = _notesRepository.DelNote(id);

            return isDeleted;

        }

        public Note UpdateNote(int id, string title, List<string> text)
        {
            if (id > 0 && title.IsNullOrEmpty() && text.IsNullOrEmpty())
            {
                throw new NullReferenceException("El id, el título y el texto de la nota no pueden estar vacíos.");
            }


            DateTime timeUpdated = DateTime.Now;

            // Creamos la nota
            Note note = new Note(id, title, text, timeUpdated);

            _notesRepository.UpdateNote(note);

            return note;
        }

        public List<Note> GetNotes()
        {
            List<Note> noteList = _notesRepository.GetNotes();

            if (!noteList.Any()) {
                throw new NullReferenceException("No hay ninguna nota");
            }

            return noteList;

        }
    }
}
