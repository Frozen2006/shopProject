using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
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
