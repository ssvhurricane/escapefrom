using Commands.Button;
using Zenject;

namespace Services.Window
{
    public class HideWindowButtonCommand : AbstractButtonCommand
    {
        private IWindowService _wndowService;

        [Inject]
        public void Construct(IWindowService windowService)
        {
            _wndowService = windowService;
        }
        public override void Activate()
        {
            _wndowService.HideWindow<IWindow>();
        }
    }
}