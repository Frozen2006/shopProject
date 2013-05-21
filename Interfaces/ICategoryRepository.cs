using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetRootCategory();
    }
}
