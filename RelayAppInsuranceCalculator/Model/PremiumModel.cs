namespace RelayAppInsuranceCalculator
{
    /// <summary>
    /// Represents the premium for an insurance policy.
    /// </summary>
    public class PremiumModel
    {
        public bool IsDeclined { get; private set; }
        public decimal Amount { get; private set; }
        public string Message { get; private set; }

        public PremiumModel(bool isDeclined, decimal amount, string message)
        {
            IsDeclined = isDeclined;
            Amount = amount;
            Message = message;
        }

        /// <summary>
        /// Create a declined premium and add the reason to the message
        /// </summary>
        /// <param name="declineReason"></param>
        /// <returns></returns>
        public static PremiumModel CreateDeclinedPremium(string declineReason)
        {
            return new PremiumModel(true, 0.0M, declineReason);
        }

        /// <summary>
        /// Create an accepted premium and set the premium amount
        /// </summary>
        /// <param name="premiumAmount"></param>
        /// <returns></returns>
        public static PremiumModel CreateAcceptedPremium(decimal premiumAmount)
        {
            return new PremiumModel(false, premiumAmount, "Policy Accepted");
        }
    }
}