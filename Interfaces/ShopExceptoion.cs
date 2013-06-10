using System;
using iTechArt.Shop.Common.Enumerations;

namespace iTechArt.Shop.Common
{
    public class ShopExceptoion : Exception
    {
        public ShopExceptoion() { }

        public ShopExceptoion(string message)
            : base(message) { }

        public ErrorCode Code { get; set; }
    }
}
