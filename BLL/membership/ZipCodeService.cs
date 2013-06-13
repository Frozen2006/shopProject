using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.Shop.Common.Services;
using iTechArt.Shop.DataAccess.ConcreteRepositories;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Logic.Membership
{
    public class ZipCodeService : IZipCodeService
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
            var city = _repo.ReadAll().FirstOrDefault(m => m.ZipCode.CompareTo(particalZip) == 0);

            if (city == null)
            {
                city = _repo.ReadAll().FirstOrDefault(m => m.ZipCode.Substring(0,particalZip.Length).CompareTo(particalZip) == 0);

                if (city == null)
                {
                    return String.Empty;
                }
            }
            return String.Format("{0}, {1}", city.City, city.SubCity);
        }

        // Return list of possible sity
        // Item1 = zip code
        // Item2 = ciy name
        public List<Tuple<string,string>> GetCities(string particalZip)
        {
            Zip oneCity = _repo.ReadAll().FirstOrDefault(m => m.ZipCode.CompareTo(particalZip) == 0);

            //if finded one city (100% mutch)
            if (oneCity != null)
            {
                var outDataF = new List<Tuple<string, string>>();
                outDataF.Add(Tuple.Create(oneCity.ZipCode, String.Format("{0}, {1}", oneCity.City, oneCity.SubCity)));
                return outDataF;
            }


            //try get many city's zip
            IQueryable<Zip> citys = _repo.ReadAll().Where(m => m.ZipCode.Substring(0, particalZip.Length).CompareTo(particalZip) == 0).Take(5);

            List<Tuple<string, string>> outData = new List<Tuple<string, string>>();

            foreach (var city in citys)
            {
                outData.Add(Tuple.Create(city.ZipCode, String.Format("{0}, {1}", city.City, city.SubCity)));
            }

            return outData;
        }
    }
}
