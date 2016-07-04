using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RelayAppInsuranceCalculator.Exceptions;

namespace RelayAppInsuranceCalculator
{
    /// <summary>
    /// Class which calculates insurance premium based on a set of rules
    /// </summary>
    public class PremiumCalculator : IPremiumCalculator
    {
        //Declarations 
        private decimal _initialPremium = 500.00M;
        private IDateTimeProvider _datetimeProvider;
        private Dictionary<string, decimal> OccupationAdjustments = new Dictionary<string, decimal>
        {
            //Occupations which can affect the premium cost
            { "CHAUFFEUR", 0.1M }, //Costs 10% more to insure a chauffer than other professions
            { "ACCOUNTANT", -0.1M } //Costs 10% less to insure an accountant than other professions
        };
      
        public PremiumCalculator() : this(new DateTimeProvider())
        {
        }

        public PremiumCalculator(decimal initialPremium) : this(new DateTimeProvider())
        {
            _initialPremium = initialPremium;
        }

        public PremiumCalculator(IDateTimeProvider datetimeProvider)
        {
            _datetimeProvider = datetimeProvider;
        }

        public PremiumCalculator(decimal initialPremium, IDateTimeProvider datetimeProvider)
        {
            _datetimeProvider = datetimeProvider;
        }      

        #region IPremiumCalculator Methods
        /// <summary>
        /// Calculates the insurance premium for a given policy.
        /// If the policy breaks any of the rules, return decline message along with reason.
        /// Else return the price of the premium. 
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public PremiumModel CalculatePremium(PolicyModel policy)
        {
            try
            {
                // Process decline rules first
                var result = ProcessDeclineRules(policy);
                if (result != null)
                {
                    return result; //If any rules broken return without premium calculation
                }

                // Calculate the amount for the premium
                decimal premiumAmount = CalculatePremiumPrice(policy);

                return PremiumModel.CreateAcceptedPremium(premiumAmount);
            }
            catch (Exception exception)
            {
                throw new PremiumCalcExceptions("An error occured when calculating the premium.", exception);
            }
        }
        #endregion IPremiumCalc Methods

        private decimal CalculatePremiumPrice(PolicyModel policy)
        {
            decimal premium = _initialPremium;

            // Occupation Rules
            foreach (DriverModel driver in policy.Drivers)
            {
                if (OccupationAdjustments.ContainsKey(driver.Occupation.ToUpper()))
                {
                    premium = premium + (premium * OccupationAdjustments[driver.Occupation.ToUpper()]);
                }
            }

            // Age Rules
            int youngestDriversAge = policy.GetAgeOfYoungestDriver();
            if (youngestDriversAge >= 21 && youngestDriversAge <= 25)
            {
                premium = premium + (premium * 0.2M);
            }
            else if (youngestDriversAge >= 26 && youngestDriversAge <= 75)
            {
                premium = premium + (premium * -0.1M);
            }

            // Claim rules
            IList<ClaimModel> claims = policy.GetAllClaims();
            if (claims != null)
            {
                foreach (ClaimModel claim in claims)
                {
                    // Go a year back from the policy start date and check if the claim date is after this
                    if (policy.StartDate.AddYears(-1) < claim.DateOfClaim)
                    {
                        premium = premium + (premium * 0.2M);
                    }
                    // Go 5 years back from the policy start date and check if the claim date is after this
                    else if (policy.StartDate.AddYears(-5) < claim.DateOfClaim)
                    {
                        premium = premium + (premium * 0.1M);
                    }
                }
            }
            return premium;
        }

        private PremiumModel ProcessDeclineRules(PolicyModel policy)
        {
            if (policy.StartDate.Date < _datetimeProvider.Now.Date)
            {
                return PremiumModel.CreateDeclinedPremium("Start Date of Policy");
            }
            if (policy.Drivers == null || policy.Drivers.Count == 0)
            {
                return PremiumModel.CreateDeclinedPremium("Policy must have at least one driver");
            }
            if (policy.Drivers.Count > 5)
            {
                return PremiumModel.CreateDeclinedPremium("Maximum of five drivers per policy");
            }

            int totalClaims = 0;
            foreach (DriverModel driver in policy.Drivers)
            {
                if (driver.GetAgeAtDate(policy.StartDate) < 21)
                {
                    return PremiumModel.CreateDeclinedPremium(string.Format("Age of Youngest Driver: {0}", driver.Name));
                }
                else if (driver.GetAgeAtDate(policy.StartDate) > 75)
                {
                    return PremiumModel.CreateDeclinedPremium(string.Format("Age of Oldest Driver: {0}", driver.Name));
                }
                else if (driver.Claims != null && driver.Claims.Count > 0)
                {
                    if (driver.Claims.Count > 2)
                    {
                        return PremiumModel.CreateDeclinedPremium(string.Format("Driver has more than 2 claims: {0}", driver.Name));
                    }
                    else
                    {
                        totalClaims = totalClaims + driver.Claims.Count;
                        if (totalClaims > 3)
                        {
                            return PremiumModel.CreateDeclinedPremium("Policy has more than 3 claims");
                        }
                    }
                }
            }
            return null;
        }
        
    }
}