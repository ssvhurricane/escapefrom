using Constants;
using Data.Settings;
using Model;
using Presenters;
using Presenters.Window;
using Services.Ability;
using Services.Animation;
using Services.Pool;
using Services.Resources;
using Services.Window;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using View;
using View.Window;
using Zenject;

namespace Services.Input
{
    public class InputService : IFixedTickable
    {
        private readonly SignalBus _signalBus;

        private readonly InputServiceSettings[] _inputServiceSettings;
        private InputServiceSettings _settings;
       
        private readonly AbilityService _abilityService;
        private readonly AnimationService _animationService;
        private PoolService _poolService;
        private ResourcesService _resourcesService;

        private readonly IWindowService _windowService;

        private readonly PauseMenuPresenter _pauseMenuPresenter;
        private readonly MainHUDPresenter _mainHUDPresenter;
        private IPresenter _playerPresenter;
       
        private TopDownGameInput _topDownGameInput;

        private IAbility _playerNoneAbility,
                                _playerIdleAbility,
                                     _playerMoveAbility,
                                         _playerRotateAbility,
                                             _playerJumpAbility;
                                                            
                                                                   
        private IEnumerable<IAbility> _playerAttackAbilities;
        private IEnumerable<IAbility> _wolfAbilities;
                                               

        private bool _startProc, 
                        _shiftModifier = false;
    
        private MainHUDView _mainHudView;
        private PlayerView _playerView;
    
        private Dictionary<int, PlayerAbilityItemView> _playerAbilityItems = new Dictionary<int, PlayerAbilityItemView>();
        KeyValuePair<int, PlayerAbilityItemView> _playerAbility;

