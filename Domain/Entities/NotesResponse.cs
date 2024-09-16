using MyNotes.Domain.Extensions;

namespace MyNotes.Domain.Entities
{
    public class NotesResponse
    {
        public Note Note { get; set; }

        public List<NotesResponse> GetNotesResponses()
        {
            List<NotesResponse> ls = new List<NotesResponse>();

            if ("".IsNullOrEmpty())
            {
                return ls;
            }
            return null;
        }
    }
}
