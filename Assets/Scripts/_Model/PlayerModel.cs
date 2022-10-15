using Constants;
using Data.Settings;
using Model.Inventory;
using Services.Ability;
using Services.Item;
using UniRx;

namespace Model
{
    public class PlayerModel : ILiveModel
    {
        public string Id => _settings.Id;

        public ModelType ModelType { get; set; }

        private readonly PlayerSettings _settings;
        private ReactiveProperty<PlayerAbilityContainer> _playerAbilityContainer;
        private ReactiveProperty<PlayerInventoryContainer> _playerInventoryContainer;

        private PlayerIdleAbility _playerIdleAbility;
        private PlayerMoveAbility _playerMoveAbility;
        private PlayerRotateAbility _playerRotateAbility;
        private PlayerJumpAbility _playerJumpAbility;
        
        private PlayerAttackAbility _playerAttackAbility;
        private PlayerRangeAttackAbility _playerRangeAttackAbility;
        
        private PlayerNoneAbility _playerNoneCurrentAbility;
        private PlayerDeathAbility _playerDeathAbility;
        private PlayerDetectionAbility _playerDetectionAbility;
        private PlayerHealthAbility _playerHealthAbility;
        private PlayerStaminaAbility _playerStaminaAbility;
        private PlayerSpeakAbility _playerSpeakAbility;

        private IAbility _currentAbility;

        private AxeItem _axeItem;
        private BowItem _bowItem;
        public PlayerModel(PlayerSettings settings,
                PlayerIdleAbility playerIdleAbility,
                PlayerMoveAbility playerMoveAbility,
                PlayerRotateAbility playerRotateAbility,
                PlayerJumpAbility playerJumpAbility,
               
                PlayerAttackAbility playerAttackAbility,
                PlayerRangeAttackAbility playerRangeAttackAbility,
                
                PlayerNoneAbility playerNoneCurrentAbility,
                PlayerDeathAbility playerDeathAbility,
                PlayerDetectionAbility playerDetectionAbility,
                PlayerHealthAbility playerHealthAbility,
                PlayerStaminaAbility playerStaminaAbility,
                PlayerSpeakAbility playerSpeakAbility,

                AxeItem axeItem,
                BowItem bowItem

            )
        {
            _settings = settings;

            _playerIdleAbility = playerIdleAbility;
            _playerMoveAbility = playerMoveAbility;
            _playerRotateAbility = playerRotateAbility;
            _playerJumpAbility = playerJumpAbility;
           
            _playerAttackAbility = playerAttackAbility;
            _playerRangeAttackAbility = playerRangeAttackAbility;

            _playerNoneCurrentAbility = playerNoneCurrentAbility;
            _playerDeathAbility = playerDeathAbility;
            _playerDetectionAbility = playerDetectionAbility;
            _playerHealthAbility = playerHealthAbility;
            _playerStaminaAbility = playerStaminaAbility;
            _playerSpeakAbility = playerSpeakAbility;

            _axeItem = axeItem;
            _bowItem = bowItem;

            //Init Base Ability.
            _playerAbilityContainer = new ReactiveProperty<PlayerAbilityContainer>();
            _playerAbilityContainer.Value = new PlayerAbilityContainer();
            
            _playerAbilityContainer.Value.abilities.Add(_playerIdleAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerMoveAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerRotateAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerJumpAbility);
            
            _playerAbilityContainer.Value.abilities.Add(_playerAttackAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerRangeAttackAbility);
            
            _playerAbilityContainer.Value.abilities.Add(_playerNoneCurrentAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerDeathAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerDetectionAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerHealthAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerStaminaAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerSpeakAbility); 
            
            // Init Inventory.
            _playerInventoryContainer = new ReactiveProperty<PlayerInventoryContainer>();
            _playerInventoryContainer.Value = new PlayerInventoryContainer();
           
            _playerInventoryContainer.Value.Items.Add(_axeItem);
            _playerInventoryContainer.Value.Items.Add(_bowItem);
        }

        public IAbilityContainer GetAbilityContainer()
        {
            return _playerAbilityContainer.Value;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            return _playerInventoryContainer.Value;
        }

        public void SetCurrentAbility(IAbility ability) 
        {
            _currentAbility = ability;
        }

        public IAbility GetCurrentAbility() 
        {
            return _currentAbility;
        }

        public void Dispose()
        {

        }
    }
}
