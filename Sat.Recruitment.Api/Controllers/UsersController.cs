using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Validators;
using Sat.Recruitment.Api.Repositories;


namespace Sat.Recruitment.Api.Controllers
{ 
    [ApiController]
    public partial class UsersController : ControllerBase
    {
        private readonly List<User> _users = new List<User>();
        private Validator validator;
        private Result result;
        private Repository repo;
        
        public UsersController()
        {
            validator = new Validator();
            result = new Result();
            repo = new Repository();
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(User u)
        {
            try
            { 
                var errors = validator.ValidateErrors(u);

                //Validate the new User
                if (errors != "")
                {
                    result.IsSuccess = false;
                    result.Errors = errors;
                    return result;
                }

                //Calculate Money
                u.Money = validator.CalculateMoney(u);
           
                //Normalize email
                u.Email = validator.NormalizeEmail(u.Email);

                //Read file with users
                var reader = repo.ReadUsersFromFile();
            
                //Add user from file to List
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var user = repo.AddUserToList(line);
                    if(user != null)                
                        _users.Add(user);
                }
                reader.Close();

                //Validate user duplicated
                if(validator.ValidateUserDuplicate(_users, u))
                {
                    Debug.WriteLine("The user is duplicated");                 
                    result.IsSuccess = false;
                    result.Errors = "The user is duplicated";
                }
                else
                {
                    //Save user to file
                    _users.Add(u);
                    repo.WriteUserInFile(_users, u);
                    Debug.WriteLine("User Created");
                    result.IsSuccess = true;
                    result.Errors = "User Created";
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Internal Error: " + ex.Message);
                result.IsSuccess = false;
                result.Errors = "Internal Error";
            }
            return result;
        }   
    }    
}
