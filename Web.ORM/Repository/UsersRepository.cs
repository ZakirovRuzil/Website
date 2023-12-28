using Npgsql;
using Web.ORM.Entities;

namespace Web.ORM.Repository;

public class UsersRepository
{
    private DbContext.DbContext _context = new();
    
    /// <summary>
    /// Метод получения всех сущностей
    /// </summary>
    /// <returns>Список сущностей</returns>
    public async Task<IEnumerable<User>> Select()
    {
        await _context.OpenConnection();
        
        string sqlExpression = "SELECT * FROM \"users\"";

        var command = _context.GetCommand(sqlExpression);

        NpgsqlDataReader reader = command.ExecuteReader();

        List<User> result = new List<User>();
        
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                User user = new User
                {
                    user_name = reader["user_name"].ToString()!,
                    user_password = reader["user_password"].ToString()!,
                    is_admin = (bool)reader["is_admin"]
                };
                
                result.Add(user);
            }
        }

        reader.Close();

        await _context.CloseConnection();
        return result;
    }

    /// <summary>
    /// Метод добавления сущности в базу данных
    /// </summary>
    /// <param name="entity"></param>
    // public async Task Add(User entity)
    // {
    //     await _context.OpenConnection();
    //
    //     string sqlExpression = "INSERT INTO \"users\" (\"user_name\", \"user_password\", \"is_admin\") VALUES (@user_name, @user_password, @is_admin)";
    //
    //     var command = _context.GetCommand(sqlExpression);
    //     
    //     command.Parameters.AddWithValue("@user_name", entity.user_name);
    //     command.Parameters.AddWithValue("@user_password", entity.user_password);
    //     command.Parameters.AddWithValue("@is_admin", entity.is_admin);
    //
    //     int rowsAffected = command.ExecuteNonQuery();
    //     await _context.CloseConnection();
    // }
}