using Services.Window;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/DebugSettingsView", WindowType.BaseWindow)]
    public class DebugSettingsView : BaseWindow
    {
        [SerializeField] protected WindowType Type;

        private SignalBus _signalBus;

        // TODO: Если выбран режим Debug
        // то в низу окна появляется текстовая подсказка с комбинацией клавиш которые необходимо нажать
        // чтобы вызвать меню отладки и меню читов.
        // 1. Добавить вверху таб меню с прокруткой вверх или низ.
        // 2. Добавить возможность добавлять элементы на каждую вкладку
        // 3. Меню состоит из элементов:
        // Base Debug Info Tab;
        // NetWork Info;
        // Show Debug Scenes;
        // etc.

        [Inject]
        public void Constгuсt(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }
    }
}