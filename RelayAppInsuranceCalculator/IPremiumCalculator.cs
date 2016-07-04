using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelayAppInsuranceCalculator
{
    public interface IPremiumCalculator
    {
        PremiumModel CalculatePremium(PolicyModel policy);
    }
}