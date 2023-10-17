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
            this.name = name;
            this.coach = coach;
            this.players = players;
        }
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

    }
}
