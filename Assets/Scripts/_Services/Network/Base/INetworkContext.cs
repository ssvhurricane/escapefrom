namespace Services.Network
{
    public interface INetworkContext 
    {
        public void StartServer();

        public void StopServer();

        public void StartClient();

        public void StopClient();

        public void StartHost();

        public void StopHost();
    }
}