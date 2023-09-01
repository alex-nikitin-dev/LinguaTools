﻿// Author: Markus Scholtes, 2023
// Version 1.13, 2023-06-25
// Version for Windows 10 1809 to 22H2
// Compile with:
// C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe DesktopHelper.cs

using System.Runtime.InteropServices;
using System.Text;

// set attributes


// Based on http://stackoverflow.com/a/32417530, Windows 10 SDK, github project Grabacr07/DesktopHelper and own research

namespace DesktopHelperWin10
{
    #region COM API
    internal static class Guids
    {
        public static readonly Guid CLSID_ImmersiveShell = new Guid("C2F03A33-21F5-47FA-B4BB-156362A2F239");
        public static readonly Guid CLSID_DesktopHelperManagerInternal = new Guid("C5E0CDCA-7B6E-41B2-9FC4-D93975CC467B");
        public static readonly Guid CLSID_DesktopHelperManager = new Guid("AA509086-5CA9-4C25-8F95-589D3C07B48A");
        public static readonly Guid CLSID_DesktopHelperPinnedApps = new Guid("B5A399E7-1C87-46B8-88E9-FC5747B171BD");
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Size
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    internal enum APPLICATION_VIEW_CLOAK_TYPE : int
    {
        AVCT_NONE = 0,
        AVCT_DEFAULT = 1,
        AVCT_VIRTUAL_DESKTOP = 2
    }

