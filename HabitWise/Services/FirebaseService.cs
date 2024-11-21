using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Services
{
    public class FirebaseAuthService
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

        public FirebaseAuthService()
        { 
            _firebaseAuthClient = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
               ApiKey = "AIzaSyDE60AG4IAg0tN3vNE9w5Ca5ghjeM4SZWA",
               AuthDomain = "habitwise-8030b.firebaseapp.com",
               Providers = [new EmailProvider()]
            });
        }

        public async Task<UserCredential> SignUpAsync(string email, string password, string username = "")
        {
            try
            {
                return await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., invalid credentials)
                throw new Exception("Sign-up failed", ex);
                
            }
        }

        //public async Task<FirebaseAuth> SignUpAsync(string email, string password)
        //{
        //    try
        //    {
        //        return await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle errors (e.g., account already exists)
        //        throw new Exception("Login failed", ex);
        //    }
        //}

    }
}
