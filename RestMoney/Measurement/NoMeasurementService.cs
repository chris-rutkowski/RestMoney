namespace RestMoney.Measurement
{
    public class NoMeasurementService : IMeasurementService
    {
        public Task CountIncomeRequest(string countryCode)
        {
            return Task.CompletedTask;
        }
    }
}
