using DotNetMP.Carting.Infrastructure.Data.Options;
using LiteDB;
using Microsoft.Extensions.Options;

namespace DotNetMP.Carting.Infrastructure.Data;

public class LiteDatabaseFactory : IClientFactory<ILiteDatabase>
{
    private readonly LiteDbOptions _settings;

    public LiteDatabaseFactory(IOptions<LiteDbOptions> options)
    {
        _settings = options.Value;
    }

    public ILiteDatabase GetClient()
    {
        return new LiteDatabase(_settings.ConnectionString);
    }
}
