using System;
using System.Collections.Generic;
using System.Linq;

namespace RelayAppInsuranceCalculator
{
    /// <summary>
    /// Represents a motor insurance policy
    /// </summary>
    [Serializable]
    public class PolicyModel
    {
        private DateTime _startDate;

        public IList<DriverModel> Drivers { get; set; }
        public DateTime StartDate
        {
            get
            {
                return _startDate.Date;
            }
            set
            {
                _startDate = value.Date;
            }
        }

        public PolicyModel()
        {
            Drivers = new List<DriverModel>();
        }

        public PolicyModel(DateTime startDate, IList<DriverModel> drivers)
        {
            StartDate = startDate;
            Drivers = drivers;
        }

        /// <summary>
        /// Get the age of the youngest driver on the policy
        /// </summary>
        /// <returns></returns>
        public int GetAgeOfYoungestDriver()
        {
            DriverModel youngest = Drivers.OrderByDescending(a => a.DateOfBirth).First();
            return youngest.GetAgeAtDate(StartDate);
        }

        /// <summary>
        /// Combine the claims from all drivers on the policy into one list
        /// </summary>
        /// <returns></returns>
        public IList<ClaimModel> GetAllClaims()
        {
            IList<ClaimModel> claims = new List<ClaimModel>();
            foreach (DriverModel driver in Drivers)
            {
                if (driver.Claims != null)
                {
                    foreach (ClaimModel claim in driver.Claims)
                    {
                        claims.Add(claim);
                    }
                }
            }
            return claims;
        }
    }
}