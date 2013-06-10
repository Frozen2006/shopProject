namespace iTechArt.Shop.Common
{
    public interface ISessionContext
    {
        void SetSessionData(string guid);
        string GetSessionDataIfExist();
        void RemoveSessionData();
        string GetUserDataFromCash(string guid);
        void AddUserDataToCash(string guid, string email);
    }
}
