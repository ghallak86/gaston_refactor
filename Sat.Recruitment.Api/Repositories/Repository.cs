using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Repositories
{
    public class Repository
    {
        private string path;

        public Repository ()
        {
            path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        }

        public StreamReader ReadUsersFromFile()
        {
            StreamReader reader;
            try
            { 
                FileStream fileStream = new FileStream(path, FileMode.Open);
                reader = new StreamReader(fileStream);
            }
            catch
            {
                reader = null;
            }
            return reader;
        }

        public User AddUserToList(string line)
        {
            User user;
            try
            {
               user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
            }
            catch(Exception ex)
            {
                user = null;
            }
            return user;
        }

        public void WriteUserInFile(List<User> users, User u)
        {
            try
            { 
                string[] lineas = new string[users.Count];
                int i = 0;
                foreach(var user in users)
                {
                    lineas[i] = user.ToString();
                    i++;
                }

                File.WriteAllLines(path, lineas);       
            }
            catch(Exception ex) 
            { 
               
            }
        }
    }
}
