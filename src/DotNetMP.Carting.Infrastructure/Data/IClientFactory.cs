namespace DotNetMP.Carting.Infrastructure.Data;

public interface IClientFactory<T>
{
    public T GetClient();
}
