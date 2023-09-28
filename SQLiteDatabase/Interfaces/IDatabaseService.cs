using Microsoft.EntityFrameworkCore;
using SQLiteDatabase.Models;
using System.Threading.Tasks;

namespace SQLiteDatabase.Interfaces
{
    public interface IDatabaseService
    {
        Task InitializeAsync(); // Método para inicializar y migrar la base de datos
        DbContext GetContext(); // Método para obtener el contexto de la base de datos
    }
}
