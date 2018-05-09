using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Reflection;

namespace EK.EmptyKingdom
{
    public class EkWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var classes = Types.FromThisAssembly();
            var r = container.Register(Classes.FromAssembly(Assembly.GetExecutingAssembly()));
            //.Configure(c => c.LifeStyle.));
        }
    }
}
