using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.DB.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<TransactionTypes>();
    }
}