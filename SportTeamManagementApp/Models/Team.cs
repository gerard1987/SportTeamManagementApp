using SportTeamManagementApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public class Team
    {
        private static int nextId = 1;
        private int id;
        public string name;
        public Coach coach;
        public List<Player> players = new List<Player>();

        public Team(string name, Coach coach, List<Player> players)
        {
            Id = nextId++;
            this.Name = name;
            this.coach = coach;
            this.players = players;
        }
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
                else
                {
                    throw new ArgumentException("Name cannot be empty or null.");
                }
            }
        }

        public void AddPlayer(Player player)
        {
            if (!players.Exists(p => p.Id == player.Id))
            {
                players.Add(player);
            }
            else
            {
                throw new InvalidOperationException($"Player {player?.FirstName} is already in team!");
            }
        }

        public void RemovePlayer(Player player)
        {
            if (players.Exists(p => p.Id == player.Id))
            {
                players.Remove(player);
            }
            else
            {
                throw new InvalidOperationException($"Could not find player {player?.FirstName}");
            }
        }
    }
}
