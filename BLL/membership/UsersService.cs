﻿using System;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using iTechArt.Shop.Common;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Models;


namespace iTechArt.Shop.Logic.Membership
{
	//class to work with users db: create user, manage user, delete user, etc...
	public class UsersService : IUserService
	{
		private readonly IUserRepository _repository;
		private readonly ISessionRepository _sessionRepository;
		private readonly ISessionContext _sessionContext;

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
		public bool CeateUser(User newUser)
		{

			User exUser = _repository.ReadAll().FirstOrDefault(m => m.Email == newUser.Email);

			if (exUser != null)
			{
				return false;
			}

			newUser.Password = GetHash(newUser.Password);

			_repository.Create(newUser);

			return true;
		}

		public bool CheckUser(string email, string password)
		{
			return GetUser(email, password) != null;
		}

		public bool LogIn(string email, string password)
		{
			User user = GetUser(email, password);

			if (user == null)
			{
				return false;
			}

			string guid = StartSession(user.Email);
			_sessionContext.SetSessionData(guid);

			return true;
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
			User user = GetUser(email);

			if (user == null)
			{
				return null;
			}

			UserDetails userDetails = Mapper.Map<User, UserDetails>(user);

			return userDetails;
		}

		//Change user password. 
		//Return true on success, and false on fail.
		//
		public bool ChangePassword(string email, string oldPassword, string newPassword)
		{
			User user = GetUser(email, oldPassword);

			if (user == null)
			{
				return false;
			}

			user.Password = GetHash(newPassword);

			_repository.Update(user);

			return true;
		}

		// Get User role name
		//
		//
		public RolesType GetUserRole(string userEmail)
		{
			User user = GetUser(userEmail);

			if (user != null)
			{
				return (RolesType)user.Role;
			}

			return (RolesType)(-1);
		}

		public void ChangeRole(string userEmail, RolesType newRole)
		{
			User user = GetUser(userEmail);

			if (user == null)
			{
				return;
			}

			user.Role = (int)newRole;

			_repository.Update(user);
		}

		//Method to check user role 
		//Return User, if login
		//
		public User AttributeCheck(RolesType role)
		{
			string guid = _sessionContext.GetSessionDataIfExist();

			if (guid != null)
			{
				if (!string.IsNullOrWhiteSpace(guid) && CheckSession(guid))
				{
					//role check disabled
					if (role == RolesType.Any)
					{
						Session user = _sessionRepository.ReadAll().FirstOrDefault(m => m.Guid == guid);

						if (user != null)
						{
							return user.User;
						}
						return null;
					}

					//role check
					Session firstOrDefault = _sessionRepository.ReadAll().FirstOrDefault(m => m.Guid == guid);
					if (firstOrDefault != null)
					{
						User tmpoUser = firstOrDefault.User;
						if (tmpoUser.Role == (int)role)
							return tmpoUser;
					}
				}
			}
			return null;
		}

		public void ChangeDeliveryData(string email, ChangeDeliveryAddressModel data)
		{
			User user = GetUser(email);

			if (user == null)
			{
				return;
			}

			user = Mapper.Map(data, user);
			_repository.Update(user);

		}

		public string StartSession(string userEmail)
		{
			string guid = Guid.NewGuid().ToString();
			User userAccount = GetUser(userEmail);

			if (userAccount == null)
			{
				return null;
			}

			var currentSession = new Session
			{
				Guid = guid, 
				UserId = userAccount.Id
			};

			_sessionRepository.Create(currentSession);

			_sessionContext.AddUserDataToCash(guid, userAccount.Email);

			return guid;
		}

		// Remove all data about session
		//
		//
		public void RemoveSession(string guid)
		{
			Session session = _sessionRepository.ReadAll().FirstOrDefault(m => m.Guid == guid);
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
			Session session = _sessionRepository.ReadAll().FirstOrDefault(m => m.Guid == guid);
			if (session != null)
			{

				_sessionContext.AddUserDataToCash(guid, session.User.Email);
				return true;
			}

			return false;
		}

		//Return user Title and first last name
		//
		//
		public string GetUserFullTitle(string email)
		{
			User user = GetUser(email);

			if (user == null)
			{
				return null;
			}

			return String.Format("{0} {1} {2}", user.Title, user.FirstName, user.LastName);
		}

		public string GetUserEmailFromSession(string guid)
		{
			//part 1 - find in cash
			string userEmail = _sessionContext.GetUserDataFromCash(guid);
			if (userEmail != null)
			{
				return userEmail;
			}

			//part 2 - DB req
			Session session = _sessionRepository.ReadAll().FirstOrDefault(m => m.Guid == guid);
			if (session != null)
			{

				_sessionContext.AddUserDataToCash(guid, session.User.Email);

				return session.User.Email;
			}

			return null;
		}

		// Generate md5 hash for string
		// return data in string type
		//
		private string GetHash(string input)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(input);

			var md5 = new MD5CryptoServiceProvider();
			byte[] byteHash = md5.ComputeHash(bytes);

			//convert bytes to string
			return byteHash.Aggregate(String.Empty, (current, b) => current + string.Format("{0:x2}", b));
		}

		private User GetUser(string email)
		{
			return _repository.ReadAll().FirstOrDefault(m => m.Email == email);
		}

		private User GetUser(string email, string password)
		{
			string hashedPass = GetHash(password);

			return _repository.ReadAll().FirstOrDefault(m => m.Email == email && m.Password == hashedPass);
		}
	}
}
