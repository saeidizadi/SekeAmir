using Application.Contracts.Shop;
using Application.Tools;
using Quartz;

namespace SekeAmir.Web.Jobs;

[DisallowConcurrentExecution]
public class GetYaranPriceFromApiJob(IProductPrice productPriceRepository,IWebHostEnvironment webHostEnvironment) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            //var isDebugMode = Settings.IsDebugMode();
            if (webHostEnvironment.IsProduction())
            {
                await productPriceRepository.GetData();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}