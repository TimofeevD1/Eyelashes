namespace DataAccess.Interfaces
{
    public interface IDatabaseInitializer
    {
        Task ApplyMigrationsAsync();
    }
}