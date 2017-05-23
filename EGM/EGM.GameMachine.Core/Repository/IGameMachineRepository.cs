using EGM.GameMachines.Core.DataAccess;
using System.Collections.Generic;

namespace EGM.GameMachines.Core.Repository
{
    /// <summary>
    /// Game machine interface for dependency injection
    /// </summary>
    public interface IGameMachineRepository
    {
        List<GameMachine> GetAllGameMachines();
        GameMachine GetGamemachineById(string id);
        bool UpdateGamemachine(GameMachine gamemachine);
        bool AddGamemachine(GameMachine gamemachine);
        bool DeleteGamemachine(string gamemachineId);
    }
}
