using System.ComponentModel.DataAnnotations;

namespace EGM.GameMachines.Core.DataAccess
{
    /// <summary>
    /// Game Machine Entity class
    /// </summary>
    public class GameMachine
    {
        [Key]
        public int Id { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public string MachineType { get; set; }
        public string Vendor { get; set; }        
    }
}