    internal enum APPLICATION_VIEW_COMPATIBILITY_POLICY : int
    {
        AVCP_NONE = 0,
        AVCP_SMALL_SCREEN = 1,
        AVCP_TABLET_SMALL_SCREEN = 2,
        AVCP_VERY_SMALL_SCREEN = 3,
        AVCP_HIGH_SCALE_FACTOR = 4
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    [Guid("372E1D3B-38D3-42E4-A15B-8AB2B178F513")]
    internal interface IApplicationView
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(APPLICATION_VIEW_CLOAK_TYPE cloakType, int unknown);
        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */, out IntPtr /* IApplicationViewPosition** */ position);
        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetDesktopHelperId(out Guid guid);
        int SetDesktopHelperId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int SetCompatibilityPolicyType(APPLICATION_VIEW_COMPATIBILITY_POLICY flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
        int Unknown5(out int unknown);
        int Unknown6(int unknown);
        int Unknown7();
        int Unknown8(out int unknown);
        int Unknown9(int unknown);
        int Unknown10(int unknownX, int unknownY);
        int Unknown11(int unknown);
        int Unknown12(out Size size1);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
    internal interface IApplicationViewCollection
    {
        int GetViews(out IObjectArray array);
        int GetViewsByZOrder(out IObjectArray array);
        int GetViewsByAppUserModelId(string id, out IObjectArray array);
        int GetViewForHwnd(IntPtr hwnd, out IApplicationView view);
        int GetViewForApplication(object application, out IApplicationView view);
        int GetViewForAppUserModelId(string id, out IApplicationView view);
        int GetViewInFocus(out IntPtr view);
        int Unknown1(out IntPtr view);
        void RefreshCollection();
        int RegisterForApplicationViewChanges(object listener, out int cookie);
        int UnregisterForApplicationViewChanges(int cookie);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("FF72FFDD-BE7E-43FC-9C03-AD81681E88E4")]
    internal interface IDesktopHelper
    {
        bool IsViewVisible(IApplicationView view);
        Guid GetId();
    }

    /*
    IDesktopHelper2 not used now (available since Win 10 2004), instead reading names out of registry for compatibility reasons
    Excample code:
    IDesktopHelper2 ivd2;
    string desktopName;
    ivd2.GetName(out desktopName);
    Console.WriteLine("Name of desktop: " + desktopName);

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("31EBDE3F-6EC3-4CBD-B9FB-0EF6D09B41F4")]
        internal interface IDesktopHelper2
        {
            bool IsViewVisible(IApplicationView view);
            Guid GetId();
            void GetName([MarshalAs(UnmanagedType.HString)] out string name);
        }
    */

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("F31574D6-B682-4CDC-BD56-1827860ABEC6")]
    internal interface IDesktopHelperManagerInternal
    {
        int GetCount();
        void MoveViewToDesktop(IApplicationView view, IDesktopHelper desktop);
        bool CanViewMoveDesktops(IApplicationView view);
        IDesktopHelper GetCurrentDesktop();
        void GetDesktops(out IObjectArray desktops);
        [PreserveSig]
        int GetAdjacentDesktop(IDesktopHelper from, int direction, out IDesktopHelper desktop);
        void SwitchDesktop(IDesktopHelper desktop);
        IDesktopHelper CreateDesktop();
        void RemoveDesktop(IDesktopHelper desktop, IDesktopHelper fallback);
        IDesktopHelper FindDesktop(ref Guid desktopid);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0F3A72B0-4566-487E-9A33-4ED302F6D6CE")]
    internal interface IDesktopHelperManagerInternal2
    {
        int GetCount();
        void MoveViewToDesktop(IApplicationView view, IDesktopHelper desktop);
        bool CanViewMoveDesktops(IApplicationView view);
        IDesktopHelper GetCurrentDesktop();
        void GetDesktops(out IObjectArray desktops);
        [PreserveSig]
        int GetAdjacentDesktop(IDesktopHelper from, int direction, out IDesktopHelper desktop);
        void SwitchDesktop(IDesktopHelper desktop);
        IDesktopHelper CreateDesktop();
        void RemoveDesktop(IDesktopHelper desktop, IDesktopHelper fallback);
        IDesktopHelper FindDesktop(ref Guid desktopid);
        void Unknown1(IDesktopHelper desktop, out IntPtr unknown1, out IntPtr unknown2);
        void SetName(IDesktopHelper desktop, [MarshalAs(UnmanagedType.HString)] string name);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("A5CD92FF-29BE-454C-8D04-D82879FB3F1B")]
    internal interface IDesktopHelperManager
    {
        bool IsWindowOnCurrentDesktopHelper(IntPtr topLevelWindow);
        Guid GetWindowDesktopId(IntPtr topLevelWindow);
        void MoveWindowToDesktop(IntPtr topLevelWindow, ref Guid desktopId);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4CE81583-1E4C-4632-A621-07A53543148F")]
    internal interface IDesktopHelperPinnedApps
    {
        bool IsAppIdPinned(string appId);
        void PinAppID(string appId);
        void UnpinAppID(string appId);
        bool IsViewPinned(IApplicationView applicationView);
        void PinView(IApplicationView applicationView);
        void UnpinView(IApplicationView applicationView);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9")]
    internal interface IObjectArray
    {
        void GetCount(out int count);
        void GetAt(int index, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object obj);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
    internal interface IServiceProvider10
    {
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object QueryService(ref Guid service, ref Guid riid);
    }
    #endregion

    #region COM wrapper
    internal static class DesktopManager
    {
        static DesktopManager()
        {
            var shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell));
            DesktopHelperManagerInternal = (IDesktopHelperManagerInternal)shell.QueryService(Guids.CLSID_DesktopHelperManagerInternal, typeof(IDesktopHelperManagerInternal).GUID);
            try
            {
                DesktopHelperManagerInternal2 = (IDesktopHelperManagerInternal2)shell.QueryService(Guids.CLSID_DesktopHelperManagerInternal, typeof(IDesktopHelperManagerInternal2).GUID);
            }
            catch
            {
                DesktopHelperManagerInternal2 = null;
            }
            DesktopHelperManager = (IDesktopHelperManager)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_DesktopHelperManager));
            ApplicationViewCollection = (IApplicationViewCollection)shell.QueryService(typeof(IApplicationViewCollection).GUID, typeof(IApplicationViewCollection).GUID);
            DesktopHelperPinnedApps = (IDesktopHelperPinnedApps)shell.QueryService(Guids.CLSID_DesktopHelperPinnedApps, typeof(IDesktopHelperPinnedApps).GUID);
        }

