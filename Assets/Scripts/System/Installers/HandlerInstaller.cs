using Handlers.SystemHandler;
using Zenject;

namespace System.Installers
{
    public sealed class HandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StorageHandler>().AsSingle();
            Container.Bind<ErrorHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<DownloadHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<HandlersProvider>().AsSingle();
        }
    }
}