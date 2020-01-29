using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Models
{
    public class User
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool isAuthenticated { get; set; }
        public bool isAdmin { get; set; }

    }

    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }


    }
    /// <summary>
    /// Model for Graph User Collections
    /// </summary>
    public class UserResources
    {
        //public int itemsPerPage { get; set; }
        //public int startIndex { get; set; }
        //public int totalResults { get; set; }
        public List<User> resources { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string next { get; set; }

    }
}
