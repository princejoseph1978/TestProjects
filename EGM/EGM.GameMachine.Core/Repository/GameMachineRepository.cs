using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using EGM.GameMachines.Core.DataAccess;
using LoggingExtensions.Logging;

namespace EGM.GameMachines.Core.Repository
{
    /// <summary>
    /// Game machine repository class to handle database fuctionality
    /// </summary>
    public class GameMachineRepository : IGameMachineRepository
    {
        private GameMachineDBContext contextObj = new GameMachineDBContext();
        private readonly ILog logger;

        public GameMachineRepository()
        {
            Log.InitializeWith<LoggingExtensions.log4net.Log4NetLog>();
            logger = Log.GetLoggerFor("RollingFileAppender");
            
        }
        //Game machines
        public List<GameMachine> GetAllGameMachines()
        {
             return contextObj.gamemachine.ToList();            
        }
        //Game machine by Id
        public GameMachine GetGamemachineById(string id)
        {
            int gid = 0;
            GameMachine result = null;
            if (int.TryParse(id, out gid))
            {
                var gamemachineId = Convert.ToInt32(id);
                result = contextObj.gamemachine.Find(gamemachineId);
            }           
            return result;
        }
        //Update Game Machine
        public bool UpdateGamemachine(GameMachine gamemachine)
        {
            bool result = false;
            if (gamemachine != null)
            {
                int gamemachineId = Convert.ToInt32(gamemachine.Id);
                using (var scope = new TransactionScope())
                {
                    GameMachine _gamemachine = contextObj.gamemachine.Where(b => b.Id == gamemachineId).FirstOrDefault();
                    _gamemachine.MachineName = gamemachine.MachineName;
                    _gamemachine.Description = gamemachine.Description;
                    _gamemachine.Vendor = gamemachine.Vendor;
                    _gamemachine.MachineType = gamemachine.MachineType;
                    contextObj.SaveChanges();
                    scope.Complete();
                    result = true;

                    logger.Info("Updated GameMachine. " + gamemachine.MachineName);
                }
                             
            }
            return result;
        }
        // Add Game machine
        public bool AddGamemachine(GameMachine gamemachine)
        {
            bool result = false;
            if (gamemachine != null)
            {
                using (var scope = new TransactionScope())
                {
                    contextObj.gamemachine.Add(gamemachine);
                    contextObj.SaveChanges();
                    scope.Complete();
                    result = true;

                    logger.Info( "Added GameMachine. " + gamemachine.MachineName);
                }
                
            }
            return result;
        }
        // Delete Gamemachine
        public bool DeleteGamemachine(string gamemachineId)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(gamemachineId))
            {
                try
                {
                    int _gamemachineId = Int32.Parse(gamemachineId);
                    using (var scope = new TransactionScope())
                    {
                        var _gamemachine = contextObj.gamemachine.Find(_gamemachineId);
                        contextObj.gamemachine.Remove(_gamemachine);
                        contextObj.SaveChanges();
                        scope.Complete();
                        result = true;
                        logger.Info("Deleted GameMachine. " + _gamemachine.MachineName);
                    }
                   
                }
                catch (Exception ex)
                {
                    logger.Fatal(
                    "Exception in GameMachineRepository-DeleteGamemachine." + ex.Message, ex);
                    result = false;
                }
            }
           return result;
        }
    }
}
