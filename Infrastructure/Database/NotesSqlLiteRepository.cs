using Microsoft.Data.Sqlite;
using MyNotes.Application.Database;
using MyNotes.Domain.Entities;

namespace MyNotes.Infrastructure.Database
{
    public class NotesSqlLiteRepository : INotesRepository
    {
        // READONLY: Se inicializa en el contructor o cuando creas una propiedad igualandola a new
        private readonly IConfiguration _configuration;

        // Inyectar IConfiguration para acceder a appsettings.json
        public NotesSqlLiteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CreateNote(Note note)
        {
            bool isCreated = false;

            try
            {
                // Obtenenemos la cadena de conexión desde appsettings.json
                string connectionString = _configuration.GetConnectionString("SQLiteConnection");

                // Extraemos title y text de Note para insertalo en la query
                string title = note.Title;
                List<string> text = note.Text;

                // Creamos la query
                string query = "INSERT INTO Notes (Title, Text) VALUES (@Title, @Text)";

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Envio de query a base de datos
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Text", string.Join("\n", text));  // Convierte el texto a una sola cadena
                        command.ExecuteNonQuery();
                    }
                }

                isCreated = true;
            }
            catch (SqliteException ex)
            {
                throw new ApplicationException("No se ha podido crear la nota en base de datos" + ex.Message);
            }



            return isCreated;
        }

        public bool DelNote(int id)
        {
            throw new NotImplementedException();
        }

        public Note GetNote(int id)
        {
            throw new NotImplementedException();
        }

        public ArrayList<Note> GetNotes(ArrayList<int> ids)
        {
            throw new NotImplementedException();
        }

        public Note UpdateNote(int id)
        {
            throw new NotImplementedException();
        }
    }
}
