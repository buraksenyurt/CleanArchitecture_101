using GamersWorld.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;

namespace GamersWorld.Data.Contexts;

public class ArchiveDataService
: IArchiveService
{
    private readonly string _connStr;

    public ArchiveDataService(IConfiguration configuration)
    {
        _connStr = configuration.GetConnectionString("DevConStr");
    }
    public async Task MoveAsync(int keyId)
    {
        using (var conn = new SqlConnection(_connStr))
        {
            string sql = "UPDATE Games SET IsArchived = 1 WHERE Id = @GameId";
            var parameters = new { GameId = keyId };
            await conn.ExecuteAsync(sql, parameters);
        }
    }
}