namespace BizDiagClientSample
{
    public class GrossMarginInput
    {
        public double CostOfGoodsSold { get; set; }

        public double SalesRevenue { get; set; }
    }

    public sealed class GrossMarginPeriodicInput : GrossMarginInput
    {
        public int Year { get; set; }
    }

    public sealed class GrossMarginOutput
    {
        public int Year { get; set; }

        public double? GrossMargin { get; set; }
    }
}
