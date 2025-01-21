using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Essential_Lib.API
{
    public static class ValidatorAPI
    {
        public static string ValidatePassword(string password)
        {
            int score = 0;

            if (string.IsNullOrWhiteSpace(password))
                return ("Very Weak");//, Colors.Red);           

            if (password.Length >= 12)
                score++;

            // if (Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]"))
            if (password.Any(char.IsUpper) && password.Any(char.IsLower))
                score++;

            // if (Regex.IsMatch(password, @"\d"))
            if (password.Any(char.IsDigit))
                score++;

            //if (Regex.IsMatch(password, @"[!@#\$%\^&\*\?_~\-\(\\)\\.]"))
            if (password.Any(ch => !char.IsLetterOrDigit(ch)))
                score++;


            return score
                 switch
            {
                4 => ("Very Strong"),//, Colors.Green),
                3 => ("Strong"),//, Colors.GreenYellow),
                2 => ("Medium"),//, Colors.Orange),
                1 => ("Weak"),// Colors.OrangeRed),
                _ => ("Very Weak"),// Colors.Red)
            };
        }


        public static string ValidateEmail(string email)
        {


            if (string.IsNullOrWhiteSpace(email))
            {
                return "ُEmail is required";
            }

            string basicPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, basicPattern))
            {
                return "Invalid Email Format";
            }


            if (email.Length < 7)
            {
                return "Very Short";
            }


            string localPart = email.Split('@')[0];
            if (localPart.Length > 60)
            {
                return "The User Name Is Very Long";
            }

            string domain = email.Split('@')[1];
            if (domain.Length > 50)
            {
                return "The Domain Name Is Very Long";
            }

            if (!IsValidDomainExtension(domain))
            {
                return "Invalid Domain ُExtension";
            }

            string strictPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            if (!Regex.IsMatch(email, strictPattern))
            {
                return "Email Contains Invalid Characters";
            }

            if (email.Contains(".."))
            {
                return "Email cannot contain consecutive dots";
            }

            string result = email.Split('@')[1];
            if (result.StartsWith("-") || result.EndsWith("-"))
            {
                return "Domain Cannot Start Or End With a Hyphen";
            }


            return "Email Accepted";
        }


        public static string ValidatePhoneNumber(string phonenumber)
        {

            if (phonenumber.Any(char.IsUpper) || phonenumber.Any(char.IsLower) || phonenumber.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return "There Are Characters";
            }

            if (phonenumber.Length < 11)
            {
                return "Very Short";
            }
            
            if (phonenumber.Length > 11)
            {
                return "Very Long";
            }

            if (string.IsNullOrWhiteSpace(phonenumber))
            {
                return "Invalid";
            }


            // Example regex for Egyptian phone numbers
            string pattern = @"^(?:\+2)?01[0125][0-9]{8}$";

            bool isFormatValid = Regex.IsMatch(phonenumber, pattern);

            if (!isFormatValid)
            {
                return "Invalid";
            }



            string BlockedNumbers = "01234567890";
            bool isBlocked = BlockedNumbers.Contains(phonenumber);

            if (isBlocked) 
            { 
                return "Incorrect"; 
            }

            return "Phone Number Accepted";
        }


        private static bool IsValidDomainExtension(string domain)
        {
            var validExtensions = new HashSet<string>
            {
                ".com", ".net", ".org", ".edu", ".gov", ".mil",
                ".info", ".biz", ".name", ".me", ".io", ".co",
                ".eg", ".sa", ".ae", ".uk", ".us", ".ca", ".au"
            };

            return validExtensions.Any(ext => domain.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }
    }
}
