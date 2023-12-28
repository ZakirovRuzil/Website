using Npgsql;
using Web.ORM.Entities;

namespace Web.ORM.Repository;

public class BossesRepository
{
    private DbContext.DbContext _context = new();
    
    /// <summary>
    /// Метод получения всех сущностей
    /// </summary>
    /// <returns>Список сущностей</returns>
    public async Task<IEnumerable<Boss>> Select()
    {
        await _context.OpenConnection();
        
        string sqlExpression = "SELECT * FROM \"Bosses\"";

        var command = _context.GetCommand(sqlExpression);

        NpgsqlDataReader reader = command.ExecuteReader();

        List<Boss> result = new List<Boss>();
        
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Boss boss = new Boss
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()!,
                    Picture = reader["Picture"].ToString()!,
                    Discription = reader["Discription"].ToString()!
                };
                
                result.Add(boss);
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
    public async Task Add(Boss entity)
    {
        await _context.OpenConnection();

        string sqlExpression = "INSERT INTO \"Bosses\" (\"Name\", \"Picture\", \"Discription\") VALUES (@Name, @Picture, @Discription)";

        var command = _context.GetCommand(sqlExpression);
        
        command.Parameters.AddWithValue("@Name", entity.Name);
        command.Parameters.AddWithValue("@Picture", entity.Picture);
        command.Parameters.AddWithValue("@Discription", entity.Discription);

        int rowsAffected = command.ExecuteNonQuery();
        await _context.CloseConnection();
    }
    /// <summary>
    /// Метод удаления сущности из базы данных
    /// </summary>
    /// <param name="id"></param>
    public async Task Delete(string id)
    {
        await _context.OpenConnection();

        string sqlExpression = "DELETE FROM \"Bosses\" WHERE \"Id\" = @Id";

        var command = _context.GetCommand(sqlExpression);
        

        command.Parameters.AddWithValue("@Id", int.Parse(id));

        int rowsAffected = command.ExecuteNonQuery();

        await _context.CloseConnection();
    }
    
    /// <summary>
    /// Метод изменения сущности в базе данных
    /// </summary>
    /// <param name="entity"></param>
    public async Task Edit(Boss entity)
    {
        await _context.OpenConnection();

        string sqlExpression = "UPDATE \"Bosses\" SET \"Name\" = @Name, \"Picture\" = @Picture, \"Discription\" = @Discription WHERE \"Id\" = @Id";

        var command = _context.GetCommand(sqlExpression);

        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@Name", entity.Name);
        command.Parameters.AddWithValue("@Picture", entity.Picture);
        command.Parameters.AddWithValue("@Discription", entity.Discription);

        int rowsAffected = command.ExecuteNonQuery();

        await _context.CloseConnection();
    }
}