using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Web.Common;

namespace iTechArt.Shop.Common.Services
{
    public interface IErrorService
    {
        string GetErrorDescription(ErrorCode code);
    }
}
