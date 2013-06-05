﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace Interfaces
{
    public interface IUserService
    {
        bool CeateUser(string email, string password, string title, string firstName, string lastName,
                       string address, string address2, string phone, string phone2, int zip, string city, RolesType role);

        bool CheckUser(string email, string password);
        bool LogIn(string email, string password);
        void LogOut(string email);
        string GetEmailIfLoginIn();
        UserDetails GetUserDetails(string email);
        bool ChangePassword(string email, string oldPassword, string newPassword);
        RolesType GetUserRole(string userEmail);
        void ChangeRole(string userEmail, RolesType newRole);
        string AtributeCheck(RolesType roleName);

        void ChangeDeliveryData(string email, string address, string address2, string phone, string phone2,
                                int zip, string city);

        string StartSession(string userEmail);
        void RemoveSession(string guid);
        bool CheckSession(string guid);
        string GetUserFoolTitle(string email);
        string GetUserEmailFromSession(string guid);

    }
}
