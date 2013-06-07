using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities;

namespace Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetRootCategory();
    }
}
