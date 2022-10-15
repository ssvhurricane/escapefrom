using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Network;
using Services.Window;
using System.Linq;
using UnityEngine;
using View.Window;
using Zenject;

namespace Presenters.Window
{
    public class NetConnectionPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly IWindowService _windowService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private readonly NetworkService _networkService;

        private NetConnectionView _netConnectionView;

        public NetConnectionPresenter(SignalBus signalBus,
            LogService logService,
            IWindowService windowService,
            FactoryService factoryService,
            HolderService holderService,
            NetworkService networkService) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;

            _factoryService = factoryService;
            _holderService = holderService;

            _networkService = networkService;

            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message, 
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
        }

        public void ShowView()
        {
            if (_windowService.IsWindowShowing<NetConnectionView>()) return;

            if (_windowService.GetWindow<NetConnectionView>() != null)
                _netConnectionView = (NetConnectionView)_windowService.ShowWindow<NetConnectionView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _netConnectionView = _factoryService.Spawn<NetConnectionView>(holderTansform);
            }
        }

        public IWindow GetView() 
        {
            return _netConnectionView;
        }
    }
}
