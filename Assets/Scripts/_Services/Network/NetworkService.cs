using Data.Settings;
using Signals;
using Zenject;

namespace Services.Network
{
    public class NetworkService
    {
        private readonly SignalBus _signalBus;

        private NetworkServiceSettings _networkServiceSettings;

        public NetworkService(SignalBus signalBus,
            NetworkServiceSettings networkServiceSettings)
        {
            _signalBus = signalBus;

            _networkServiceSettings = networkServiceSettings;

            _signalBus.Subscribe<NetworkServiceSignals.Connect>(signal =>
            {
                OnConnect(signal.Host, signal.NetworkConnectAsType);
            });

            _signalBus.Subscribe<NetworkServiceSignals.Disconnect>(signal =>
            {
                OnDisconnect();
            });
        }

        public void Initialize()
        {
            switch ( _networkServiceSettings.NetworkEngine)
            {
                case NetworkEngine.Custom: 
                    {
                       // TODO:
                        break;
                    }
            }
        }

        public NetworkType GetNetworkType() 
        {
            return _networkServiceSettings.NetworkType;
        }

        public NetworkEngine GetNetworkEngine() 
        { 
            return _networkServiceSettings.NetworkEngine; 
        }

        public NetworkAuthMode GetNetworkAuthMode()
        { 
            return _networkServiceSettings.NetworkAuthMode;
        }

        public INetworkContext GetCurrnetContext()
        {
            // TODO:
            return null;
        }

        private void OnConnect(string hostName, NetworkConnectAsType networkConnectAsType)
        {
            switch (networkConnectAsType)
            {
                case NetworkConnectAsType.Client:
                    {
                        this.StartClient();

                        break;
                    }

                case NetworkConnectAsType.Host:
                    {
                        this.StartHost();

                        break;
                    }
                case NetworkConnectAsType.Server:
                    {
                        this.StartServer();

                        break;
                    }
            }
          
        }

        private void OnDisconnect()
        {
            // TODO:
        }

        private void StartServer()
        {
           // TODO:
        }

        private void StopServer()
        {
            // TODO:
        }

        private void StartClient()
        {
            // TODO:
        }

        private void StopClient()
        {
            // TODO:
        }

        private void StartHost()
        {
            // TODO:
        }

        private void StopHost()
        {
            // TODO:
        }
    }
}
