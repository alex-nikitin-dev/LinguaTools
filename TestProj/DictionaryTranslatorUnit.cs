using CefSharp;

namespace TestProj
{
    internal class DictionaryTranslatorUnit
    {
        BrowserItem _dictionary;
        BrowserItem _translator;

        public BrowserItem Dictionary => _dictionary;
        public BrowserItem Translator => _translator;

        public static string DefaultTranslatorUrl { get; set; }
        public static string DefaultTranslatorName { get; set; }

        private readonly BoundObject _boundObject;
        public DictionaryTranslatorUnit(BrowserItem dictionary, BrowserItem translator = null)
        {
            _dictionary = dictionary;
            _translator = translator ?? new BrowserItem(DefaultTranslatorUrl, DefaultTranslatorName);

            _boundObject = new BoundObject();
            _boundObject.BrowserTextSelected += _boundObject_BrowserTextSelected;
            _dictionary.Browser.JavascriptObjectRepository.Register("b1", _boundObject, false);
        }
        
        private void _boundObject_BrowserTextSelected(object sender, BrowserTextSelectedEventArgs e)
        {
            _translator.Go(e.Text);
        }
        public void Go(string text)
        {
            _dictionary.Go(text);
            _translator.Go(text);
        }

        public void ReLoad()
        {
            _dictionary.ReLoad();
            _translator.ReLoad();
        }
    }
}
