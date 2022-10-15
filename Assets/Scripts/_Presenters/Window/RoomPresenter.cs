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
    public class RoomPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly IWindowService _windowService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private RoomView _roomView;

        public RoomPresenter(SignalBus signalBus,
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
            if (_windowService.IsWindowShowing<RoomView>()) return;

            if (_windowService.GetWindow<RoomView>() != null)
                _roomView = (RoomView)_windowService.ShowWindow<RoomView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.BaseWindow).Value;

                if (holderTansform != null)
                    _roomView = _factoryService.Spawn<RoomView>(holderTansform);
            }
        }

        public IWindow GetView() 
        {
            return _roomView;
        }
    }
}
