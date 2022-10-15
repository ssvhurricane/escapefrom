using Services.Log;
using TMPro;
using UnityEngine;
using Zenject;

namespace Services.Localization
{
    public class TranslatableComponent : MonoBehaviour
    {
        private SignalBus _signalBus;
        private LocalizationService _localizationService;
        private LogService _logService;

        [SerializeField] private bool _isUpdateOnStart = true;
        [SerializeField] private string _key;
        private TextMeshProUGUI _text;

        [Inject]
        public void Construct(SignalBus signalBus, LocalizationService localizationService, LogService logService) 
        {
            _signalBus = signalBus;
            _localizationService = localizationService;
            _logService = logService;

            if (_isUpdateOnStart) UpdateLabel();
        }
        public void UpdateLabel()
        {
            if (_text != null && !TryAssignText(gameObject, out _text)) return;
           
            _text.SetText(_localizationService.TranslateById(_key));
        }

        public  bool TryAssignText(GameObject go, out TextMeshProUGUI text)
        {
            text = go.GetComponent<TextMeshProUGUI>();

            if (text == null)
            {
                _logService.ShowLog(GetType().Name, 
                    Log.LogType.Error, 
                    ($"Object have no text for translate"),
                    LogOutputLocationType.Console);
            }

            return text != null;
        }
    }
}
