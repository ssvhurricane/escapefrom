using Zenject;

namespace Bootstrap
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<CoreInstaller>();
        }
    }
}