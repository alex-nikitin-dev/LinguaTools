﻿namespace TestProj
{
    internal interface IBrowserJS
    {
         string PrepareJSCode { get; set; }
         string MainFrameJSCode { get; set; }
         string OtherJSCode { get; set; }
    }
}
