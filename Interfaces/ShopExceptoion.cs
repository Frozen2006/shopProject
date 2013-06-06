using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class ShopExceptoion : Exception
    {
        public ShopExceptoion() { }

        public ShopExceptoion(string message)
            : base(message) { }

        public ErrorCode Code { get; set; }
    }
}
