using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;

namespace RelayAppInsuranceCalculator.Exceptions
{
    public class PremiumCalcExceptions : Exception
    {
        public PremiumCalcExceptions()
        {
        }

        public PremiumCalcExceptions(string message) : base(message)
        {
        }

        public PremiumCalcExceptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}