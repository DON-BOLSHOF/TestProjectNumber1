using Zenject;

namespace System.Installers
{
    public class LocalGalleryStorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LocalGalleryStorage>().AsSingle();
        }
    }
}