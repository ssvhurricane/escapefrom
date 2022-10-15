namespace Services.Window
{
    public class BaseWindowWithInput<TWindowInput> : BaseWindow, IWindowWithInput<TWindowInput>
        where TWindowInput : IWindowInput
    {
        public void Show(TWindowInput data)
        {
            Show();
        }
    }
}