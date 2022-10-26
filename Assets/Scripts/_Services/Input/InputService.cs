using Constants;
using Data.Settings;
using Model;
using Presenters;
using Presenters.Window;
using Services.Ability;
using Services.Animation;
using Services.Log;
using Services.Pool;
using Services.Resources;
using Services.Window;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using View;
using View.Camera;
using View.Window;
using Zenject;

namespace Services.Input
{
    public class InputService : IFixedTickable, ILateTickable
    {
        private readonly SignalBus _signalBus;

        private readonly InputServiceSettings[] _inputServiceSettings;
        private InputServiceSettings _settings;
       
        private readonly AbilityService _abilityService;
        private readonly AnimationService _animationService;
        private PoolService _poolService;
        private ResourcesService _resourcesService;
        private LogService _logService;

        private readonly IWindowService _windowService;

        private readonly PauseMenuPresenter _pauseMenuPresenter;
        private readonly MainHUDPresenter _mainHUDPresenter;
        private readonly CameraPresenter _cameraPresenter;

        private IPresenter _playerPresenter;
       
        private TopDownGameInput _topDownGameInput;

        private IAbility _playerNoneAbility,
                                _playerIdleAbility,
                                     _playerMoveAbility,
                                         _playerRotateAbility,
                                            _cameraRotateAbility,
                                               // _cameraFollowAbility,
                                                   // _cameraParentAbility,
                                                      _playerJumpAbility;
                                                            
                                                                   
        private IEnumerable<IAbility> _playerAttackAbilities;
                                               

