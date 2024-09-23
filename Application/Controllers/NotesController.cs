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
        public IActionResult CreateNote([FromHeader] string title, [FromBody] List<string> text)
        {
            if (title.IsNullOrEmpty() && text.IsNullOrEmpty())
            {
                String textMessage = "El título y el texto de la nota no pueden estar vacíos.";
                _logger.LogWarning(textMessage);
                return BadRequest(textMessage);
            }

            try
            {
                Note note = _notesService.CreateNote(title, text);

                return Ok(note);
            }
            catch (Exception ex)
            {
                String textMessage = "Ha ocurrido un error";
                _logger.LogError(textMessage + ex);
                return BadRequest(textMessage);
            }
        }

        // GET api/<NotesController>
        [HttpGet("get-note/{id}")]
        public IActionResult GetNote([FromRoute] int id)
        {
            if (id.IsNullOrEmpty())
            {
                String textMessage = "El id no es correcto.";
                _logger.LogWarning(textMessage);
                return BadRequest(textMessage);
            }

            try
            {
                Note note = _notesService.GetNote(id);

                return Ok(note);
            }
            catch (Exception ex)
            {
                String textMessage = "Ha ocurrido un error";
                _logger.LogError(textMessage + ex);
                return BadRequest(textMessage);
            }
        }

        // DELETE api/<NotesController>
        [HttpDelete("del-note/{id}")]
        public IActionResult DelNote([FromRoute] int id)
        {

            if (id.IsNullOrEmpty())
            {
                String textMessage = "El id no es correcto.";
                _logger.LogWarning(textMessage);
                return BadRequest(textMessage);
            }

            try
            {
                bool isDeleted = _notesService.DelNote(id);

                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                String textMessage = "Ha ocurrido un error";
                _logger.LogError(textMessage + ex);
                return BadRequest(textMessage);
            }
        }

        // PUT api/<NotesController>
        [HttpPut("update-note/{id}")]
        public IActionResult UpdateNote([FromRoute] int id, [FromHeader] string title, [FromBody] List<string> text)
        {
            if (title.IsNullOrEmpty() && text.IsNullOrEmpty())
            {
                String textMessage = "EEl id, el título y el texto de la nota no pueden estar vacíos.";
                _logger.LogWarning(textMessage);
                return BadRequest(textMessage);
            }

            try
            {
                Note note = _notesService.UpdateNote(id, title, text);

                return Ok(note);
            }
            catch (Exception ex)
            {
                String textMessage = "Ha ocurrido un error";
                _logger.LogError(textMessage + ex);
                return BadRequest(textMessage);
            }
        }

        // GET api/<NotesController>
        [HttpGet("get-notes-list")]
        public IActionResult GetNotes()
        {
            try
            {
                List<Note> noteList = _notesService.GetNotes();

                if (!noteList.Any())
                {
                    String textMessage = "La lista esta vacía";
                    _logger.LogWarning(textMessage);
                    return BadRequest(textMessage);
                }

                return Ok(noteList);
            }
            catch (Exception ex)
            {
                String textMessage = "Ha ocurrido un error";
                _logger.LogError(textMessage + ex);
                return BadRequest(textMessage);
            }
        }

    }
}
