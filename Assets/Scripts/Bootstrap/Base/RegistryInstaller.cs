using Config;
using Data.Settings;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(fileName = "RegistryInstaller", menuName ="Installers/RegistryInstaller")]
    public class RegistryInstaller : ScriptableObjectInstaller
    {
        [Header("BaseSettings")]
        [SerializeField]  protected ProjectServiceSettingsRegistry ProjectServiceSettingsRegistry;
        [SerializeField] protected SceneServiceSettingsRegistry SceneSettingsRegistry;
        [SerializeField] protected PlayerSettingsRegistry PlayerSettingsRegistry;
        [SerializeField] protected WindowServiceSettingsRegistry WindowServiceSettingsRegistry;
        [SerializeField] protected ResourcesServiceSettingsRegistry ResourcesServiceSettingsRegistry;
        [SerializeField] protected CameraServiceSettingsRegistry CameraServiceSettingsRegistry;
        [SerializeField] protected InputServiceSettingsRegistry InputServiceSettingsRegistry;
        [SerializeField] protected MovementServiceSettingsRegistry MovementServiceSettingsRegistry;
        [SerializeField] protected AbilitySettingsRegistry AbilitySettingsRegistry;
        [SerializeField] protected PoolServiceSettingsRegistry PoolServiceSettingsRegistry;
        [SerializeField] protected ItemServiceSettingsRegistry ItemServiceSettingsRegistry;
        [SerializeField] protected LogServiceSettingsRegistry LogServiceSettingsRegistry;

        //[Header("GameplaySettings")] TODO:

        protected new DiContainer Container
        {
            get => _container;
            set => _container = value;
        }
        private DiContainer _container;

        public override void InstallBindings()
        {
            _container = base.Container;
            InstallRegistries();
            InstallListSettings();
        }

        private void InstallRegistries()
        {
            InstallRegistry(ProjectServiceSettingsRegistry);
            InstallRegistry(SceneSettingsRegistry);
            InstallRegistry(PlayerSettingsRegistry);
            InstallRegistry(WindowServiceSettingsRegistry);
            InstallRegistry(ResourcesServiceSettingsRegistry);
            InstallRegistry(CameraServiceSettingsRegistry);
            InstallRegistry(InputServiceSettingsRegistry);
            InstallRegistry(MovementServiceSettingsRegistry);

            InstallRegistry(AbilitySettingsRegistry);
            InstallRegistry(PoolServiceSettingsRegistry);

            InstallRegistry(ItemServiceSettingsRegistry);

            InstallRegistry(LogServiceSettingsRegistry);
        }
        private void InstallListSettings()
        {
            InstallRegistryData<ProjectServiceSettings>(ProjectServiceSettingsRegistry); 

            InstallRegistryData<PlayerSettings>(PlayerSettingsRegistry);
            InstallRegistryData<ResourcesServiceSettings>(ResourcesServiceSettingsRegistry);

            Container.Bind<SceneServiceSettings[]>()
               .FromInstance(SceneSettingsRegistry.GetItems().ToArray())
               .AsSingle();

            Container.Bind<WindowServiceSettings[]>()
            .FromInstance(WindowServiceSettingsRegistry.GetItems().ToArray())
            .AsSingle();

            Container.Bind<CameraServiceSettings[]>()
              .FromInstance(CameraServiceSettingsRegistry.GetItems().ToArray())
              .AsSingle();

            Container.Bind<InputServiceSettings[]>()
              .FromInstance(InputServiceSettingsRegistry.GetItems().ToArray())
              .AsSingle();
            
            Container.Bind<MovementServiceSettings[]>()
              .FromInstance(MovementServiceSettingsRegistry.GetItems().ToArray())
              .AsSingle();


            Container.Bind<AbilitySettings[]>()
            .FromInstance(AbilitySettingsRegistry.GetItems().ToArray())
            .AsSingle();

            Container.Bind<PoolServiceSettings[]>()
          .FromInstance(PoolServiceSettingsRegistry.GetItems().ToArray())
          .AsSingle();

            InstallRegistryData<LogServiceSettings>(LogServiceSettingsRegistry);

            Container.Bind<ItemServiceSettings[]>()
        .FromInstance(ItemServiceSettingsRegistry.GetItems().ToArray())
        .AsSingle();
        }

        private void InstallRegistry<TRegistry>(TRegistry registry)
        {
            Container
                .BindInterfacesAndSelfTo<TRegistry>()
                .FromInstance(registry)
                .AsSingle();
        }

        private void InstallRegistryData<T>(RegistryBase<T> instance) where T : class
        {
            Container.Bind<T>()
                .FromInstance(instance.Data)
                .AsSingle();
        }

        private void InstallArrayRegistryData<T>(RegistryListBase<T> instance) where T : class, IRegistryData
        {
            Container.Bind<T[]>()
                .FromInstance(instance
                    .GetItems()
                    .ToArray())
                .AsSingle();
        }

        public void InstallBindingsWithCustomContainer(DiContainer container)
        {
            _container = container;
            InstallRegistries();
            InstallListSettings();
        }
    }
}
