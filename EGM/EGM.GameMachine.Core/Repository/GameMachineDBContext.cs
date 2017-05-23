using System.Data;
using System.Data.Entity;
using EGM.GameMachines.Core.DataAccess;
namespace EGM.GameMachines.Core.Repository
{
    /// <summary>
    /// Game machine database context class for database operations
    /// using EF
    /// </summary>
    public class GameMachineDBContext : DbContext
    {
        public DbSet<GameMachine> gamemachine { get; set; }        
    }
}