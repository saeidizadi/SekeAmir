using Quartz;

namespace SekeAmir.Web.Jobs.Configurations;

public static class JobsServiceRegistration
{
    public static void ConfigureJobsService(this IServiceCollection services)
    {
        var every5SecondCronExpression = "0/5 * * * * ?";
        var every9PmCronExpression = "0 0 9 * * ?";
        var every9AmCronExpression = "0 20 20 * * ?";
        var every5MinutesCronExpression = "0 0/5 * * * ?";
        var every10MinutesCronExpression = "0 0/10 * * * ?";
        var every30MinutesCronExpression = "0 0/30 * * * ?";
        var every1MinutesCron = "0 0/1 * * * ?";        
        var every2MinutesCron = "* 0/2 * * * ?";
		var every1HoursCron = "0 0 0/1 ? * * *";

		services.AddQuartz(q =>
		{
			q.UseMicrosoftDependencyInjectionJobFactory();
			var jobKey = new JobKey("GetYaranPriceFromApiJob");
			q.AddJob<GetYaranPriceFromApiJob>(opts => opts.WithIdentity(jobKey));
			q.AddTrigger(opts => opts
				.ForJob(jobKey)
				.WithIdentity("GetYaranPriceFromApiJob-trigger")
				.WithCronSchedule(every1HoursCron)
			);
		});

		services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}