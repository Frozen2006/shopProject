using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Common.Services
{
    public interface IUserService
    {
        bool CeateUser(User newUser);

        bool CheckUser(string email, string password);
        bool LogIn(string email, string password);
        void LogOut(string email);
        string GetEmailIfLoginIn();
        UserDetails GetUserDetails(string email);
        bool ChangePassword(string email, string oldPassword, string newPassword);
        RolesType GetUserRole(string userEmail);
        void ChangeRole(string userEmail, RolesType newRole);
        string AtributeCheck(RolesType roleName);

        void ChangeDeliveryData(string email, ChangeDeliveryAddressModel data);
        string StartSession(string userEmail);
        void RemoveSession(string guid);
        bool CheckSession(string guid);
        string GetUserFoolTitle(string email);
        string GetUserEmailFromSession(string guid);

    }
}
