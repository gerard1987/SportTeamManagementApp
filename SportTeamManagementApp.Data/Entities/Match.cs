using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        private int homeTeamScore;
        public int HomeTeamScore
        {
            get
            {
                return this.Goals.Count > 0 ? this.Goals.Where(g => g.GoalScoredForTeamId.Equals(this.HomeTeamId)).Select(g => g.Id).Count() : 0;
            }
            set
            {
                homeTeamScore = this.Goals.Count > 0 ? this.Goals.Where(g => g.GoalScoredForTeamId.Equals(this.HomeTeamId)).Select(g => g.Id).Count() : 0;
            }
        }
        private int awayTeamScore;
        public int AwayTeamScore
        {
            get
            {
                return this.Goals.Count > 0 ? this.Goals.Where(g => g.GoalScoredForTeamId.Equals(this.AwayTeamId)).Select(g => g.Id).Count() : 0;
            }
            set
            {
                awayTeamScore = this.Goals.Count > 0 ? this.Goals.Where(g => g.GoalScoredForTeamId.Equals(this.AwayTeamId)).Select(g => g.Id).Count() : 0;
            }
        }

        public List<Goal> Goals { get; set; } = new List<Goal>();
    }
}
