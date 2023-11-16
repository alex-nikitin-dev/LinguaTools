using System;
using System.Diagnostics;
using System.Management.Automation;
using System.Threading.Tasks;

namespace LinguaHelper
{
    /// <summary>
    /// Class that manages the virtual desktops via powershell.
    /// </summary>
    static class VirtualDesktopPowerShell
    {
        private static readonly Lazy<Task<PowerShellOperator>> _powerShellTask = new(PowerShellOperator.CreateAsync);

        private static Task<PowerShellOperator> PowerShellTask => _powerShellTask.Value;


        /// <summary>
        /// Returns the index of the current virtual desktop.
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetCurrentDesktopIndexAsync()
        {
            var powerShell = await PowerShellTask;
            var result = await powerShell.ExecuteCommandAsync("Get-DesktopIndex -Desktop (Get-CurrentDesktop)");
            if (result.Count == 0) { throw new InvalidOperationException("Cannot get the current desktop index with powershell"); }
            return Convert.ToInt32(result[0].BaseObject);
        }

        /// <summary>
        /// Make the desktop with the given index the current virtual desktop.
        /// </summary>
        /// <param name="desktopIndex"></param>
        public static async Task SwitchToDesktopAsync(int desktopIndex)
        {
            var powerShell = await PowerShellTask;
            await powerShell.ExecuteCommandAsync($"Switch-Desktop {desktopIndex}");
        }

        /// <summary>
        /// Returns the index of the virtual desktop that contains the window with the given handle.
        /// </summary>
        /// <param name="handle">the handle of the window</param>
        /// <returns>Virtual desktop index</returns>
        public static async Task<int> GetDesktopIndexFromHandleAsync(nint handle)
        {
            var powerShell = await PowerShellTask;
            var result = await powerShell.ExecuteCommandAsync($"Get-DesktopIndex -Desktop (Get-DesktopFromWindow -Hwnd {handle})");
            if (result.Count == 0) { throw new InvalidOperationException($"Cannot get virtual desktop index form the handle {handle}"); }
            return Convert.ToInt32(result[0].BaseObject);
        }


        ///// <summary>
        ///// Returns the virtual desktop that contains the window with the given handle.
        ///// Worning: This method is possibly slow.
        ///// </summary>
        ///// <param name="handle"></param>
        ///// <returns></returns>
        //public static VirtualDesktopItem GetDesktopFromHandle(nint handle)
        //{
        //    int desktopIndex = GetDesktopIndexFromHandle(handle);
        //    var desktopList = GetDesktopList();
        //    foreach (var desktop in desktopList)
        //    {
        //        if (desktop.Index == desktopIndex)
        //        {
        //            return desktop;
        //        }
        //    }
        //    throw new InvalidOperationException($"Cannot get virtual desktop form the handle {handle}");
        //}

        public static async Task<string> GetDesktopNameAsync(int desktopIndex)
        {
            var powerShell = await PowerShellTask;
            var result = await powerShell.ExecuteCommandAsync($"Get-DesktopName {desktopIndex}");
            if (result.Count == 0) { throw new InvalidOperationException($"Cannot get virtual desktop name form the index {desktopIndex}"); }
            return result[0].BaseObject.ToString();
        }

        ///// <summary>
        ///// Returns true if the window with the given handle is on the current virtual desktop.
        ///// </summary>
        ///// <param name="handle">the handle of the window</param>
        ///// <returns></returns>
        //public static bool IsWindowOnCurrentDesktop(nint handle)
        //{
        //    return GetDesktopIndexFromHandle(handle) == GetCurrentDesktopIndex();
        //}

        /// <summary>
        /// Returns the list of virtual desktops.
        /// </summary>
        /// <returns></returns>
        public static async Task<VirtualDesktopCollection> GetDesktopListAsync()
        {
            var powerShell = await PowerShellTask;
            var result = await powerShell.ExecuteCommandAsync("Get-DesktopList");

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
                                Convert.ToInt32(desktop.Members["Number"].Value),
                                desktop.Members["Name"].Value.ToString(),
                                Convert.ToBoolean(desktop.Members["Visible"].Value));
        }

        public static async Task<bool> IsVirtualDesktopInstalled()
        {
            var powerShell = await PowerShellTask;
            var result = await powerShell.ExecuteCommandAsync("Get-Module -ListAvailable -Name VirtualDesktop");
            return result.Count > 0;
        }

        public static async Task InstallVirtualDesktopAsync()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "pwsh",
                Arguments = "-NoProfile -Command \"Install-Module -Name VirtualDesktop -Scope CurrentUser -Force -Confirm:$false\"",
                Verb = "runas", // This will run the process as administrator
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true, // This prevents the window from being created
                WindowStyle = ProcessWindowStyle.Hidden // This ensures the window remains hidden
            };

            using (var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true })
            {
                var tcs = new TaskCompletionSource<bool>();

                process.Exited += (sender, args) =>
                {
                    tcs.SetResult(true);
                };

                process.Start();

                await tcs.Task;

                // Optionally, you can check the process.ExitCode here for any errors
                if (process.ExitCode != 0)
                {
                    var errorOutput = await process.StandardError.ReadToEndAsync();
                    throw new InvalidOperationException($"Error during execution: {errorOutput}");
                }
            }
        }
    }
}

