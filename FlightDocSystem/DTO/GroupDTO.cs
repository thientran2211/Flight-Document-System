using System.ComponentModel.DataAnnotations;

namespace FlightDocSystem.DTO   
{
    public class GroupDTO
    {
        [Required]   
        public string? GroupName { get; set; }

        [Required]
        public int PermissionID { get; set; }

        public int NumberOfUser { get; set; }
    }
}
