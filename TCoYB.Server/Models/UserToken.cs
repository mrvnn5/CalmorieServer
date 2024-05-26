using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace TCoYB.Server.Models
{
    public class UserToken
    {
        [Required]
        public string Username { get; set; }
        [Key]
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public DateTime ExpiredAt { get; set; }

        public UserToken(string username)
        {            
            Username = username;
            AccessToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Update();
        }

        public void Update()
        {
            ExpiredAt = DateTime.Now.AddMonths(1);
        }

        public bool Check(UserToken newToken)
        {
            return (Username == newToken.Username && AccessToken == newToken.AccessToken && ExpiredAt > DateTime.Now);
        }
    }
}
