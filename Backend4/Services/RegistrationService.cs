using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Backend4.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ILogger logger;
        private readonly List<User> users = new List<User>();

        public RegistrationService(ILogger<IRegistrationService> logger)
        {
            this.logger = logger;
        }

        public Boolean CheckUser(String firstName,
                                 String lastName,
                                 int day,
                                 String month,
                                 int year,
                                 String gender)
        {
            lock (this.users)
            {
                this.logger.LogInformation($"Search {firstName} {lastName}");
                var user = this.users.FirstOrDefault(x => x.FirstName == firstName &&
                                                          x.LastName == lastName &&
                                                          x.Day == day &&
                                                          x.Month == month &&
                                                          x.Year == year &&
                                                          x.Gender == gender);
                if (user != null)
                {
                    //this.users.Remove(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void RegistrationUser(String firstName,
                                     String lastName,
                                     int day,
                                     String month,
                                     int year,
                                     String gender,
                                     String email,
                                     String password,
                                     bool remembered)
        {
            lock (this.users)
            {
                var user = new User(firstName,
                                    lastName,
                                    day,
                                    month,
                                    year,
                                    gender,
                                    remembered,
                                    email,
                                    password);

                this.users.Add(user);
                this.logger.LogInformation($"Registration user {firstName} {lastName}");
            }
        }

        public Boolean VerifyPassword(String password, String confirmPassword)
        {
            this.logger.LogInformation($"Validating passwords");
            return password == confirmPassword ? true : false;
        }

        private sealed class User
        {
            public String FirstName { get; }
            public String LastName { get; }
            public int Day { get; }
            public String Month { get; }
            public int Year { get; }
            //public Birthday Birthday { get; set; }
            public String Gender { get; }
            //public String UserExisted { get; }
            public bool Remembered { get; }
            public String Email { get; }
            public String Password { get; }

            public User(String firstName,
                        String lastName,
                        int day,
                        String month,
                        int year,
                        String gender,
                        bool remembered,
                        String email,
                        String password)
            {

                this.FirstName = firstName;
                this.LastName = lastName;
                this.Day = day;
                this.Month = month;
                this.Year = year;
                this.Gender = gender;
                this.Remembered = remembered;
                this.Email = email;
                this.Password = password;
            }
        }
    }
}
