using Constants;
using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Project;
using Services.Scene;
using Services.Window;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using View.Window;
using Zenject;

namespace Presenters.Window
{
    public class MainMenuPresenter 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly IWindowService _windowService;
        private readonly ISceneService _sceneService;
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private readonly GameSettingsPresenter _gameSettingsPresenter;

        private MainMenuView _mainMenuView;
        private GameSettingsView _gameSettingsView;

        public MainMenuPresenter(SignalBus signalBus,
            LogService logService,
            ISceneService sceneService, 
            IWindowService windowService,
            FactoryService factoryService,
            HolderService holderService,
            GameSettingsPresenter gameSettingsPresenter)
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;
            _sceneService = sceneService;
            _factoryService = factoryService;
            _holderService = holderService;

            _gameSettingsPresenter = gameSettingsPresenter;

            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call Constructor Method",
                LogOutputLocationType.Console);
        }

        public void ShowView(ProjectType projectType = ProjectType.Offline) 
        {
            if (_windowService.IsWindowShowing<MainMenuView>()) return;

            OnDisposeAll();

            if (_windowService.GetWindow<MainMenuView>() != null)
                _mainMenuView = (MainMenuView)_windowService.ShowWindow<MainMenuView>();
            else 
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.BaseWindow).Value;

                if (holderTansform != null)
                     _mainMenuView = _factoryService.Spawn<MainMenuView>(holderTansform);
            } 
            
            if (_mainMenuView._startButton != null)
            {
                OnDispose(_mainMenuView._startButton);
                
               _mainMenuView._startButton.onClick.AddListener(()=> OnMainMenuViewButtonClick(_mainMenuView._startButton.GetInstanceID(), projectType));
               
            }
            else 
            {
                _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Error,
                $"{_mainMenuView._startButton.name} = null!",
                LogOutputLocationType.Console);
            }

            if (_mainMenuView._settingsButton != null)
            {
                OnDispose(_mainMenuView._settingsButton);

                _mainMenuView._settingsButton.onClick.AddListener(() => OnMainMenuViewButtonClick(_mainMenuView._settingsButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                  Services.Log.LogType.Error,
                  $"{_mainMenuView._settingsButton} = null!",
                  LogOutputLocationType.Console);
            }

            if (_mainMenuView._quitButton != null)
            {
                OnDispose(_mainMenuView._quitButton);

                _mainMenuView._quitButton.onClick.AddListener(()=> OnMainMenuViewButtonClick(_mainMenuView._quitButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Error,
                $"{_mainMenuView._quitButton} = null!",
                LogOutputLocationType.Console);
            }
        }

        private void OnMainMenuViewButtonClick(int buttonId, 
            ProjectType projectType = ProjectType.Offline)
            
        {
            if (buttonId == _mainMenuView._startButton.GetInstanceID()) 
            {
                _logService.ShowLog(GetType().Name,
               Services.Log.LogType.Message,
               "Call OnStartButtonClick Method.",
               LogOutputLocationType.Console);

                if (projectType == ProjectType.Offline)
                {
                    _sceneService.LoadLevelAdvanced(SceneServiceConstants.OfflineLevel1, SceneService.LoadMode.Unitask);
                }
                else if(projectType == ProjectType.Online)
                {
                   // TODO:
                }
            }

            if (buttonId == _mainMenuView._settingsButton.GetInstanceID())
            {
                _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call OnSettingsButtonClick Method.",
                LogOutputLocationType.Console);

                _gameSettingsPresenter.ShowView();
                _gameSettingsView = (GameSettingsView) _gameSettingsPresenter.GetView();


                if (_gameSettingsView._backButton != null)
                {
                    OnDispose(_gameSettingsView._backButton);

                    _gameSettingsView._backButton.onClick.AddListener(() => OnGameSettingsViewButtonClick(_gameSettingsView._backButton.GetInstanceID()));
                }

                _windowService.HideWindow<MainMenuView>();

                // ToDo clear subscribe, etc...
            }

            if (buttonId == _mainMenuView._quitButton.GetInstanceID())
            {
                _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call OnQuitButtonClick Method.",
                LogOutputLocationType.Console);
               
                Application.Quit();
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

                _windowService.ShowWindow<MainMenuView>();
            }
        }

        private void OnNetConnectionViewButtonClick(int buttonId)
        {
           // TODO:
        }
        private void OnDisposeAll()
        {
            _mainMenuView?._startButton?.onClick.RemoveAllListeners();
            _mainMenuView?._settingsButton?.onClick.RemoveAllListeners();
            _mainMenuView?._quitButton?.onClick.RemoveAllListeners();

           _gameSettingsView?._backButton?.onClick.RemoveAllListeners();
        }

        private void OnDispose(Button disposable)
        {
            disposable?.onClick.RemoveAllListeners();
        }
    }
}