        internal static IDesktopHelperManagerInternal DesktopHelperManagerInternal;
        internal static IDesktopHelperManagerInternal2 DesktopHelperManagerInternal2;
        internal static IDesktopHelperManager DesktopHelperManager;
        internal static IApplicationViewCollection ApplicationViewCollection;
        internal static IDesktopHelperPinnedApps DesktopHelperPinnedApps;

        internal static IDesktopHelper GetDesktop(int index)
        {   // get desktop with index
            int count = DesktopHelperManagerInternal.GetCount();
            if (index < 0 || index >= count) throw new ArgumentOutOfRangeException("index");
            IObjectArray desktops;
            DesktopHelperManagerInternal.GetDesktops(out desktops);
            object objdesktop;
            desktops.GetAt(index, typeof(IDesktopHelper).GUID, out objdesktop);
            Marshal.ReleaseComObject(desktops);
            return (IDesktopHelper)objdesktop;
        }

        internal static int GetDesktopIndex(IDesktopHelper desktop)
        { // get index of desktop
            int index = -1;
            Guid IdSearch = desktop.GetId();
            IObjectArray desktops;
            DesktopHelperManagerInternal.GetDesktops(out desktops);
            object objdesktop;
            for (int i = 0; i < DesktopHelperManagerInternal.GetCount(); i++)
            {
                desktops.GetAt(i, typeof(IDesktopHelper).GUID, out objdesktop);
                if (IdSearch.CompareTo(((IDesktopHelper)objdesktop).GetId()) == 0)
                {
                    index = i;
                    break;
                }
            }
            Marshal.ReleaseComObject(desktops);
            return index;
        }

        internal static IApplicationView GetApplicationView(this IntPtr hWnd)
        { // get application view to window handle
            IApplicationView view;
            ApplicationViewCollection.GetViewForHwnd(hWnd, out view);
            return view;
        }

        internal static string GetAppId(IntPtr hWnd)
        { // get Application ID to window handle
            string appId;
            hWnd.GetApplicationView().GetAppUserModelId(out appId);
            return appId;
        }
    }
    #endregion

    #region public interface
    public class WindowInformation
    { // stores window informations
        public string Title { get; set; }
        public int Handle { get; set; }
    }

    public class Desktop
    {
        // get process id to window handle
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        // get thread id of current process
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        // attach input to thread
        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        // get handle of active window
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // try to set foreground window
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)] static extern bool SetForegroundWindow(IntPtr hWnd);

        // send message to window
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_MINIMIZE = 6;

        private static readonly Guid AppOnAllDesktops = new Guid("BB64D5B7-4DE3-4AB2-A87C-DB7601AEA7DC");
        private static readonly Guid WindowOnAllDesktops = new Guid("C2DDEA68-66F2-4CF9-8264-1BFD00FBBBAC");

        private IDesktopHelper ivd;
        private Desktop(IDesktopHelper desktop) { this.ivd = desktop; }

        public override int GetHashCode()
        { // get hash
            return ivd.GetHashCode();
        }

        public override bool Equals(object obj)
        { // compare with object
            var desk = obj as Desktop;
            return desk != null && object.ReferenceEquals(this.ivd, desk.ivd);
        }

        public static int Count
        { // return the number of desktops
            get { return DesktopManager.DesktopHelperManagerInternal.GetCount(); }
        }

        public static Desktop Current
        { // returns current desktop
            get { return new Desktop(DesktopManager.DesktopHelperManagerInternal.GetCurrentDesktop()); }
        }

        public static Desktop FromIndex(int index)
        { // return desktop object from index (-> index = 0..Count-1)
            return new Desktop(DesktopManager.GetDesktop(index));
        }

