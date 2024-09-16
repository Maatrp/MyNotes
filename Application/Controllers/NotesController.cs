using Microsoft.AspNetCore.Mvc;
using MyNotes.Application.Services;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Extensions;


namespace MyNotes.Application.Controllers
{
    [Route("api/NotesController")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly INotesService _notesService;
        private readonly ILogger<NotesController> _logger;

        public NotesController(IConfiguration configuration, INotesService notesService, ILogger<NotesController> logger)
        {
            _configuration = configuration;
            _notesService = notesService;
            _logger = logger;

        }

        // POST api/<NotesController>
        [HttpPost("create-note")]
        public void CreateNote([FromHeader] string title, [FromBody] List<string> text)
        {
            if (title.IsNullOrEmpty() && text.IsNullOrEmpty()) {
                _logger.LogWarning("El título y el texto de la nota no pueden estar vacíos.");
            }



        }

    }
}
