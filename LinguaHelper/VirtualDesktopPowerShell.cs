using System;
using System.Management.Automation;

namespace LinguaHelper
{
    /// <summary>
    /// Class that manages the virtual desktops via powershell.
    /// </summary>
    static class VirtualDesktopPowerShell
    {
        /// <summary>
        /// The static PowerShellOperator instance.
        /// </summary>
        private static readonly PowerShellOperator _powerShell;
       
        /// <summary>
        /// Constructor.
        /// </summary>
        static VirtualDesktopPowerShell()
        {
            _powerShell = new();
        }
        
        /// <summary>
        /// Returns the index of the current virtual desktop.
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentDesktopIndex()
        {
            var result = _powerShell.ExecuteCommand("Get-DesktopIndex -Desktop (Get-CurrentDesktop)");
            if (result.Count == 0) { ThrowExeption("Cannot get the current desktop index with powershell"); }
            return Convert.ToInt32(result[0].BaseObject);
        }

        private static void ThrowExeption(string msg)
        {
            throw new InvalidOperationException(msg);
        }

        /// <summary>
        /// Make the desktop with the given index the current virtual desktop.
        /// </summary>
        /// <param name="desktopIndex"></param>
        public static void SwitchToDesktop(int desktopIndex)
        {
            _powerShell.ExecuteCommand($"Switch-Desktop {desktopIndex}");
        }

        /// <summary>
        /// Returns the index of the virtual desktop that contains the window with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the window</param>
        /// <returns>Virtual desktop index</returns>
        public static int GetDesktopIndexFromHandle(nint handle)
        {
            var result = _powerShell.ExecuteCommand($"Get-DesktopIndex -Desktop (Get-DesktopFromWindow -Hwnd {handle})");
            if (result.Count == 0) { ThrowExeption($"Cannot get virtual desktop index form the handle {handle}"); }
            return Convert.ToInt32(result[0].BaseObject);
        }

        /// <summary>
        /// Returns the virtual desktop that contains the window with the given handle.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static VirtualDesktopItem GetDesktopFromHandle(nint handle)
        {
            var result = _powerShell.ExecuteCommand($"Get-DesktopFromWindow -Hwnd {handle}");
            if (result.Count == 0) { ThrowExeption($"Cannot get virtual desktop form the handle {handle}"); }
            return RenderVirtualDesktop(result[0]);
        }

        /// <summary>
        /// Returns true if the window with the given handle is on the current virtual desktop.
        /// </summary>
        /// <param name="handle">the handle of the window</param>
        /// <returns></returns>
        public static bool IsWindowOnCurrentDesktop(nint handle)
        {
            return GetDesktopIndexFromHandle(handle) == GetCurrentDesktopIndex();
        }

        /// <summary>
        /// Returns the list of virtual desktops.
        /// </summary>
        /// <returns></returns>
        public static VirtualDesktopCollection GetDesktopList()
        {
            var result = _powerShell.ExecuteCommand("Get-DesktopList");

            var desktops = new VirtualDesktopCollection();
            foreach (var desktop in result)
            {
                desktops.Add(RenderVirtualDesktop(desktop));
            }

            return desktops;
        }

        /// <summary>
        /// Create a new virtual desktop with PSObject using its "members" property.
        /// That includes the index, name and visibility of the desktop.
        /// </summary>
        /// <param name="desktop">the object which is the result of powershell command</param>
        /// <returns>An VirtualDesktopItem object</returns>
        private static VirtualDesktopItem RenderVirtualDesktop(PSObject desktop)
        {
            return new VirtualDesktopItem(
                                Convert.ToInt32(desktop.Members["Index"].Value),
                                desktop.Members["Name"].Value.ToString(),
                                Convert.ToBoolean(desktop.Members["Visible"].Value));
        }
    }
}

