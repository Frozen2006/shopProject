using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.membership;

namespace BLL.membership
{
    public class ZipCodeService : Interfaces.IZipCode
    {
        private ZipRepository _repo;

        public ZipCodeService(ZipRepository zr)
        {
            _repo = zr;
        }

        // Return first city, wich have partical zip
        // Partical zip - zip code
        //
        public string GetFirstCityPartical(string particalZip)
        {
            var city = _repo.ReadAll().FirstOrDefault(m => m.zip1.CompareTo(particalZip) == 0);

            if (city == null)
            {
                city = _repo.ReadAll().FirstOrDefault(m => m.zip1.Substring(0,particalZip.Length).CompareTo(particalZip) == 0);

                if (city == null)
                {
                    return String.Empty;
                }
                else
                {
                    return city.city + ", " + city.sub_city;
                }
            }
            else
            {
                return city.city + ", " + city.sub_city;
            }
        }

        // Return list of possible sity
        // Item1 = zip code
        // Item2 = ciy name
        public List<Tuple<string,string>> GetCities(string particalZip)
        {
            var city = _repo.ReadAll().Where(m => m.zip1.CompareTo(particalZip) == 0);

             //city = _repo.ReadAll().FirstOrDefault(m => m.zip1.CompareTo(particalZip) > 0);

            if (city.Count() == 0)
            {
                city = _repo.ReadAll().Where(m => m.zip1.Substring(0, particalZip.Length).CompareTo(particalZip) == 0);
            }

            List<Tuple<string, string>> outData = new List<Tuple<string, string>>();

            foreach (var oneCity in city)
            {
                outData.Add(Tuple.Create(oneCity.zip1,oneCity.city+", "+oneCity.sub_city));
            }

            return outData;
        }
    }
}
