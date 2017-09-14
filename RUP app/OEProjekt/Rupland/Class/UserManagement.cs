using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProjectRepository.Repository;
using CurrentProject.Database;
using System.Security.Cryptography;
using CurrentProjectRepository.GenericRepository;

namespace CurrentProject.Class
{
    public class UserManagement
    {

        CurrentProjectRepository.GenericRepository.Interface.IGenericRepository<User> userRepository;
        CurrentProjectRepository.GenericRepository.Interface.IGenericRepository<SecurityQuestion> securityQuestionRepository;

        public UserManagement()
        {
            userRepository = new GenericRepository<User>();
            securityQuestionRepository = new GenericRepository<SecurityQuestion>();
        }

        public UserManagement(CurrentProjectRepository.GenericRepository.Interface.IGenericRepository<User> userRepository)
        {
            // tesztelhetőség miatt
            this.userRepository = userRepository;

        }

        public bool Login(string email, string password)
        {
            string hash = Helper.PasswordHash(password, email);
            var q1 = userRepository.SearchFor(user => user.Email == email && user.Password == hash).SingleOrDefault();

            if (q1 != null)
            {
                Session.Set("U_Id", q1.UserId);
                Session.Set("U_Firstname", q1.Firstname);
                Session.Set("U_Lastname", q1.Lastname);
                Session.Set("U_Login", true);

                User temp = userRepository.GetById(q1.UserId);
                temp.LastLogin = Helper.CurrentTimestamp();

                userRepository.Update(temp);
                userRepository.Save();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Registration(string firstname, string lastname, string email, string password, int securityQuestionId, string securityQuestionAnswer)
        {
            userRepository.Create(new User()
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = Helper.PasswordHash(password, email),
                SecurityQuestionId = securityQuestionId,
                SecurityQuestionAnswer = securityQuestionAnswer,
                LastLogin = Helper.CurrentTimestamp(),
                Registered = Helper.CurrentTimestamp()
            });

            userRepository.Save();

            return true;
        }

        public bool PasswordRecoveryCheck(string email, int securityQuestionId, string securityQuestionAnswer)
        {
            var q1 = userRepository.SearchFor(user => user.Email == email && user.SecurityQuestionId == securityQuestionId && user.SecurityQuestionAnswer == securityQuestionAnswer).SingleOrDefault();
            return q1 != null ? true : false;
        }

        public string PasswordReset(string email)
        {
            User user = userRepository.SearchFor(u => u.Email == email).SingleOrDefault();
            string newPass = Helper.Random();
            user.Password = Helper.PasswordHash(Helper.Random(), email);
            userRepository.Update(user);
            userRepository.Save();
            return newPass;
        }

        public bool CheckEmail(string email)
        {
            var q1 = userRepository.SearchFor(user => user.Email == email).SingleOrDefault();
            return q1 != null ? true : false;
        }

        public List<SecurityQuestion> GetSecurityQuestion()
        {
            return securityQuestionRepository.Read().ToList();
        }

        public User CurrentUser(int userId)
        {
            return userRepository.GetById(userId);
        }

        public bool UpdateUserData(string firstname, string lastname, int securityQuestionId, string securityQuestionAnswer,  string password = null)
        {

            User temp = userRepository.GetById(Session.Get<int>("U_Id"));

            temp.Firstname = firstname;
            temp.Lastname = lastname;
            temp.SecurityQuestionId = securityQuestionId;
            temp.SecurityQuestionAnswer = securityQuestionAnswer;

            if (password != null)
            {
                temp.Password = Helper.PasswordHash(password, temp.Email);
            }

            userRepository.Update(temp);

            userRepository.Save();

            return true;
        }

    }
}
