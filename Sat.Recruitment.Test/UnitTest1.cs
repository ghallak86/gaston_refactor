using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private UsersController userController;
        private User u;
        public UnitTest1()
        {
            userController = new UsersController();
            u = new User();
        }

        [Fact]
        public void NameRequired()
        {
            u.Name = "";
            u.Email = "Juan@marmol.com";
            u.Phone = "+5491154762312";
            u.Address = "Peru 2464";
            var result = userController.CreateUser(u).Result;
            Assert.Equal("The name is required", result.Errors);
        }

        [Fact]
        public void EmailRequired()
        {
            u.Name = "Juan";
            u.Email = "";
            u.Phone = "+5491154762312";
            u.Address = "Peru 2464";
            var result = userController.CreateUser(u).Result;
            Assert.Equal("The email is required", result.Errors);
        }

        [Fact]
        public void PhoneRequired()
        {
            u.Name = "Juan";
            u.Email = "Juan@marmol.com";
            u.Phone = "";
            u.Address = "Peru 2464";
            var result = userController.CreateUser(u).Result;
            Assert.Equal("The phone is required", result.Errors);
        }

        [Fact]
        public void AdressRequired()
        {
            u.Name = "Juan";
            u.Email = "Juan@marmol.com";
            u.Phone = "+5491154762312";
            u.Address = "";
            var result = userController.CreateUser(u).Result;
            Assert.Equal("The address is required", result.Errors);
        }

        [Fact]
        public void UserCreated()
        {
            u.Name = "Gaston";
            u.Email = "gaston.hallak@gmail.com";
            u.Phone = "+5493516985495";
            u.Address = "Echeverria 79";
            u.Money = 300;
            u.UserType = "Normal";
            var result = userController.CreateUser(u).Result;
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void UserDuplicated()
        {
            u.Name = "Gaston";
            u.Email = "gaston.hallak@gmail.com";
            u.Phone = "+5493516985495";
            u.Address = "Echeverria 79";
            u.Money = 300;
            u.UserType = "Normal";
            var result = userController.CreateUser(u).Result;
            Assert.Equal("User Duplicated", result.Errors);
        }
    }
}
