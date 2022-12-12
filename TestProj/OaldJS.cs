using TestProj.Properties;

namespace TestProj
{
    internal static class OaldJS
    {
        //OaldPrepareJS
        private static string PrepareJSCode =>
            $@"function setItem(itemId,itemValue){{
            document.getElementById(itemId).value = itemValue;
            }}
            
            setItem('{Settings.Default.OALDUserID}','{Settings.Default.OALDUser}');
            setItem('{Settings.Default.OALDPassID}','{Settings.Default.OALDPass}');
            document.getElementById('{Settings.Default.OALDSubmitID}').click();
            ";

        //DictionaryOnSelectJS
        private static string MainFrameJSCode =>
            $@"
              document.body.onmouseup = function()
              {{
                    b1.onselect(document.getSelection().toString());
              }};
            ";

        //OaldDeleteAdJS
        private static string OtherJSCode =>
            $@"
            polls = document.querySelectorAll('[id ^= ""ad_""]');
            Array.prototype.forEach.call(polls, callback);

            function callback(element, iterator)
            {{
                element.remove();
            }}
            
            document.getElementById('{Settings.Default.OALD_AcceptAllCookies_ID}').click();
            ";

        public static IBrowserJS GetInstance(IBrowserJS browserJS)
        {
            return new BrowserJS(browserJS.PrepareJSCode == null ? null: PrepareJSCode,
                browserJS.MainFrameJSCode == null ? null : MainFrameJSCode,
                browserJS.OtherJSCode == null ? null : OtherJSCode);
        }
        public static IBrowserJS GetInstance()
        {
            return new BrowserJS(PrepareJSCode,
                MainFrameJSCode,
                OtherJSCode);
        }
    }
}
