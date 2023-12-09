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

        private ColorTheme _colorTheme;
        public ColorTheme ColorTheme
        {
            get => _colorTheme;
            set
            {
                _colorTheme = value;
                ColorThemeUpdate();
            }
        }
        private void ColorThemeUpdate()
        {
            _dictionary.ColorTheme = _colorTheme;
            _translator.ColorTheme = _colorTheme;
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
            _translator.Go(e.Text, false);
        }
        public void Go(string text,bool isUserCommand, bool force = false)
        {
            _dictionary.Go(text, isUserCommand, force);
           // _translator.Go(text, force);
        }

        public void ReLoad()
        {
            _dictionary.ReLoad();
            _translator.ReLoad();
        }

        public void Show()
        {
            _dictionary.Show();
            _translator.Show();
        }
    }
}
