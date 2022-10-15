using Zenject;

namespace Bootstrap
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameInstaller>();
        }
    }
}
