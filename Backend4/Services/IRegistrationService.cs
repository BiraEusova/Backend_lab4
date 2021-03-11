using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Services
{
    public interface IRegistrationService
    {

        Boolean CheckUser(String firstName,
                          String lastName,
                          int day,
                          String month,
                          int year,
                          String gender);
        Boolean VerifyPassword (String password, String confirmPassword);

        void RegistrationUser(String firstName,
                              String lastName,
                              int day,
                              String month,
                              int year,
                              String gender,
                              String email,
                              String password,
                              bool remembered);
    }
}
