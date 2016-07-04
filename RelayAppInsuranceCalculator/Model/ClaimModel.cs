using System;
namespace RelayAppInsuranceCalculator
{
    /// <summary>
    /// Represents an insurance claim
    /// </summary>
    [Serializable]
    public class ClaimModel
    {
        private DateTime _dateOfClaim;
        public DateTime DateOfClaim
        {
            get
            {
                return _dateOfClaim.Date;
            }
            set
            {
                _dateOfClaim = value.Date;
            }
        }
    }
}