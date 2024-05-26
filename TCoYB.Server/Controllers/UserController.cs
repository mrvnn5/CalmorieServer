using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TCoYB.Server.Models;
using TCoYB.Server.Repository;

namespace TCoYB.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository repository;

        public UserController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("isExists")]
        public bool Get(string username)
        {
            AppUser? appUser = repository.GetUser(username);
            if (appUser != null)
                return true;
            return false;
        }

        [HttpGet("byUsername")]
        public AppUser? Get(string username, string password)
        {
            AppUser? appUser = repository.GetUser(username);
            if(appUser != null)
                if(BCrypt.Net.BCrypt.Verify(password, appUser.PasswordHash))
                {
                    appUser.UserToken.Update();
                    repository.UpdateUser(appUser);
                    
                    return appUser.GetUserForApp();
                }
            return null;
        }

        [HttpPost("byToken")]
        public AppUser? Get(UserToken userToken)
        {
            AppUser? appUser = repository.GetUser(userToken.Username);
            if (appUser != null)
                if(appUser.UserToken.Check(userToken))
                {
                    appUser.UserToken.Update();
                    repository.UpdateUser(appUser);
                    
                    return appUser.GetUserForApp();
                }
            return null;
        }

        [HttpPost("createUser")]
        public AppUser Post(string username, string password)
        {
            if (repository.GetUser(username) != null)
            {
                Console.WriteLine("User exists!");
                return null;
            }
                

            AppUser user = new AppUser(username, 0, 0, 0, 0, 0, DateTime.Now, BCrypt.Net.BCrypt.HashPassword(password));
            user.UserToken = new UserToken(user.Username);
            repository.CreateUser(user);

            return user.GetUserForApp();
        }

        [HttpPost("updateUser")]
        public AppUser Post(AppUser user)
        {
            AppUser? appUser = repository.GetUser(user.Username);
            if (appUser != null)
                //Console.WriteLine("user not null");
                if (appUser.UserToken.Check(user.UserToken))
                {
                    //Console.WriteLine("Token is good");
                    user.UserToken.Update();
                    repository.UpdateUser(user);
                    appUser = repository.GetUser(user.Username);

                    return appUser.GetUserForApp();
                }
            return null;
        }

        [HttpGet("status")]
        public bool GetStatus()
        {
            return true;
        }
    }
}