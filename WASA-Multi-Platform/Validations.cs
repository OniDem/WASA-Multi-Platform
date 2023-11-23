using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASA_Multi_Platform
{
    static class Validations
    {

        public static bool LoginValid(string login)
        {
            if (string.IsNullOrEmpty(login))
                return false;
            else
                return true;
        }

        public static bool PasswordValid(string password)
        {
            if (!string.IsNullOrEmpty(password) && password.Length > 4)
                return false;
            else
                return true;
        }
    }
}
