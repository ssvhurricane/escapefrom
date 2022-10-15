using Services.Network;

namespace Signals
{
    public class NetworkServiceSignals
    {

        public class Connect
        {
            //public int ID { get; }

            public string Host { get; }

            public NetworkConnectAsType NetworkConnectAsType { get; }

            public Connect(string host, NetworkConnectAsType networkConnectAsType)
            {
                Host = host;

                NetworkConnectAsType = networkConnectAsType;
            }
        }

        public class Disconnect
        {
            public Disconnect()
            {
                // TODO:
            }
        }
    }
}