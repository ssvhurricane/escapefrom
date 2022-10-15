using Commands.Button;
using System;
using UnityEngine;
using Zenject;

namespace Services.Window
{
    public class ShowWindowButtonCommand : AbstractButtonCommand
    {
#pragma warning disable 0649
#if UNITY_EDITOR
        [AttributeWindowType]
#endif
        [SerializeField] private string _windowType;
        [SerializeField] private bool _secondTapCloseWindow;
        [SerializeField] private bool _hideAllPreviousWindows = true;
        [SerializeField] private bool _showOnAwake;

        private IWindowService _windowService;

        [Inject]
        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;

            if (_showOnAwake)
                Activate();
        }

        protected override void Awake()
        {
            if (string.IsNullOrEmpty(_windowType))
                Debug.LogError($"[ShowWindowButtonCommand] view type {_windowType} can't be null or empty!");

            base.Awake();
        }

        public override void Activate()
        {
            if (string.IsNullOrEmpty(_windowType))
            {
                Debug.LogError($"[ShowWindowButtonCommand] window type {_windowType} can't be null or empty!");
                return;
            }

            var type = Type.GetType(_windowType);

            if (_windowService.IsWindowShowing(type))
            {
                if (_secondTapCloseWindow)
                    _windowService.HideWindow(type);
            }
            else
            {
                if (_hideAllPreviousWindows)
                    _windowService.HideAllWindows();

                _windowService.ShowWindow(type);
            }
        }
    }
}