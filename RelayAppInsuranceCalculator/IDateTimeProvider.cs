using System;

namespace RelayAppInsuranceCalculator
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}