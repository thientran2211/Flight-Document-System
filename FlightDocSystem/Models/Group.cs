using System.Security;

namespace FlightDocSystem.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string? GroupName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int NumberOfUser { get; set; }
        public int PermissionID { get; set; }

        //public ICollection<User> Users { get; set; }
    }
}
