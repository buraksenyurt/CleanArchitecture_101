using GamersWorld.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

namespace GamersWorld.Data.Contexts;

public class ArchiveDataService
: IArchiveService
{
    private readonly string _connStr;

    public ArchiveDataService(IConfiguration configuration)
    {
        _connStr = configuration.GetConnectionString("DevConStr");
    }
    public async Task<int> MoveAsync(int keyId)
    {
        using IDbConnection conn = new NpgsqlConnection(_connStr);
        string sql = "UPDATE \"Games\" SET \"IsArchived\" = TRUE WHERE \"Id\" = @GameId";
        var parameters = new { GameId = keyId };
        var updated = await conn.ExecuteAsync(sql, parameters);
        return updated;
    }
}