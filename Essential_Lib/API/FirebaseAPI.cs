using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essential_Lib.API
{
    public static class FirebaseAPI
    {
        public static async Task<string> CreateAccount(string email, string password)
        {
            var result = await client.CreateUserWithEmailAndPasswordAsync(email, password);
            return result.User.Info.Uid.ToString(); 
        }
        

        private static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyBAbekxQMS2yKkx_fZIexpxiMXPP1ThLmY",
            AuthDomain = "budge-tracker.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        };
        private static FirebaseAuthClient client = new FirebaseAuthClient(config);
    }
}
