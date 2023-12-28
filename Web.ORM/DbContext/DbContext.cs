using Npgsql;

namespace Web.ORM.DbContext;

public class DbContext
{
    private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=F5nKP62cR;Database=SiteData";
    private static readonly NpgsqlConnection Connection = new (ConnectionString);

    public async Task OpenConnection()
    {
        await Connection.OpenAsync();
        Console.WriteLine("Подключение открыто");
    }

    public async Task CloseConnection()
    {
        await Connection.CloseAsync();
        Console.WriteLine("Подключение закрыто");
    }

    public NpgsqlCommand GetCommand(string exp)
    {
        NpgsqlCommand command = new NpgsqlCommand(exp, Connection);
        return command;
    }
}