using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareComputerClient.Model
{
    class HandleInput
    {
        public bool stringValidation(string stringToValidate)
        {
            if (stringToValidate == string.Empty)
            {
                
                return false;
            }
            return true;
        }
        public string uppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
