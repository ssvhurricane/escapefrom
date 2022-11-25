using Constants;
using Data.Settings;
using Model.Inventory;
using Services.Ability;
using Services.Item;

namespace Model
{
    public class PlayerModel : ILiveModel
    {
        public string Id => _settings.Id;

        public ModelType ModelType { get; set; }

        private readonly PlayerSettings _settings;
        private PlayerAbilityContainer _playerAbilityContainer;
        private PlayerInventoryContainer _playerInventoryContainer;

        private PlayerIdleAbility _playerIdleAbility;
        private PlayerMoveAbility _playerMoveAbility;
        private PlayerRotateAbility _playerRotateAbility;
        private PlayerHeadRotateAbility _playerHeadRotateAbility;
        private PlayerJumpAbility _playerJumpAbility;
        
        private PlayerBaseAttackAbility _playerAttackAbility;
        private PlayerAdvancedAttackAbility _playerRangeAttackAbility;
        
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
                PlayerHeadRotateAbility playerHeadRotateAbility,
                PlayerJumpAbility playerJumpAbility,
               
                PlayerBaseAttackAbility playerAttackAbility,
                PlayerAdvancedAttackAbility playerRangeAttackAbility,
                
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
            _playerHeadRotateAbility = playerHeadRotateAbility;
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
            _playerAbilityContainer= new PlayerAbilityContainer();
            
            _playerAbilityContainer.abilities.Add(_playerIdleAbility);
            _playerAbilityContainer.abilities.Add(_playerMoveAbility);
            _playerAbilityContainer.abilities.Add(_playerRotateAbility);
            _playerAbilityContainer.abilities.Add(_playerHeadRotateAbility);
            _playerAbilityContainer.abilities.Add(_playerJumpAbility);
            
            _playerAbilityContainer.abilities.Add(_playerAttackAbility);
            _playerAbilityContainer.abilities.Add(_playerRangeAttackAbility);
            
            _playerAbilityContainer.abilities.Add(_playerNoneCurrentAbility);
            _playerAbilityContainer.abilities.Add(_playerDeathAbility);
            _playerAbilityContainer.abilities.Add(_playerDetectionAbility);
            _playerAbilityContainer.abilities.Add(_playerHealthAbility);
            _playerAbilityContainer.abilities.Add(_playerStaminaAbility);
            _playerAbilityContainer.abilities.Add(_playerSpeakAbility); 
            
            // Init Inventory.
            _playerInventoryContainer = new PlayerInventoryContainer();
           
            _playerInventoryContainer.Items.Add(_axeItem);
            _playerInventoryContainer.Items.Add(_bowItem);
        }

        public IAbilityContainer GetAbilityContainer()
        {
            return _playerAbilityContainer;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            return _playerInventoryContainer;
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
