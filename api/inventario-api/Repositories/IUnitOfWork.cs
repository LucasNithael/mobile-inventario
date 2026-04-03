namespace inventario_api.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
