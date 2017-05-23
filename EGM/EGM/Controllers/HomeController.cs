using EGM.GameMachines.Core.DataAccess;
using EGM.GameMachines.Core.Repository;
using System.Web.Mvc;

namespace EGM.Controllers
{
    public class HomeController : Controller
    {        
        private readonly IGameMachineRepository _gamerepo;

        public HomeController(IGameMachineRepository gamerepo)
        {
            _gamerepo = gamerepo;
        }
        // GET Gamemachine
        public ActionResult Index()
        {
            return View();
        }

        // GET All Game machines
        public JsonResult GetAllGameMachines()
        {
            var gamemachineList = _gamerepo.GetAllGameMachines();
            return Json(gamemachineList, JsonRequestBehavior.AllowGet);            
        }
        //GET Game machine by Id
        public JsonResult GetGamemachineById(string id)
        {            
            var getgamemachineById = _gamerepo.GetGamemachineById(id);
            return Json(getgamemachineById, JsonRequestBehavior.AllowGet);            
        }
        //Update Game Machine
        public string UpdateGamemachine(GameMachine gamemachine)
        {
            if (_gamerepo.UpdateGamemachine(gamemachine))
                return "Game machine details updated successfully";
            else
                return "Invalid Game machine details.";
            
        }
        // Add Game machine
        public string AddGamemachine(GameMachine gamemachine)
        {
            if (_gamerepo.AddGamemachine(gamemachine))
                return "Game machine details added successfully";
            else
                return "Invalid Game machine details.";
            
        }
        // Delete Gamemachine
        public string DeleteGamemachine(string gamemachineId)
        {
            if (_gamerepo.DeleteGamemachine(gamemachineId))
                return "Selected Game machine deleted sucessfully";
            else
                return "Invalid operation";
            
        }
    }
}