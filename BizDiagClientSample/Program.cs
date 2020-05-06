using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizDiagClientSample
{
    class Program
    {
        // Subscription key for KPMG-Kosmos-Developer
        const string SubscriptionKey = "fee8695699f44630904a6dea621ca8e0";

        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient();
                services.AddSingleton(new BizDiagClientConfiguration { SubscriptionKey = SubscriptionKey });
                services.AddTransient<BizDiagClient>();
            }).UseConsoleLifetime();

            var host = builder.Build();
            using var serviceScope = host.Services.CreateScope();

            var bizDiagClient = serviceScope.ServiceProvider.GetRequiredService<BizDiagClient>();
            try
            {
                var input = CreateInput();

                var result = await bizDiagClient.CalculateGrossMarginByYearAsync(input);
                foreach (var item in result)
                {
                    Console.WriteLine("Gross Margin {0}: {1}", item.Year, item.GrossMargin.GetValueOrDefault(0).ToString("P"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static List<GrossMarginPeriodicInput> CreateInput()
        {
            return new List<GrossMarginPeriodicInput>
            {
                new GrossMarginPeriodicInput
                {
                    Year = 2019,
                    SalesRevenue = 185_000_000,
                    CostOfGoodsSold = 78_000_000
                },
                new GrossMarginPeriodicInput
                {
                    Year = 2020,
                    SalesRevenue = 195_000_000,
                    CostOfGoodsSold = 82_000_000
                }
            };
        }
    }
}
