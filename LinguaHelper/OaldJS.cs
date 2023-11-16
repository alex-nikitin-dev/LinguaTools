namespace LinguaHelper
{
    //internal static class OaldJS
    //{
    //    //OaldPrepareJS
    //    //public static string PrepareJSCode =>
    //    //    $@"function setItem(itemId,itemValue){{
    //    //    document.getElementById(itemId).value = itemValue;
    //    //    }}

    //    //    setItem('{Settings.Default.OALDUserID}','{Settings.Default.OALDUser}');
    //    //    setItem('{Settings.Default.OALDPassID}','{Settings.Default.OALDPass}');
    //    //    document.getElementById('{Settings.Default.OALDSubmitID}').click();
    //    //    ";

    //    ////OaldDeleteAdJS
    //    //public static string OtherJSCode =>
    //    //    $@"
    //    //    polls = document.querySelectorAll('[id ^= ""ad_""]');
    //    //    Array.prototype.forEach.call(polls, callback);

    //    //    function callback(element, iterator)
    //    //    {{
    //    //        element.remove();
    //    //    }}

    //    //    document.getElementById('{Settings.Default.OALD_AcceptAllCookies_ID}').click();
    //    //    ";

    //    private static string SpeakOnBrowsing =>
    //         @$"
    //        document.querySelector('.sound.audio_play_button.pron-us.icon-audio').click();
    //        ";
    //    public static string ForAsyncEvalJSCode =>
    //        @$"
    //        {(Settings.Default.SpeakOnBrowsingOALD ? SpeakOnBrowsing : "")}
    //        ";

    //    public static IBrowserJS GetInstance()
    //    {

    //        return new BrowserJS(PrepareJSCode,
    //            null,
    //            GenericJS.MainFrameJSCode,
    //            OtherJSCode,
    //            ForAsyncEvalJSCode);
    //    }
    //}
}
