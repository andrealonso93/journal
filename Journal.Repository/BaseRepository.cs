using Microsoft.Extensions.Configuration;

namespace Journal.Repository;

public class BaseRepository
{
    private readonly string connectionString;

    public BaseRepository(IConfiguration config)
    {
        this.connectionString = config.GetConnectionString("SQL_Connection");
    }
}