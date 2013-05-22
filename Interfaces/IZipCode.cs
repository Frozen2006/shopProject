using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IZipCode
    {
        string GetFirstCityPartical(string particalZip);
        List<Tuple<string,string>> GetCities(string particalZip);
    }

}
