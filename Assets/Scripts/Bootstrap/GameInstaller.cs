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
            Container.InstallElementAsSingle<PlayerHeadRotateAbility>();
            Container.InstallElementAsSingle<PlayerJumpAbility>();
           
            Container.InstallElementAsSingle<PlayerBaseAttackAbility>();
            Container.InstallElementAsSingle<PlayerAdvancedAttackAbility>();

            Container.InstallElementAsSingle<PlayerNoneAbility>();
            Container.InstallElementAsSingle<PlayerDeathAbility>();
            Container.InstallElementAsSingle<PlayerDetectionAbility>();
            Container.InstallElementAsSingle<PlayerHealthAbility>(); 
            Container.InstallElementAsSingle<PlayerStaminaAbility>(); 
            Container.InstallElementAsSingle<PlayerSpeakAbility>();

            // Camera Abilities.
            Container.InstallElementAsSingle<CameraRotateAbility>();
            Container.InstallElementAsSingle<CameraFollowAbility>();
            Container.InstallElementAsSingle<CameraParentAbility>();
        }

        public void InstallItems() 
        {
            Container.InstallElementAsSingle<AxeItem>();
            Container.InstallElementAsSingle<BowItem>();
        }

        private void InstallModels() 
        {
            Container.InstallModel<PlayerModel>();
            Container.InstallModel<CameraModel>();
        }

        private void InstallSignals() 
        {
          
        }
    }
}