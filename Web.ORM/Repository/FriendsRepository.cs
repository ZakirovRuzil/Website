using Npgsql;
using Web.ORM.Entities;

namespace Web.ORM.Repository;

public class FriendsRepository
{
    private DbContext.DbContext _context = new();
    
    // Метод получения всех сущностей
    public async Task<IEnumerable<Friend>> Select()
    {
        await _context.OpenConnection();
        
        string sqlExpression = "SELECT * FROM \"Friends\"";

        var command = _context.GetCommand(sqlExpression);

        NpgsqlDataReader reader = command.ExecuteReader();

        List<Friend> result = new List<Friend>();
        
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Friend friend = new Friend
                {
                    Id = (int)reader["Id"],
                    Picture = reader["Picture"].ToString()!,
                    Link = reader["Link"].ToString()!,
                };
                
                result.Add(friend);
            }
        }

        reader.Close();

        await _context.CloseConnection();
        return result;
    }
}