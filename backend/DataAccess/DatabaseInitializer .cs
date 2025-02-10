using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly AppContext _context;

        public DatabaseInitializer(AppContext context)
        {
            _context = context;
        }

        public async Task ApplyMigrationsAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