        public static Desktop FromWindow(IntPtr hWnd)
        { // return desktop object to desktop on which window <hWnd> is displayed
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            Guid id = DesktopManager.DesktopHelperManager.GetWindowDesktopId(hWnd);
            if ((id.CompareTo(AppOnAllDesktops) == 0) || (id.CompareTo(WindowOnAllDesktops) == 0))
                return new Desktop(DesktopManager.DesktopHelperManagerInternal.GetCurrentDesktop());
            else
                return new Desktop(DesktopManager.DesktopHelperManagerInternal.FindDesktop(ref id));
        }

        public static int FromDesktop(Desktop desktop)
        { // return index of desktop object or -1 if not found
            return DesktopManager.GetDesktopIndex(desktop.ivd);
        }

        public static string DesktopNameFromDesktop(Desktop desktop)
        { // return name of desktop or "Desktop n" if it has no name
            Guid guid = desktop.ivd.GetId();

            // read desktop name in registry
            string desktopName = null;
            try
            {
                desktopName = (string)Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\DesktopHelpers\\Desktops\\{" + guid.ToString() + "}", "Name", null);
            }
            catch { }

            // no name found, generate generic name
            if (string.IsNullOrEmpty(desktopName))
            { // create name "Desktop n" (n = number starting with 1)
                desktopName = "Desktop " + (DesktopManager.GetDesktopIndex(desktop.ivd) + 1).ToString();
            }
            return desktopName;
        }

        public static string DesktopNameFromIndex(int index)
        { // return name of desktop from index (-> index = 0..Count-1) or "Desktop n" if it has no name
            Guid guid = DesktopManager.GetDesktop(index).GetId();

            // read desktop name in registry
            string desktopName = null;
            try
            {
                desktopName = (string)Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\DesktopHelpers\\Desktops\\{" + guid.ToString() + "}", "Name", null);
            }
            catch { }

            // no name found, generate generic name
            if (string.IsNullOrEmpty(desktopName))
            { // create name "Desktop n" (n = number starting with 1)
                desktopName = "Desktop " + (index + 1).ToString();
            }
            return desktopName;
        }

        public static bool HasDesktopNameFromIndex(int index)
        { // return true is desktop is named or false if it has no name
            Guid guid = DesktopManager.GetDesktop(index).GetId();

            // read desktop name in registry
            string desktopName = null;
            try
            {
                desktopName = (string)Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\DesktopHelpers\\Desktops\\{" + guid.ToString() + "}", "Name", null);
            }
            catch { }

            // name found?
            if (string.IsNullOrEmpty(desktopName))
                return false;
            else
                return true;
        }

