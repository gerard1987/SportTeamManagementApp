using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Entities
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }

        public int PlayerId { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }

        public int MatchId { get; set; }
        [JsonIgnore]
        public Match Match { get; set; }

        public int GoalScoredAgainstTeamId { get; set; }
        [JsonIgnore]
        public Team GoalScoredAgainstTeam { get; set; }

        public int GoalScoredForTeamId { get; set; }
        [JsonIgnore]
        public Team GoalScoredForTeam { get; set; }
    }

}
