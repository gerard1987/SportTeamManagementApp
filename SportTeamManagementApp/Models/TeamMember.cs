using SportTeamManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Models
{
    public abstract class TeamMember
    {
        private static int nextId = 1;
        public string firstName;
        public string lastName;
        private int age;
        public double salary;
        public Enum role;

        public int Id { get; private set; }

        public TeamMember()
        {
            Id = nextId++;
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value >= 0 && value <= 120)
                {
                    this.age = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value needs to be between 0 and 120");
                }
            }
        }
    }
}