        private bool _startProc, 
                        _shiftModifier, 
                            _crouchModifier = false;
    
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
            CameraPresenter cameraPresenter,
            PoolService poolService,
            ResourcesService resourcesService,
            LogService logService
            )
        {
            _signalBus = signalBus;
            _inputServiceSettings = inputServiceSettings;

            _abilityService = abilityService;
            _animationService = animationService;
            _windowService = windowService;

            _pauseMenuPresenter = pauseMenuPresenter;
            _mainHUDPresenter = mainHUDPresenter;
            _cameraPresenter = cameraPresenter;

            _poolService = poolService;
            _resourcesService = resourcesService;
            _logService = logService;
            
            _settings = _inputServiceSettings?.FirstOrDefault(s => s.Id == InputServiceConstants.TopDownGameId);

            _topDownGameInput = new TopDownGameInput();

            // Bind Select Ability(Weapon).
            _topDownGameInput.Player.SelectWeapon.performed += value => 
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    var nameControl = value.control.name;

                    if (nameControl == "upArrow" || nameControl == "up")
                    {
                        _logService.ShowLog(GetType().Name,
                            Services.Log.LogType.Message,
                            "Press Up(d-pad).",
                            LogOutputLocationType.Console);
                       
                       // TODO:
                   
                    }
                    else if (nameControl == "downArrow" || nameControl == "down")
                    {
                        _logService.ShowLog(GetType().Name,
                           Services.Log.LogType.Message,
                           "Press Down(d-pad).",
                           LogOutputLocationType.Console);

                      // TODO:
                    }
                }
            };

            // Interaction Ability.
            _topDownGameInput.Player.Interaction.performed += value =>
            {

                _logService.ShowLog(GetType().Name,
                    Services.Log.LogType.Message,
                    "Press E(X).",
                    LogOutputLocationType.Console);

                // TODO:
            };

            // Crouch Ability.
            _topDownGameInput.Player.Crouch.performed += value =>
            {
                _logService.ShowLog(GetType().Name,
                    Services.Log.LogType.Message,
                    "Press C(B).",
                    LogOutputLocationType.Console);

                _crouchModifier = true;
            };

            _topDownGameInput.Player.Crouch.canceled+= value =>
            {
                _logService.ShowLog(GetType().Name,
                    Services.Log.LogType.Message,
                    "Press C(B).",
                    LogOutputLocationType.Console);

                _crouchModifier = false;
            };


            // Bind Player Base Attack Ability.
            _topDownGameInput.Player.Attack1.performed += value =>
            {
                _logService.ShowLog(GetType().Name,
                            Services.Log.LogType.Message,
                            "Press LeftMouseButton(LT).",
                            LogOutputLocationType.Console);

                if (_startProc && Time.timeScale == 1.0f)
                {
                    

                    // TODO:
                }
            };

             // Bind Player Aim Down Sights Ability.
            _topDownGameInput.Player.Attack2.performed += value =>
            { 
                _logService.ShowLog(GetType().Name,
                            Services.Log.LogType.Message,
                            "Press RightMouseButton(RT).",
                            LogOutputLocationType.Console);

                if (_startProc && Time.timeScale == 1.0f)
                {
                   // TODO:
                }
            };

            // Bind Player Jump Ability.
            _topDownGameInput.Player.Jump.performed += value =>
            {
                if (_startProc && Time.timeScale == 1.0f)
                {
                    _logService.ShowLog(GetType().Name,
                            Services.Log.LogType.Message,
                            "Press Space(A).",
                            LogOutputLocationType.Console);

                    if(!_topDownGameInput.Player.Crouch.IsPressed())
                                        _abilityService.UseAbility((IAbilityWithOutParam)_playerJumpAbility, _playerPresenter, ActionModifier.None);
                }
            };

            // Press Shift Button.
            _topDownGameInput.Player.Run.started += value => 
            {
                _logService.ShowLog(GetType().Name,
                            Services.Log.LogType.Message,
                            "Press ShiftStart(LeftTrigger).",
                            LogOutputLocationType.Console);

                _shiftModifier = true;
            };

            _topDownGameInput.Player.Run.canceled += value =>
            {
                _shiftModifier = false;
            };

            // Back Menu.
            _topDownGameInput.Player.Pause.performed += value =>
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
            _crouchModifier = false;
            _playerAbilityItems.Clear();
            _topDownGameInput.Disable();
        }

        public void FixedTick()
        {
            if (_startProc && Time.timeScale == 1.0f)
            {
                if (_topDownGameInput.Player.Move.IsPressed())
                {
                    if (!_crouchModifier)
                    {
                        if (!_shiftModifier)
                        {
                            // Bind Player Move Ability.
                            _abilityService.UseAbility((IAbilityWithVector2Param)_playerMoveAbility
                             , _playerPresenter,
                             _topDownGameInput.Player.Move.ReadValue<Vector2>(), ActionModifier.None);
                        }
                        else
                        {
                            _abilityService.UseAbility((IAbilityWithVector2Param)_playerMoveAbility
                             , _playerPresenter,
                             _topDownGameInput.Player.Move.ReadValue<Vector2>(), ActionModifier.Shift);
                        }
                    }
                    else
                    {
                        _abilityService.UseAbility((IAbilityWithVector2Param)_playerMoveAbility
                            , _playerPresenter,
                            _topDownGameInput.Player.Move.ReadValue<Vector2>(), ActionModifier.Crouch);
                    }
                }
                else 
                    _abilityService.UseAbility((IAbilityWithOutParam)_playerIdleAbility, _playerPresenter, ActionModifier.None);

            }
        }

        public void LateTick()
        {
            if (_startProc && Time.timeScale == 1.0f && _topDownGameInput.Player.Look.triggered)
            {
                _abilityService.UseAbility((IAbilityWithVector2Param)_playerRotateAbility
               , _playerPresenter,
               _topDownGameInput.Player.Look.ReadValue<Vector2>(), ActionModifier.None);

                // Bind Camera Rotate Ability.
                _logService.ShowLog(GetType().Name,
                       Services.Log.LogType.Message,
                       "Control: " + _topDownGameInput.Player.Look.ReadValue<Vector2>().x + " | " + _topDownGameInput.Player.Look.ReadValue<Vector2>().y,
                       LogOutputLocationType.Console);

                _abilityService.UseAbility((IAbilityWithVector2Param)_cameraRotateAbility
                     , _cameraPresenter,
                _topDownGameInput.Player.Look.ReadValue<Vector2>(), ActionModifier.None);
            }
        }

        public void TakePossessionOfObject(IPresenter presenter)
        {
            _playerPresenter = (PlayerPresenter) presenter; 
            
            _playerView = (PlayerView) _playerPresenter.GetView();

            _cameraPresenter.ShowView<FPSCameraView>(CameraServiceConstants.FPSCamera, _playerPresenter.GetView());

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

            // Caching Camera Rotate Ability.
            _cameraRotateAbility = _abilityService.GetAbilityById(_cameraPresenter,
                AbilityServiceConstants.CameraRotateAbility);

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