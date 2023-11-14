using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Entities
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int GoalScoredAgainstTeamId { get; set; }
        public Team GoalScoredAgainstTeam { get; set; }

        public int GoalScoredForTeamId { get; set; }
        public Team GoalScoredForTeam { get; set; }
    }

}
