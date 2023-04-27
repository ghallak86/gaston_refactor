using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Validators
{
    public class Validator
    {
        private decimal gif = 0;
        private decimal percentage = 0;

        public string ValidateErrors(User u)
        {
            var error = "";
            if (string.IsNullOrEmpty(u.Name))
                error = "The name is required";
            else if (string.IsNullOrEmpty(u.Email))
                error = "The email is required";
            else if (string.IsNullOrEmpty(u.Address))
                error = "The address is required";
            else if (string.IsNullOrEmpty(u.Phone))
                error = "The phone is required";
        
            return error;
        }

        public decimal CalculateMoney(User u)
        {         
            switch (u.UserType)
            {
                case "Normal":
                    if (u.Money >= 100)
                    {
                        percentage = Convert.ToDecimal(0.12);
                        u.Money = CalculatedMoney(u.Money);
                    }
                    else if (u.Money < 100 && u.Money > 10)
                    {
                       percentage = Convert.ToDecimal(0.8);
                       u.Money = CalculatedMoney(u.Money);
                    }
                    break;

                case "SuperUser":
                    if (u.Money > 100)
                    {
                        percentage = Convert.ToDecimal(0.20);
                        u.Money = CalculatedMoney(u.Money);
                    }
                    break;

                case "Premium":
                    if (u.Money > 100)
                    {
                        percentage = u.Money * 2;
                        u.Money = CalculatedMoney(u.Money);
                    }
                    break;
            }
            return u.Money;
        }

        public string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });

            return email;
        }

        public bool ValidateUserDuplicate(List<User> _users, User u)
        {
            bool isDuplicated = false;

            foreach (var user in _users)
            {
               if (user.Email.ToLower() == u.Email.ToLower() || user.Phone == u.Phone || user.Name.ToLower() == u.Name.ToLower() || user.Address.ToLower() == u.Address.ToLower())
               { 
                  isDuplicated = true;
                  break;
               }
            }
            return isDuplicated;
        }

        #region Private Methods
        private decimal CalculatedMoney(decimal money)
        {
            gif = money * percentage;
            money = money + gif;

            return money;
        }
        #endregion
    }
}
