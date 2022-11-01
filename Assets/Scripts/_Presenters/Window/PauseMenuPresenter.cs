using Constants;
using Services.Anchor;
using Services.Camera;
using Services.Essence;
using Services.Factory;
using Services.Item;
using Services.Log;
using Services.Pool;
using Services.Project;
using Services.Scene;
using Services.Window;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using View.Window;
using Zenject;

namespace Presenters.Window
{
    public class PauseMenuPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly IWindowService _windowService;
        private readonly ISceneService _sceneService;
        private readonly EssenceService _essenceService;
        private readonly CameraService _cameraService;
        private readonly PoolService _poolService;
        private readonly ItemService _itemService; 
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;
        private readonly ProjectService _projectService;

        private readonly GameSettingsPresenter _gameSettingsPresenter;

        private PauseMenuView _pauseMenuView;
        private MainHUDView _mainHUDView;
        private GameSettingsView _gameSettingsView;
        public PauseMenuPresenter(SignalBus signalBus,
            LogService logService,
            ISceneService sceneService,
            IWindowService windowService,
            EssenceService essenceService,
            CameraService cameraService,
            PoolService poolService,
            ItemService itemService, 
            FactoryService factoryService,
            HolderService holderService,
            ProjectService projectService,
            GameSettingsPresenter gameSettingsPresenter
            )
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;
            _sceneService = sceneService;
            _essenceService = essenceService;
            _cameraService = cameraService;
            _poolService = poolService;
            _itemService = itemService;

            _factoryService = factoryService;
            _holderService = holderService;
            _projectService = projectService;

           _gameSettingsPresenter = gameSettingsPresenter;
          
            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
          
        }
        public void ShowView()
        {
            if (_windowService.IsWindowShowing<PauseMenuView>())
                return;

            OnDisposeAll();

            _projectService.CursorLocked(true, CursorLockMode.Confined);
            _projectService.PauseGame(); 
            
            if (_windowService.GetWindow<PauseMenuView>() != null)
                _pauseMenuView = (PauseMenuView)_windowService.ShowWindow<PauseMenuView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _pauseMenuView = _factoryService.Spawn<PauseMenuView>(holderTansform);
            }

            if (_windowService.IsWindowShowing<MainHUDView>())
            {
                _mainHUDView = (MainHUDView)_windowService.GetWindow<MainHUDView>();
                _windowService.HideWindow<MainHUDView>();
            }

            if (_pauseMenuView._backToGameButton != null)
            {
                OnDispose(_pauseMenuView._backToGameButton);

                _pauseMenuView._backToGameButton.onClick.AddListener(() => OnPauseMenuViewButtonClick(_pauseMenuView._backToGameButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                 Services.Log.LogType.Error,
                 $"{_pauseMenuView._backToGameButton} = null!",
                 LogOutputLocationType.Console);
            }

            if (_pauseMenuView._settingsButton != null)
            {
                OnDispose(_pauseMenuView._settingsButton);

                _pauseMenuView._settingsButton.onClick.AddListener(()=> OnPauseMenuViewButtonClick(_pauseMenuView._settingsButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                   Services.Log.LogType.Error,
                   $"{_pauseMenuView._settingsButton} = null!",
                   LogOutputLocationType.Console);
            }

            if (_pauseMenuView._quitMainMenuButton != null)
            {
                OnDispose(_pauseMenuView._quitMainMenuButton);

                _pauseMenuView._quitMainMenuButton.onClick.AddListener(()=> OnPauseMenuViewButtonClick(_pauseMenuView._quitMainMenuButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                      Services.Log.LogType.Error,
                      $"{_pauseMenuView._quitMainMenuButton} = null!",
                      LogOutputLocationType.Console);
            }
        }

        public IWindow GetView() 
        {
            return _pauseMenuView;
        }

        private void OnPauseMenuViewButtonClick(int buttonId)
        {
            _projectService.CursorLocked(true, CursorLockMode.Confined);

            if (buttonId == _pauseMenuView._backToGameButton.GetInstanceID())
            {
                _projectService.CursorLocked(false, CursorLockMode.Locked);

                _logService.ShowLog(GetType().Name,
                      Services.Log.LogType.Message,
                      "Call OnBackToGameButtonClick Method.",
                      LogOutputLocationType.Console);
               
                _windowService.HideWindow<PauseMenuView>();
                _windowService.ShowWindow<MainHUDView>();
              
                _projectService.StartGame();
            }

            if (buttonId == _pauseMenuView._settingsButton.GetInstanceID())
            {
                _logService.ShowLog(GetType().Name,
                        Services.Log.LogType.Message,
                        "Call  OnSettingsButtonClick Method.",
                        LogOutputLocationType.Console);

                _gameSettingsPresenter.ShowView();
                _gameSettingsView = (GameSettingsView)_gameSettingsPresenter.GetView();

                if (_gameSettingsView._backButton != null)
                {
                    OnDispose(_gameSettingsView._backButton);

                    _gameSettingsView._backButton.onClick.AddListener(() => OnGameSettingsViewButtonClick(_gameSettingsView._backButton.GetInstanceID()));
                }

                _windowService.HideWindow<PauseMenuView>();
            }

            if (buttonId == _pauseMenuView._quitMainMenuButton.GetInstanceID())
            {
                _logService.ShowLog(GetType().Name,
                        Services.Log.LogType.Message,
                        "Call OnQuitMainMenuButtonClick Method.",
                        LogOutputLocationType.Console);

                _projectService.CursorLocked(true, CursorLockMode.Confined);
                _projectService.StopGame();

                _cameraService.ClearServiceValues();
                _essenceService.ClearServiceValues();
                _poolService.ClearServiceValues();
                _windowService.ClearServiceValues();
                _itemService.ClearServiceValues();

                _sceneService.LoadLevelAdvanced(SceneServiceConstants.MainMenu, SceneService.LoadMode.Unitask);
            }
        }
        private void OnGameSettingsViewButtonClick(int buttonId)
        {
            if (buttonId == _gameSettingsView._backButton.GetInstanceID())
            {
                _logService.ShowLog(GetType().Name,
                      Services.Log.LogType.Message,
                      "Call OnBackButtonClick Method.",
                      LogOutputLocationType.Console);

                _windowService.HideWindow<GameSettingsView>();

               _windowService.ShowWindow<PauseMenuView>();
            }
        }
        private void OnDisposeAll()
        {
            _pauseMenuView?._backToGameButton?.onClick.RemoveAllListeners();
            _pauseMenuView?._settingsButton?.onClick.RemoveAllListeners();
            _pauseMenuView?._quitMainMenuButton?.onClick.RemoveAllListeners();
            _pauseMenuView?._backToGameButton?.onClick.RemoveAllListeners();
        }

        private void OnDispose(Button disposable)
        {
            disposable?.onClick.RemoveAllListeners();
        }
    }
}