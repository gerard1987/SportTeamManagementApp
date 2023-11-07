using SportTeamManagementApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Models
{
    public abstract class TeamMember
    {
        private static int nextId = 1;
        public string firstName;
        public string lastName;
        private int age;
        public double salary;
        protected Enum role;

        public int Id { get; private set; }

        public TeamMember()
        {
            Id = nextId++;
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.firstName = value;
                }
                else
                {
                    throw new ArgumentException("First name cannot be empty or null.");
                }
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.lastName = value;
                }
                else
                {
                    throw new ArgumentException("Last name cannot be empty or null.");
                }
            }
        }


        public string Age
        {
            get
            {
                return this.age.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int result))
                {
                    this.age = (result >= 0 && result <= 120) ? result : throw new ArgumentOutOfRangeException(nameof(value), "Value needs to be between 0 and 120");
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Cannot convert type of {value} to integer");
                }
            }
        }

        public string Salary
        {
            get
            {
                return this.salary.ToString();
            }
            set
            {
                if (double.TryParse(value.ToString(), out double salaryResult))
                {
                    this.salary = salaryResult;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Cannot convert type of {value} to double");
                }
            }
        }
    }
}
