﻿using System;
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
using DAL.Repositories.DbFirstRepository;
using DAL.membership;
using System.Web;
using Entities;
using Helpers;
using Interfaces;

namespace BLL.membership
{
    //class to work with users db: create user, manage user, delete user, etc...
    public class UsersService : IUserService
    {
        private IUserRepository _repository;
        private ISessionRepository _sessionRepository;
        private ISessionContext _sessionContext;

        //constructor to ninjecting
        public UsersService(IUserRepository repository, ISessionRepository sessionRepository, ISessionContext sc)
        {
            _repository = repository;
            _sessionRepository = sessionRepository;
            _sessionContext = sc;
        }

        // Create new user
        // Argument's adress2 and phone2 may be null.
        //
        public bool CeateUser(string email, string password, string title, string firstName, string lastName,
                              string address, string address2, string phone, string phone2, int zip, string city, RolesType role)
        {
            if ((email == null) || (password == null) || (title == null) || (firstName == null) || (lastName == null) ||
                (address == null) || (phone == null) || (zip == 0) || (city == null))
            {
                throw new InvalidDataException("Somethind argument's is not set");
            }

            User exUser = _repository.ReadAll().FirstOrDefault(m => m.email == email);

            if (exUser != null)
                return false;

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

            nUser.Role = (int) role;


            _repository.Create(nUser);

            return true;
        }

        public bool CheckUser(string email, string password)
        {
            string hassedPass = GetHash(password);

            User user =
                _repository.ReadAll().FirstOrDefault(m => (m.email == email) && (m.password == hassedPass));

            return user != null;
        }

        public bool LogIn(string email, string password)
        {

            string hassedPass = GetHash(password);

            User user =
                _repository.ReadAll().FirstOrDefault(m => (m.email == email) && (m.password == hassedPass));

            if (user == null)
                return false;
            else
            {
                string guid = StartSession(user.email);
                _sessionContext.SetSessionData(guid);
                return true;
            }
        }

        public void LogOut(string email)
        {
            string guid = _sessionContext.GetSessionDataIfExist();
            if (guid != null)
            {
                RemoveSession(guid);

                _sessionContext.RemoveSessionData(); //remove cookie
            }
        }

        public string GetEmailIfLoginIn()
        {
            string guid = _sessionContext.GetSessionDataIfExist();

            if (guid != null)
            {
                return GetUserEmailFromSession(guid);
            }

            return null;
        }

        public UserDetails GetUserDetails(string email)
        {
            User us = _repository.ReadAll().FirstOrDefault(m => m.email == email);

            if (us == null)
            {
                throw new InstanceNotFoundException("User not found");
            }
                

            UserDetails userDetails = new UserDetails()
            {
                    Id = us.Id,
                    Role = (RolesType)us.Role,
                    address = us.address,
                    address2 = us.address2,
                    city = us.city,
                    zip = us.zip,
                    email = us.email,
                    phone = us.phone,
                    phone2 = us.phone2,
                    first_name = us.first_name,
                    last_name = us.last_name,
                    title = us.title
                };

            return userDetails;
        }

        //Change user password. 
        //Return true on success, and false on fail.
        //
        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            string hassedPass = GetHash(oldPassword);

            User user = _repository.ReadAll().FirstOrDefault(m => (m.email == email) && (m.password == hassedPass));

            if (user == null)
                return false;

            user.password = GetHash(newPassword);

            _repository.Update(user);

            return true;
        }

        // Get User role name
        //
        //
        public RolesType GetUserRole(string userEmail)
        {
            User user = _repository.ReadAll().FirstOrDefault(m => m.email == userEmail);

            if (user != null)
            {
                return (RolesType)user.Role;
            }
            else
            {
                throw new InstanceNotFoundException("No user with this email adress");
            }
        }

        public void ChangeRole(string userEmail, RolesType newRole)
        {
            User user = _repository.ReadAll().FirstOrDefault(m => m.email == userEmail);

            if (user == null)
            {
                throw new InstanceNotFoundException("User not found!");
            }

            user.Role = (int) newRole;

            _repository.Update(user);
        }

        //Method to check user role 
        //
        //
        public string AtributeCheck(RolesType roleName)
        {
            string guid = _sessionContext.GetSessionDataIfExist();

            if (guid != null)
            {
                if (!string.IsNullOrWhiteSpace(guid) && CheckSession(guid))
                {
                    //role check disabled
                    if ((int)roleName == -1)
                    { 
                        var user = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);

                        if (user != null)
                        {
                            return user.User.email;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    //role check
                    var firstOrDefault = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);
                    if (firstOrDefault != null)
                    {
                        User tmpoUser = firstOrDefault.User;
                        if (tmpoUser.Role == (int)roleName)
                            return tmpoUser.email;
                    }
                }
            }
            return null;
        }

        public void ChangeDeliveryData(string email, string address, string address2, string phone, string phone2,
                                       int zip, string city)
        {
            User us = _repository.ReadAll().FirstOrDefault(m => m.email == email);

            if (us == null)
                throw new InstanceNotFoundException("User not found");

            us.address = address;
            us.address2 = address2;
            us.phone = phone;
            us.phone2 = phone2;
            us.zip = zip;
            us.city = city;
        
            _repository.Update(us);
        
        }

        public string StartSession(string userEmail)
        {
            string guid = Guid.NewGuid().ToString();
            User userAccount = _repository.ReadAll().FirstOrDefault(m => m.email == userEmail);
            Session currentSession = new Session() { guid = guid, UserId = userAccount.Id };
            _sessionRepository.Create(currentSession);

            HttpContext.Current.Cache.Add(guid, userAccount.email, null, DateTime.Now.AddDays(1.0), TimeSpan.Zero,
                                          CacheItemPriority.Normal, null);

            return guid;
        }

        // Remove all data about session
        //
        //
        public void RemoveSession(string guid)
        {
            var session = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);
            _sessionRepository.Delete(session);
        }

        // Check user session.
        //  If user is log in, and session correct -
        // return true
        public bool CheckSession(string guid)
        {
            //part 1 - find in cash
            string userName = _sessionContext.GetUserDataFromCash(guid);
            if (userName != null)
            {
                return true;
            }

            //part 2 - DB req
            Session session = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);
            if (session != null)
            {
                
                _sessionContext.AddUserDataToCash(guid, session.User.email);
                return true;
            }
            return false;
        }

        //Return user title and first last name
        //
        //
        public string GetUserFoolTitle(string email)
        {
            User user = _repository.ReadAll().FirstOrDefault(m => m.email == email);

            if (user == null)
                throw new InstanceNotFoundException("User not found!");

            return user.title + " " + user.first_name + " " + user.last_name;
        }

        public string GetUserEmailFromSession(string guid)
        {
            //part 1 - find in cash
            string UserEmail = _sessionContext.GetUserDataFromCash(guid);
            if (UserEmail != null)
            {
                return UserEmail;
            }

            //part 2 - DB req
            Session session = _sessionRepository.ReadAll().FirstOrDefault(m => m.guid == guid);
            if (session != null)
            {

                _sessionContext.AddUserDataToCash(guid, session.User.email);

                return session.User.email;
            }

            return null;
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
