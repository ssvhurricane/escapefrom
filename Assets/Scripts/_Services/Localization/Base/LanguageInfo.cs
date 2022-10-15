using System.Collections.Generic;

namespace Services.Localization
{
    public class LanguageInfo 
    {
        public Language Language { get; }
        public string LanguageName { get; }

        private string _languageCode = string.Empty;
        private List<string> _languageCodeWithRegion = new List<string>();

        public LanguageInfo(Language language, string languageName)
        {
            Language = language;
            LanguageName = languageName;
        }

        public LanguageInfo AddLanguageCode(string languageCode)
        {
            _languageCode = languageCode;
            return this;
        }

        public LanguageInfo AddLanguageCodeRegion(string languageCodeRegion)
        {
            _languageCodeWithRegion.Add(languageCodeRegion);
            return this;
        }

        public bool IsCurrentLanguage(string language)
        {
            language = language.ToLower();
            return !string.IsNullOrEmpty(_languageCode) ? language.Contains(_languageCode) : _languageCodeWithRegion.Contains(language);
        }
    }
}
