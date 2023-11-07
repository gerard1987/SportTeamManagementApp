using System.ComponentModel.DataAnnotations;

namespace SportTeamManagementApp.Data.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoachId { get; set; }
    }
}
