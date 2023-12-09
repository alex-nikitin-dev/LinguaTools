using CefSharp;
using CefSharp.Handler;
namespace LinguaHelper
{
    public class CustomRequestHandler : RequestHandler
    {
        public delegate void UserGestureDelegate();
        public event UserGestureDelegate UserGesture;
        public void OnUserGesture()
        {
            UserGesture?.Invoke();
        }
        protected override bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            // Check if the navigation is initiated by a user gesture, such as a mouse click
            if (userGesture)
            {
                // Log or handle the link click here
                OnUserGesture();
            }

            return base.OnBeforeBrowse(chromiumWebBrowser, browser, frame, request, userGesture, isRedirect);
        }
    }
}