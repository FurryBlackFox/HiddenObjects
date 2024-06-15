using Services;
using Zenject;

namespace Installers
{
    public class RootServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ServerDataProvider>().AsSingle();
        }
    }
}