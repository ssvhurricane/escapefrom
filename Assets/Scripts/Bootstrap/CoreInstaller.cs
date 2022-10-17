using Signals;
using Services.Project;
using Services.Resources;
using Services.Scene;
using Services.Window;
using Utils.Helpers;
using Zenject;
using Presenters;
using Services.Essence;
using Model;
using Services.Factory;
using Presenters.Window;
using Services.Anchor;
using Services.Pool;
using Services.Log;
using Services.Localization;
using Services.SavePlayerData;

namespace Bootstrap
{
    public class CoreInstaller : Installer
    {
        public override void InstallBindings()
        {
            BindPresenters();

            BindServices();

            InstallSignals();

            InstallModels();
           
            InstallAnimations();

            InitExecutionOrder();

            InstallPools();
        }

        private void BindPresenters()
        { 
            Container.InstallElementAsSingle<MainMenuPresenter>();

            Container.Bind<IInitializable>().To<ProjectPresenter>().AsSingle(); 

            Container.InstallElementAsSingle<GameSettingsPresenter>();
        }
        void InitExecutionOrder() { }
       
        private void BindServices()
        {
            Container.InstallElementAsSingle<ProjectService>();

            Container.Bind<ISceneService>().To<SceneService>().AsSingle();
            
            Container.InstallElementAsSingle<EssenceService>();

            Container.Bind<IWindowService>().To<WindowService>().AsSingle();

            Container.InstallElementAsSingle<ResourcesService>();
           
            Container.InstallElementAsSingle<HolderService>();

            Container.InstallElementAsSingle<AnchorService>(); 
            
            Container.InstallElementAsSingle<FactoryService>();

            Container.InstallElementAsSingle<PoolService>();

            Container.InstallElementAsSingle<LogService>();

            Container.InstallElementAsSingle<LocalizationService>(); 
            
            Container.InstallElementAsSingle<SavePlayerDataService>();
        }
      
        private void InstallAnimations()
        {
            Container
                .BindInterfacesAndSelfTo<FadeAndScaleWindowAnimation>()
                .AsTransient();
        }
        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            // Scene Service Signals.
            Container.DeclareSignal<SceneServiceSignals.SceneLoadingStarted>();
            Container.DeclareSignal<SceneServiceSignals.SceneLoadingCompleted>();
            Container.DeclareSignal<SceneServiceSignals.SceneUnloadingCompleted>();

            // Service Signals.
            Container.DeclareSignal<EssenceServiceSignals.Register>();
            Container.DeclareSignal<EssenceServiceSignals.AddHolder>();
            Container.DeclareSignal<EssenceServiceSignals.Shown>().OptionalSubscriber();
            Container.DeclareSignal<EssenceServiceSignals.Hidden>().OptionalSubscriber();

            // Window Service Signals.
            Container.DeclareSignal<WindowServiceSignals.Register>();
            Container.DeclareSignal<WindowServiceSignals.AddHolder>();
            Container.DeclareSignal<WindowServiceSignals.Shown>().OptionalSubscriber();
            Container.DeclareSignal<WindowServiceSignals.Hidden>().OptionalSubscriber();

            // Anchor Service Signals.
            Container.DeclareSignal<AnchorServiceSignals.Add>();
            Container.DeclareSignal<AnchorServiceSignals.Activate>();
            Container.DeclareSignal<AnchorServiceSignals.Deactivate>();
        }

       public void InstallModels()
        {
            Container.InstallModel<ProjectModel>();
            Container.InstallModel<NetworkModel>();
        }

        private void InstallPools()
        {
            Container.InstallElementAsSingle<ViewPool>();
        }
    }
}