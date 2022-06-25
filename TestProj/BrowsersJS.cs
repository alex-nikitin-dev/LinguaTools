using TestProj.Properties;

namespace TestProj
{
    internal static class BrowsersJS
    {
        public static string OaldPrepareJS
        {
            get { return $@"function setItem(itemId,itemValue){{
            document.getElementById(itemId).value = itemValue;
            }}
            
            setItem('{Settings.Default.OALDUserID}','{Settings.Default.OALDUser}');
            setItem('{Settings.Default.OALDPassID}','{Settings.Default.OALDPass}');
            document.getElementById('{Settings.Default.OALDSubmitID}').click();
            "; }
        }
        public static string DictionaryOnSelectJS
        {
            get { return $@"
              document.body.onmouseup = function()
              {{
                    b1.onselect(document.getSelection().toString());
              }};
            "; }
        }
        public static string OaldDeleteAdJS
        {
            get { return $@"
            polls = document.querySelectorAll('[id ^= ""ad_""]');
            Array.prototype.forEach.call(polls, callback);

            function callback(element, iterator)
            {{
                element.remove();
            }}
            "; }
        }
    }
}
