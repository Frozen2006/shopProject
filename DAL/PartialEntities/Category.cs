using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Category : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, name: {1}", this.Id, this.Name);
            return st;
        }
    }
}
