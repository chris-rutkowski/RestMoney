namespace RestMoney.Measurement.Influx
{
    public class Influx2Settings
    {
        public string? Url { get; set; }
        public string Token { get; set; } = String.Empty;
        public string Organization { get; set; } = String.Empty;
        public string Bucket { get; set; } = String.Empty;
    }
}
