using Data.Settings;
using Model.Inventory;
using Services.Ability;
using UniRx;

namespace Model
{
    public class WolfModel : ILiveModel
    {
        public string Id => _settings.Id;

        public ModelType ModelType { get; set; }

        private readonly WolfSettings _settings;
        private ReactiveProperty<WolfAbilityContainer> _wolfAbilityContainer;
        private IAbility _currentAbility;

        private WolfIdleAbility _wolfIdleAbility;
        private WolfMoveAbility _wolfMoveAbility;
        private WolfRotateAbility _wolfRotateAbility;

        private WolfHealingAbility _wolfHealingAbility;

        private WolfPromptAbility _wolfPromptAbility;
        private WolfSpeakAbility _wolfSpeakAbility;
        private WolfNoneAbility _wolfNoneAbility;

        public WolfModel(WolfSettings settings,
            WolfIdleAbility wolfIdleAbility,
            WolfMoveAbility wolfMoveAbility,
            WolfRotateAbility wolfRotateAbility,
            WolfHealingAbility wolfHealingAbility,
            WolfPromptAbility wolfPromptAbility,
            WolfSpeakAbility wolfSpeakAbility,
            WolfNoneAbility wolfNoneAbility) 
        {
            _settings = settings;

            _wolfIdleAbility = wolfIdleAbility;
            _wolfMoveAbility = wolfMoveAbility;
            _wolfRotateAbility = wolfRotateAbility;
            _wolfHealingAbility = wolfHealingAbility;
            _wolfPromptAbility = wolfPromptAbility;
            _wolfSpeakAbility = wolfSpeakAbility;
            _wolfNoneAbility = wolfNoneAbility;

            _wolfAbilityContainer = new ReactiveProperty<WolfAbilityContainer>();
            _wolfAbilityContainer.Value = new WolfAbilityContainer();

            //Init Base Ability.
            _wolfAbilityContainer.Value.abilities.Add(_wolfIdleAbility); 
            _wolfAbilityContainer.Value.abilities.Add(_wolfMoveAbility);
            _wolfAbilityContainer.Value.abilities.Add(_wolfRotateAbility);
            
            _wolfAbilityContainer.Value.abilities.Add(_wolfHealingAbility);

            _wolfAbilityContainer.Value.abilities.Add(_wolfPromptAbility);
            _wolfAbilityContainer.Value.abilities.Add(_wolfSpeakAbility);
            _wolfAbilityContainer.Value.abilities.Add(_wolfNoneAbility);
        }


        public IAbilityContainer GetAbilityContainer()
        {
            return _wolfAbilityContainer.Value;
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

        public IInventoryContainer GetInventoryContainer()
        {
            return null;
        }
    }
}