        public InputService(SignalBus signalBus,
            InputServiceSettings[] inputServiceSettings,
            AbilityService abilityService,
            AnimationService animationService,
            IWindowService windowService,
            PauseMenuPresenter pauseMenuPresenter,
            MainHUDPresenter mainHUDPresenter,
            PoolService poolService,
            ResourcesService resourcesService
            )
        {
            _signalBus = signalBus;
            _inputServiceSettings = inputServiceSettings;

            _abilityService = abilityService;
            _animationService = animationService;
            _windowService = windowService;

            _pauseMenuPresenter = pauseMenuPresenter;
            _mainHUDPresenter = mainHUDPresenter;

            _poolService = poolService;
            _resourcesService = resourcesService;
            
            _settings = _inputServiceSettings?.FirstOrDefault(s => s.Id == InputServiceConstants.TopDownGameId);

            _topDownGameInput = new TopDownGameInput();

            // Bind Select Ability.
            _topDownGameInput.Player.Arrow.performed += value => 
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    var nameControl = value.control.name;

                    if (nameControl == "upArrow" || nameControl == "up")
                    {
                        if (!_mainHudView.VerticalAbilityPanel.gameObject.activeSelf)
                            _mainHudView.VerticalAbilityPanel.gameObject.SetActive(true);

                        if (_playerAbility.Value != null)
                            _playerAbility.Value._image.color = Color.white;

                        if (((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Id == AbilityServiceConstants.PlayerNoneAbility
                        || _playerAbility.Key <= 0)
                        {
                            _playerAbility = _playerAbilityItems.LastOrDefault();
                        }
                        else
                        {
                             _playerAbility = _playerAbilityItems.FirstOrDefault(item=>item.Key == _playerAbility.Key - 1);
                        } 
                        
                        if (_playerAbility.Value != null)
                                _playerAbility.Value._image.color = Color.red;
                        
                         ((ILiveModel)_playerPresenter.GetModel())
                             .SetCurrentAbility(_playerAttackAbilities.FirstOrDefault(ability => ability.Id == _playerAbility.Value.Id));

                         _mainHudView.PlayerAbilityContainer.GetComponent<Image>().sprite = ((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Icon;
                   
                    }
                    else if (nameControl == "downArrow" || nameControl == "down")
                    {
                        if(!_mainHudView.VerticalAbilityPanel.gameObject.activeSelf)
                            _mainHudView.VerticalAbilityPanel.gameObject.SetActive(true);

                        // ToDo move logic in View...
                        if (_playerAbility.Value != null)
                            _playerAbility.Value._image.color = Color.white;

                        if (((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Id == AbilityServiceConstants.PlayerNoneAbility 
                        || _playerAbility.Key >= _playerAbilityItems.Count() - 1)
                        {
                            _playerAbility = _playerAbilityItems.FirstOrDefault();
                        }
                        else
                        {
                            _playerAbility = _playerAbilityItems.FirstOrDefault(item => item.Key == _playerAbility.Key + 1);
                        }
                        // ToDo move logic in View...
                        if (_playerAbility.Value != null)
                            _playerAbility.Value._image.color = Color.red;

                        ((ILiveModel)_playerPresenter.GetModel())
                            .SetCurrentAbility(_playerAttackAbilities.FirstOrDefault(ability => ability.Id == _playerAbility.Value.Id));

                        _mainHudView.PlayerAbilityContainer.GetComponent<Image>().sprite = ((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Icon;
                       
                    }
                }
            };

            // Hide Show Ability.
            _topDownGameInput.Player.F.performed += value =>
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    if (_mainHudView.VerticalAbilityPanel.gameObject.activeSelf)
                        _mainHudView.VerticalAbilityPanel.gameObject.SetActive(false);

                    ((ILiveModel)_playerPresenter.GetModel())
                           .SetCurrentAbility(_playerNoneAbility);
                    _mainHudView.PlayerAbilityContainer.GetComponent<Image>().sprite = ((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Icon;
                    _abilityService.UseAbility((IAbilityWithOutParam)((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility(), _playerPresenter, ActionModifier.None);

                }
            };


            // Bind Player Base Attack Ability.
            _topDownGameInput.Player.LeftMouseButton.performed += value =>
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    if (_mainHudView.VerticalAbilityPanel.gameObject.activeSelf)
                        _mainHudView.VerticalAbilityPanel.gameObject.SetActive(false);

                    if (((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Id != AbilityServiceConstants.PlayerNoneAbility) 
                                _abilityService.UseAbility((IAbilityWithOutParam)((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility(), _playerPresenter, ActionModifier.None);
                }
            };

            // Bind Player Power Attack Ability.
            _topDownGameInput.Player.MiddleMouseButton.performed += value =>
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    if (_mainHudView.VerticalAbilityPanel.gameObject.activeSelf)
                        _mainHudView.VerticalAbilityPanel.gameObject.SetActive(false);

                    if (((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Id != AbilityServiceConstants.PlayerNoneAbility)
                         _abilityService.UseAbility((IAbilityWithOutParam)((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility(), _playerPresenter, ActionModifier.Power);
                }
            };

             // Bind Player Wolf Attack Ability.
            _topDownGameInput.Player.RightMouseButton.performed += value =>
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    // TODO:
                }
            };

            // Bind Player Jump Ability.
            _topDownGameInput.Player.Space.performed += value =>
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    _abilityService.UseAbility((IAbilityWithOutParam)_playerJumpAbility, _playerPresenter, ActionModifier.None);
                }
            };

            // Press Shift Button.
            _topDownGameInput.Player.Shift.started += value => 
            {
                _shiftModifier = true;
            };

            _topDownGameInput.Player.Shift.canceled += value =>
            {
                _shiftModifier = false;
            };

            // Back Menu.
            _topDownGameInput.Player.Esc.performed += value =>
            {
                if (_windowService.IsWindowShowing<PauseMenuView>()) return;

                if (_windowService.IsWindowShowing<GameSettingsView>()) return;

                _pauseMenuPresenter.ShowView();
            };
        }
        public void ClearServiceValues()
        {
            _startProc = false;
            _shiftModifier = false;
            _playerAbilityItems.Clear();
            _topDownGameInput.Disable();
        }

        public void FixedTick()
        {
            if (_startProc && Time.timeScale == 1.0f)
            {
                if (_topDownGameInput.Player.WASD.ReadValue<Vector2>() != Vector2.zero)
                {
                    if (!_shiftModifier)
                    {
                        // Bind Player Move Ability.
                        _abilityService.UseAbility((IAbilityWithVector2Param)_playerMoveAbility
                         , _playerPresenter,
                         _topDownGameInput.Player.WASD.ReadValue<Vector2>(), ActionModifier.None);

                        // Bind Player Rotate Ability.
                        _abilityService.UseAbility((IAbilityWithVector2Param)_playerRotateAbility
                            , _playerPresenter,
                            _topDownGameInput.Player.WASD.ReadValue<Vector2>(), ActionModifier.None);
                    }
                    else
                    {
                        // Bind Player Move Ability.
                        _abilityService.UseAbility((IAbilityWithVector2Param)_playerMoveAbility
                         , _playerPresenter,
                         _topDownGameInput.Player.WASD.ReadValue<Vector2>(), ActionModifier.Shift);

                        // Bind Player Rotate Ability.
                        _abilityService.UseAbility((IAbilityWithVector2Param)_playerRotateAbility
                            , _playerPresenter,
                            _topDownGameInput.Player.WASD.ReadValue<Vector2>(), ActionModifier.Shift);
                    }
                }
                else 
                {
                    _abilityService.UseAbility((IAbilityWithOutParam)_playerIdleAbility, _playerPresenter, ActionModifier.None);
                }
            }
        }

        public void TakePossessionOfObject(IPresenter presenter)
        {
            _playerPresenter = (PlayerPresenter) presenter;

            _playerView = (PlayerView) _playerPresenter.GetView();

            CachingAbilities();

            InitAbilities();

            _startProc = true;
            _topDownGameInput.Enable();
        }

        private void InitAbilities() 
        {
            PlayerAbilityItemView playerAbilityItemView;

            _mainHudView = (MainHUDView)_mainHUDPresenter.GetView();
            _mainHudView.VerticalAbilityPanel.gameObject.SetActive(false);

            // Init Player Ability.
            ((ILiveModel)_playerPresenter.GetModel())
                .SetCurrentAbility(_playerNoneAbility);

            _mainHudView.PlayerAbilityContainer.GetComponent<Image>().sprite = ((ILiveModel) _playerPresenter.GetModel()).GetCurrentAbility().Icon;

            _abilityService.UseAbility((IAbilityWithOutParam)((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility(), _playerPresenter, ActionModifier.None);

            _poolService.InitPool(PoolServiceConstants.PlayerAbilityItemViewPool);

            for (var item = 0; item < _playerAttackAbilities.Count(); item++) 
            {
                 playerAbilityItemView
                    = (PlayerAbilityItemView)_poolService.Spawn<PlayerAbilityItemView>(_mainHudView.VerticalAbilityPanel.transform);

                 playerAbilityItemView._image.sprite = _playerAttackAbilities.ToList()[item].Icon;

                 playerAbilityItemView.Id =_playerAttackAbilities.ToList()[item].Id;

                 _playerAbilityItems.Add(item, playerAbilityItemView);
            }
        }

        private void CachingAbilities()
        {  
            // Caching Player None Ability.
            _playerNoneAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerNoneAbility);

            // Caching Player Move Ability.
            _playerMoveAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerMoveAbility);

            // Caching Player Rotate Ability.
            _playerRotateAbility = _abilityService.GetAbilityById(_playerPresenter, 
                AbilityServiceConstants.PlayerRotateAbility);

            // Caching Player Jump Ability.
            _playerJumpAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerJumpAbility);

            // Caching Player Idle Ability.
            _playerIdleAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerIdleAbility);

            _playerAttackAbilities = _abilityService.GetAbilitiesyByAbilityType(_playerPresenter,
                AbilityType.AttackAbility);
        }
    }
}