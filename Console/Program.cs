using System;
using System.Diagnostics;
using System.IO;
using log4net.Config;
using Quartz;
using Topshelf;
using Topshelf.Nancy;
using Topshelf.Quartz;

namespace WebSite {
    static class Program {
        private static readonly String ApplicationName = "Application";

        public static void Main(string[] args) {
            // Run Visual Studio as Administrator or the ports will not be opened.

            XmlConfigurator.ConfigureAndWatch(new FileInfo(@"log4net.config"));

            var host = HostFactory.New(hostConfiguration => {
                hostConfiguration.UseLog4Net();

                hostConfiguration.Service<NancySelfHost>(serviceFactoryBuilder => {
                    serviceFactoryBuilder.ScheduleQuartzJob(
                        quartzConfigurator => quartzConfigurator.WithJob(() => JobBuilder.Create<MainJob>().Build())
                        .AddTrigger(() => TriggerBuilder.Create().WithSimpleSchedule(builder => builder.WithIntervalInSeconds(5).RepeatForever()).Build())
                    );

                    serviceFactoryBuilder.WithNancyEndpoint(hostConfiguration, nancyConfigurator => {
                        nancyConfigurator.AddHost(scheme: NancySelfHost.DomainSchema, domain: NancySelfHost.LocalhostDomainName, port: NancySelfHost.DomainPortNumber);
                        nancyConfigurator.AddHost(scheme: NancySelfHost.DomainSchema, domain: NancySelfHost.MachineDomainName, port: NancySelfHost.DomainPortNumber);
                        nancyConfigurator.CreateUrlReservationsOnInstall();
                        nancyConfigurator.DeleteReservationsOnUnInstall();
                        nancyConfigurator.OpenFirewallPortsOnInstall(firewallRuleName: ApplicationName + "." + "FirewallRule");
                    });

                    serviceFactoryBuilder.ConstructUsing(hostSettings => new NancySelfHost());

                    serviceFactoryBuilder.BeforeStartingService(webService => { });
                    serviceFactoryBuilder.AfterStartingService(webService => { });

                    serviceFactoryBuilder.BeforeStoppingService(webService => { });
                    serviceFactoryBuilder.AfterStoppingService(webService => { });

                    serviceFactoryBuilder.WhenStarted(webService => webService.Start());
                    serviceFactoryBuilder.WhenStopped(webService => webService.Stop());

                    serviceFactoryBuilder.WhenPaused(webService => webService.Pause());

                    serviceFactoryBuilder.WhenContinued(webService => webService.Continue());
                    serviceFactoryBuilder.WhenShutdown(webService => webService.Shutdown());
                });

                hostConfiguration.EnableServiceRecovery(serviceRecovers => {
                    serviceRecovers.RestartService(3);
                    serviceRecovers.RestartComputer(5, "Restarting " + ApplicationName);
                    serviceRecovers.SetResetPeriod(2);
                });

                hostConfiguration.SetDescription("Description of " + ApplicationName);
                hostConfiguration.SetDisplayName(ApplicationName);
                hostConfiguration.SetServiceName(ApplicationName);
                hostConfiguration.EnablePauseAndContinue();
                hostConfiguration.EnableShutdown();
                hostConfiguration.UseAssemblyInfoForServiceInfo();
                hostConfiguration.RunAsLocalSystem();
                hostConfiguration.StartAutomatically();
            });

            host.Run();

            if (Environment.UserInteractive) {
                var uriText = NancySelfHost.DomainSchema + "://" + NancySelfHost.LocalhostDomainName + ":" + NancySelfHost.DomainPortNumber;
                Process.Start(uriText);
                Console.WriteLine("Press any key to Shutdown " + ApplicationName + ".");
                Console.ReadKey();
            }
        }
        public class MainJob : IJob {
            public void Execute(IJobExecutionContext argIJobExecutionContext) {
                Console.WriteLine("The current time is: {0}", DateTime.Now);
            }
        }
    }
}