        public static int SearchDesktop(string partialName)
        { // get index of desktop with partial name, return -1 if no desktop found
            int index = -1;

            for (int i = 0; i < DesktopManager.DesktopHelperManagerInternal.GetCount(); i++)
            { // loop through all virtual desktops and compare partial name to desktop name
                if (DesktopNameFromIndex(i).ToUpper().IndexOf(partialName.ToUpper()) >= 0)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public static Desktop Create()
        { // create a new desktop
            return new Desktop(DesktopManager.DesktopHelperManagerInternal.CreateDesktop());
        }

        public void Remove(Desktop fallback = null)
        { // destroy desktop and switch to <fallback>
            IDesktopHelper fallbackdesktop;
            if (fallback == null)
            { // if no fallback is given use desktop to the left except for desktop 0.
                Desktop dtToCheck = new Desktop(DesktopManager.GetDesktop(0));
                if (this.Equals(dtToCheck))
                { // desktop 0: set fallback to second desktop (= "right" desktop)
                    DesktopManager.DesktopHelperManagerInternal.GetAdjacentDesktop(ivd, 4, out fallbackdesktop); // 4 = RightDirection
                }
                else
                { // set fallback to "left" desktop
                    DesktopManager.DesktopHelperManagerInternal.GetAdjacentDesktop(ivd, 3, out fallbackdesktop); // 3 = LeftDirection
                }
            }
            else
                // set fallback desktop
                fallbackdesktop = fallback.ivd;

            DesktopManager.DesktopHelperManagerInternal.RemoveDesktop(ivd, fallbackdesktop);
        }

        public void SetName(string Name)
        { // set name for desktop, empty string removes name
            if (DesktopManager.DesktopHelperManagerInternal2 != null)
            { // only if interface to set name is present
                DesktopManager.DesktopHelperManagerInternal2.SetName(this.ivd, Name);
            }
        }

        public bool IsVisible
        { // return true if this desktop is the current displayed one
            get { return object.ReferenceEquals(ivd, DesktopManager.DesktopHelperManagerInternal.GetCurrentDesktop()); }
        }

        public void MakeVisible()
        { // make this desktop visible
            WindowInformation wi = FindWindow("Program Manager");

            // activate desktop to prevent flashing icons in taskbar
            int dummy;
            uint DesktopThreadId = GetWindowThreadProcessId(new IntPtr(wi.Handle), out dummy);
            uint ForegroundThreadId = GetWindowThreadProcessId(GetForegroundWindow(), out dummy);
            uint CurrentThreadId = GetCurrentThreadId();

            if ((DesktopThreadId != 0) && (ForegroundThreadId != 0) && (ForegroundThreadId != CurrentThreadId))
            {
                AttachThreadInput(DesktopThreadId, CurrentThreadId, true);
                AttachThreadInput(ForegroundThreadId, CurrentThreadId, true);
                SetForegroundWindow(new IntPtr(wi.Handle));
                AttachThreadInput(ForegroundThreadId, CurrentThreadId, false);
                AttachThreadInput(DesktopThreadId, CurrentThreadId, false);
            }

            DesktopManager.DesktopHelperManagerInternal.SwitchDesktop(ivd);

            // direct desktop to give away focus
            ShowWindow(new IntPtr(wi.Handle), SW_MINIMIZE);
        }

        public Desktop Left
        { // return desktop at the left of this one, null if none
            get
            {
                IDesktopHelper desktop;
                int hr = DesktopManager.DesktopHelperManagerInternal.GetAdjacentDesktop(ivd, 3, out desktop); // 3 = LeftDirection
                if (hr == 0)
                    return new Desktop(desktop);
                else
                    return null;
            }
        }

        public Desktop Right
        { // return desktop at the right of this one, null if none
            get
            {
                IDesktopHelper desktop;
                int hr = DesktopManager.DesktopHelperManagerInternal.GetAdjacentDesktop(ivd, 4, out desktop); // 4 = RightDirection
                if (hr == 0)
                    return new Desktop(desktop);
                else
                    return null;
            }
        }

        public void MoveWindow(IntPtr hWnd)
        { // move window to this desktop
            int processId;
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            GetWindowThreadProcessId(hWnd, out processId);

            if (System.Diagnostics.Process.GetCurrentProcess().Id == processId)
            { // window of process
                try // the easy way (if we are owner)
                {
                    DesktopManager.DesktopHelperManager.MoveWindowToDesktop(hWnd, ivd.GetId());
                }
                catch // window of process, but we are not the owner
                {
                    IApplicationView view;
                    DesktopManager.ApplicationViewCollection.GetViewForHwnd(hWnd, out view);
                    DesktopManager.DesktopHelperManagerInternal.MoveViewToDesktop(view, ivd);
                }
            }
            else
            { // window of other process
                IApplicationView view;
                DesktopManager.ApplicationViewCollection.GetViewForHwnd(hWnd, out view);
                try
                {
                    DesktopManager.DesktopHelperManagerInternal.MoveViewToDesktop(view, ivd);
                }
                catch
                { // could not move active window, try main window (or whatever windows thinks is the main window)
                    DesktopManager.ApplicationViewCollection.GetViewForHwnd(System.Diagnostics.Process.GetProcessById(processId).MainWindowHandle, out view);
                    DesktopManager.DesktopHelperManagerInternal.MoveViewToDesktop(view, ivd);
                }
            }
        }

        public void MoveActiveWindow()
        { // move active window to this desktop
            MoveWindow(GetForegroundWindow());
        }

        public bool HasWindow(IntPtr hWnd)
        { // return true if window is on this desktop
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            Guid id = DesktopManager.DesktopHelperManager.GetWindowDesktopId(hWnd);
            if ((id.CompareTo(AppOnAllDesktops) == 0) || (id.CompareTo(WindowOnAllDesktops) == 0))
                return true;
            else
                return ivd.GetId() == id;
        }

        public static bool IsWindowPinned(IntPtr hWnd)
        { // return true if window is pinned to all desktops
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager.DesktopHelperPinnedApps.IsViewPinned(hWnd.GetApplicationView());
        }

        public static void PinWindow(IntPtr hWnd)
        { // pin window to all desktops
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = hWnd.GetApplicationView();
            if (!DesktopManager.DesktopHelperPinnedApps.IsViewPinned(view))
            { // pin only if not already pinned
                DesktopManager.DesktopHelperPinnedApps.PinView(view);
            }
        }

        public static void UnpinWindow(IntPtr hWnd)
        { // unpin window from all desktops
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = hWnd.GetApplicationView();
            if (DesktopManager.DesktopHelperPinnedApps.IsViewPinned(view))
            { // unpin only if not already unpinned
                DesktopManager.DesktopHelperPinnedApps.UnpinView(view);
            }
        }

        public static bool IsApplicationPinned(IntPtr hWnd)
        { // return true if application for window is pinned to all desktops
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            return DesktopManager.DesktopHelperPinnedApps.IsAppIdPinned(DesktopManager.GetAppId(hWnd));
        }

        public static void PinApplication(IntPtr hWnd)
        { // pin application for window to all desktops
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            string appId = DesktopManager.GetAppId(hWnd);
            if (!DesktopManager.DesktopHelperPinnedApps.IsAppIdPinned(appId))
            { // pin only if not already pinned
                DesktopManager.DesktopHelperPinnedApps.PinAppID(appId);
            }
        }

        public static void UnpinApplication(IntPtr hWnd)
        { // unpin application for window from all desktops
            if (hWnd == IntPtr.Zero) throw new ArgumentNullException();
            var view = hWnd.GetApplicationView();
            string appId = DesktopManager.GetAppId(hWnd);
            if (DesktopManager.DesktopHelperPinnedApps.IsAppIdPinned(appId))
            { // unpin only if pinned
                DesktopManager.DesktopHelperPinnedApps.UnpinAppID(appId);
            }
        }

        // prepare callback function for window enumeration
        private delegate bool CallBackPtr(int hwnd, int lParam);
        private static CallBackPtr callBackPtr = Callback;
        // list of window informations
        private static List<WindowInformation> WindowInformationList = new List<WindowInformation>();

        // enumerate windows
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(CallBackPtr lpEnumFunc, IntPtr lParam);

        // get window title length
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        // get window title
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        // callback function for window enumeration
        private static bool Callback(int hWnd, int lparam)
        {
            int length = GetWindowTextLength((IntPtr)hWnd);
            if (length > 0)
            {
                StringBuilder sb = new StringBuilder(length + 1);
                if (GetWindowText((IntPtr)hWnd, sb, sb.Capacity) > 0)
                { WindowInformationList.Add(new WindowInformation { Handle = hWnd, Title = sb.ToString() }); }
            }
            return true;
        }

        // get list of all windows with title
        public static List<WindowInformation> GetWindows()
        {
            WindowInformationList = new List<WindowInformation>();
            EnumWindows(callBackPtr, IntPtr.Zero);
            return WindowInformationList;
        }

        // find first window with string in title
        public static WindowInformation FindWindow(string WindowTitle)
        {
            WindowInformationList = new List<WindowInformation>();
            EnumWindows(callBackPtr, IntPtr.Zero);
            WindowInformation result = WindowInformationList.Find(x => x.Title.IndexOf(WindowTitle, StringComparison.OrdinalIgnoreCase) >= 0);
            return result;
        }
    }
    #endregion
}