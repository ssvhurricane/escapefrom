using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Services.Localization
{
    public class LocalizationService
    {
        private readonly SignalBus _signalBus;

        private Dictionary<string, string> _localization = new Dictionary<string, string>();

        public string CurrentSystemLocalization { private get; set; }

        // TODO: ref Settings
        public string DefaultConfigFileName = "fac_localization";

        public readonly IReadOnlyList<LanguageInfo> IngameLanguages = new List<LanguageInfo>()
        {
            new LanguageInfo(Language.EN, "English").AddLanguageCode("en"),
            new LanguageInfo(Language.DE, "Deutsche").AddLanguageCode("de"),
            new LanguageInfo(Language.RU, "Русский").AddLanguageCode("ru"),
            new LanguageInfo(Language.ES, "Español").AddLanguageCode("es"),
            new LanguageInfo(Language.FR, "Français").AddLanguageCode("fr"),
            new LanguageInfo(Language.IT, "Italiano").AddLanguageCode("it"),
            new LanguageInfo(Language.BR, "Portuguese (Brazil)").AddLanguageCodeRegion("pt-br"),
            new LanguageInfo(Language.PT, "Portuguese").AddLanguageCodeRegion("pt-pt"),
            new LanguageInfo(Language.DA, "Dansk").AddLanguageCodeRegion("dk"),
            new LanguageInfo(Language.NO, "Norsk").AddLanguageCodeRegion("no"),
            new LanguageInfo(Language.NL, "Nederlands").AddLanguageCodeRegion("nl"),
            new LanguageInfo(Language.SE, "Svenska").AddLanguageCodeRegion("sv"),
            new LanguageInfo(Language.JA, "日本語").AddLanguageCodeRegion("ja"),
            new LanguageInfo(Language.KO, "한국어").AddLanguageCodeRegion("ko"),
            new LanguageInfo(Language.TW, "台湾").AddLanguageCodeRegion("tw"),
        };

        public LocalizationService(SignalBus signalBus)
        {
            _signalBus = signalBus; 
            
            if (CurrentLanguage == Language.Undefined)
            {
                ChangeLanguage(GetSystemLanguage());
            }
        }

        public bool HaveId(string id) => _localization?.ContainsKey(id) ?? false;

        public Language CurrentLanguage { get; private set; } = Language.EN;

        public LanguageInfo GetLanguageInfo(Language language)
        {
            return IngameLanguages.FirstOrDefault(info => info.Language == language);
        }

        public string TranslateByName(string name)
        {
            // TODO:

            return "";
        }

        public string TranslateById(string id)
        {
            // TODO:
            return "";
        }

        public string TranslateByIdWithParams(string id, params string[] inputs)
        {
            // TODO:

            return "";
        }

        public void ChangeLanguage(Language newLanguage) 
        {
            // TODO: 
        }

        private Language GetSystemLanguage()
        {
            return IngameLanguages.FirstOrDefault(info => info.IsCurrentLanguage(CurrentSystemLocalization))
                       ?.Language ??
                   Language.EN;
        }
    }
}
