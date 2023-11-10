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
            using (var context = new AppDbContext())
            {
                Player playerAlreadyExists = _dbContext.Players.Find(player.Id);

                if (playerAlreadyExists is null)
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Player already exists!");
                }
            }
        }

        public void CreateCoach(Coach coach)
        {
            using (var context = new AppDbContext())
            {
                Coach coachAlreadyExists = _dbContext.Coaches.Find(coach.Id);

                if (coachAlreadyExists is null)
                {
                    context.Coaches.Add(coach);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Coach already exists!");
                }
            }
        }

        public void CreateTeam(Team team)
        {
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();

            Team teamInserted = _dbContext.Teams.Find(team.Id)
        }

        #endregion

        #region EDITS

        public void EditTeam(Team team)
        {
            Team teamToEdit = _dbContext.Teams.Find(team.Id);
            teamToEdit = team;

            _dbContext.SaveChanges();
        }

        //public void AddPlayerToTeam(Team team, Player player)
        //{
        //    using (var context = new AppDbContext())
        //    {
        //        Player existingPlayer = context.Players.Find(player.Id);

        //        if (existingPlayer.teamId is null)
        //        {
        //            existingPlayer.teamId = team.Id;
        //            context.SaveChanges();

        //            this.Players.Add(existingPlayer);
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException($"Player {player?.FirstName} is already in a team!");
        //        }
        //    }
        //}

        //public void RemovePlayer(Player player)
        //{
        //    using (var context = new AppDbContext())
        //    {
        //        Player existingPlayer = context.Players.Find(player.Id);

        //        if (existingPlayer.teamId != null && existingPlayer.teamId.Equals(this.Id))
        //        {
        //            existingPlayer.teamId = null;
        //            context.SaveChanges();
        //            this.Players.Remove(existingPlayer);
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException($"Cant find player {player?.FirstName} in team for removal!");
        //        }
        //    }
        //}

        //public void RemoveCoach(Coach coach)
        //{
        //    using (var context = new AppDbContext())
        //    {
        //        bool isCoachInTeam = context.Coaches.Any(c => c.teamId == this.Id);

        //        if (isCoachInTeam)
        //        {
        //            Coach coachToUpdate = context.Coaches.Find(coach);
        //            // Assign the teamId to the new coach
        //            coachToUpdate.teamId = null;
        //            context.SaveChanges();
        //            this.Coach = null;
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException($"A coach is already associated with the team!");
        //        }
        //    }
        //}

        #endregion

        #region DELETES
        public void RemoveTeam(Team team)
        {
            Team teamToDelete = _dbContext.Teams.Find(team.Id);

            _dbContext.Teams.Remove(teamToDelete);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}
