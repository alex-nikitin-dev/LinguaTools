namespace TestProj
{
    public class BrowserTextSelectedEventArgs
    {
        public string Text { get; set; }

        public BrowserTextSelectedEventArgs(string text)
        {
            Text = text;
        }
    }
}
