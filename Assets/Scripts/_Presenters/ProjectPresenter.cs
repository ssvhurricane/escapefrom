using Bootstrap;
using Constants;
using Model;
using Presenters.Window;
using Services.Input;
using Services.Log;
using Services.Project;
using Signals;
using UnityEngine;
using View.Camera;
using Zenject;

namespace Presenters
{
    public class ProjectPresenter : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly MainMenuPresenter _mainMenuPresenter;
        private readonly ProjectModel _projectModel;
        private readonly ProjectService _projectService;

        private MainHUDPresenter _mainHUDPresenter;
        private PlayerPresenter _playerPresenter;
        private CameraPresenter _cameraPresenter;

        private  InputService _inputService;

        public ProjectPresenter(SignalBus signalBus,
            LogService logService,
            MainMenuPresenter mainMenuPresenter,
            MainHUDPresenter mainHUDPresenter,
            PlayerPresenter playerPresenter,
            CameraPresenter cameraPresenter,
            ProjectModel projectModel,
            ProjectService projectService,
            InputService inputService
           )
        {
            _signalBus = signalBus;
            _logService = logService;
            _mainMenuPresenter = mainMenuPresenter;
            _mainHUDPresenter = mainHUDPresenter;
            _playerPresenter = playerPresenter;
            _cameraPresenter = cameraPresenter;

            _projectModel = projectModel;
            _projectService = projectService;
        

            _inputService = inputService;

            _logService.ShowLog(GetType().Name, 
                Services.Log.LogType.Message,
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
           
            _signalBus.Subscribe<SceneServiceSignals.SceneLoadingCompleted>(data =>
            {
                if (data.Data == SceneServiceConstants.MainMenu)
                {
                    _logService.ShowLog(GetType().Name,
                          Services.Log.LogType.Message,
                          $"Subscribe SceneServiceSignals.SceneLoadingCompleted, Data = {data.Data}",
                          LogOutputLocationType.Console);

                    _inputService?.ClearServiceValues();

                    _mainMenuPresenter.ShowView(_projectService.GetProjectType());
                }

                // Offline Levels.
                if (data.Data == SceneServiceConstants.OfflineLevel1)
                {
                    //_logService.ShowLog(GetType().Name,
                    //    Services.Log.LogType.Message,
                    //    $"Subscribe SceneServiceSignals.SceneLoadingCompleted, Data ={data.Data}",
                    //    LogOutputLocationType.Console);

                    CreateGame();
                    
                   // Cursor.visible = false;
                }
            });


            _signalBus.Subscribe<SceneServiceSignals.SceneLoadingStarted>(data =>
            {
                _logService.ShowLog(GetType().Name,
                             Services.Log.LogType.Message,
                             $"Subscribe SceneServiceSignals.SceneLoadingStarted, Data ={data.Data}",
                             LogOutputLocationType.Console);
            });
        }

        public void Initialize()
        {
            // Entry point. 
            _projectService.Configurate();


            _inputService?.ClearServiceValues();

            _mainMenuPresenter.ShowView(_projectService.GetProjectType());
        }

        private void CreateRoom() 
        {
            // TODO: Need Update Spawn
            _logService.ShowLog(GetType().Name,
                           Services.Log.LogType.Message,
                           $"Create Room!",
                           LogOutputLocationType.Console);
        }

        private void CreateGame()
        {
            if (_projectService.GetProjectType() == ProjectType.Offline)
            {
                _mainHUDPresenter.ShowView();
               
                _playerPresenter.ShowView();
               
                _cameraPresenter.ShowView<FPSCameraView>(CameraServiceConstants.FPSCamera, _playerPresenter.GetView());
                
                _inputService.TakePossessionOfObject(_playerPresenter);

                //5. Get Game Flow

                //6. Start Game
                StartGame();
            } 
        }

        private void StartGame()
        {
            _logService.ShowLog(GetType().Name,
                            Services.Log.LogType.Message,
                            "Call StartGame Method.",
                            LogOutputLocationType.Console);
        }

        private void PauseGame() { }
    }
}