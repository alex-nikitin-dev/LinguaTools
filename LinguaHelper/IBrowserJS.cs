﻿namespace TestProj
{
    internal interface IBrowserJS
    {
        string PrepareJSCode { get; set; }
        string[] ColorThemeJSCode { get; set; }
        string MainFrameJSCode { get; set; }
        string OtherJSCode { get; set; }
        string ForAsyncEvalJSCode { get; set; }
        string LoadingStateChanedJSCode { get; set; }
        string ColorThemePrepareJSCode { get; set; }
    }
}
