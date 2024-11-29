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
        public UserCredential? userCredential;

        public FirebaseAuthService()
        { 
            _firebaseAuthClient = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
               ApiKey = "AIzaSyDE60AG4IAg0tN3vNE9w5Ca5ghjeM4SZWA",
               AuthDomain = "habitwise-8030b.firebaseapp.com",
               Providers = [new EmailProvider()]
            });
        }

        /// <summary>
        /// Signs up a new user with email, password, and optional username.
        /// </summary>
        public virtual async Task<bool> SignUpAsync(string email, string password, string username = "")
        {
            try
            {
                userCredential =  await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
                return userCredential?.User?.Info.Email != null;
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.Reason)
                { 
                    case AuthErrorReason.EmailExists:
                        throw new Exception("An account with this email already exists");
                    default:
                        throw new Exception("An error occurred during sign-up.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sign-up failed", ex);
            }
        }

        /// <summary>
        /// Signs in an existing user with email and password.
        /// </summary>
        public virtual async Task<bool> SignInAsync(string email, string password)
        {
            try
            {
                userCredential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
                return userCredential?.User?.Info.Email != null;
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.Reason)
                {
                    case AuthErrorReason.WrongPassword:
                        throw new Exception("The password is incorrect.");
                    case AuthErrorReason.UnknownEmailAddress:
                        throw new Exception("No account exists with this email.");
                    default:
                        throw new Exception("An error occurred during sign-in.");
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred during sign-in.");
            }
        }

    }
}
