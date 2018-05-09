using Castle.MicroKernel.Registration;
using Castle.Windsor;
using log4net.Config;
using System;
using Topshelf;

namespace EK.EmptyKingdom
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            using (var container = new WindsorContainer())
            {
                container.Register(Classes.FromAssemblyInThisApplication()
                    .Where(a => true)
                    .LifestyleSingleton()
                    .WithServiceDefaultInterfaces());

                var host = HostFactory.New(x =>
                {
                    x.Service<EmptyKingdomService>(s =>
                    {
                        s.ConstructUsing(n => new EmptyKingdomService(container));
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsLocalService();

                    x.SetDescription("EmptyKingdom");
                    x.SetDisplayName("EmptyKingdom vkontakte wall updater");
                });
				var ec = host.Run();
                var exitCode = (int)Convert.ChangeType(ec, ec.GetTypeCode());
                Environment.ExitCode = exitCode;
            }

        }
    }
}
