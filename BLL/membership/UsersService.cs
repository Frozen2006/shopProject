using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using DAL;
using DAL.membership;
using System.Web;

namespace BLL.membership
{
    //class to work with users db: create user, manage user, delete user, etc...
    public class UsersService
    {
        private UserRepository _repository;
        private RoleRepository _roleRepository;
        private SessionRepository _sessionRepository;

        //constructor to ninjecting
        public UsersService(UserRepository repository, RoleRepository roleRepository, SessionRepository sessionRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _sessionRepository = sessionRepository;
        }

        // Create new user
        // Argument's adress2 and phone2 may be null.
        //
        public void CeateUser(string email, string password, string title, string firstName, string lastName,
                              string address, string address2, string phone, string phone2, int zip, string city)
        {
            if ((email == null) || (password == null) || (title == null) || (firstName == null) || (lastName == null) ||
                (address == null) || (phone == null) || (zip == 0) || (city == null))
            {
                throw new InvalidDataException("Somethind argument's is not set");
            }

            User nUser = new User();

            nUser.email = email;
            nUser.password = GetHash(password);
            nUser.title = title;
            nUser.first_name = firstName;
            nUser.last_name = lastName;
            nUser.address = address;
            nUser.address2 = address2;
            nUser.phone = phone;
            nUser.phone2 = phone2;
            nUser.zip = zip;
            nUser.city = city;

            nUser.RoleId = 0; // 0 - user, 1 - administrator


            _repository.Create(nUser);
        }

        public bool CheckUser(string email, string password)
        {
            User user =
                _repository.ReadAll().FirstOrDefault(m => (m.email == email) && (m.password == GetHash(password)));

            if (user == null)
                return false;
            else
                return true;
        }


        public bool LogIn(string email, string password)
        {
            User user =
                _repository.ReadAll().FirstOrDefault(m => (m.email == email) && (m.password == GetHash(password)));

            if (user == null)
                return false;
            else
            {
                HttpCookie cookie = new HttpCookie("session_data");
                cookie.Value = StartSession(user.email);
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
        }

        public void LogOut(string email)
        {
            if (HttpContext.Current.Request.Cookies["session_data"] != null)
            {
                RemoveSession(HttpContext.Current.Request.Cookies["session_data"].Value);
                var cookie = new HttpCookie("session_data")
                {
                    Expires = DateTime.Now.AddDays(-1d)
                };
                HttpContext.Current.Response.Cookies.Add(cookie); 
            }
        }


        //Change user password. 
        //Return true on success, and false on fail.
        //
        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            User user = _repository.ReadAll().FirstOrDefault(m => (m.email == email) && (m.password == GetHash(oldPassword)));

            if (user == null)
                return false;

            user.password = GetHash(newPassword);

            _repository.Update(user);
        }

        // Get User role name
        //
        //
        public string GetUserRole(string userEmail)
        {
            User user = _repository.ReadAll().FirstOrDefault(m => m.email == userEmail);

            if (user != null)
            {
                return user.Role.name;
            }
            else
            {
                throw new InstanceNotFoundException("No user with this email adress");
            }
        }

        public void ChangeRole(string userEmail, string newRoleName)
        {
            Role role = _roleRepository.ReadAll().FirstOrDefault(m => m.name == newRoleName);

            if (role == null)
            {
                throw new InstanceNotFoundException("Role not found!");
            }
            User user = _repository.ReadAll().FirstOrDefault(m => m.email == userEmail);

            if (user == null)
            {
                throw new InstanceNotFoundException("User not found!");
            }

            user.Role = role;

            _repository.Update(user);
        }

        //Method to check user role 
        //
        //
        public bool AtributeCheck(string roleName)
        {
            if (HttpContext.Current.Request.Cookies.Get("session_data") != null)
            {
                string guid = HttpContext.Current.Request.Cookies.Get("session_data").Value;
                if (!string.IsNullOrWhiteSpace(guid) && CheckSession(guid))
                {
                    //role check disabled
                    if (string.IsNullOrWhiteSpace(roleName))
                        return true;

                    //role check
                    var firstOrDefault = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);
                    if (firstOrDefault != null)
                    {
                        User tmpoUser = firstOrDefault.User;
                        string nowUserRole = tmpoUser.Role.name;
                        nowUserRole = nowUserRole.Replace(" ", "");
                        if (nowUserRole.CompareTo(roleName) == 0)
                            return true;
                    }
                }
            }
            return false;
        }


        public string StartSession(string userEmail)
        {
            string guid = Guid.NewGuid().ToString();
            User userAccount = _repository.ReadAll().FirstOrDefault(m => m.email == userEmail);
            Session currentSession = new Session() { guid = guid, User = userAccount };
            _sessionRepository.Create(currentSession);

            HttpContext.Current.Cache.Add(guid, currentSession.User.email, null, DateTime.Now.AddDays(1.0), TimeSpan.Zero,
                                          CacheItemPriority.Normal, null);

            return guid;
        }

        // Remove all data about session
        //
        //
        public void RemoveSession(string guid)
        {
            HttpContext.Current.Cache.Remove(guid);

            var session = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);
            _sessionRepository.Delete(session);
        }

        // Check user session.
        //  If user is log in, and session correct -
        // return true
        public bool CheckSession(string guid)
        {
            //part 1 - find in cash
            string userName = (string)HttpContext.Current.Cache.Get(guid);
            if (userName != null)
            {
                return true;
            }

            //part 2 - DB req
            Session session = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);
            if (session != null)
            {
                
                HttpContext.Current.Cache.Add(guid, session.User.email, null, DateTime.Now.AddDays(1.0), TimeSpan.Zero,
                                              CacheItemPriority.Normal, null);
                return true;
            }
            return false;
        }

        // Generate md5 hash for string
        // return data in string type
        //
        private string GetHash(string input)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(input);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] byteHash = md5.ComputeHash(bytes);

            string hash = String.Empty;

            //convert bytes to string
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }


    }
}
