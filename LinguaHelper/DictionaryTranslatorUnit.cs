using System.Runtime.Versioning;
using System.Text.Json.Serialization;

namespace LinguaHelper
{
    [SupportedOSPlatform("windows")]
    internal class DictionaryTranslatorUnit
    {
        readonly BrowserItem _dictionary;
        readonly BrowserItem _translator;

        public BrowserItem Dictionary => _dictionary;
        public BrowserItem Translator => _translator;

        public ColorTheme ColorTheme
        {
            set
            {
                _dictionary.ColorTheme = value;
                _translator.ColorTheme = value;
            }
        }

        [JsonConstructor]
        public DictionaryTranslatorUnit(BrowserItem dictionary, BrowserItem translator)
        {
            _dictionary = dictionary;
            _translator = translator;
            _dictionary.BoundObject.BrowserTextSelected += _boundObject_BrowserTextSelected;
        }

        private void _boundObject_BrowserTextSelected(object sender, BrowserTextSelectedEventArgs e)
        {
            if (_dictionary.TranslateOnSelection)
                _translator.Go(e.Text, false);
        }
        public void Go(string text,bool isUserCommand, bool force = false)
        {
            _dictionary.Go(text, isUserCommand, force);
            _translator.Go(text, isUserCommand, force);
        }
        public void LoadDefaultPage()
        {
            _dictionary.LoadDefaultPage();
            _translator.LoadDefaultPage();
        }
        public void ReLoad(bool force = false)
        {
            _dictionary.ReLoad(force);
            _translator.ReLoad(force);
        }

        public void Show()
        {
            _dictionary.Show();
            _translator.Show();
        }

        public void ResetTranslateOnSelection()
        {
            _dictionary.ResetTranslateOnSelection();
        }
    }
}
