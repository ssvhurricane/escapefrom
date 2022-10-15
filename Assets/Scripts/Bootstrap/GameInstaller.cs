using Model;
using Presenters;
using Presenters.Window;
using Services.Ability;
using Services.Movement;
using Utils.Helpers;
using Zenject;
using Services.Animation;
using Services.SFX;
using Services.VFX;
using Services.Detection;
using Services.Item;
using Services.Input;
using Services.Camera;
using Services.Tutorial;

namespace Bootstrap
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            InitExecutionOrder();

            InstallPresenters();
            InstallServices();
            InstallModels();
            InstallAbilities();
            InstallItems();
            InstallSignals();
        }
        void InitExecutionOrder() { }
       
        private void InstallPresenters()
        {
            // Window Presenters.
            Container.InstallElementAsSingle<MainHUDPresenter>();
            Container.InstallElementAsSingle<PauseMenuPresenter>();

            // View Presenters.
            Container.InstallElementAsSingle<PlayerPresenter>();
            Container.InstallElementAsSingle<WolfPresenter>();

            Container.InstallElementAsSingle<CameraPresenter>();
        }

        private void InstallServices() 
        {
            Container.InstallElementAsTransient<MovementService>(); 
            Container.InstallElementAsSingle<AbilityService>();
            Container.InstallElementAsSingle<ItemService>();
           
            Container.InstallElementAsSingle<AnimationService>();
            Container.InstallElementAsSingle<SFXService>();
            Container.InstallElementAsSingle<VFXService>();
            Container.InstallElementAsSingle<DetectionService>();

            Container.InstallElementAsSingle<CameraService>();

            Container.InstallElementAsSingle<InputService>(); 

            Container.InstallElementAsSingle<TutorialService>();
        }

        private void InstallAbilities() 
        {
            // Player Abilities.
            Container.InstallElementAsSingle<PlayerIdleAbility>();
            Container.InstallElementAsSingle<PlayerMoveAbility>();
            Container.InstallElementAsSingle<PlayerRotateAbility>();
            Container.InstallElementAsSingle<PlayerJumpAbility>();
           
            Container.InstallElementAsSingle<PlayerAttackAbility>();
            Container.InstallElementAsSingle<PlayerRangeAttackAbility>();

            Container.InstallElementAsSingle<PlayerNoneAbility>();
            Container.InstallElementAsSingle<PlayerDeathAbility>();
            Container.InstallElementAsSingle<PlayerDetectionAbility>();
            Container.InstallElementAsSingle<PlayerHealthAbility>(); 
            Container.InstallElementAsSingle<PlayerStaminaAbility>(); 
            Container.InstallElementAsSingle<PlayerSpeakAbility>();

            // Wolf Abilities.
            Container.InstallElementAsSingle<WolfIdleAbility>();
            Container.InstallElementAsSingle<WolfMoveAbility>(); 
            Container.InstallElementAsSingle<WolfRotateAbility>(); 

            Container.InstallElementAsSingle<WolfHealingAbility>(); 
            
            Container.InstallElementAsSingle<WolfPromptAbility>(); 
            Container.InstallElementAsSingle<WolfSpeakAbility>();
            Container.InstallElementAsSingle<WolfNoneAbility>();
        }

        public void InstallItems() 
        {
            Container.InstallElementAsSingle<AxeItem>();
            Container.InstallElementAsSingle<BowItem>();
        }

        private void InstallModels() 
        {
            Container.InstallModel<PlayerModel>();
            Container.InstallModel<WolfModel>();
        }

        private void InstallSignals() 
        {
          
        }
    }
}