using Microsoft.AspNetCore.Http.HttpResults;
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

                // Creamos la query
                string query = "INSERT INTO note (Title, Text, Tc, Tu) VALUES (@Title, @Text, @TimeCreation, @TimeUpdated)";

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Envio de query a base de datos
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", note.Title);
                        command.Parameters.AddWithValue("@Text", string.Join("\n", note.Text));  // Convierte el texto a una sola cadena
                        command.Parameters.AddWithValue("@TimeCreation", note.TimeCreation);
                        command.Parameters.AddWithValue("@TimeUpdated", note.TimeUpdated);
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

        public Note GetNote(int id)
        {
            Note note = null;

            try
            {
                // Obtenenemos la cadena de conexión desde appsettings.json
                string connectionString = _configuration.GetConnectionString("SQLiteConnection");

                // Creamos la query
                string query = "SELECT * FROM note WHERE id = @id";

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Envío de query a base de datos
                    using (var command = new SqliteCommand(query, connection))
                    {
                        // Agregamos el parámetro @id a la query
                        command.Parameters.AddWithValue("@id", id);

                        // Usamos ExecuteReader para obtener los resultados
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())  // Si hay resultados
                            {
                                // Mapeamos los resultados de la base de datos al objeto Note
                                note = new Note(
                                    id,
                                    reader["Title"].ToString(),
                                    reader["Text"].ToString().Split('\n').ToList(),
                                    DateTime.Parse(reader["Tc"].ToString()),
                                    DateTime.Parse(reader["Tu"].ToString())
                                );
                            }
                        }
                    }
                }

            }
            catch (SqliteException ex)
            {
                throw new ApplicationException("No se ha podido encontrar la nota en base de datos" + ex.Message);
            }


            return note;
        }

        public bool DelNote(int id)
        {
            bool isDeleted = false;

            try
            {
                // Obtenenemos la cadena de conexión desde appsettings.json
                string connectionString = _configuration.GetConnectionString("SQLiteConnection");

                // Creamos la query
                string query = "DELETE FROM note WHERE id = @id";

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Envío de query a base de datos
                    using (var command = new SqliteCommand(query, connection))
                    {
                        // Agregamos el parámetro @id a la query
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                    }
                }

                isDeleted = true;
            }
            catch (SqliteException ex)
            {
                throw new ApplicationException("No se ha podido eliminar la nota en base de datos" + ex.Message);
            }


            return isDeleted;
        }


        public bool UpdateNote(Note note)
        {
            bool isUpdated = false;

            try
            {
                // Obtenenemos la cadena de conexión desde appsettings.json
                string connectionString = _configuration.GetConnectionString("SQLiteConnection");

                // Creamos la query
                string query = "UPDATE note " +
                       "SET Title = @Title, Text = @Text, Tu = @TimeUpdated " +
                       "WHERE id = @id";

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Envio de query a base de datos
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", note.Id);
                        command.Parameters.AddWithValue("@Title", note.Title);
                        command.Parameters.AddWithValue("@Text", string.Join("\n", note.Text));
                        command.Parameters.AddWithValue("@TimeUpdated", note.TimeUpdated);
                        command.ExecuteNonQuery();
                    }
                }

                isUpdated = true;

            }
            catch (SqliteException ex)
            {
                throw new ApplicationException("No se ha podido actualizar la nota en base de datos" + ex.Message);
            }


            return isUpdated;
        }

        public List<Note> GetNotes()
        {
            List<Note> noteList = new List<Note>();

            try
            {
                // Obtenenemos la cadena de conexión desde appsettings.json
                string connectionString = _configuration.GetConnectionString("SQLiteConnection");

                // Creamos la query
                string query = "SELECT * FROM note";

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Envío de query a base de datos
                    using (var command = new SqliteCommand(query, connection))
                    {

                        // Usamos ExecuteReader para obtener los resultados
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())  // Mientras haya resultados los añade en la lista
                            {
                                // Mapeamos los resultados de la base de datos al objeto Note
                                Note note = new Note(
                                    Convert.ToInt32(reader["id"]),
                                    reader["Title"].ToString(),
                                    reader["Text"].ToString().Split('\n').ToList(),
                                    DateTime.Parse(reader["Tc"].ToString()),
                                    DateTime.Parse(reader["Tu"].ToString())
                                );
                                noteList.Add(note);
                            }
                        }
                    }
                }

            }
            catch (SqliteException ex)
            {
                throw new ApplicationException("No se ha podido extraer la lista de base de datos" + ex.Message);
            }


            return noteList;
        }
    }
}
