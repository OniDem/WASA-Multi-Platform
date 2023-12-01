using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASA_Multi_Platform
{
    static class Validations
    {

        public static bool EntryValid(string entry)
        {
            if (string.IsNullOrEmpty(entry))
                return false;
            else
                return true;
        }
    }
}
