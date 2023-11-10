using SportTeamManagementApp.Data.Enums;
using SportTeamManagementApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeamManagementApp.Data.Entities
{
    public class Player : TeamMember
    {
        [Key]
        public int Id { get; set; }
        public string Role
        {
            get
            {
                return this.role.ToString();
            }
            set
            {
                if (value != null && Enum.TryParse(value.ToString(), out SoccerPlayerRole soccerCoachRole))
                {
                    this.role = soccerCoachRole;
                }
                else
                {
                    throw new FormatException($"Could not parse value {value} to a SoccerCoachRole");
                }
            }
        }
    }
}
