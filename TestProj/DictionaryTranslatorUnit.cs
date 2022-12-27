using CefSharp;
using CefSharp.JavascriptBinding;
using System.Windows.Forms;

namespace TestProj
{
    internal class DictionaryTranslatorUnit
    {
        readonly BrowserItem _dictionary;
        readonly BrowserItem _translator;

        public BrowserItem Dictionary => _dictionary;
        public BrowserItem Translator => _translator;

        public static string DefaultTranslatorUrl { get; set; }
        public static string DefaultTranslatorName { get; set; }

        private readonly BoundObject _boundObject;

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

        private DictionaryTranslatorUnit(BrowserItem dictionary, BrowserItem translator = null)
        {
            _dictionary = dictionary;
            _translator = translator ?? new BrowserItem(DefaultTranslatorUrl,
                DefaultTranslatorName, 
                _dictionary.CSSDarkTheme,
                _dictionary.ColorTheme
                /*dictionary.ColorThemes*/);

            _boundObject = new BoundObject();
            _boundObject.BrowserTextSelected += _boundObject_BrowserTextSelected;
            //_dictionary.Browser.JavascriptObjectRepository.Register("dictionaryOperator1", _boundObject);
            //_dictionary.Browser.LoadingStateChanged += (s, a) =>
            //{
            //    if (a.IsLoading) return;
            //    _dictionary.Browser.ExecuteScriptAsync("CefSharp.BindObjectAsync('dictionaryOperator1');");
            //};

            _dictionary.Browser.JavascriptObjectRepository.ResolveObject += (sender, e) =>
            {
                var repo = e.ObjectRepository;
                if (e.ObjectName == "dictionaryOperator1")
                {
                    repo.Register("dictionaryOperator1", _boundObject, new BindingOptions());
                }
            };

            _dictionary.Browser.KeyboardHandler = new KeyboardHandler();
            _translator.Browser.KeyboardHandler = new KeyboardHandler();
        }

        public DictionaryTranslatorUnit(BrowserItem dictionary, string cssDarkTranslator)
            :this(dictionary)
        {
            _translator.CSSDarkTheme = cssDarkTranslator;
        }

        private void _boundObject_BrowserTextSelected(object sender, BrowserTextSelectedEventArgs e)
        {
            _translator.Go(e.Text);
        }
        public void Go(string text, bool force = false)
        {
            _dictionary.Go(text, force);
            _translator.Go(text, force);
        }

        public void ReLoad()
        {
            _dictionary.ReLoad();
            _translator.ReLoad();
        }

        public void Show()
        {
            _dictionary.Browser.Show();
            _translator.Browser.Show();
        }
    }
}
