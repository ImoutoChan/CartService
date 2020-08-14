namespace CartService.Quartz
{
    public class QuartzSettings
    {
        public int CleanupCartsEveryMinutes { get; set; } = 60;

        public string GenerateReportDailyCron { get; set; } = "0 0 2 ? * * *";
    }
}