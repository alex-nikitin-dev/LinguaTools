using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace LinguaHelper
{
    internal class PowerShellOperator
    {
        // <summary>
        /// The static powershell instance.
        /// </summary>
        private readonly PowerShell _powerShell;

        private PowerShellOperator()
        {
            _powerShell = PowerShell.Create();
            if (_powerShell == null) { throw new ArgumentException("Can't create a powershell instance"); }
        }

        public static async Task<PowerShellOperator> CreateAsync()
        {
            var instance = new PowerShellOperator();
            await instance.SetExecutionPolicyAsync();
            return instance;
        }
        private async Task SetExecutionPolicyAsync()
        {
            await ExecuteCommandAsync("Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process");
        }
        public PowerShell Get_powerShell()
        {
            return _powerShell;
        }


        //public Collection<PSObject> ExecuteCommand(string command)
        //{
        //    _powerShell.Commands.Clear();
        //    _powerShell.AddScript(command);
        //  //  System.Windows.Forms.MessageBox.Show($"ExecuteCommand before _powerShell.Invoke: {command}");
        //    HandleErrors();
        //    var output = _powerShell.Invoke();
        //   // System.Windows.Forms.MessageBox.Show($"ExecuteCommand after _powerShell.Invoke: {command}");
        //    HandleErrors();
        //    return output;
        //}
        public async Task<PSDataCollection<PSObject>> ExecuteCommandAsync(string command)
        {
            _powerShell.Commands.Clear();
            _powerShell.AddScript(command);

            var asyncResult = _powerShell.BeginInvoke();
            await Task.Factory.FromAsync(asyncResult, _ => _powerShell.EndInvoke(asyncResult));

            HandleErrors();
            var output = await Task<PSDataCollection<PSObject>>.Factory.FromAsync(asyncResult, _ => _powerShell.EndInvoke(asyncResult));
            return output;
        }

        /// <summary>
        /// Throws an exception if the powershell instance had errors.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private  void HandleErrors()
        {
            if (_powerShell.Streams.Error.Count > 0)
            {
                // Handle errors
                var msg = new StringBuilder();
                foreach (var error in _powerShell.Streams.Error)
                {
                    msg.AppendLine(error.Exception.ToString());
                }
                _powerShell.Streams.Error.Clear();

                throw new InvalidOperationException(msg.ToString());
            }
        }
    }
}
