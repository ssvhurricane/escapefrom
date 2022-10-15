using System;
using Services.Window;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using System.Linq;
using Services.Network;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/NetConnectionView", WindowType.PopUpWindow)]
    public class NetConnectionView : PopUpWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] public Button BackButton; // TODO: ref;

        [SerializeField] protected Button ConnectionButton;

        [SerializeField] protected TMP_Dropdown DropdownSelectAs;

        [SerializeField] protected TMP_InputField InputFieldHostName;

        private SignalBus _signalBus;

        private IDisposable _disposableConnectButton;

        private NetworkConnectAsType _networkConnectAsType = NetworkConnectAsType.Server;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));

            OnDispose(_disposableConnectButton);

            _disposableConnectButton = ConnectionButton
           .OnClickAsObservable()
           .Subscribe(_ =>
           {
               switch (DropdownSelectAs.value)
               {
                   case 0:
                       {
                           _networkConnectAsType = NetworkConnectAsType.Client;
                           break;
                       }
                   case 1:
                       {
                           _networkConnectAsType = NetworkConnectAsType.Host;
                           break;
                       }
                   case 2:
                       {
                           _networkConnectAsType = NetworkConnectAsType.Server;
                           break;
                       }
               }
              
               signalBus.Fire(new NetworkServiceSignals.Connect(InputFieldHostName.text, _networkConnectAsType));

           }); // TODO:

        }

        private void OnDispose(IDisposable disposable)
        {
            disposable?.Dispose();
        }
    }
}