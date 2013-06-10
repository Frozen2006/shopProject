using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTechArt.Shop.Common.Enumerations;

namespace iTechArt.Shop.Web.Common
{
    public class ShopExceptoion : Exception
    {
        public ShopExceptoion() { }

        public ShopExceptoion(string message)
            : base(message) { }

        public ErrorCode Code { get; set; }
    }
}
