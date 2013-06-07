using System;
using System.Collections.Generic;

namespace iTechArt.Shop.Common.Services
{
    public interface IZipCodeService
    {
        string GetFirstCityPartical(string particalZip);
        List<Tuple<string,string>> GetCities(string particalZip);
    }

}
