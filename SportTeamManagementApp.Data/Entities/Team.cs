using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SportTeamManagementApp.Data.Entities
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CoachId { get; set; } // Foreign key for one-to-one relationship
        public Coach Coach { get; set; } // Navigation property for one-to-one relationship

        public List<Player> Players { get; set; } // Navigation property for one-to-many relationship

        public Team()
        {
            Players = new List<Player>();
        }

        public Team(string name, Coach coach, List<Player> players)
        {
            this.Name = name;
            this.Coach = coach;
            this.Players = players;
            this.CoachId = coach.Id;
        }
    }
}
