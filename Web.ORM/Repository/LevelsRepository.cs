using Npgsql;
using Web.ORM.Entities;

namespace Web.ORM.Repository;

public class LevelsRepository
{
    private DbContext.DbContext _context = new();
    
    // Метод получения всех сущностей
    public async Task<IEnumerable<Level>> Select()
    {
        await _context.OpenConnection();
        
        string sqlExpression = "SELECT * FROM \"Levels\"";

        var command = _context.GetCommand(sqlExpression);

        NpgsqlDataReader reader = command.ExecuteReader();

        List<Level> result = new List<Level>();
        
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Level level = new Level
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()!,
                    Picture = reader["Picture"].ToString()!,
                };
                
                result.Add(level);
            }
        }

        reader.Close();

        await _context.CloseConnection();
        return result;
    }
}