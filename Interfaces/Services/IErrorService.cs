using iTechArt.Shop.Common.Enumerations;

namespace iTechArt.Shop.Common.Services
{
    public interface IErrorService
    {
        string GetErrorDescription(ErrorCode code);
    }
}
