using System;
using System.Collections.Generic;

namespace RelayAppInsuranceCalculator
{
    /// <summary>
    /// Represents a driver on an insurance policy
    /// </summary>
    [Serializable]
    public class DriverModel
    {
        private DateTime _dateOfBirth;

        public string Name { get; set; }
        public string Occupation { get; set; }
        public IList<ClaimModel> Claims { get; set; }
        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth.Date;
            }
            set
            {
                _dateOfBirth = value.Date;
            }
        }

        public DriverModel()
        {
            Claims = new List<ClaimModel>();
        }

        /// <summary>
        /// Get the age of the driver at the given date
        /// </summary>
        /// <param name="ageAtDate"></param>
        /// <returns></returns>
        public int GetAgeAtDate(DateTime ageAtDate)
        {
            // Get age based on year
            int age = ageAtDate.Year - DateOfBirth.Year;
            // Check if birthday has already happened this year. If not subtract one year from the age
            if (DateOfBirth > ageAtDate.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}