using SportTeamManagementApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data
{
    public class DataProvider
    {
        private AppDbContext _dbContext;

        public DataProvider(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        #region GETS

        public List<Coach> Coaches
        {
            get
            {
                return _dbContext.Coaches.ToList();
            }
        }
        public List<Player> Players
        {
            get
            {
                return _dbContext.Players.ToList();
            }
        }
        public List<Team> Teams
        {
            get
            {
                List<Team> teams = _dbContext.Teams.ToList();
                foreach (Team team in teams)
                {
                    team.Coach = _dbContext.Coaches.FirstOrDefault(c => c.teamId.Equals(team.Id));
                    team.Players = _dbContext.Players.Select(p => p).Where(p => p.teamId.Equals(team.Id)).ToList();
                }

                return teams;
            }
        }

        public List<Player> GetAvailablePlayers()
        {
            return _dbContext.Players.ToList()
                        .Where(p => p.teamId.Equals(null))
                        .Select(player => player)
                        .ToList();
        }

        public List<Coach> GetAvailableCoaches()
        {
            return _dbContext.Coaches.ToList()
                      .Where(t => t.teamId.Equals(null))
                      .Select(c => c).ToList();
        }

        public List<Team> GetAvailableTeams()
        {
            return _dbContext.Teams.ToList().Select(t => t).ToList();
        }

        public Team GetTeamComplete(int teamId)
        {
            Team team = _dbContext.Teams.FirstOrDefault(t => t.Id.Equals(teamId));

            // Retrieve the coach for the specified team
            team.Coach = _dbContext.Coaches.FirstOrDefault(c => c.teamId == team.Id);

            // Retrieve the players for the specified team
            team.Players = _dbContext.Players
                .Where(p => p.teamId == team.Id)
                .ToList();

            return team;
        }

        #endregion

        #region CREATES

        public void CreatePlayer(Player player)
        {
            Player playerAlreadyExists = _dbContext.Players.Find(player.Id);

            if (playerAlreadyExists is null)
            {
                _dbContext.Players.Add(player);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Player already exists!");
            }
        }

        public void CreateCoach(Coach coach)
        {
            Coach coachAlreadyExists = _dbContext.Coaches.Find(coach.Id);

            if (coachAlreadyExists is null)
            {
                _dbContext.Coaches.Add(coach);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Coach already exists!");
            }
        }

        public void CreateTeam(Team team)
        {
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();
        }

        #endregion

        #region EDITS

        public void EditTeam(Team team)
        {
            Team teamToEdit = _dbContext.Teams.Find(team.Id);
            teamToEdit = team;

            _dbContext.SaveChanges();
        }

        public void EditCoach(Coach coach)
        {
            Coach coachToEdit = _dbContext.Coaches.Find(coach.Id);
            coachToEdit = coach;

            _dbContext.SaveChanges();
        }

        public void EditPlayer(Player player)
        {
            Player playerToEdit = _dbContext.Players.Find(player.Id);
            playerToEdit = player;

            _dbContext.SaveChanges();
        }

        public void AddCoachToTeam(Team team)
        {
            Coach coachToUpdate = _dbContext.Coaches.Find(team.Coach.Id);
            coachToUpdate.teamId = team.Id;

            _dbContext.SaveChanges();
        }

        #endregion

        #region DELETES
        public void RemoveTeam(Team team)
        {
            Team teamToDelete = _dbContext.Teams.Find(team.Id);

            _dbContext.Teams.Remove(teamToDelete);
            _dbContext.SaveChanges();
        }

        public void RemoveCoach(Coach coach)
        {
            Coach coachToDelete = _dbContext.Coaches.Find(coach.Id);

            _dbContext.Coaches.Remove(coachToDelete);
            _dbContext.SaveChanges();
        }

        public void RemovePlayer(Player player)
        {
            Player playerToDelete = _dbContext.Players.Find(player.Id);

            _dbContext.Players.Remove(playerToDelete);
            _dbContext.SaveChanges();
        }

        #endregion
    }
}
