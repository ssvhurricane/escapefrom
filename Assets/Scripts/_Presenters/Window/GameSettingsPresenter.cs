using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Window;
using System.Linq;
using UnityEngine;
using View.Window;
using Zenject;

namespace Presenters.Window
{
    public class GameSettingsPresenter 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly IWindowService _windowService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private GameSettingsView _gameSettingsView;

        public GameSettingsPresenter(SignalBus signalBus,
            LogService logService,
            IWindowService windowService,
            FactoryService factoryService,
            HolderService holderService) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;

            _factoryService = factoryService;
            _holderService = holderService;

            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message, 
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
        }

        public void ShowView()
        {
            if (_windowService.IsWindowShowing<GameSettingsView>()) return;
            
            if (_windowService.GetWindow<GameSettingsView>() != null)
                _gameSettingsView = (GameSettingsView)_windowService.ShowWindow<GameSettingsView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _gameSettingsView = _factoryService.Spawn<GameSettingsView>(holderTansform);
            }
        }

        public IWindow GetView() 
        {
            return _gameSettingsView;
        }
    }
}
