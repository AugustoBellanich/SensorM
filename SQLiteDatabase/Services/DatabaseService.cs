using System.Threading.Tasks;
using SQLiteDatabase.Interfaces;
using SQLiteDatabase.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace SQLiteDatabase.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly AppDbContext _dbContext;

        public DatabaseService()
        {
            _dbContext = new AppDbContext();
        }

        public async Task InitializeAsync()
        {
            // Aplica las migraciones pendientes y crea la base de datos si no existe
            await _dbContext.Database.MigrateAsync();
        }

        public DbContext GetContext()
        {
            return _dbContext;
        }
    }
}


