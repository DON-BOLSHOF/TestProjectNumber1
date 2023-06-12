using Handlers;
using Loaders;
using Zenject;

namespace System.Installers
{
    public class GalleryLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GalleryLoader>().AsSingle();
        }
    }
}