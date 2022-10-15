using UniRx;

namespace Signals
{
    public class SceneServiceSignals
    {
        public class SceneLoadingStarted 
        {
            public Subject<float> Data { get; private set; }

            public SceneLoadingStarted(Subject<float> data)
            {
                Data = data;
            }
        }

        public class SceneLoadingCompleted 
        {
            public string Data { get; private set; }

            public SceneLoadingCompleted(string data) 
            {
                Data = data;
            }
        }

        public class SceneUnloadingCompleted 
        {
            public string Data { get; private set; }

            public SceneUnloadingCompleted(string data) 
            {
                Data = data;
            }
        }
    }
}