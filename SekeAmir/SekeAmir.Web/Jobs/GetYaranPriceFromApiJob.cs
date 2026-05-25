using Application.Contracts.Shop;
using Application.Tools;
using Quartz;

namespace SekeAmir.Web.Jobs;

[DisallowConcurrentExecution]
public class GetYaranPriceFromApiJob(IProductPrice productPriceRepository) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var isDebugMode = Settings.IsDebugMode();
            if (!isDebugMode)
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