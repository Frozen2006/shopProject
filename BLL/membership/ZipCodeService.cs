using System;
using System.Collections.Generic;
using System.Linq;
using DAL.membership;
using Entities;

namespace BLL.membership
{
    public class ZipCodeService : Interfaces.IZipCode
    {
        private readonly ZipRepository _repo;

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
            }
            return city.city + ", " + city.sub_city;
        }

        // Return list of possible sity
        // Item1 = zip code
        // Item2 = ciy name
        public List<Tuple<string,string>> GetCities(string particalZip)
        {
            Zip oneTCity = _repo.ReadAll().FirstOrDefault(m => m.zip1.CompareTo(particalZip) == 0);

            if (oneTCity != null)
            {
                var outDataF = new List<Tuple<string, string>>();
                outDataF.Add(Tuple.Create(oneTCity.zip1, oneTCity.city + ", " + oneTCity.sub_city));
                return outDataF;
            }

            IQueryable<Zip> citys = null;

            citys = _repo.ReadAll().Where(m => m.zip1.Substring(0, particalZip.Length).CompareTo(particalZip) == 0).Take(5);

            return citys.Select(oneCity => Tuple.Create(oneCity.zip1, oneCity.city + ", " + oneCity.sub_city)).ToList();
        }
    }
}
