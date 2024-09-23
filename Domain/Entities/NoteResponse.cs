using MyNotes.Domain.Extensions;

namespace MyNotes.Domain.Entities
{
    public class NoteResponse
    {
        public Note Note { get; set; }

        public List<NoteResponse> GetNotesResponses()
        {
            List<NoteResponse> ls = new List<NoteResponse>();

            if ("".IsNullOrEmpty())
            {
                return ls;
            }
            return null;
        }
    }
}
